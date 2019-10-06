using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using NLog;
using NLog.Targets;

namespace Coldairarrow.Business
{
    public class BaseTarget : TargetWithLayout
    {
        public BaseTarget()
        {
            Name = "系统日志";
            Layout = LoggerConfig.Layout;
        }

        protected Base_Log GetBase_SysLogInfo(LogEventInfo logEventInfo)
        {
            Base_Log newLog = new Base_Log
            {
                Id = IdHelper.GetId(),
                Data = logEventInfo.Properties[LoggerConfig.Data] as string,
                Level = logEventInfo.Level.ToString(),
                LogContent = logEventInfo.Message,
                LogType = logEventInfo.Properties[LoggerConfig.LogType] as string,
                CreateTime = logEventInfo.TimeStamp,
                CreatorId = logEventInfo.Properties[LoggerConfig.CreatorId] as string,
                CreatorRealName = logEventInfo.Properties[LoggerConfig.CreatorRealName] as string
            };

            return newLog;
        }
    }
}
