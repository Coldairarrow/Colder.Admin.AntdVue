using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

                if (logConfig.console.enabled)
                    loggerConfiguration.WriteTo.Console();
                if (logConfig.debug.enabled)
                    loggerConfiguration.WriteTo.Debug();
                if (logConfig.file.enabled)
                    loggerConfiguration.WriteTo.File(path, rollingInterval: RollingInterval.Day, shared: true);
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
}
