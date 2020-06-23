using Coldairarrow.IBusiness;
using Coldairarrow.Util;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Coldairarrow.Business
{
    public class DataDeleteLogAttribute : WriteDataLogAttribute
    {
        public DataDeleteLogAttribute(UserLogType logType, string nameField, string dataName)
            : base(logType, nameField, dataName)
        {
        }

        private string _names;
        public override async Task Befor(IAOPContext context)
        {
            List<string> ids = context.Arguments[0] as List<string>;
            var q = context.InvocationTarget.GetType().GetMethod("GetIQueryable").Invoke(context.InvocationTarget, new object[] { }) as IQueryable;
            var deleteList = q.Where("@0.Contains(Id)", ids).CastToList<object>();

            _names = string.Join(",", deleteList.Select(x => x.GetPropertyValue(_nameField)?.ToString()));

            await Task.CompletedTask;
        }
        public override async Task After(IAOPContext context)
        {
            var op = context.ServiceProvider.GetService<IOperator>();

            op.WriteUserLog(_logType, $"删除{_dataName}:{_names}");

            await Task.CompletedTask;
        }
    }
}
