using Castle.DynamicProxy;
using Coldairarrow.Util;

namespace Coldairarrow.Business
{
    public class DataAddLogAttribute : WriteDataLogAttribute
    {
        public DataAddLogAttribute(LogType logType, string nameField, string dataName)
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
                Logger.Info(_logType, $"添加{_dataName}:{obj.GetPropertyValue(_nameField)?.ToString()}");
            }
        }
    }
}
