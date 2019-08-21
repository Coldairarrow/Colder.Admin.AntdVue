using MySql.Data.MySqlClient;
using Npgsql;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

namespace Coldairarrow.Util
{
    /// <summary>
    /// 数据库操作提供源工厂帮助类
    /// </summary>
    public class DbProviderFactoryHelper
    {
        #region 外部接口

        /// <summary>
        /// 获取提供工厂
        /// </summary>
        /// <param name="dbType">数据库类型</param>
        /// <returns></returns>
        public static DbProviderFactory GetDbProviderFactory(DatabaseType dbType)
        {
            DbProviderFactory factory = null;
            switch (dbType)
            {
                case DatabaseType.SqlServer: factory = SqlClientFactory.Instance; break;
                case DatabaseType.MySql: factory = MySqlClientFactory.Instance; break;
                case DatabaseType.PostgreSql: factory = NpgsqlFactory.Instance; break;
                case DatabaseType.Oracle: factory = OracleClientFactory.Instance; break;

                default: throw new Exception("请传入有效的数据库！");
            }

            return factory;
        }

        /// <summary>
        /// 获取DbConnection
        /// </summary>
        /// <param name="dbType">数据库类型</param>
        /// <returns></returns>
        public static DbConnection GetDbConnection(DatabaseType dbType)
        {
            var con = GetDbProviderFactory(dbType).CreateConnection();

            //请求结束自动释放
            try
            {
                AutofacHelper.GetScopeService<IDisposableContainer>().AddDisposableObj(con);
            }
            catch
            {

            }

            return con;
        }

        /// <summary>
        /// 获取DbCommand
        /// </summary>
        /// <param name="dbType">数据库类型</param>
        /// <returns></returns>
        public static DbCommand GetDbCommand(DatabaseType dbType)
        {
            return GetDbProviderFactory(dbType).CreateCommand();
        }

        /// <summary>
        /// 获取DbParameter
        /// </summary>
        /// <param name="dbType">数据库类型</param>
        /// <returns></returns>
        public static DbParameter GetDbParameter(DatabaseType dbType)
        {
            return GetDbProviderFactory(dbType).CreateParameter();
        }

        /// <summary>
        /// 获取DataAdapter
        /// </summary>
        /// <param name="dbType">数据库类型</param>
        /// <returns></returns>
        public static DataAdapter GetDataAdapter(DatabaseType dbType)
        {
            return GetDbProviderFactory(dbType).CreateDataAdapter();
        }

        /// <summary>
        /// 将数据库类型字符串转换为对应的数据库类型
        /// </summary>
        /// <param name="dbTypeStr">数据库类型字符串</param>
        /// <returns></returns>
        public static DatabaseType DbTypeStrToDbType(string dbTypeStr)
        {
            if (dbTypeStr.IsNullOrEmpty())
                throw new Exception("请输入数据库类型字符串！");
            else
            {
                switch (dbTypeStr.ToLower())
                {
                    case "sqlserver": return DatabaseType.SqlServer;
                    case "mysql": return DatabaseType.MySql;
                    case "oracle": return DatabaseType.Oracle;
                    case "postgresql": return DatabaseType.PostgreSql;
                    default: throw new Exception("请输入合法的数据库类型字符串！");
                }
            }
        }

        /// <summary>
        /// 将数据库类型转换为对应的数据库类型字符串
        /// </summary>
        /// <param name="dbTypeStr">数据库类型字符串</param>
        /// <returns></returns>
        public static string DbTypeToDbTypeStr(DatabaseType dbType)
        {
            if (dbType.IsNullOrEmpty())
                throw new Exception("请输入数据库类型！");
            else
            {
                switch (dbType)
                {
                    case DatabaseType.SqlServer: return "SqlServer";
                    case DatabaseType.MySql: return "MySql";
                    case DatabaseType.Oracle: return "Oracle";
                    case DatabaseType.PostgreSql: return "PostgreSql";
                    default: throw new Exception("请输入合法的数据库类型！");
                }
            }
        }

        /// <summary>
        /// 通过连接名或连接字符串获取连接字符串
        /// </summary>
        /// <param name="nameOrconStr">连接名或者连接字符串</param>
        /// <returns></returns>
        public static string GetConStr(string nameOrconStr)
        {
            string conStr = string.Empty;
            string nameOfDbcon = string.Empty;
            //若为连接字符串
            if (nameOrconStr.Contains(";"))
                conStr = nameOrconStr;
            //若为"name=BaseDb"形式
            else if (nameOrconStr.Contains("name="))
            {
                var strArray = nameOrconStr.Split("=".ToArray());
                nameOfDbcon = strArray[1];
            }
            //为连接名
            else
                nameOfDbcon = nameOrconStr;

            if (!nameOfDbcon.IsNullOrEmpty())
            {
                conStr = ConfigHelper.GetConnectionString(nameOfDbcon);
            }

            return conStr;
        }

        /// <summary>
        /// 获取数据库连接对象
        /// </summary>
        /// <param name="conStr">连接字符串</param>
        /// <param name="dbType">数据库类型</param>
        /// <returns></returns>
        public static DbConnection GetDbConnection(string conStr, DatabaseType dbType)
        {
            if (conStr.IsNullOrEmpty())
                conStr = GlobalSwitch.DefaultDbConName;
            DbConnection dbConnection = GetDbConnection(dbType);
            dbConnection.ConnectionString = GetConStr(conStr);

            return dbConnection;
        }

        #endregion
    }
}
