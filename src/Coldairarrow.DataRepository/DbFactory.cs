using Coldairarrow.Util;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace Coldairarrow.DataRepository
{
    /// <summary>
    /// 数据库工厂
    /// </summary>
    public class DbFactory
    {
        #region 外部接口

        /// <summary>
        /// 根据配置文件获取数据库类型，并返回对应的工厂接口
        /// </summary>
        /// <param name="conString">链接字符串,默认为GlobalSwitch.DefaultDbConName</param>
        /// <param name="dbType">数据库类型,默认为GlobalSwitch.DatabaseType</param>
        /// <returns></returns>
        public static IRepository GetRepository(string conString = null, DatabaseType? dbType = null)
        {
            conString = conString.IsNullOrEmpty() ? GlobalSwitch.DefaultDbConName : conString;
            conString = DbProviderFactoryHelper.GetFullConString(conString);
            dbType = dbType.IsNullOrEmpty() ? GlobalSwitch.DatabaseType : dbType;
            Type dbRepositoryType = Type.GetType("Coldairarrow.DataRepository." + DbProviderFactoryHelper.DbTypeToDbTypeStr(dbType.Value) + "Repository");

            var repository = Activator.CreateInstance(dbRepositoryType, new object[] { conString }) as IRepository;

            return repository;
        }

        /// <summary>
        /// 获取ShardingRepository
        /// </summary>
        /// <returns></returns>
        public static IShardingRepository GetShardingRepository()
        {
            return new ShardingRepository(GetRepository());
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="conString"></param>
        /// <param name="dbType"></param>
        /// <returns></returns>
        internal static BaseDbContext GetDbContext([NotNull] string conString, DatabaseType dbType)
        {
            if (conString.IsNullOrEmpty())
                throw new Exception("conString能为空");
            var dbConnection = DbProviderFactoryHelper.GetDbConnection(conString, dbType);
            var model = DbModelFactory.GetDbCompiledModel(conString, dbType);
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder();

            switch (dbType)
            {
                case DatabaseType.SqlServer: builder.UseSqlServer(dbConnection, x => x.UseRowNumberForPaging()); break;
                case DatabaseType.MySql: builder.UseMySql(dbConnection); break;
                case DatabaseType.PostgreSql: builder.UseNpgsql(dbConnection); break;
                case DatabaseType.Oracle: builder.UseOracle(dbConnection, x => x.UseOracleSQLCompatibility("11")); break;
                default: throw new Exception("暂不支持该数据库！");
            }
            builder.EnableSensitiveDataLogging();
            builder.UseModel(model);
            builder.UseLoggerFactory(_loggerFactory);

            return new BaseDbContext(builder.Options);
        }

        private static ILoggerFactory _loggerFactory =
            new LoggerFactory(new ILoggerProvider[] { new EFCoreSqlLogeerProvider() });

        #endregion
    }
}
