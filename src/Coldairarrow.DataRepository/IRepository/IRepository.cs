using Coldairarrow.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Coldairarrow.DataRepository
{
    public interface IRepository : IBaseRepository, ITransaction
    {
        #region 数据库相关

        /// <summary>
        /// SQL日志处理方法
        /// </summary>
        /// <value>
        /// The handle SQL log.
        /// </value>
        Action<string> HandleSqlLog { set; }

        /// <summary>
        /// 连接字符串
        /// </summary>
        string ConnectionString { get; }

        /// <summary>
        /// 数据库类型
        /// </summary>
        DatabaseType DbType { get; }

        #endregion

        #region 增加数据

        /// <summary>
        /// 使用Bulk批量导入,速度快
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <param name="entities">实体集合</param>
        void BulkInsert<T>(List<T> entities) where T : class, new();

        #endregion

        #region 删除数据

        /// <summary>
        /// 删除所有记录
        /// </summary>
        /// <param name="type">实体类型</param>
        int DeleteAll(Type type);

        /// <summary>
        /// 删除所有记录
        /// </summary>
        /// <param name="type">实体类型</param>
        Task<int> DeleteAllAsync(Type type);

        /// <summary>
        /// 删除单条记录
        /// </summary>
        /// <param name="type">实体类型</param>
        /// <param name="key">主键</param>
        int Delete(Type type, string key);

        /// <summary>
        /// 删除单条记录
        /// </summary>
        /// <param name="type">实体类型</param>
        /// <param name="key">主键</param>
        Task<int> DeleteAsync(Type type, string key);

        /// <summary>
        /// 删除多条记录
        /// </summary>
        /// <param name="type">实体类型</param>
        /// <param name="keys">多条记录主键集合</param>
        int Delete(Type type, List<string> keys);

        /// <summary>
        /// 删除多条记录
        /// </summary>
        /// <param name="type">实体类型</param>
        /// <param name="keys">多条记录主键集合</param>
        Task<int> DeleteAsync(Type type, List<string> keys);

        /// <summary>
        /// 删除单条记录
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <param name="key">主键</param>
        int Delete<T>(string key) where T : class, new();

        /// <summary>
        /// 删除单条记录
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <param name="key">主键</param>
        Task<int> DeleteAsync<T>(string key) where T : class, new();

        /// <summary>
        /// 删除多条记录
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <param name="keys">多条记录主键集合</param>
        int Delete<T>(List<string> keys) where T : class, new();

        /// <summary>
        /// 删除多条记录
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <param name="keys">多条记录主键集合</param>
        Task<int> DeleteAsync<T>(List<string> keys) where T : class, new();

        /// <summary>
        /// 使用SQL语句按照条件删除数据
        /// 用法:Delete_Sql"Base_User"(x=>x.Id == "Admin")
        /// 注：生成的SQL类似于DELETE FROM [Base_User] WHERE [Name] = 'xxx' WHERE [Id] = 'Admin'
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <param name="where">条件</param>
        /// <returns>影响条数</returns>
        int Delete_Sql<T>(Expression<Func<T, bool>> where) where T : class, new();

        /// <summary>
        /// 使用SQL语句按照条件删除数据
        /// 用法:Delete_Sql"Base_User"(x=>x.Id == "Admin")
        /// 注：生成的SQL类似于DELETE FROM [Base_User] WHERE [Name] = 'xxx' WHERE [Id] = 'Admin'
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <param name="where">条件</param>
        /// <returns>影响条数</returns>
        Task<int> Delete_SqlAsync<T>(Expression<Func<T, bool>> where) where T : class, new();

        /// <summary>
        /// 使用SQL语句按照条件删除数据
        /// </summary>
        /// <param name="entityType">实体类型</param>
        /// <param name="where">动态where</param>
        /// <param name="paramters">where参数</param>
        /// <returns></returns>
        int Delete_Sql(Type entityType, string where, params object[] paramters);

        /// <summary>
        /// 使用SQL语句按照条件删除数据
        /// </summary>
        /// <param name="entityType">实体类型</param>
        /// <param name="where">动态where</param>
        /// <param name="paramters">where参数</param>
        /// <returns></returns>
        Task<int> Delete_SqlAsync(Type entityType, string where, params object[] paramters);

        /// <summary>
        /// 删除指定数据源
        /// </summary>
        /// <param name="source">数据源</param>
        /// <returns></returns>
        int Delete_Sql(IQueryable source);

        /// <summary>
        /// 删除指定数据源
        /// </summary>
        /// <param name="source">数据源</param>
        /// <returns></returns>
        Task<int> Delete_SqlAsync(IQueryable source);

        #endregion

        #region 更新数据

        /// <summary>
        /// 使用SQL语句按照条件更新
        /// 用法:UpdateWhere_Sql"Base_User"(x=>x.Id == "Admin",("Name","小明"))
        /// 注：生成的SQL类似于UPDATE [TABLE] SET [Name] = 'xxx' WHERE [Id] = 'Admin'
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="where">筛选条件</param>
        /// <param name="values">字段值设置</param>
        /// <returns>影响条数</returns>
        int UpdateWhere_Sql<T>(Expression<Func<T, bool>> where, params (string field, UpdateType updateType, object value)[] values) where T : class, new();

        /// <summary>
        /// 使用SQL语句按照条件更新
        /// 用法:UpdateWhere_Sql"Base_User"(x=>x.Id == "Admin",("Name","小明"))
        /// 注：生成的SQL类似于UPDATE [TABLE] SET [Name] = 'xxx' WHERE [Id] = 'Admin'
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="where">筛选条件</param>
        /// <param name="values">字段值设置</param>
        /// <returns>影响条数</returns>
        Task<int> UpdateWhere_SqlAsync<T>(Expression<Func<T, bool>> where, params (string field, UpdateType updateType, object value)[] values) where T : class, new();

        /// <summary>
        /// 使用SQL语句按照条件更新
        /// 用法:UpdateWhere_Sql"Base_User"(x=>x.Id == "Admin",("Name","小明"))
        /// 注：生成的SQL类似于UPDATE [TABLE] SET [Name] = 'xxx' WHERE [Id] = 'Admin'
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="where">筛选条件</param>
        /// <param name="values">字段值设置</param>
        /// <returns>影响条数</returns>
        int UpdateWhere_Sql(Type entityType, string where, object[] paramters, params (string field, UpdateType updateType, object value)[] values);

        /// <summary>
        /// 使用SQL语句按照条件更新
        /// 用法:UpdateWhere_Sql"Base_User"(x=>x.Id == "Admin",("Name","小明"))
        /// 注：生成的SQL类似于UPDATE [TABLE] SET [Name] = 'xxx' WHERE [Id] = 'Admin'
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="where">筛选条件</param>
        /// <param name="values">字段值设置</param>
        /// <returns>影响条数</returns>
        Task<int> UpdateWhere_SqlAsync(Type entityType, string where, object[] paramters, params (string field, UpdateType updateType, object value)[] values);

        /// <summary>
        /// 使用SQL语句按照条件更新
        /// 用法:UpdateWhere_Sql"Base_User"(x=>x.Id == "Admin",("Name","小明"))
        /// 注：生成的SQL类似于UPDATE [TABLE] SET [Name] = 'xxx' WHERE [Id] = 'Admin'
        /// </summary>
        /// <param name="source">数据源</param>
        /// <param name="values">字段值设置</param>
        /// <returns>影响条数</returns>
        int UpdateWhere_Sql(IQueryable source, params (string field, UpdateType updateType, object value)[] values);

        /// <summary>
        /// 使用SQL语句按照条件更新
        /// 用法:UpdateWhere_Sql"Base_User"(x=>x.Id == "Admin",("Name","小明"))
        /// 注：生成的SQL类似于UPDATE [TABLE] SET [Name] = 'xxx' WHERE [Id] = 'Admin'
        /// </summary>
        /// <param name="source">数据源</param>
        /// <param name="values">字段值设置</param>
        /// <returns>影响条数</returns>
        Task<int> UpdateWhere_SqlAsync(IQueryable source, params (string field, UpdateType updateType, object value)[] values);

        #endregion

        #region 查询数据

        /// <summary>
        /// 获取单条记录
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        T GetEntity<T>(params object[] keyValue) where T : class, new();

        /// <summary>
        /// 获取单条记录
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        Task<T> GetEntityAsync<T>(params object[] keyValue) where T : class, new();

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="type">实体类型</param>
        /// <returns></returns>
        List<object> GetList(Type type);

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="type">实体类型</param>
        /// <returns></returns>
        Task<List<object>> GetListAsync(Type type);

        /// <summary>
        /// 获取IQueryable
        /// 注:默认取消实体追踪
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <returns></returns>
        IQueryable<T> GetIQueryable<T>() where T : class, new();

        /// <summary>
        /// 获取IQueryable
        /// 注:默认取消实体追踪
        /// </summary>
        /// <param name="type">实体泛型</param>
        /// <returns></returns>
        IQueryable GetIQueryable(Type type);

        /// <summary>
        /// 通过SQL获取DataTable
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">SQL参数</param>
        /// <returns></returns>
        DataTable GetDataTableWithSql(string sql, params (string paramterName, object value)[] parameters);

        /// <summary>
        /// 通过SQL获取DataTable
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">SQL参数</param>
        /// <returns></returns>
        Task<DataTable> GetDataTableWithSqlAsync(string sql, params (string paramterName, object value)[] parameters);

        /// <summary>
        /// 通过SQL获取List
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <param name="sqlStr">SQL语句</param>
        /// <param name="parameters">SQL参数</param>
        /// <returns></returns>
        List<T> GetListBySql<T>(string sqlStr, params (string paramterName, object value)[] parameters) where T : class, new();

        /// <summary>
        /// 通过SQL获取List
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <param name="sqlStr">SQL语句</param>
        /// <param name="parameters">SQL参数</param>
        /// <returns></returns>
        Task<List<T>> GetListBySqlAsync<T>(string sqlStr, params (string paramterName, object value)[] parameters) where T : class, new();

        #endregion

        #region 执行Sql语句

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">SQL参数</param>
        int ExecuteSql(string sql, params (string paramterName, object paramterValue)[] paramters);

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">SQL参数</param>
        Task<int> ExecuteSqlAsync(string sql, params (string paramterName, object paramterValue)[] paramters);

        #endregion
    }
}
