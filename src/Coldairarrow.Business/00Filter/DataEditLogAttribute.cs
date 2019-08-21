using Castle.DynamicProxy;
using Coldairarrow.Util;

namespace Coldairarrow.Business
{
    class DataEditLogAttribute : WriteDataLogAttribute
    {
        public DataEditLogAttribute(LogType logType, string nameField, string dataName)
            : base(logType, nameField, dataName)
        {
        }

        public override void OnActionExecuting(IInvocation invocation)
        {

        }

        public override void OnActionExecuted(IInvocation invocation)
        {
            if ((invocation.ReturnValue as AjaxResult).Success)
            {
                var obj = invocation.Arguments[0];
                Logger.Info(_logType, $"修改{_dataName}:{obj.GetPropertyValue(_nameField)?.ToString()}");
            }
        }
    }
}
