using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using EFCore.Sharding;
using LinqKit;
using System.Linq;
using System.Threading.Tasks;

namespace Coldairarrow.Business.Base_Manage
{
    public class Base_UserLogBusiness : BaseBusiness<Base_UserLog>, IBase_UserLogBusiness, ITransientDependency
    {
        public Base_UserLogBusiness(IRepository repository)
            : base(repository)
        {
        }

        public async Task<PageResult<Base_UserLog>> GetLogListAsync(UserLogsInputDTO input)
        {
            var whereExp = LinqHelper.True<Base_UserLog>();
            if (!input.logContent.IsNullOrEmpty())
                whereExp = whereExp.And(x => x.LogContent.Contains(input.logContent));
            if (!input.logType.IsNullOrEmpty())
                whereExp = whereExp.And(x => x.LogType == input.logType);
            if (!input.opUserName.IsNullOrEmpty())
                whereExp = whereExp.And(x => x.CreatorRealName.Contains(input.opUserName));
            if (!input.startTime.IsNullOrEmpty())
                whereExp = whereExp.And(x => x.CreateTime >= input.startTime);
            if (!input.endTime.IsNullOrEmpty())
                whereExp = whereExp.And(x => x.CreateTime <= input.endTime);

            return await GetIQueryable().Where(whereExp).GetPageResultAsync(input);
        }
    }
}