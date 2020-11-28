using EFCore.Sharding;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace Coldairarrow.Util
{
    /// <summary>
    /// MySql数据库操作帮助类
    /// </summary>
    public class MySqlHelper : DbHelper
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="conString">完整连接字符串</param>
        public MySqlHelper(string conString)
            : base(DatabaseType.MySql, conString)
        {
        }

        #endregion

        #region 私有成员

        protected override Dictionary<string, Type> DbTypeDic { get; } = new Dictionary<string, Type>
        {
            { "boolean",typeof(bool)},
            { "bit(1)",typeof(bool)},
            { "tinyint unsigned",typeof(bool)},
            { "binary",typeof(byte[])},
            { "varbinary",typeof(byte[])},
            { "blob",typeof(byte[])},
            { "longblob",typeof(byte[])},
            { "datetime",typeof(DateTime)},
            { "double",typeof(double)},
            { "char(36)",typeof(Guid)},
            { "smallint",typeof(Int16)},
            { "int",typeof(Int32)},
            { "bigint",typeof(Int64)},
            { "tinyint",typeof(bool)},
            { "float",typeof(float)},
            { "decimal",typeof(decimal)},
            { "char",typeof(string)},
            { "varchar",typeof(string)},
            { "text",typeof(string)},
            { "longtext",typeof(string)},
            { "time",typeof(TimeSpan)}
        };

        #endregion

        #region 外部接口

        /// <summary>
        /// 获取数据库中的所有表
        /// </summary>
        /// <param name="schemaName">模式（架构）</param>
        /// <returns></returns>
        public override List<DbTableInfo> GetDbAllTables(string schemaName = null)
        {
            DbProviderFactory dbProviderFactory = DbProviderFactoryHelper.GetDbProviderFactory(_dbType);
            string dbName = string.Empty;
            using (DbConnection conn = dbProviderFactory.CreateConnection())
            {
                conn.ConnectionString = _conString;
                dbName = conn.Database;
            }
            string sql = @"SELECT TABLE_NAME as TableName,table_comment as Description 
FROM INFORMATION_SCHEMA.TABLES 
WHERE TABLE_SCHEMA = @dbName";
            return GetListBySql<DbTableInfo>(sql, new List<DbParameter> { new MySqlParameter("@dbName", dbName) });
        }

        /// <summary>
        /// 通过连接字符串和表名获取数据库表的信息
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public override List<TableInfo> GetDbTableInfo(string tableName)
        {
            DbProviderFactory dbProviderFactory = DbProviderFactoryHelper.GetDbProviderFactory(_dbType);
            string dbName = string.Empty;
            using (DbConnection conn = dbProviderFactory.CreateConnection())
            {
                conn.ConnectionString = _conString;
                dbName = conn.Database;
            }

            string sql = @"select DISTINCT
	a.COLUMN_NAME as Name,
	a.DATA_TYPE as Type,
	(a.COLUMN_KEY = 'PRI') as IsKey,
	(a.IS_NULLABLE = 'YES') as IsNullable,
	a.COLUMN_COMMENT as Description,
    a.ORDINAL_POSITION
from information_schema.columns a 
where table_name=@tableName and table_schema=@dbName
ORDER BY a.ORDINAL_POSITION";
            return GetListBySql<TableInfo>(sql, new List<DbParameter> { new MySqlParameter("@tableName", tableName), new MySqlParameter("@dbName", dbName) });
        }

        /// <summary>
        /// 生成实体文件
        /// </summary>
        /// <param name="infos">表字段信息</param>
        /// <param name="tableName">表名</param>
        /// <param name="tableDescription">表描述信息</param>
        /// <param name="filePath">文件路径（包含文件名）</param>
        /// <param name="nameSpace">实体命名空间</param>
        /// <param name="schemaName">架构（模式）名</param>
        public override void SaveEntityToFile(List<TableInfo> infos, string tableName, string tableDescription, string filePath, string nameSpace, string schemaName = null)
        {
            base.SaveEntityToFile(infos, tableName, tableDescription, filePath, nameSpace, schemaName);
        }

        #endregion
    }
}
