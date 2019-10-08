using Castle.DynamicProxy;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Coldairarrow.Business
{
    public class DataDeleteLogAttribute : WriteDataLogAttribute
    {
        public DataDeleteLogAttribute(LogType logType, string nameField, string dataName)
            : base(logType, nameField, dataName)
        {
        }

        private List<object> _deleteList { get; set; }

        public override void OnActionExecuting(IInvocation invocation)
        {
            List<string> ids = invocation.Arguments[0] as List<string>;
            var q = invocation.InvocationTarget.GetType().GetMethod("GetIQueryable").Invoke(invocation.InvocationTarget, new object[] { }) as IQueryable;
            _deleteList = q.Where("@0.Contains(Id)", ids).CastToList<object>();
        }

        public override void OnActionExecuted(IInvocation invocation)
        {
            if ((invocation.ReturnValue as AjaxResult).Success)
            {
                string names = string.Join(",", _deleteList.Select(x => x.GetPropertyValue(_nameField)?.ToString()));
                Logger.Info(_logType, $"删除{_dataName}:{names}", _deleteList.ToJson());
            }
        }
    }
}
