using System;

namespace Coldairarrow.Util
{
    public interface ILogger
    {
        void Log(LogLevel logLevel, LogType logType, string msg);
        void Log(LogLevel logLevel, LogType logType, string msg, string data);
        void Trace(LogType logType, string msg);
        void Trace(LogType logType, string msg, string data);
        void Debug(LogType logType, string msg);
        void Debug(LogType logType, string msg, string data);
        void Info(LogType logType, string msg);
        void Info(LogType logType, string msg, string data);
        void Warn(LogType logType, string msg);
        void Warn(LogType logType, string msg, string data);
        void Error(LogType logType, string msg);
        void Error(LogType logType, string msg, string data);
        void Error(Exception ex);
        void Fatal(LogType logType, string msg);
        void Fatal(LogType logType, string msg, string data);
    }
}
