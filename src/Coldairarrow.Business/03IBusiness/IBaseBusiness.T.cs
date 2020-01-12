using Coldairarrow.DataRepository;
using Coldairarrow.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

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
        int Insert(T entity);

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        Task<int> InsertAsync(T entity);

        /// <summary>
        /// 添加多条数据
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        int Insert(List<T> entities);

        /// <summary>
        /// 添加多条数据
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        Task<int> InsertAsync(List<T> entities);

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
        int DeleteAll();

        /// <summary>
        /// 删除所有数据
        /// </summary>
        Task<int> DeleteAllAsync();

        /// <summary>
        /// 删除指定主键数据
        /// </summary>
        /// <param name="key"></param>
        int Delete(string key);

        /// <summary>
        /// 删除指定主键数据
        /// </summary>
        /// <param name="key"></param>
        Task<int> DeleteAsync(string key);

        /// <summary>
        /// 通过主键删除多条数据
        /// </summary>
        /// <param name="keys"></param>
        int Delete(List<string> keys);

        /// <summary>
        /// 通过主键删除多条数据
        /// </summary>
        /// <param name="keys"></param>
        Task<int> DeleteAsync(List<string> keys);

        /// <summary>
        /// 删除单条数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        int Delete(T entity);

        /// <summary>
        /// 删除单条数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        Task<int> DeleteAsync(T entity);

        /// <summary>
        /// 删除多条数据
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        int Delete(List<T> entities);

        /// <summary>
        /// 删除多条数据
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        Task<int> DeleteAsync(List<T> entities);

        /// <summary>
        /// 删除指定条件数据
        /// </summary>
        /// <param name="condition">筛选条件</param>
        int Delete(Expression<Func<T, bool>> condition);

        /// <summary>
        /// 删除指定条件数据
        /// </summary>
        /// <param name="condition">筛选条件</param>
        Task<int> DeleteAsync(Expression<Func<T, bool>> condition);

        /// <summary>
        /// 使用SQL语句按照条件删除数据
        /// 用法:Delete_Sql"Base_User"(x=>x.Id == "Admin")
        /// 注：生成的SQL类似于DELETE FROM [Base_User] WHERE [Name] = 'xxx' WHERE [Id] = 'Admin'
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns>影响条数</returns>
        int Delete_Sql(Expression<Func<T, bool>> where);

        /// <summary>
        /// 使用SQL语句按照条件删除数据
        /// 用法:Delete_Sql"Base_User"(x=>x.Id == "Admin")
        /// 注：生成的SQL类似于DELETE FROM [Base_User] WHERE [Name] = 'xxx' WHERE [Id] = 'Admin'
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns>影响条数</returns>
        Task<int> Delete_SqlAsync(Expression<Func<T, bool>> where);

        #endregion

        #region 更新数据

        /// <summary>
        /// 更新单条数据
        /// </summary>
        /// <param name="entity"></param>
        int Update(T entity);

        /// <summary>
        /// 更新单条数据
        /// </summary>
        /// <param name="entity"></param>
        Task<int> UpdateAsync(T entity);

        /// <summary>
        /// 更新多条数据
        /// </summary>
        /// <param name="entities"></param>
        int Update(List<T> entities);

        /// <summary>
        /// 更新多条数据
        /// </summary>
        /// <param name="entities"></param>
        Task<int> UpdateAsync(List<T> entities);

        /// <summary>
        /// 通过条件更新数据
        /// </summary>
        /// <param name="whereExpre">筛选条件</param>
        /// <param name="set">更新操作</param>
        int UpdateWhere(Expression<Func<T, bool>> whereExpre, Action<T> set);

        /// <summary>
        /// 通过条件更新数据
        /// </summary>
        /// <param name="whereExpre">筛选条件</param>
        /// <param name="set">更新操作</param>
        Task<int> UpdateWhereAsync(Expression<Func<T, bool>> whereExpre, Action<T> set);

        /// <summary>
        /// 使用SQL语句按照条件更新
        /// 用法:UpdateWhere_Sql"Base_User"(x=>x.Id == "Admin",("Name","小明"))
        /// 注：生成的SQL类似于UPDATE [TABLE] SET [Name] = 'xxx' WHERE [Id] = 'Admin'
        /// </summary>
        /// <param name="where">筛选条件</param>
        /// <param name="values">字段值设置</param>
        /// <returns>影响条数</returns>
        int UpdateWhere_Sql(Expression<Func<T, bool>> where, params (string field, UpdateType updateType, object value)[] values);

        /// <summary>
        /// 使用SQL语句按照条件更新
        /// 用法:UpdateWhere_Sql"Base_User"(x=>x.Id == "Admin",("Name","小明"))
        /// 注：生成的SQL类似于UPDATE [TABLE] SET [Name] = 'xxx' WHERE [Id] = 'Admin'
        /// </summary>
        /// <param name="where">筛选条件</param>
        /// <param name="values">字段值设置</param>
        /// <returns>影响条数</returns>
        Task<int> UpdateWhere_SqlAsync(Expression<Func<T, bool>> where, params (string field, UpdateType updateType, object value)[] values);

        #endregion

        #region 查询数据

        /// <summary>
        /// 通过主键获取单条数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        T GetEntity(params object[] keyValue);

        /// <summary>
        /// 通过主键获取单条数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        Task<T> GetEntityAsync(params object[] keyValue);

        /// <summary>
        /// 获取所有数据
        /// 注:会获取所有数据,数据量大请勿使用
        /// </summary>
        /// <returns></returns>
        List<T> GetList();

        /// <summary>
        /// 获取所有数据
        /// 注:会获取所有数据,数据量大请勿使用
        /// </summary>
        /// <returns></returns>
        Task<List<T>> GetListAsync();

        /// <summary>
        /// 获取IQueryable
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetIQueryable();

        #endregion
    }
}
