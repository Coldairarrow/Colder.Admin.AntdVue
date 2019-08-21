namespace Coldairarrow.Util
{
    /// <summary>
    /// 描述：数据库帮助类工厂
    /// 作者：Coldairarrow
    /// </summary>
    public class DbHelperFactory
    {
        static DbHelperFactory()
        {
            _container = new IocHelper();
            _container.RegisterType<DbHelper, SqlServerHelper>(DatabaseType.SqlServer.ToString());
            _container.RegisterType<DbHelper, MySqlHelper>(DatabaseType.MySql.ToString());
            _container.RegisterType<DbHelper, PostgreSqlHelper>(DatabaseType.PostgreSql.ToString());
            _container.RegisterType<DbHelper, OracleHelper>(DatabaseType.Oracle.ToString());
        }

        private static IocHelper _container { get; }

        /// <summary>
        /// 获取指定的数据库帮助类
        /// </summary>
        /// <param name="dbType">数据库类型</param>
        /// <param name="conStr">连接字符串</param>
        /// <returns></returns>
        public static DbHelper GetDbHelper(DatabaseType dbType, string conStr)
        {
            return _container.Resolve<DbHelper>(dbType.ToString(), conStr);
        }

        /// <summary>
        /// 获取指定的数据库帮助类
        /// </summary>
        /// <param name="dbType">数据库类型字符串</param>
        /// <param name="conStr">连接字符串</param>
        /// <returns></returns>
        public static DbHelper GetDbHelper(string dbTypeStr, string conStr)
        {
            DatabaseType dbType = DbProviderFactoryHelper.DbTypeStrToDbType(dbTypeStr);
            return GetDbHelper(dbType, conStr);
        }
    }
}
