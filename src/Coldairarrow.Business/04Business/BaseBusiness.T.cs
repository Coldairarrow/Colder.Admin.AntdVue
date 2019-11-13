using Coldairarrow.DataRepository;
using Coldairarrow.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

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
        private object _serviceLock = new object();
        protected virtual string _valueField { get; } = "Id";
        protected virtual string _textField { get => throw new Exception("请在子类重写"); }

        #endregion

        #region 外部属性

        /// <summary>
        /// 底层仓储接口,支持跨表操作
        /// 注：仅支持单线程操作
        /// </summary>
        /// <value>
        /// The service.
        /// </value>
        public IRepository Service
        {
            get
            {
                if (_service == null) //双if +lock
                {
                    lock (_serviceLock)
                    {
                        if (_service == null)
                        {
                            _service = DbFactory.GetRepository(_conString, _dbType);
                            _service = new BusRepository(_service);
                        }
                    }
                }

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
            return new BusRepository(DbFactory.GetRepository(_conString, _dbType));
        }

        /// <summary>
        /// 获取新的数据仓储
        /// 注:支持多线程(每个线程需要单独的IRepository)
        /// </summary>
        /// <param name="conString">连接字符串</param>
        /// <param name="dbType">数据库类型</param>
        /// <returns></returns>
        public IRepository GetNewService(string conString, DatabaseType dbType)
        {
            return new BusRepository(DbFactory.GetRepository(conString, dbType));
        }

        public void UseRepository(IRepository repository)
        {
            _service = repository;
        }

        #endregion

        #region 事物提交

        public (bool Success, Exception ex) RunTransaction(Action action, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            return Service.RunTransaction(action, isolationLevel);
        }

        #endregion

        #region 增加数据

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        public void Insert(T entity)
        {
            Service.Insert<T>(entity);
        }

        /// <summary>
        /// 添加多条数据
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        public void Insert(List<T> entities)
        {
            Service.Insert<T>(entities);
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
        public void DeleteAll()
        {
            Service.DeleteAll<T>();
        }

        /// <summary>
        /// 删除指定主键数据
        /// </summary>
        /// <param name="key"></param>
        public void Delete(string key)
        {
            Service.Delete<T>(key);
        }

        /// <summary>
        /// 通过主键删除多条数据
        /// </summary>
        /// <param name="keys"></param>
        public void Delete(List<string> keys)
        {
            Service.Delete<T>(keys);
        }

        /// <summary>
        /// 删除单条数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        public void Delete(T entity)
        {
            Service.Delete<T>(entity);
        }

        /// <summary>
        /// 删除多条数据
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        public void Delete(List<T> entities)
        {
            Service.Delete<T>(entities);
        }

        /// <summary>
        /// 删除指定条件数据
        /// </summary>
        /// <param name="condition">筛选条件</param>
        public void Delete(Expression<Func<T, bool>> condition)
        {
            Service.Delete(condition);
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

        #endregion

        #region 更新数据

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        public void Update(T entity)
        {
            Service.Update(entity);
        }

        /// <summary>
        /// 更新多条数据
        /// </summary>
        /// <param name="entities">数据列表</param>
        public void Update(List<T> entities)
        {
            Service.Update<T>(entities);
        }

        /// <summary>
        /// 更新一条数据,某些属性
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="properties">需要更新的字段</param>
        public void UpdateAny(T entity, List<string> properties)
        {
            Service.UpdateAny(entity, properties);
        }

        /// <summary>
        /// 更新多条数据,某些属性
        /// </summary>
        /// <param name="entities">数据列表</param>
        /// <param name="properties">需要更新的字段</param>
        public void UpdateAny(List<T> entities, List<string> properties)
        {
            Service.UpdateAny(entities, properties);
        }

        /// <summary>
        /// 指定条件更新
        /// </summary>
        /// <param name="whereExpre">筛选表达式</param>
        /// <param name="set">更改属性回调</param>
        public void UpdateWhere(Expression<Func<T, bool>> whereExpre, Action<T> set)
        {
            Service.UpdateWhere(whereExpre, set);
        }

        /// <summary>
        /// 使用SQL语句按照条件更新
        /// 用法:UpdateWhere_Sql"Base_User"(x=&gt;x.Id == "Admin",("Name","小明"))
        /// 注：生成的SQL类似于UPDATE [TABLE] SET [Name] = 'xxx' WHERE [Id] = 'Admin'
        /// </summary>
        /// <param name="where">筛选条件</param>
        /// <param name="values">字段值设置</param>
        /// <returns>
        /// 影响条数
        /// </returns>
        public int UpdateWhere_Sql(Expression<Func<T, bool>> where, params (string field, object value)[] values)
        {
            return Service.UpdateWhere_Sql(where, values);
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
        /// 获取表的所有数据，当数据量很大时不要使用！
        /// </summary>
        /// <returns></returns>
        public List<T> GetList()
        {
            return Service.GetList<T>();
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

        /// <summary>
        /// 通过Sql查询返回DataTable
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns></returns>
        public DataTable GetDataTableWithSql(string sql)
        {
            return Service.GetDataTableWithSql(sql);
        }

        /// <summary>
        /// 通过Sql参数查询返回DataTable
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <param name="parameters">查询参数</param>
        /// <returns></returns>
        public DataTable GetDataTableWithSql(string sql, List<DbParameter> parameters)
        {
            return Service.GetDataTableWithSql(sql, parameters);
        }

        /// <summary>
        /// 通过sql返回List
        /// </summary>
        /// <param name="sqlStr">sql语句</param>
        /// <returns></returns>
        public List<U> GetListBySql<U>(string sqlStr) where U : class, new()
        {
            return Service.GetListBySql<U>(sqlStr);
        }

        /// <summary>
        /// 通过sql返回list
        /// </summary>
        /// <param name="sqlStr">sql语句</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public List<U> GetListBySql<U>(string sqlStr, List<DbParameter> param) where U : class, new()
        {
            return Service.GetListBySql<U>(sqlStr, param);
        }

        #endregion

        #region 执行Sql语句

        /// <summary>
        /// 执行Sql语句
        /// </summary>
        /// <param name="sql">Sql语句</param>
        public int ExecuteSql(string sql)
        {
            return Service.ExecuteSql(sql);
        }

        public int ExecuteSql(string sql, List<DbParameter> parameters)
        {
            return Service.ExecuteSql(sql, parameters);
        }

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
        public List<SelectOption> GetOptionList(string selectedValueJson, string q)
        {
            return GetOptionList(selectedValueJson, q, _textField, _valueField, null);
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
        public List<SelectOption> GetOptionList(string selectedValueJson, string q, string textFiled, string valueField, IQueryable<T> source = null)
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
                selectedList = GetNewQ().Where($"@0.Contains({valueField})", ids).ToList();

                where += $" && !@0.Contains({valueField})";
            }

            if (!q.IsNullOrEmpty())
            {
                where += $" && it.{textFiled}.Contains(@1)";
            }
            List<T> newQList = GetNewQ().Where(where, ids, q).GetPagination(pagination).ToList();

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

        /// <summary>
        /// 执行与释放或重置非托管资源关联的应用程序定义的任务。
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public virtual void Dispose()
        {
            _service?.Dispose();
        }

        #endregion
    }
}
