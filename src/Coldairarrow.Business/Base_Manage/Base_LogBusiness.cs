using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using EFCore.Sharding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public async Task<List<Base_Log>> GetLogListAsync(
            Pagination pagination,
            int? level,
            string logContent,
            DateTime? startTime,
            DateTime? endTime)
        {
            var whereExp = LinqHelper.True<Base_Log>();
            if (!level.IsNullOrEmpty())
                whereExp = whereExp.And(x => x.Level == level);
            if (!logContent.IsNullOrEmpty())
                whereExp = whereExp.And(x => x.LogContent.Contains(logContent));
            if (!startTime.IsNullOrEmpty())
                whereExp = whereExp.And(x => x.CreateTime >= startTime);
            if (!endTime.IsNullOrEmpty())
                whereExp = whereExp.And(x => x.CreateTime <= endTime);

            return await GetIQueryable().Where(whereExp).GetPagination(pagination).ToListAsync();
        }
    }
}