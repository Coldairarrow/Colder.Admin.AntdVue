using EFCore.Sharding;
using System;

namespace Coldairarrow.Util
{
    /// <summary>
    /// 数据库帮助类工厂
    /// </summary>
    public class DbHelperFactory
    {
        /// <summary>
        /// 获取指定的数据库帮助类
        /// </summary>
        /// <param name="dbType">数据库类型</param>
        /// <param name="conString">连接字符串</param>
        /// <returns></returns>
        public static DbHelper GetDbHelper(DatabaseType dbType, string conString)
        {
            switch (dbType)
            {
                case DatabaseType.SqlServer: return new SqlServerHelper(conString);
                case DatabaseType.MySql: return new MySqlHelper(conString);
                case DatabaseType.Oracle: return new OracleHelper(conString);
                case DatabaseType.PostgreSql: return new PostgreSqlHelper(conString);
                default: throw new Exception("暂不支持");
            }
        }
    }
}
