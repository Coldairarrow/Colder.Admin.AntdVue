using Coldairarrow.Entity.Base_SysManage;
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

        protected Base_SysLog GetBase_SysLogInfo(LogEventInfo logEventInfo)
        {
            Base_SysLog newLog = new Base_SysLog
            {
                Id = IdHelper.GetId(),
                Data = logEventInfo.Properties[LoggerConfig.Data] as string,
                Level = logEventInfo.Level.ToString(),
                LogContent = logEventInfo.Message,
                LogType = logEventInfo.Properties[LoggerConfig.LogType] as string,
                OpTime = logEventInfo.TimeStamp,
                OpUserName = logEventInfo.Properties[LoggerConfig.OpUserName] as string
            };

            return newLog;
        }
    }
}
