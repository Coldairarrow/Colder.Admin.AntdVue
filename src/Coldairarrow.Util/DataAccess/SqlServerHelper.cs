using EFCore.Sharding;
using System;
using System.Collections.Generic;
using System.Data.Common;
using Microsoft.Data.SqlClient;

namespace Coldairarrow.Util
{
    /// <summary>
    /// SqlServer数据库操作帮助类
    /// </summary>
    public class SqlServerHelper : DbHelper
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="conString">完整连接字符串</param>
        public SqlServerHelper(string conString)
            : base(DatabaseType.SqlServer, conString)
        {
        }

        #endregion

        #region 私有成员

        protected override Dictionary<string, Type> DbTypeDic { get; } = new Dictionary<string, Type>()
        {
            { "int", typeof(Int32) },
            { "text", typeof(string) },
            { "bigint", typeof(Int64) },
            { "binary", typeof(byte[]) },
            { "bit", typeof(bool) },
            { "char", typeof(string) },
            { "date", typeof(DateTime) },
            { "datetime", typeof(DateTime) },
            { "datetime2", typeof(DateTime) },
            { "decimal", typeof(decimal) },
            { "float", typeof(double) },
            { "image", typeof(byte[]) },
            { "money", typeof(decimal) },
            { "nchar", typeof(string) },
            { "ntext", typeof(string) },
            { "numeric", typeof(decimal) },
            { "nvarchar", typeof(string) },
            { "real", typeof(Single) },
            { "smalldatetime", typeof(DateTime) },
            { "smallint", typeof(Int16) },
            { "smallmoney", typeof(decimal) },
            { "timestamp", typeof(DateTime) },
            { "tinyint", typeof(byte) },
            { "varbinary", typeof(byte[]) },
            { "varchar", typeof(string) },
            { "variant", typeof(object) },
            { "uniqueidentifier", typeof(Guid) },
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
            if (schemaName.IsNullOrEmpty())
                schemaName = "dbo";

            string sql = @"select
[TableName] = a.name,
[Description] = g.value
from
  sys.tables a left join sys.extended_properties g
  on (a.object_id = g.major_id AND g.minor_id = 0 AND g.name= 'MS_Description')
UNION
select
[TableName] = a.name,
[Description] = g.value
from
  sys.views a left join sys.extended_properties g
  on (a.object_id = g.major_id AND g.minor_id = 0 AND g.name= 'MS_Description')";
            return GetListBySql<DbTableInfo>(sql);
        }

        /// <summary>
        /// 通过连接字符串和表名获取数据库表的信息
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public override List<TableInfo> GetDbTableInfo(string tableName)
        {
            string sql = @"
select 
sys.columns.column_id as [ColumnId],
sys.columns.name as [Name],
sys.types.name as [Type], 
sys.columns.is_nullable [IsNullable], 
[IsIdentity]=CONVERT(BIT, (select count(*) from sys.identity_columns where sys.identity_columns.object_id = sys.columns.object_id and sys.columns.column_id = sys.identity_columns.column_id)),
  (select value from sys.extended_properties where sys.extended_properties.major_id = sys.columns.object_id and sys.extended_properties.minor_id = sys.columns.column_id and name='MS_Description') as [Description],
  [IsKey] =CONVERT(bit,(case when sys.columns.name in (select b.column_name
from information_schema.table_constraints a
inner join information_schema.constraint_column_usage b
on a.constraint_name = b.constraint_name
where a.constraint_type = 'PRIMARY KEY' and a.table_name = @table_name) then 1 else 0 end))
  from sys.columns, sys.views, sys.types where sys.columns.object_id = sys.views.object_id and sys.columns.system_type_id=sys.types.system_type_id and sys.views.name=@table_name and sys.types.name !='sysname' 

union

select
sys.columns.column_id as [ColumnId],
sys.columns.name as [Name],
sys.types.name as [Type], 
sys.columns.is_nullable [IsNullable], 
[IsIdentity]=CONVERT(BIT, (select count(*) from sys.identity_columns where sys.identity_columns.object_id = sys.columns.object_id and sys.columns.column_id = sys.identity_columns.column_id)),
  (select value from sys.extended_properties where sys.extended_properties.major_id = sys.columns.object_id and sys.extended_properties.minor_id = sys.columns.column_id and name='MS_Description') as [Description],
  [IsKey] =CONVERT(bit,(case when sys.columns.name in (select b.column_name
from information_schema.table_constraints a
inner join information_schema.constraint_column_usage b
on a.constraint_name = b.constraint_name
where a.constraint_type = 'PRIMARY KEY' and a.table_name = @table_name) then 1 else 0 end))
  from sys.columns, sys.tables, sys.types where sys.columns.object_id = sys.tables.object_id and sys.columns.system_type_id=sys.types.system_type_id and sys.tables.name=@table_name and sys.types.name !='sysname' 
order by sys.columns.column_id asc";
            return GetListBySql<TableInfo>(sql, new List<DbParameter> { new SqlParameter("@table_name", tableName) });
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
