using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.Base_Manage
{
    public interface IBase_UserLogBusiness
    {
        Task<List<Base_UserLog>> GetLogListAsync(
           Pagination pagination,
           string logContent,
           string logType,
           string opUserName,
           DateTime? startTime,
           DateTime? endTime);
    }
}