using Coldairarrow.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Coldairarrow.DataRepository
{
    /// <summary>
    /// 分库分表读写分离配置
    /// </summary>
    public class ShardingConfig
    {
        #region 构造函数

        /// <summary>
        /// 私有构造函数
        /// </summary>
        private ShardingConfig()
        {
            
        }

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static ShardingConfig()
        {
            Init();
        }

        #endregion

        #region 外部接口

        /// <summary>
        /// 初始化配置
        /// </summary>
        public static void Init()
        {
        }

        /// <summary>
        /// 获取实例
        /// </summary>
        public static ShardingConfig Instance { get => _instance; }

        /// <summary>
        /// 是否启用
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <returns></returns>
        public bool IsSharding<T>()
        {
            return _absDb.Any(x => x.Tables.Any(y => y.AbsTableName == typeof(T).Name));
        }

        /// <summary>
        /// 添加数据源
        /// </summary>
        /// <param name="dataSourceName">数据源名</param>
        /// <param name="dbType">数据库类型</param>
        /// <param name="dbs">一组读写数据库</param>
        public void AddDataSource(string dataSourceName, DatabaseType dbType, List<(string conString, ReadWriteType opType)> dbs)
        {
            _dataSource.Add(new DataSource
            {
                DataSourceName = dataSourceName,
                DbType = dbType,
                Dbs = dbs
            });
        }

        /// <summary>
        /// 添加抽象数据库
        /// </summary>
        /// <param name="absDbName">抽象数据库名</param>
        /// <param name="tables">抽象数据表</param>
        public void AddAbsDatabase(string absDbName, List<AbstractTable> tables)
        {
            _absDb.Add(new AbstractDatabse
            {
                AbsDbName=absDbName,
                Tables=tables
            });
        }

        /// <summary>
        /// 获取读表
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <param name="absDbName">抽象数据库名</param>
        /// <returns></returns>
        public List<(string tableName, string conString, DatabaseType dbType)> GetReadTables(string absTableName, string absDbName = null)
        {
            return GetTargetTables(absTableName, ReadWriteType.Read, absDbName);
        }

        /// <summary>
        /// 获取特定写表
        /// </summary>
        /// <param name="absTableName">抽象表名</param>
        /// <param name="obj">实体对象</param>
        /// <param name="absDbName">抽象数据库名</param>
        /// <returns></returns>
        public (string tableName, string conString, DatabaseType dbType) GetTheWriteTable(string absTableName, object obj, string absDbName = null)
        {
            return GetTargetTables(absTableName, ReadWriteType.Write, absDbName, obj).Single();
        }

        /// <summary>
        /// 获取所有的写表
        /// </summary>
        /// <param name="absTableName">抽象表名</param>
        /// <param name="absDbName">抽象数据库名</param>
        /// <returns></returns>
        public List<(string tableName, string conString, DatabaseType dbType)> GetAllWriteTables(string absTableName, string absDbName = null)
        {
            return GetTargetTables(absTableName, ReadWriteType.Write, absDbName, null);
        }

        #endregion

        #region 私有成员

        private static ShardingConfig _instance { get; } = new ShardingConfig();
        private List<(string tableName, string conString, DatabaseType dbType)> GetTargetTables(string absTableName, ReadWriteType opType, string absDbName, object obj = null)
        {
            //获取抽象数据库
            AbstractDatabse db = null;
            if (absDbName.IsNullOrEmpty())
                db = _absDb.Single();
            else
                db = _absDb.Where(x => x.AbsDbName == absDbName).Single();
            if (db == null)
                throw new Exception("请配置抽象数据库");

            //获取抽象数据表
            var absTable = db.Tables.Where(x => x.AbsTableName == absTableName).Single();

            //获取物理表
            List<(string physicTableName, string dataSourceName)> physicTables = null;
            //读操作获取全部表
            if (opType == ReadWriteType.Read)
            {
                physicTables = absTable.PhysicTables;
            }
            //写操作
            else
            {
                //找特定表
                if (!obj.IsNullOrEmpty())
                {
                    var theTable = absTable.FindTable(obj);
                    physicTables = absTable.PhysicTables.Where(x => x.physicTableName == theTable).ToList();
                }
                //所有表
                else
                    physicTables = absTable.PhysicTables;
            }

            //获取数据源
            var dataSources = _dataSource
                .Where(x => physicTables.Select(y => y.dataSourceName).Contains(x.DataSourceName))
                .Select(x => new { x.DataSourceName, x.DbType, RandomHelper.Next(x.Dbs.Where(y => y.opType.HasFlag(opType)).ToList()).conString })
                .ToList();

            var q = from a in physicTables
                    join b in dataSources on a.dataSourceName equals b.DataSourceName
                    select (a.physicTableName, b.conString, b.DbType);
            var resList = q.ToList();

            return resList;
        }
        private SynchronizedCollection<DataSource> _dataSource { get; } = new SynchronizedCollection<DataSource>();
        private SynchronizedCollection<AbstractDatabse> _absDb { get; } = new SynchronizedCollection<AbstractDatabse>();

        #endregion
    }

    /// <summary>
    /// 读写类型
    /// </summary>
    public enum ReadWriteType
    {
        Read = 1,
        Write = 2,
        ReadAndWrite = 3
    }

    /// <summary>
    /// 数据源
    /// 注：一组读写数据库为一个数据源,它们中表结构一致,需要开启主从复制或主主复制
    /// </summary>
    public class DataSource
    {
        public string DataSourceName { get; set; }
        public DatabaseType DbType { get; set; }
        public List<(string conString, ReadWriteType opType)> Dbs { get; set; } = new List<(string conString, ReadWriteType opType)>();
    }

    /// <summary>
    /// 抽象数据库
    /// 注：即将所有读库与写库视为同一个整体数据库
    /// </summary>
    public class AbstractDatabse
    {
        public string AbsDbName { get; set; }
        public List<AbstractTable> Tables { get; set; } = new List<AbstractTable>();
    }

    /// <summary>
    /// 抽象表
    /// 注：属于抽象数据库
    /// </summary>
    public class AbstractTable
    {
        public string AbsTableName { get; set; }
        public List<(string physicTableName, string dataSourceName)> PhysicTables { get; set; } = new List<(string physicTableName, string dataSourceName)>();
        public Func<object, string> FindTable { get; set; }
    }
}
