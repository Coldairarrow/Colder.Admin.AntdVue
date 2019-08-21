using Coldairarrow.DataRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace Coldairarrow.Business
{
    /// <summary>
    /// 业务逻辑
    /// </summary>
    /// <typeparam name="T">实体泛型</typeparam>
    /// <seealso cref="Coldairarrow.DataRepository.ITransaction" />
    public interface IBaseBusiness<T> : ITransaction where T : class, new()
    {
        #region 增加数据

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        void Insert(T entity);

        /// <summary>
        /// 添加多条数据
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        void Insert(List<T> entities);

        /// <summary>
        /// 批量添加数据,速度快
        /// </summary>
        /// <param name="entities"></param>
        void BulkInsert(List<T> entities);

        #endregion

        #region 删除数据

        /// <summary>
        /// 删除所有数据
        /// </summary>
        void DeleteAll();

        /// <summary>
        /// 删除指定主键数据
        /// </summary>
        /// <param name="key"></param>
        void Delete(string key);

        /// <summary>
        /// 通过主键删除多条数据
        /// </summary>
        /// <param name="keys"></param>
        void Delete(List<string> keys);

        /// <summary>
        /// 删除单条数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        void Delete(T entity);

        /// <summary>
        /// 删除多条数据
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        void Delete(List<T> entities);

        /// <summary>
        /// 删除指定条件数据
        /// </summary>
        /// <param name="condition">筛选条件</param>
        void Delete(Expression<Func<T, bool>> condition);

        /// <summary>
        /// 使用SQL语句按照条件删除数据
        /// 用法:Delete_Sql"Base_User"(x=>x.Id == "Admin")
        /// 注：生成的SQL类似于DELETE FROM [Base_User] WHERE [Name] = 'xxx' WHERE [Id] = 'Admin'
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <param name="where">条件</param>
        /// <returns>影响条数</returns>
        int Delete_Sql(Expression<Func<T, bool>> where);

        #endregion

        #region 更新数据

        /// <summary>
        /// 更新单条数据
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);

        /// <summary>
        /// 更新多条数据
        /// </summary>
        /// <param name="entities"></param>
        void Update(List<T> entities);

        /// <summary>
        /// 更新单条数据指定属性
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="properties">属性</param>
        void UpdateAny(T entity, List<string> properties);

        /// <summary>
        /// 更新多条数据执行属性
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        /// <param name="properties">属性</param>
        void UpdateAny(List<T> entities, List<string> properties);

        /// <summary>
        /// 通过条件更新数据
        /// </summary>
        /// <param name="whereExpre">筛选条件</param>
        /// <param name="set">更新操作</param>
        void UpdateWhere(Expression<Func<T, bool>> whereExpre, Action<T> set);

        /// <summary>
        /// 使用SQL语句按照条件更新
        /// 用法:UpdateWhere_Sql"Base_User"(x=>x.Id == "Admin",("Name","小明"))
        /// 注：生成的SQL类似于UPDATE [TABLE] SET [Name] = 'xxx' WHERE [Id] = 'Admin'
        /// </summary>
        /// <param name="where">筛选条件</param>
        /// <param name="values">字段值设置</param>
        /// <returns>影响条数</returns>
        int UpdateWhere_Sql(Expression<Func<T, bool>> where, params (string field, object value)[] values);

        #endregion

        #region 查询数据

        /// <summary>
        /// 通过主键获取单条数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        T GetEntity(params object[] keyValue);

        /// <summary>
        /// 获取所有数据
        /// 注:会获取所有数据,数据量大请勿使用
        /// </summary>
        /// <returns></returns>
        List<T> GetList();

        /// <summary>
        /// 获取IQueryable
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetIQueryable();

        /// <summary>
        /// 通过SQL获取DataTable
        /// </summary>
        /// <param name="sql">SQL</param>
        /// <returns></returns>
        DataTable GetDataTableWithSql(string sql);

        /// <summary>
        /// 通过SQL获取DataTable
        /// </summary>
        /// <param name="sql">SQL</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        DataTable GetDataTableWithSql(string sql, List<DbParameter> parameters);

        /// <summary>
        /// 通过SQL获取List
        /// </summary>
        /// <typeparam name="U">泛型</typeparam>
        /// <param name="sqlStr">SQL</param>
        /// <returns></returns>
        List<U> GetListBySql<U>(string sqlStr) where U : class, new();

        /// <summary>
        /// 通过SQL获取List
        /// </summary>
        /// <typeparam name="U">泛型</typeparam>
        /// <param name="sqlStr">SQL</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        List<U> GetListBySql<U>(string sqlStr, List<DbParameter> param) where U : class, new();

        #endregion

        #region 执行Sql语句

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="sql">SQL</param>
        int ExecuteSql(string sql);

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="sql">SQL</param>
        /// <param name="spList">参数</param>
        int ExecuteSql(string sql, List<DbParameter> spList);

        #endregion

        #region 业务操作

        #endregion
    }
}
