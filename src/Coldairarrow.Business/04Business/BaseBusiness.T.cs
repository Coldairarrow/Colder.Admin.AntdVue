using Coldairarrow.DataRepository;
using Coldairarrow.Util;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Coldairarrow.Business
{
    /// <summary>
    /// 描述：业务处理基类
    /// 作者：Coldairarrow
    /// </summary>
    /// <typeparam name="T">泛型约束（数据库实体）</typeparam>
    public class BaseBusiness<T> : IBaseBusiness<T>, IDependency where T : class, new()
    {
        #region DI

        public ILogger Logger { protected get; set; }
        public IOperator Operator { get => AutofacHelper.GetScopeService<IOperator>(); }

        #endregion

        #region 构造函数

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public BaseBusiness()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="conStr">连接名或连接字符串</param>
        public BaseBusiness(string conStr)
        {
            _conString = conStr;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="conStr">连接名或连接字符串</param>
        /// <param name="dbType">数据库类型</param>
        public BaseBusiness(string conStr, DatabaseType dbType)
        {
            _conString = conStr;
            _dbType = dbType;
        }

        #endregion

        #region 私有成员

        private string _conString { get; }
        private DatabaseType? _dbType { get; }
        private IRepository _service { get; set; }
        private IRepository _fullService { get; set; }
        private object _serviceLock = new object();
        protected virtual string _valueField { get; } = "Id";
        protected virtual string _textField { get => throw new Exception("请在子类重写"); }
        private SynchronizedCollection<IRepository> _dbs { get; }
            = new SynchronizedCollection<IRepository>();
        private IRepository GetBusRepository(string conString, DatabaseType? dbType, bool autoDispose)
        {
            var db = new BusRepository(DbFactory.GetRepository(conString, dbType));
            if (autoDispose)
                _dbs.Add(db);

            return db;
        }
        private IRepository GetBusRepository(IRepository fullRepository, bool autoDispose)
        {
            var db = new BusRepository(fullRepository);
            if (autoDispose)
                _dbs.Add(db);

            return db;
        }
        private IRepository GetFullRepository(string conString, DatabaseType? dbType, bool autoDispose)
        {
            var db = DbFactory.GetRepository(conString, dbType);
            if (autoDispose)
                _dbs.Add(db);

            return db;
        }
        private void InitDb()
        {
            if (_service == null) //双if +lock
            {
                lock (_serviceLock)
                {
                    if (_service == null)
                    {
                        _fullService = GetFullRepository(_conString, _dbType, true);
                        _service = GetBusRepository(_fullService, true);
                    }
                }
            }
        }

        #endregion

        #region 外部属性

        /// <summary>
        /// 业务仓储接口(支持软删除),支持联表操作
        /// 注：仅支持单线程操作
        /// 注：多线程请使用GetNewService(conString,dbType,false),并且需要手动释放
        /// </summary>
        public IRepository Service
        {
            get
            {
                InitDb();

                return _service;
            }
        }

        /// <summary>
        /// 获取新的数据仓储
        /// 注:支持多线程(每个线程需要单独的IRepository)
        /// </summary>
        /// <returns></returns>
        public IRepository GetNewService()
        {
            return GetBusRepository(_conString, _dbType, true);
        }

        /// <summary>
        /// 获取新的数据仓储
        /// 注:支持多线程(每个线程需要单独的IRepository)
        /// </summary>
        /// <param name="conString">连接字符串</param>
        /// <param name="dbType">数据库类型</param>
        /// <param name="autoDispose">自动释放</param>
        /// <returns></returns>
        public IRepository GetNewService(string conString, DatabaseType dbType, bool autoDispose)
        {
            return GetBusRepository(conString, dbType, autoDispose);
        }

        /// <summary>
        /// 完整仓储接口(不支持软删除,直接操作数据库),支持联表操作
        /// 注：仅支持单线程操作
        /// 注：多线程请使用GetNewFullService(conString,dbType,false),并且需要手动释放
        /// </summary>
        public IRepository FullService
        {
            get
            {
                InitDb();

                return _fullService;
            }
        }

        /// <summary>
        /// 获取新的数据仓储
        /// 注:支持多线程(每个线程需要单独的IRepository)
        /// </summary>
        /// <returns></returns>
        public IRepository GetNewFullService()
        {
            return GetFullRepository(_conString, _dbType, true);
        }

        /// <summary>
        /// 获取新的数据仓储
        /// 注:支持多线程(每个线程需要单独的IRepository)
        /// </summary>
        /// <param name="conString">连接字符串</param>
        /// <param name="dbType">数据库类型</param>
        /// <param name="autoDispose">自动释放</param>
        /// <returns></returns>
        public IRepository GetNewFullService(string conString, DatabaseType dbType, bool autoDispose)
        {
            return GetFullRepository(conString, dbType, autoDispose);
        }

        #endregion

        #region 事物提交

        public (bool Success, Exception ex) RunTransaction(Action action, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            return Service.RunTransaction(action, isolationLevel);
        }
        public async Task<(bool Success, Exception ex)> RunTransactionAsync(Func<Task> action, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            return await Service.RunTransactionAsync(action, isolationLevel);
        }

        #endregion

        #region 增加数据

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        public int Insert(T entity)
        {
            return Service.Insert(entity);
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        public async Task<int> InsertAsync(T entity)
        {
            return await Service.InsertAsync(entity);
        }

        /// <summary>
        /// 添加多条数据
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        public int Insert(List<T> entities)
        {
            return Service.Insert(entities);
        }

        /// <summary>
        /// 添加多条数据
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        public async Task<int> InsertAsync(List<T> entities)
        {
            return await Service.InsertAsync(entities);
        }

        /// <summary>
        /// 批量添加数据,速度快
        /// </summary>
        /// <param name="entities"></param>
        public void BulkInsert(List<T> entities)
        {
            Service.BulkInsert(entities);
        }

        #endregion

        #region 删除数据

        /// <summary>
        /// 删除所有数据
        /// </summary>
        public int DeleteAll()
        {
            return Service.DeleteAll<T>();
        }

        /// <summary>
        /// 删除所有数据
        /// </summary>
        public async Task<int> DeleteAllAsync()
        {
            return await Service.DeleteAllAsync<T>();
        }

        /// <summary>
        /// 删除指定主键数据
        /// </summary>
        /// <param name="key"></param>
        public int Delete(string key)
        {
            return Service.Delete<T>(key);
        }

        /// <summary>
        /// 删除指定主键数据
        /// </summary>
        /// <param name="key"></param>
        public async Task<int> DeleteAsync(string key)
        {
            return await Service.DeleteAsync<T>(key);
        }

        /// <summary>
        /// 通过主键删除多条数据
        /// </summary>
        /// <param name="keys"></param>
        public int Delete(List<string> keys)
        {
            return Service.Delete<T>(keys);
        }

        /// <summary>
        /// 通过主键删除多条数据
        /// </summary>
        /// <param name="keys"></param>
        public async Task<int> DeleteAsync(List<string> keys)
        {
            return await Service.DeleteAsync<T>(keys);
        }

        /// <summary>
        /// 删除单条数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        public int Delete(T entity)
        {
            return Service.Delete<T>(entity);
        }

        /// <summary>
        /// 删除单条数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        public async Task<int> DeleteAsync(T entity)
        {
            return await Service.DeleteAsync(entity);
        }

        /// <summary>
        /// 删除多条数据
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        public int Delete(List<T> entities)
        {
            return Service.Delete<T>(entities);
        }

        /// <summary>
        /// 删除多条数据
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        public async Task<int> DeleteAsync(List<T> entities)
        {
            return await Service.DeleteAsync<T>(entities);
        }

        /// <summary>
        /// 删除指定条件数据
        /// </summary>
        /// <param name="condition">筛选条件</param>
        public int Delete(Expression<Func<T, bool>> condition)
        {
            return Service.Delete(condition);
        }

        /// <summary>
        /// 删除指定条件数据
        /// </summary>
        /// <param name="condition">筛选条件</param>
        public async Task<int> DeleteAsync(Expression<Func<T, bool>> condition)
        {
            return await Service.DeleteAsync(condition);
        }

        /// <summary>
        /// 使用SQL语句按照条件删除数据
        /// 用法:Delete_Sql"Base_User"(x=&gt;x.Id == "Admin")
        /// 注：生成的SQL类似于DELETE FROM [Base_User] WHERE [Name] = 'xxx' WHERE [Id] = 'Admin'
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns>
        /// 影响条数
        /// </returns>
        public int Delete_Sql(Expression<Func<T, bool>> where)
        {
            return Service.Delete_Sql(where);
        }

        /// <summary>
        /// 使用SQL语句按照条件删除数据
        /// 用法:Delete_Sql"Base_User"(x=&gt;x.Id == "Admin")
        /// 注：生成的SQL类似于DELETE FROM [Base_User] WHERE [Name] = 'xxx' WHERE [Id] = 'Admin'
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns>
        /// 影响条数
        /// </returns>
        public async Task<int> Delete_SqlAsync(Expression<Func<T, bool>> where)
        {
            return await Service.Delete_SqlAsync(where);
        }

        #endregion

        #region 更新数据

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        public int Update(T entity)
        {
            return Service.Update(entity);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        public async Task<int> UpdateAsync(T entity)
        {
            return await Service.UpdateAsync(entity);
        }

        /// <summary>
        /// 更新多条数据
        /// </summary>
        /// <param name="entities">数据列表</param>
        public int Update(List<T> entities)
        {
            return Service.Update(entities);
        }

        /// <summary>
        /// 更新多条数据
        /// </summary>
        /// <param name="entities">数据列表</param>
        public async Task<int> UpdateAsync(List<T> entities)
        {
            return await Service.UpdateAsync(entities);
        }

        /// <summary>
        /// 指定条件更新
        /// </summary>
        /// <param name="whereExpre">筛选表达式</param>
        /// <param name="set">更改属性回调</param>
        public int UpdateWhere(Expression<Func<T, bool>> whereExpre, Action<T> set)
        {
            return Service.UpdateWhere(whereExpre, set);
        }

        /// <summary>
        /// 指定条件更新
        /// </summary>
        /// <param name="whereExpre">筛选表达式</param>
        /// <param name="set">更改属性回调</param>
        public async Task<int> UpdateWhereAsync(Expression<Func<T, bool>> whereExpre, Action<T> set)
        {
            return await Service.UpdateWhereAsync(whereExpre, set);
        }

        /// <summary>
        /// 使用SQL语句按照条件更新
        /// 用法:UpdateWhere_Sql"Base_User"(x=>x.Id == "Admin",("Name","小明"))
        /// 注：生成的SQL类似于UPDATE [TABLE] SET [Name] = 'xxx' WHERE [Id] = 'Admin'
        /// </summary>
        /// <param name="where">筛选条件</param>
        /// <param name="values">字段值设置</param>
        /// <returns>影响条数</returns>
        public int UpdateWhere_Sql(Expression<Func<T, bool>> where, params (string field, UpdateType updateType, object value)[] values)
        {
            return Service.UpdateWhere_Sql(where, values);
        }

        /// <summary>
        /// 使用SQL语句按照条件更新
        /// 用法:UpdateWhere_Sql"Base_User"(x=>x.Id == "Admin",("Name","小明"))
        /// 注：生成的SQL类似于UPDATE [TABLE] SET [Name] = 'xxx' WHERE [Id] = 'Admin'
        /// </summary>
        /// <param name="where">筛选条件</param>
        /// <param name="values">字段值设置</param>
        /// <returns>影响条数</returns>
        public async Task<int> UpdateWhere_SqlAsync(Expression<Func<T, bool>> where, params (string field, UpdateType updateType, object value)[] values)
        {
            return await Service.UpdateWhere_SqlAsync(where, values);
        }

        #endregion

        #region 查询数据

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public T GetEntity(params object[] keyValue)
        {
            return Service.GetEntity<T>(keyValue);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public async Task<T> GetEntityAsync(params object[] keyValue)
        {
            return await Service.GetEntityAsync<T>(keyValue);
        }

        /// <summary>
        /// 获取表的所有数据，当数据量很大时不要使用！
        /// </summary>
        /// <returns></returns>
        public List<T> GetList()
        {
            return Service.GetList<T>();
        }

        /// <summary>
        /// 获取表的所有数据，当数据量很大时不要使用！
        /// </summary>
        /// <returns></returns>
        public async Task<List<T>> GetListAsync()
        {
            return await Service.GetListAsync<T>();
        }

        /// <summary>
        /// 获取实体对应的表，延迟加载，主要用于支持Linq查询操作
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<T> GetIQueryable()
        {
            return Service.GetIQueryable<T>();
        }

        /// <summary>
        /// 获取分页后的数据
        /// </summary>
        /// <typeparam name="U">实体类型</typeparam>
        /// <param name="query">数据源IQueryable</param>
        /// <param name="pagination">分页参数</param>
        /// <returns></returns>
        public List<U> GetPagination<U>(IQueryable<U> query, Pagination pagination)
        {
            return query.GetPagination(pagination).ToList();
        }

        /// <summary>
        /// 获取分页后的数据
        /// </summary>
        /// <typeparam name="U">实体参数</typeparam>
        /// <param name="query">IQueryable数据源</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageRows">每页行数</param>
        /// <param name="orderColumn">排序列</param>
        /// <param name="orderType">排序类型</param>
        /// <param name="count">总记录数</param>
        /// <param name="pages">总页数</param>
        /// <returns></returns>
        public List<U> GetPagination<U>(IQueryable<U> query, int pageIndex, int pageRows, string orderColumn, string orderType, ref int count, ref int pages)
        {
            Pagination pagination = new Pagination
            {
                PageIndex = pageIndex,
                PageRows = pageRows,
                SortType = orderType,
                SortField = orderColumn
            };
            count = pagination.Total = query.Count();
            pages = pagination.PageCount;

            return query.GetPagination(pagination).ToList();
        }

        #endregion

        #region 执行Sql语句

        #endregion

        #region 业务返回

        /// <summary>
        /// 返回成功
        /// </summary>
        /// <returns></returns>
        public AjaxResult Success()
        {
            AjaxResult res = new AjaxResult
            {
                Success = true,
                Msg = "请求成功！",
            };

            return res;
        }

        /// <summary>
        /// 返回成功
        /// </summary>
        /// <param name="data">返回数据</param>
        /// <returns></returns>
        public AjaxResult<U> Success<U>(U data)
        {
            AjaxResult<U> res = new AjaxResult<U>
            {
                Success = true,
                Msg = "操作成功",
                Data = data
            };

            return res;
        }

        /// <summary>
        /// 返回成功
        /// </summary>
        /// <param name="data">返回数据</param>
        /// <param name="msg">返回消息</param>
        /// <returns></returns>
        public AjaxResult<U> Success<U>(U data, string msg)
        {
            AjaxResult<U> res = new AjaxResult<U>
            {
                Success = true,
                Msg = msg,
                Data = data
            };

            return res;
        }

        /// <summary>
        /// 返回错误
        /// </summary>
        /// <returns></returns>
        public AjaxResult Error()
        {
            AjaxResult res = new AjaxResult
            {
                Success = false,
                Msg = "请求失败！",
            };

            return res;
        }

        /// <summary>
        /// 返回错误
        /// </summary>
        /// <param name="msg">错误提示</param>
        /// <returns></returns>
        public AjaxResult Error(string msg)
        {
            AjaxResult res = new AjaxResult
            {
                Success = false,
                Msg = msg,
            };

            return res;
        }

        /// <summary>
        /// 构建前端Select远程搜索数据
        /// </summary>
        /// <param name="selectedValueJson">已选择的项，JSON数组</param>
        /// <param name="q">查询关键字</param>
        /// <returns></returns>
        public async Task<List<SelectOption>> GetOptionListAsync(string selectedValueJson, string q)
        {
            return await GetOptionListAsync(selectedValueJson, q, _textField, _valueField, null);
        }

        /// <summary>
        /// 构建前端Select远程搜索数据
        /// </summary>
        /// <param name="selectedValueJson">已选择的项，JSON数组</param>
        /// <param name="q">查询关键字</param>
        /// <param name="textFiled">文本字段</param>
        /// <param name="valueField">值字段</param>
        /// <param name="source">指定数据源</param>
        /// <returns></returns>
        public async Task<List<SelectOption>> GetOptionListAsync(string selectedValueJson, string q, string textFiled, string valueField, IQueryable<T> source = null)
        {
            Pagination pagination = new Pagination
            {
                PageRows = 10
            };

            List<T> selectedList = new List<T>();
            string where = " 1=1 ";
            List<string> ids = selectedValueJson?.ToList<string>() ?? new List<string>();
            if (ids.Count > 0)
            {
                selectedList = await GetNewQ().Where($"@0.Contains({valueField})", ids).ToListAsync();

                where += $" && !@0.Contains({valueField})";
            }

            if (!q.IsNullOrEmpty())
            {
                where += $" && it.{textFiled}.Contains(@1)";
            }
            List<T> newQList = await GetNewQ().Where(where, ids, q).GetPagination(pagination).ToListAsync();

            var resList = selectedList.Concat(newQList).Select(x => new SelectOption
            {
                value = x.GetPropertyValue(valueField)?.ToString(),
                text = x.GetPropertyValue(textFiled)?.ToString()
            }).ToList();

            return resList;

            IQueryable<T> GetNewQ()
            {
                return source ?? GetIQueryable();
            }
        }

        #endregion

        #region 其它操作

        #endregion

        #region Dispose

        private bool _disposed = false;
        public virtual void Dispose()
        {
            if (_disposed)
                return;

            _disposed = true;
            _dbs?.ForEach(x => x?.Dispose());
        }

        #endregion
    }
}
