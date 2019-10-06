namespace Coldairarrow.Business
{
    class LoggerConfig
    {
        public static readonly string LoggerName = "SysLog";
        public static readonly string LogType = "LogType";
        public static readonly string CreatorId = "CreatorId";
        public static readonly string CreatorRealName = "OpUserName";
        public static readonly string Data = "Data";
        public static readonly string Layout = $@"${{date:format=yyyy-MM-dd HH\:mm\:ss}}|${{level}}|日志类型:${{event-properties:item={LogType}}}|操作员:${{event-properties:item={CreatorRealName}}}|内容:${{message}}|备份数据:${{event-properties:item={Data}}}";
    }
}