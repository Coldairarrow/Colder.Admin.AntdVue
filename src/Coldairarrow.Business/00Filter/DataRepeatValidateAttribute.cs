using Castle.DynamicProxy;
using Coldairarrow.DataRepository;
using Coldairarrow.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Coldairarrow.Business
{
    public class DataRepeatValidateAttribute : BaseFilterAttribute
    {
        public DataRepeatValidateAttribute(string[] validateFields, string[] validateFieldNames, bool allData = false)
        {
            if (validateFields.Length != validateFieldNames.Length)
                throw new Exception("校验列与列描述信息不对应!");

            _allData = allData;
            for (int i = 0; i < validateFields.Length; i++)
            {
                _validateFields.Add(validateFields[i], validateFieldNames[i]);
            }
        }
        private bool _allData { get; }
        private Dictionary<string, string> _validateFields { get; } = new Dictionary<string, string>();

        public override void OnActionExecuting(IInvocation invocation)
        {
            Type entityType = invocation.Arguments[0].GetType();
            var data = invocation.Arguments[0];
            List<string> whereList = new List<string>();
            var properties = _validateFields
                .Where(x => !data.GetPropertyValue(x.Key).IsNullOrEmpty())
                .ToList();
            properties.ForEach((aProperty, index) =>
            {
                whereList.Add($" {aProperty.Key} = @{index} ");
            });
            IQueryable q = null;
            if (_allData)
            {
                var repository = invocation.InvocationTarget.GetPropertyValue("Service") as IRepository;
                q = repository.GetIQueryable(entityType);
            }
            else
                q = invocation.InvocationTarget.GetType().GetMethod("GetIQueryable").Invoke(invocation.InvocationTarget, new object[] { }) as IQueryable;
            q = q.Where("Id != @0", data.GetPropertyValue("Id"));
            q = q.Where(
                string.Join("||", whereList),
                properties.Select(x => data.GetPropertyValue(x.Key)).ToArray());
            var list = q.CastToList<object>();
            if (list.Count > 0)
            {
                var repeatList = properties
                    .Where(x => list.Any(y => !y.GetPropertyValue(x.Key).IsNullOrEmpty()))
                    .Select(x => x.Value)
                    .ToList();

                invocation.ReturnValue = new ErrorResult($"{string.Join(",", repeatList)}已存在!");
            }
        }

        public override void OnActionExecuted(IInvocation invocation)
        {

        }
    }
}
