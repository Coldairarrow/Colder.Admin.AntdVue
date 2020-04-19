using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using EFCore.Sharding;
using LinqKit;
using System.Linq;
using System.Threading.Tasks;

namespace Coldairarrow.Business.Base_Manage
{
    public class Base_LogBusiness : BaseBusiness<Base_Log>, IBase_LogBusiness, ITransientDependency
    {
        public Base_LogBusiness(IRepository repository)
            : base(repository)
        {
        }

        public async Task<PageResult<Base_Log>> GetLogListAsync(PageInput<LogsInputDTO> input)
        {
            var whereExp = LinqHelper.True<Base_Log>();
            var search = input.Search;
            if (!search.level.IsNullOrEmpty())
                whereExp = whereExp.And(x => x.Level == search.level);
            if (!search.logContent.IsNullOrEmpty())
                whereExp = whereExp.And(x => x.LogContent.Contains(search.logContent));
            if (!search.startTime.IsNullOrEmpty())
                whereExp = whereExp.And(x => x.CreateTime >= search.startTime);
            if (!search.endTime.IsNullOrEmpty())
                whereExp = whereExp.And(x => x.CreateTime <= search.endTime);

            return await GetIQueryable().Where(whereExp).GetPageResultAsync(input);
        }
    }
}