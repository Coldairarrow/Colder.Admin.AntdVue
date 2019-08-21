using Coldairarrow.Util;
using System;
using System.Collections.Generic;

namespace Coldairarrow.DataRepository
{
    public class PostgreSqlRepository : DbRepository, IRepository
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public PostgreSqlRepository()
            : base(null, DatabaseType.PostgreSql)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="conStr">数据库连接名</param>
        public PostgreSqlRepository(string conStr)
            : base(conStr, DatabaseType.PostgreSql)
        {
        }

        #endregion

        #region 私有成员

        protected override string FormatFieldName(string name)
        {
            return $"\"{name}\"";
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
            throw new Exception("抱歉！暂不支持PostgreSql！");
        }

        #endregion
    }
}
