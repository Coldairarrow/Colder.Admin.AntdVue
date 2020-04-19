using AspectCore.DynamicProxy;
using Coldairarrow.Util;
using EFCore.Sharding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Coldairarrow.Business
{
    public class DataRepeatValidateAttribute : AbstractInterceptorAttribute
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

        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            Type entityType = context.Parameters[0].GetType();
            var data = context.Parameters[0];
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
                var repository = context.Proxy.GetPropertyValue("Service") as IRepository;
                q = repository.GetIQueryable(entityType);
            }
            else
                q = context.Implementation.GetType().GetMethod("GetIQueryable").Invoke(context.Implementation, new object[] { }) as IQueryable;
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

                throw new BusException($"{string.Join(",", repeatList)}已存在!");
            }

            await next(context);
        }
    }
}
