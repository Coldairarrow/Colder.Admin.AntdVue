using CSRedis;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

            return hostBuilder.UseSerilog((hostingContext, serilogConfig) =>
            {
                var envConfig = hostingContext.Configuration;

                LogConfig logConfig = new LogConfig();
                envConfig.GetSection("log").Bind(logConfig);

                logConfig.overrides.ForEach(aOverride =>
                {
                    serilogConfig.MinimumLevel.Override(aOverride.source, aOverride.minlevel.ToEnum<LogEventLevel>());
                });

                serilogConfig.MinimumLevel.Is(logConfig.minlevel.ToEnum<LogEventLevel>());
                if (logConfig.console.enabled)
                    serilogConfig.WriteTo.Console();
                if (logConfig.debug.enabled)
                    serilogConfig.WriteTo.Debug();
                if (logConfig.file.enabled)
                {
                    string template = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3} {SourceContext:l}] {Message:lj}{NewLine}{Exception}";

                    serilogConfig.WriteTo.File(
                        path,
                        outputTemplate: template,
                        rollingInterval: RollingInterval.Day,
                        shared: true,
                        fileSizeLimitBytes: 10 * 1024 * 1024,
                        rollOnFileSizeLimit: true
                        );
                }
                if (logConfig.elasticsearch.enabled)
                {
                    var uris = logConfig.elasticsearch.nodes.Select(x => new Uri(x)).ToList();

                    serilogConfig.WriteTo.Elasticsearch(new ElasticsearchSinkOptions(uris)
                    {
                        IndexFormat = logConfig.elasticsearch.indexformat,
                        AutoRegisterTemplate = true,
                        AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7
                    });
                }
            });
        }

        /// <summary>
        /// 使用IdHelper
        /// </summary>
        /// <param name="hostBuilder">建造者</param>
        /// <returns></returns>
        public static IHostBuilder UseIdHelper(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices((buidler, services) =>
            {
                new IdHelperBootstrapper()
                    //设置WorkerId
                    .SetWorkderId(buidler.Configuration["WorkerId"].ToLong())
                    //使用Zookeeper
                    //.UseZookeeper("127.0.0.1:2181", 200, GlobalSwitch.ProjectName)
                    .Boot();
            });

            return hostBuilder;
        }

        /// <summary>
        /// 使用缓存
        /// </summary>
        /// <param name="hostBuilder">建造者</param>
        /// <returns></returns>
        public static IHostBuilder UseCache(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices((buidlerContext, services) =>
            {
                var cacheOption = buidlerContext.Configuration.GetSection("Cache").Get<CacheOption>();
                switch (cacheOption.CacheType)
                {
                    case CacheType.Memory: services.AddDistributedMemoryCache(); break;
                    case CacheType.Redis:
                        {
                            var csredis = new CSRedisClient(cacheOption.RedisEndpoint);
                            RedisHelper.Initialization(csredis);
                            services.AddSingleton(csredis);
                            services.AddSingleton<IDistributedCache>(new CSRedisCache(RedisHelper.Instance));
                        }; break;
                    default: throw new Exception("缓存类型无效");
                }
            });

            return hostBuilder;
        }

        class CacheOption
        {
            public CacheType CacheType { get; set; }
            public string RedisEndpoint { get; set; }
        }

        class LogConfig
        {
            public string minlevel { get; set; }
            public Option console { get; set; } = new Option();
            public Option debug { get; set; } = new Option();
            public Option file { get; set; } = new Option();
            public Option elasticsearch { get; set; } = new Option();
            public List<OverrideConfig> overrides { get; set; } = new List<OverrideConfig>();
        }

        class Option
        {
            public bool enabled { get; set; }
            public List<string> nodes { get; set; } = new List<string>();
            public string indexformat { get; set; }
        }

        class OverrideConfig
        {
            public string source { get; set; }
            public string minlevel { get; set; }
        }
    }
}
