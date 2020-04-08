using EFCore.Sharding;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;
using Serilog.Parsing;
using Serilog.Sinks.Elasticsearch;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Coldairarrow.Util
{
    /// <summary>
    /// 拓展类
    /// </summary>
    public static partial class Extention
    {
        /// <summary>
        /// 配置日志
        /// </summary>
        /// <param name="hostBuilder">建造者</param>
        /// <returns></returns>
        public static IHostBuilder UseLog(this IHostBuilder hostBuilder)
        {
            var rootPath = AppDomain.CurrentDomain.BaseDirectory;
            var path = Path.Combine(rootPath, "logs", "log.txt");

            return hostBuilder.UseSerilog((hostingContext, loggerConfiguration) =>
            {
                loggerConfiguration
                    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning);

                var configStr = hostingContext.Configuration.GetSection("log");

                LogConfig logConfig = new LogConfig();
                hostingContext.Configuration.GetSection("log").Bind(logConfig);

                loggerConfiguration.MinimumLevel.Is((LogEventLevel)logConfig.minlevel);
                if (logConfig.console.enabled)
                    loggerConfiguration.WriteTo.Console();
                if (logConfig.debug.enabled)
                    loggerConfiguration.WriteTo.Debug();
                if (logConfig.file.enabled)
                    loggerConfiguration.WriteTo.File(path, rollingInterval: RollingInterval.Day, shared: true);
                if (logConfig.database.enabled)
                    loggerConfiguration.WriteTo.Database(DatabaseType.SqlServer, "");
                if (logConfig.elasticsearch.enabled)
                {
                    var uris = logConfig.elasticsearch.nodes.Select(x => new Uri(x)).ToList();

                    loggerConfiguration.WriteTo.Elasticsearch(new ElasticsearchSinkOptions(uris)
                    {
                        AutoRegisterTemplate = true,
                        AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7
                    });
                }
            });
        }

        class LogConfig
        {
            public int minlevel { get; set; }
            public Option console { get; set; } = new Option();
            public Option debug { get; set; } = new Option();
            public Option file { get; set; } = new Option();
            public Option database { get; set; } = new Option();
            public Option elasticsearch { get; set; } = new Option();
        }

        class Option
        {
            public bool enabled { get; set; }
            public List<string> nodes { get; set; } = new List<string>();
        }
    }

    public class DatabaseSink : ILogEventSink
    {
        private readonly IFormatProvider _formatProvider;
        private readonly DatabaseType _databaseType;
        private readonly string _conString;
        public DatabaseSink(DatabaseType databaseType, string conString, IFormatProvider formatProvider)
        {
            _formatProvider = formatProvider;
            _databaseType = databaseType;
            _conString = conString;
        }

        public void Emit(LogEvent logEvent)
        {
            StringBuilder stringBuilder = new StringBuilder();
            logEvent.MessageTemplate.Tokens.ForEach(aToken =>
            {
                if (aToken is TextToken textToken)
                    stringBuilder.Append(textToken.Text);
                else if (aToken is PropertyToken proppertyToken)
                {
                    var value = logEvent.Properties[proppertyToken.PropertyName];
                    if (value is ScalarValue scalarValue)
                    {
                        if (scalarValue.Value is string)
                            stringBuilder.Append(scalarValue.Value);
                        else
                            stringBuilder.Append(scalarValue.Value.ToJson());

                        stringBuilder.Append("\r\n");
                    }
                }
            });
            Console.WriteLine(DateTimeOffset.Now.ToString() + " " + stringBuilder.ToString());
        }
    }

    public static class DatabaseSinkExtensions
    {
        public static LoggerConfiguration Database(
            this LoggerSinkConfiguration loggerConfiguration,
            DatabaseType databaseType,
            string conString,
            IFormatProvider formatProvider = null)
        {
            return loggerConfiguration.Sink(new DatabaseSink(databaseType, conString, formatProvider));
        }
    }
}
