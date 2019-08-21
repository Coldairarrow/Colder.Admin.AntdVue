using Coldairarrow.Util;
using System;
using System.Collections.Generic;

namespace Coldairarrow.DataRepository
{
    /// <summary>
    /// 分库分表配置生成器
    /// </summary>
    public class ShardingConfigBootstrapper : IShardingConfigBuilder, IAddDataSource, IAddAbstractTable, IAddAbstractDatabse, IAddPhysicDb, IAddPhysicTable
    {
        #region 构造函数

        private ShardingConfigBootstrapper()
        {

        }

        #endregion

        #region 外部接口

        /// <summary>
        /// 引导
        /// </summary>
        /// <returns></returns>
        public static IShardingConfigBuilder Bootstrap()
        {
            return new ShardingConfigBootstrapper();
        }

        public IShardingConfigBuilder AddDataSource(string dataSourceName, DatabaseType dbType, Action<IAddPhysicDb> physicDbBuilder)
        {
            IAddPhysicDb builder = new ShardingConfigBootstrapper();
            physicDbBuilder(builder);
            var value = builder.GetPropertyValue("_physicDbs") as List<(string conString, ReadWriteType opType)>;
            _config.AddDataSource(dataSourceName, dbType, value);

            return this;
        }

        public IShardingConfigBuilder AddAbsDb(string absDbName, Action<IAddAbstractTable> absTableBuilder)
        {
            var builder = new ShardingConfigBootstrapper();
            absTableBuilder(builder);
            var asbTables = builder.GetPropertyValue("_absTables") as List<AbstractTable>;
            _config.AddAbsDatabase(absDbName, asbTables);

            return this;
        }

        public ShardingConfig Build()
        {
            return _config;
        }

        void IAddPhysicDb.AddPhsicDb(string conString, ReadWriteType opType)
        {
            _physicDbs.Add((conString, opType));
        }

        void IAddPhysicTable.AddPhsicTable(string physicTableName, string dataSourceName)
        {
            _physicTables.Add((physicTableName, dataSourceName));
        }

        void IAddAbstractTable.AddAbsTable(string absTableName, Action<IAddPhysicTable> physicTableBuilder, Func<object, string> findTable)
        {
            IAddPhysicTable physicBuilder = new ShardingConfigBootstrapper();
            physicTableBuilder(physicBuilder);
            var value = physicBuilder.GetPropertyValue("_physicTables") as List<(string physicTableName, string dataSourceName)>;
            _absTables.Add(new AbstractTable
            {
                AbsTableName = absTableName,
                FindTable = findTable,
                PhysicTables = value
            });
        }

        void IAddAbstractTable.AddAbsTable(string absTableName, Action<IAddPhysicTable> physicTableBuilder, IShardingRule rule)
        {
            (this as IAddAbstractTable).AddAbsTable(absTableName, physicTableBuilder, rule.FindTable);
        }

        #endregion

        #region 私有成员

        private ShardingConfig _config { get; } = ShardingConfig.Instance;
        private List<AbstractTable> _absTables { get; } = new List<AbstractTable>();
        private List<(string conString, ReadWriteType opType)> _physicDbs { get; } = new List<(string conString, ReadWriteType opType)>();
        private List<(string physicTableName, string dataSourceName)> _physicTables { get; } = new List<(string physicTableName, string dataSourceName)>();

        #endregion
    }

    public interface IShardingConfigBuilder : IAddDataSource, IAddAbstractDatabse
    {
        /// <summary>
        /// 生成配置
        /// </summary>
        /// <returns></returns>
        ShardingConfig Build();
    }

    public interface IAddDataSource
    {
        /// <summary>
        /// 添加数据源
        /// </summary>
        /// <param name="dataSourceName">数据源名</param>
        /// <param name="dbType">数据库类型</param>
        /// <param name="physicDbBuilder">物理表构造器</param>
        /// <returns></returns>
        IShardingConfigBuilder AddDataSource(string dataSourceName, DatabaseType dbType, Action<IAddPhysicDb> physicDbBuilder);
    }

    public interface IAddPhysicDb
    {
        /// <summary>
        /// 添加物理数据库
        /// </summary>
        /// <param name="conString">连接字符串</param>
        /// <param name="opType">数据库类型</param>
        void AddPhsicDb(string conString, ReadWriteType opType);
    }

    public interface IAddAbstractTable
    {
        /// <summary>
        /// 添加抽象表
        /// </summary>
        /// <param name="absTableName">抽象表名</param>
        /// <param name="physicTableBuilder">物理表构造器</param>
        /// <param name="findTable">分表规则</param>
        void AddAbsTable(string absTableName, Action<IAddPhysicTable> physicTableBuilder, Func<object, string> findTable);

        /// <summary>
        /// 添加抽象表
        /// </summary>
        /// <param name="absTableName">抽象表名</param>
        /// <param name="physicTableBuilder">物理表构造器</param>
        /// <param name="rule">找表规则</param>
        void AddAbsTable(string absTableName, Action<IAddPhysicTable> physicTableBuilder, IShardingRule rule);
    }

    public interface IAddPhysicTable
    {
        /// <summary>
        /// 添加物理表
        /// </summary>
        /// <param name="physicTableName">物理表名</param>
        /// <param name="dataSourceName">数据源名</param>
        void AddPhsicTable(string physicTableName, string dataSourceName);
    }

    public interface IAddAbstractDatabse
    {
        /// <summary>
        /// 添加抽象数据库
        /// </summary>
        /// <param name="absDbName">抽象数据库名</param>
        /// <param name="absTableBuilder">抽象表构造器</param>
        /// <returns></returns>
        IShardingConfigBuilder AddAbsDb(string absDbName, Action<IAddAbstractTable> absTableBuilder);
    }
}
