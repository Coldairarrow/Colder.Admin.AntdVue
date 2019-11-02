using Microsoft.AspNetCore.Hosting;
using System;

namespace Coldairarrow.Util
{
    /// <summary>
    /// 全局控制
    /// </summary>
    public class GlobalSwitch
    {
        #region 构造函数

        static GlobalSwitch()
        {
#if !DEBUG
            RunModel = RunModel.Publish;
#endif
        }

        #endregion

        #region 参数

        /// <summary>
        /// 超级管理员Id
        /// </summary>
        public const string AdminId = "Admin";

        /// <summary>
        /// 项目名
        /// </summary>
        public const string ProjectName = "Colder.Admin.AntdVue";

        /// <summary>
        /// 网站根地址
        /// </summary>
        public static string WebRootUrl
        {
            get
            {
                if (RunModel == RunModel.Publish)
                    return PublishRootUrl;
                else
                    return localRootUrl;
            }
        }

        /// <summary>
        /// 发布后网站根地址
        /// </summary>
        public const string PublishRootUrl = localRootUrl;

        /// <summary>
        /// 本地调试根地址
        /// </summary>
        public const string localRootUrl = "http://localhost:40000";

        #endregion

        #region 运行

        /// <summary>
        /// 运行模式
        /// </summary>
        public static readonly RunModel RunModel = RunModel.Publish;

        /// <summary>
        /// 网站文件根路径
        /// </summary>
        public static readonly string WebRootPath = AutofacHelper.GetService<IHostingEnvironment>().WebRootPath;

        #endregion

        #region 数据库

        /// <summary>
        /// 默认数据库类型
        /// </summary>
        public static readonly DatabaseType DatabaseType = DatabaseType.SqlServer;

        /// <summary>
        /// 默认数据库连接名
        /// </summary>
        public const string DefaultDbConName = "BaseDb";

        #endregion

        #region 缓存

        /// <summary>
        /// 默认缓存
        /// </summary>
        public static readonly CacheType CacheType = CacheType.SystemCache;

        /// <summary>
        /// Redis配置字符串
        /// </summary>
        public const string RedisConfig = null /*"61.153.17.101:6379"*/;

        #endregion

        #region 日志相关

        /// <summary>
        /// 日志记录方式
        /// 注:可用位运算,LoggerType.RDBMS | LoggerType.File表示同时记录到数据库和文件
        /// </summary>
        public static readonly LoggerType LoggerType = LoggerType.RDBMS;

        /// <summary>
        /// ElasticSearch服务器配置
        /// </summary>
        public static readonly Uri[] ElasticSearchNodes = new Uri[] { new Uri("http://localhost:9200/") };

        #endregion
    }

    /// <summary>
    /// 运行模式
    /// </summary>
    public enum RunModel
    {
        /// <summary>
        /// 本地测试模式，默认Admin账户，不需要登录
        /// </summary>
        LocalTest,

        /// <summary>
        /// 发布模式
        /// </summary>
        Publish
    }

    /// <summary>
    /// 默认缓存类型
    /// </summary>
    public enum CacheType
    {
        /// <summary>
        /// 系统缓存
        /// </summary>
        SystemCache,

        /// <summary>
        /// Redis缓存
        /// </summary>
        RedisCache
    }
}