using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using System;

namespace Coldairarrow.DataRepository
{
    public class EFCoreSqlLogeerProvider : ILoggerProvider
    {
        public static Action<string> HandleSqlLog { get; set; }

        public ILogger CreateLogger(string categoryName)
        {
            return new MyLogger();
        }

        public void Dispose()
        { }

        private class MyLogger : ILogger
        {
            public bool IsEnabled(LogLevel logLevel)
            {
                return true;
            }

            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
            {
                //只记录SQL执行日志
                if (eventId.Id == RelationalEventId.CommandExecuted.Id)
                {
                    string logContent = formatter(state, exception);
                    HandleSqlLog?.Invoke(logContent);
                }
            }

            public IDisposable BeginScope<TState>(TState state)
            {
                return null;
            }
        }
    }
}