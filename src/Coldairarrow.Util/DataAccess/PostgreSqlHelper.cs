using EFCore.Sharding;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Net;
using System.Net.NetworkInformation;

namespace Coldairarrow.Util
{
    /// <summary>
    /// PostgreSql数据库操作帮助类
    /// </summary>
    public class PostgreSqlHelper : DbHelper
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="conString">完整连接字符串</param>
        public PostgreSqlHelper(string conString)
            : base(DatabaseType.PostgreSql, conString)
        {

        }

        #endregion

        #region 私有成员

        protected override Dictionary<string, Type> DbTypeDic { get; } = new Dictionary<string, Type>()
        {
            { "bool", typeof(bool) },
            { "int4", typeof(int) },
            { "int8", typeof(long) },
            { "float4", typeof(float) },
            { "float8", typeof(decimal) },
            { "numeric", typeof(decimal) },
            { "money", typeof(decimal) },
            { "text", typeof(string) },
            { "varchar", typeof(string) },
            { "bpchar", typeof(string) },
            { "citext", typeof(string) },
            { "json", typeof(string) },
            { "jsonb", typeof(string) },
            { "xml", typeof(string) },
            //{ "point", typeof(NpgsqlPoint) },
            //{ "lseg", typeof(NpgsqlLSeg) },
            //{ "path", typeof(NpgsqlPath) },
            //{ "polygon", typeof(NpgsqlPolygon) },
            //{ "line", typeof(NpgsqlLine) },
            //{ "circle", typeof(NpgsqlCircle) },
            //{ "box", typeof(NpgsqlBox) },
            { "bit(1)", typeof(bool) },
            { "bit(n)", typeof(BitArray) },
            { "varbit", typeof(BitArray) },
            { "hstore", typeof(IDictionary) },
            { "uuid", typeof(Guid) },
            { "cidr", typeof(ValueTuple<IPAddress,int>) },
            { "inet", typeof(IPAddress) },
            { "macaddr", typeof(PhysicalAddress) },
            { "tsquery", typeof(NpgsqlTsQuery) },
            { "tsvector", typeof(NpgsqlTsVector) },
            { "date", typeof(DateTime) },
            { "interval", typeof(TimeSpan) },
            { "timestamp", typeof(DateTime) },
            { "timestamptz", typeof(DateTime) },
            { "time", typeof(TimeSpan) },
            { "timetz", typeof(DateTimeOffset) },
            { "bytea", typeof(byte[]) },
            { "oid", typeof(uint) },
            { "xid", typeof(uint) },
            { "cid", typeof(uint) },
            { "oidvector", typeof(uint[]) },
            { "name", typeof(string) },
            { "(internal) char", typeof(char) },
            //{ "geometry (PostGIS)", typeof(PostgisGeometry) },
            { "record", typeof(object[]) },
            { "composite types", typeof(object) },
            { "range subtypes", typeof(object) },
            { "enum types", typeof(Enum) },
            { "array types", typeof(Array) },
        };

        #endregion

        #region 外部接口

        /// <summary>
        /// 获取数据库中的所有表
        /// </summary>
        /// <param name="schemaName">模式（架构）</param>
        /// <returns></returns>
        public override List<DbTableInfo> GetDbAllTables(string schemaName=null)
        {
            if (schemaName.IsNullOrEmpty())
                schemaName = "public";
            string sql = @"(select 
	relname as ""TableName"",
	cast(obj_description(relfilenode,'pg_class') as varchar) as ""Description""
from pg_class c 
where  relkind = 'r' and relname not like 'pg_%' and relname not like 'sql_%' and relchecks=0
order by relname)

UNION ALL

(SELECT viewname as ""TableName"",NULL as ""Description""
FROM pg_views
WHERE schemaname = @schemaName)";
            return GetListBySql<DbTableInfo>(sql, new List<DbParameter> { new NpgsqlParameter("@schemaName", schemaName) });
        }

        /// <summary>
        /// 通过连接字符串和表名获取数据库表的信息
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public override List<TableInfo> GetDbTableInfo(string tableName)
        {
            string sql = @"SELECT 
	a.attname as ""Name"",

    pg_type.typname as ""Type"",
(SELECT ""count""(*) from
(SELECT
ic.column_name as ""ColumnName""
FROM
information_schema.table_constraints tc
JOIN information_schema.constraint_column_usage AS ccu USING(constraint_schema, constraint_name)
JOIN information_schema.columns AS ic ON ic.table_schema = tc.constraint_schema AND tc.table_name = ic.table_name AND ccu.column_name = ic.column_name
where constraint_type = 'PRIMARY KEY' and tc.""table_name"" = @table_name) KeyA WHERE KeyA.""ColumnName"" = a.attname)> 0 as ""IsKey"",
a.attnotnull<> True as ""IsNullable"",
	col_description(a.attrelid, a.attnum) as ""Description""


FROM pg_class as c,pg_attribute as a inner join pg_type on pg_type.oid = a.atttypid
where c.relname = @table_name and a.attrelid = c.oid and a.attnum > 0;";
            return GetListBySql<TableInfo>(sql, new List<DbParameter> { new NpgsqlParameter("@table_name", tableName) });
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
        public override void SaveEntityToFile(List<TableInfo> infos, string tableName, string tableDescription, string filePath, string nameSpace, string schemaName = "public")
        {
            base.SaveEntityToFile(infos, tableName, tableDescription, filePath, nameSpace, schemaName);
        }

        #endregion
    }
}
