using Coldairarrow.Entity.Base_Manage;
using CSRedis;
using EFCore.Sharding;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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
                    serilogConfig.WriteTo.File(path, rollingInterval: RollingInterval.Day, shared: true);
                if (logConfig.database.enabled)
                {
                    DatabaseType dbType = envConfig["DatabaseType"].ToEnum<DatabaseType>();
                    string conName = envConfig["ConnectionName"];
                    string conString = envConfig.GetConnectionString(conName);
                    serilogConfig.WriteTo.Database(dbType, conString);
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
            public Option database { get; set; } = new Option();
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
            string id = string.Empty;
            try
            {
                id = IdHelper.GetId();
            }
            catch
            {
                id = Guid.NewGuid().ToString();
            }
            Base_Log base_Log = new Base_Log
            {
                Id = id,
                CreateTime = DateTime.Now,
                Level = (int)logEvent.Level,
                LogContent = logEvent.RenderMessage(_formatProvider) + logEvent.Exception
            };
            Task.Factory.StartNew(async () =>
            {
                using (var db = DbFactory.GetRepository(_conString, _databaseType))
                {
                    await db.InsertAsync(base_Log);
                }
            }, TaskCreationOptions.LongRunning);
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
