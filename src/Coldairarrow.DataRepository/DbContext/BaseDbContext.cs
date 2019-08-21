using Coldairarrow.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;
using System;
using System.Data.Common;

namespace Coldairarrow.DataRepository
{
    public class BaseDbContext : DbContext
    {
        public BaseDbContext(DatabaseType dbType, DbConnection existingConnection, IModel model)
        {
            _dbType = dbType;
            _dbConnection = existingConnection;
            _model = model;
        }
        private DatabaseType _dbType { get; }
        private DbConnection _dbConnection { get; }
        private IModel _model { get; }
        private static ILoggerFactory _loggerFactory =
            new LoggerFactory(new ILoggerProvider[] { new EFCoreSqlLogeerProvider() });
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            switch (_dbType)
            {
                case DatabaseType.SqlServer: optionsBuilder.UseSqlServer(_dbConnection, x => x.UseRowNumberForPaging()); break;
                case DatabaseType.MySql: optionsBuilder.UseMySql(_dbConnection); break;
                case DatabaseType.PostgreSql: optionsBuilder.UseNpgsql(_dbConnection); break;
                case DatabaseType.Oracle: optionsBuilder.UseOracle(_dbConnection, x => x.UseOracleSQLCompatibility("11")); break;
                default: throw new Exception("暂不支持该数据库！");
            }
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseModel(_model);
            optionsBuilder.UseLoggerFactory(_loggerFactory);
        }
    }
}
