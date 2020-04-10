using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.Base_Manage
{
    public interface IBase_LogBusiness
    {
        Task<List<Base_Log>> GetLogListAsync(
           Pagination pagination,
           int? level,
           string logContent,
           DateTime? startTime,
           DateTime? endTime);
    }
}