using Coldairarrow.Util;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace Coldairarrow.DataRepository
{
    public class MySqlRepository : DbRepository, IRepository
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public MySqlRepository()
            : base(null, DatabaseType.MySql)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="conStr">数据库连接名</param>
        public MySqlRepository(string conStr)
            : base(conStr, DatabaseType.MySql)
        {
        }

        #endregion

        #region 私有成员

        protected override string FormatFieldName(string name)
        {
            return $"`{name}`";
        }

        #endregion

        #region 插入数据

        /// <summary>
        /// 使用Bulk批量插入数据（适合大数据量，速度非常快）
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entities">数据</param>
        public override void BulkInsert<T>(List<T> entities)
        {
            DataTable dt = entities.ToDataTable();
            using (MySqlConnection conn=new MySqlConnection())
            {
                conn.ConnectionString = ConnectionString;
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                string tableName = string.Empty;
                var tableAttribute = typeof(T).GetCustomAttributes(typeof(TableAttribute), true).FirstOrDefault();
                if (tableAttribute != null)
                    tableName = ((TableAttribute)tableAttribute).Name;
                else
                    tableName = typeof(T).Name;

                int insertCount = 0;
                string tmpPath = Path.Combine(Path.GetTempPath(), DateTime.Now.ToCstTime().Ticks.ToString() + "_" + Guid.NewGuid().ToString() + ".tmp");
                string csv = dt.ToCsvStr();
                File.WriteAllText(tmpPath, csv, Encoding.UTF8);

                using (MySqlTransaction tran = conn.BeginTransaction())
                {
                    MySqlBulkLoader bulk = new MySqlBulkLoader(conn)
                    {
                        FieldTerminator = ",",
                        FieldQuotationCharacter = '"',
                        EscapeCharacter = '"',
                        LineTerminator = "\r\n",
                        FileName = tmpPath,
                        NumberOfLinesToSkip = 0,
                        TableName = tableName,
                    };
                    try
                    {
                        bulk.Columns.AddRange(dt.Columns.Cast<DataColumn>().Select(colum => colum.ColumnName).ToList());
                        insertCount = bulk.Load();
                        tran.Commit();
                    }
                    catch (MySqlException ex)
                    {
                        if (tran != null)
                            tran.Rollback();

                        throw ex;
                    }
                }
                File.Delete(tmpPath);
            }
        }

        #endregion

        #region 删除数据

        public override void DeleteAll<T>()
        {
            Delete(GetList<T>());
        }

        #endregion
    }
}
