using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Coldairarrow.DataRepository
{
    /// <summary>
    /// 基数据仓储
    /// </summary>
    public interface IBaseRepository : ITransaction
    {
        #region 增加数据

        /// <summary>
        /// 添加单条记录
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <param name="entity">实体对象</param>
        int Insert<T>(T entity) where T : class, new();

        /// <summary>
        /// 添加单条记录
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <param name="entity">实体对象</param>
        Task<int> InsertAsync<T>(T entity) where T : class, new();

        /// <summary>
        /// 添加多条记录
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <param name="entities">实体对象集合</param>
        int Insert<T>(List<T> entities) where T : class, new();

        /// <summary>
        /// 添加多条记录
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <param name="entities">实体对象集合</param>
        Task<int> InsertAsync<T>(List<T> entities) where T : class, new();

        #endregion

        #region 删除数据

        /// <summary>
        /// 删除所有记录
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        int DeleteAll<T>() where T : class, new();

        /// <summary>
        /// 删除所有记录
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        Task<int> DeleteAllAsync<T>() where T : class, new();

        /// <summary>
        /// 删除单条记录
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <param name="entity">实体对象</param>
        int Delete<T>(T entity) where T : class, new();

        /// <summary>
        /// 删除单条记录
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <param name="entity">实体对象</param>
        Task<int> DeleteAsync<T>(T entity) where T : class, new();

        /// <summary>
        /// 删除多条记录
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <param name="entities">实体对象集合</param>
        int Delete<T>(List<T> entities) where T : class, new();

        /// <summary>
        /// 删除多条记录
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <param name="entities">实体对象集合</param>
        Task<int> DeleteAsync<T>(List<T> entities) where T : class, new();

        /// <summary>
        /// 按条件删除记录
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <param name="condition">筛选条件</param>
        int Delete<T>(Expression<Func<T, bool>> condition) where T : class, new();

        /// <summary>
        /// 按条件删除记录
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <param name="condition">筛选条件</param>
        Task<int> DeleteAsync<T>(Expression<Func<T, bool>> condition) where T : class, new();

        #endregion

        #region 更新数据

        /// <summary>
        /// 更新单条记录
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <param name="entity">实体对象</param>
        int Update<T>(T entity) where T : class, new();

        /// <summary>
        /// 更新单条记录
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <param name="entity">实体对象</param>
        Task<int> UpdateAsync<T>(T entity) where T : class, new();

        /// <summary>
        /// 更新多条记录
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <param name="entities">实体对象集合</param>
        int Update<T>(List<T> entities) where T : class, new();

        /// <summary>
        /// 更新多条记录
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <param name="entities">实体对象集合</param>
        Task<int> UpdateAsync<T>(List<T> entities) where T : class, new();

        /// <summary>
        /// 更新单条记录的某些属性
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <param name="entity">实体对象</param>
        /// <param name="properties">属性</param>
        int UpdateAny<T>(T entity, List<string> properties) where T : class, new();

        /// <summary>
        /// 更新单条记录的某些属性
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <param name="entity">实体对象</param>
        /// <param name="properties">属性</param>
        Task<int> UpdateAnyAsync<T>(T entity, List<string> properties) where T : class, new();

        /// <summary>
        /// 更新多条记录的某些属性
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <param name="entities">实体对象集合</param>
        /// <param name="properties">属性</param>
        int UpdateAny<T>(List<T> entities, List<string> properties) where T : class, new();

        /// <summary>
        /// 更新多条记录的某些属性
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <param name="entities">实体对象集合</param>
        /// <param name="properties">属性</param>
        Task<int> UpdateAnyAsync<T>(List<T> entities, List<string> properties) where T : class, new();

        /// <summary>
        /// 按照条件更新记录
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <param name="whereExpre">筛选条件</param>
        /// <param name="set">更新操作</param>
        int UpdateWhere<T>(Expression<Func<T, bool>> whereExpre, Action<T> set) where T : class, new();

        /// <summary>
        /// 按照条件更新记录
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <param name="whereExpre">筛选条件</param>
        /// <param name="set">更新操作</param>
        Task<int> UpdateWhereAsync<T>(Expression<Func<T, bool>> whereExpre, Action<T> set) where T : class, new();

        #endregion

        #region 查询数据

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <returns></returns>
        List<T> GetList<T>() where T : class, new();

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <returns></returns>
        Task<List<T>> GetListAsync<T>() where T : class, new();

        #endregion
    }
}
