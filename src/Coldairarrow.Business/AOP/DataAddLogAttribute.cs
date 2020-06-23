using Coldairarrow.IBusiness;
using Coldairarrow.Util;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Coldairarrow.Business
{
    public class DataAddLogAttribute : WriteDataLogAttribute
    {
        public DataAddLogAttribute(UserLogType logType, string nameField, string dataName)
            : base(logType, nameField, dataName)
        {
        }

        public override async Task After(IAOPContext context)
        {
            var op = context.ServiceProvider.GetService<IOperator>();
            var obj = context.Arguments[0];
            op.WriteUserLog(_logType, $"添加{_dataName}:{obj.GetPropertyValue(_nameField)?.ToString()}");

            await Task.CompletedTask;
        }
    }
}
