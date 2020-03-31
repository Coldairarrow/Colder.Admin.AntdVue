using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business
{
    public interface ILogSearcher
    {
        Task<List<Base_Log>> GetLogListAsync(
           Pagination pagination,
           string logContent,
           string logType,
           string level,
           string opUserName,
           DateTime? startTime,
           DateTime? endTime);
    }
}
