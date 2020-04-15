using AspectCore.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Coldairarrow.Util
{
    public class DataAddLogAttribute : WriteDataLogAttribute
    {
        public DataAddLogAttribute(UserLogType logType, string nameField, string dataName)
            : base(logType, nameField, dataName)
        {
        }

        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            await next(context);

            var op = context.ServiceProvider.GetService<IOperator>();
            var obj = context.Parameters[0];
            op.WriteUserLog(_logType, $"添加{_dataName}:{obj.GetPropertyValue(_nameField)?.ToString()}");
        }
    }
}
