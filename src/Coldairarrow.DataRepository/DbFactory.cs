using Coldairarrow.Util;
using Microsoft.EntityFrameworkCore;
using System;

namespace Coldairarrow.DataRepository
{
    /// <summary>
    /// 数据库工厂
    /// </summary>
    public class DbFactory
    {
        #region 外部接口

        /// <summary>
        /// 根据配置文件获取数据库类型，并返回对应的工厂接口
        /// </summary>
        /// <param name="conString">链接字符串</param>
        /// <param name="dbType">数据库类型</param>
        /// <returns></returns>
        public static IRepository GetRepository(string conString = null, DatabaseType? dbType = null)
        {
            conString = conString.IsNullOrEmpty() ? GlobalSwitch.DefaultDbConName : conString;
            conString = DbProviderFactoryHelper.GetConStr(conString);
            dbType = dbType.IsNullOrEmpty() ? GlobalSwitch.DatabaseType : dbType;
            Type dbRepositoryType = Type.GetType("Coldairarrow.DataRepository." + DbProviderFactoryHelper.DbTypeToDbTypeStr(dbType.Value) + "Repository");

            var repository= Activator.CreateInstance(dbRepositoryType, new object[] { conString }) as IRepository;

            //请求结束自动释放
            try
            {
                AutofacHelper.GetScopeService<IDisposableContainer>().AddDisposableObj(repository);
            }
            catch
            {

            }

            return repository;
        }

        /// <summary>
        /// 获取ShardingRepository
        /// </summary>
        /// <returns></returns>
        public static IShardingRepository GetShardingRepository()
        {
            return new ShardingRepository(GetRepository());
        }

        /// <summary>
        /// 根据参数获取数据库的DbContext
        /// </summary>
        /// <param name="conString">初始化参数，可为连接字符串或者DbContext</param>
        /// <param name="dbType">数据库类型</param>
        /// <returns></returns>
        public static IRepositoryDbContext GetDbContext(string conString, DatabaseType dbType)
        {
            IRepositoryDbContext dbContext = new RepositoryDbContext(conString, dbType);
            dbContext.Database.SetCommandTimeout(5 * 60);

            return dbContext;
        }

        #endregion
    }
}
