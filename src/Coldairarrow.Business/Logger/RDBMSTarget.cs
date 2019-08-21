using Coldairarrow.DataRepository;
using Coldairarrow.Entity.Base_SysManage;
using Coldairarrow.Util;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Coldairarrow.Business
{
    public class RDBMSTarget : BaseTarget, ILogSearcher
    {
        public List<Base_SysLog> GetLogList(
            Pagination pagination,
            string logContent,
            string logType,
            string level,
            string opUserName,
            DateTime? startTime,
            DateTime? endTime)
        {
            using (var db = DbFactory.GetRepository())
            {
                var whereExp = LinqHelper.True<Base_SysLog>();
                if (!logContent.IsNullOrEmpty())
                    whereExp = whereExp.And(x => x.LogContent.Contains(logContent));
                if (!logType.IsNullOrEmpty())
                    whereExp = whereExp.And(x => x.LogType == logType);
                if (!level.IsNullOrEmpty())
                    whereExp = whereExp.And(x => x.Level == level);
                if (!opUserName.IsNullOrEmpty())
                    whereExp = whereExp.And(x => x.OpUserName.Contains(opUserName));
                if (!startTime.IsNullOrEmpty())
                    whereExp = whereExp.And(x => x.OpTime >= startTime);
                if (!endTime.IsNullOrEmpty())
                    whereExp = whereExp.And(x => x.OpTime <= endTime);

                return db.GetIQueryable<Base_SysLog>().Where(whereExp).GetPagination(pagination).ToList();
            }
        }

        protected override void Write(LogEventInfo logEvent)
        {
            using (var db = DbFactory.GetRepository())
            {
                db.Insert(GetBase_SysLogInfo(logEvent));
            }
        }
    }
}
