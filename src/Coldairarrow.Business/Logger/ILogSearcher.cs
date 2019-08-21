using Coldairarrow.Entity.Base_SysManage;
using Coldairarrow.Util;
using System;
using System.Collections.Generic;

namespace Coldairarrow.Business
{
    public interface ILogSearcher
    {
        List<Base_SysLog> GetLogList(
            Pagination pagination,
            string logContent,
            string logType,
            string level,
            string opUserName,
            DateTime? startTime,
            DateTime? endTime);
    }
}
