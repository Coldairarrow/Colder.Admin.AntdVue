using AspectCore.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Coldairarrow.Util
{
    public class DataDeleteLogAttribute : WriteDataLogAttribute
    {
        public DataDeleteLogAttribute(UserLogType logType, string nameField, string dataName)
            : base(logType, nameField, dataName)
        {
        }

        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            var op = context.ServiceProvider.GetService<IOperator>();

            List<string> ids = context.Parameters[0] as List<string>;
            var q = context.Implementation.GetType().GetMethod("GetIQueryable").Invoke(context.Implementation, new object[] { }) as IQueryable;
            var deleteList = q.Where("@0.Contains(Id)", ids).CastToList<object>();

            await next(context);

            string names = string.Join(",", deleteList.Select(x => x.GetPropertyValue(_nameField)?.ToString()));
            op.WriteUserLog(_logType, $"删除{_dataName}:{names}");
        }
    }
}
