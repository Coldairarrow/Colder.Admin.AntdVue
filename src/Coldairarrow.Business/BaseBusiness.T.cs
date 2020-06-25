using Coldairarrow.Util;
using EFCore.Sharding;
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
    /// </summary>
    /// <typeparam name="T">泛型约束（数据库实体）</typeparam>
    public abstract class BaseBusiness<T> where T : class, new()
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="db">注入数据库</param>
        public BaseBusiness(IDbAccessor db)
        {
            Db = db;
        }

        #endregion

        #region 私有成员

        protected virtual string _valueField { get; } = "Id";
        protected virtual string _textField { get => throw new Exception("请在子类重写"); }

        #endregion

        #region 外部属性

        /// <summary>
        /// 业务仓储接口(支持软删除),支持联表操作
        /// 注：若需要访问逻辑删除的数据,请使用IDbAccessor.FullRepository
        /// 注：仅支持单线程操作
        /// </summary>
        public IDbAccessor Db { get; }

        #endregion

        #region 事物提交

        public (bool Success, Exception ex) RunTransaction(Action action, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            return Db.RunTransaction(action, isolationLevel);
        }
        public async Task<(bool Success, Exception ex)> RunTransactionAsync(Func<Task> action, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            return await Db.RunTransactionAsync(action, isolationLevel);
        }

        #endregion

        #region 增加数据

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        public int Insert(T entity)
        {
            return Db.Insert(entity);
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        public async Task<int> InsertAsync(T entity)
        {
            return await Db.InsertAsync(entity);
        }

        /// <summary>
        /// 添加多条数据
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        public int Insert(List<T> entities)
        {
            return Db.Insert(entities);
        }

        /// <summary>
        /// 添加多条数据
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        public async Task<int> InsertAsync(List<T> entities)
        {
            return await Db.InsertAsync(entities);
        }

        /// <summary>
        /// 批量添加数据,速度快
        /// </summary>
        /// <param name="entities"></param>
        public void BulkInsert(List<T> entities)
        {
            Db.BulkInsert(entities);
        }

        #endregion

        #region 删除数据

        /// <summary>
        /// 删除所有数据
        /// </summary>
        public int DeleteAll()
        {
            return Db.DeleteAll<T>();
        }

        /// <summary>
        /// 删除所有数据
        /// </summary>
        public async Task<int> DeleteAllAsync()
        {
            return await Db.DeleteAllAsync<T>();
        }

        /// <summary>
        /// 删除指定主键数据
        /// </summary>
        /// <param name="key"></param>
        public int Delete(string key)
        {
            return Db.Delete<T>(key);
        }

        /// <summary>
        /// 删除指定主键数据
        /// </summary>
        /// <param name="key"></param>
        public async Task<int> DeleteAsync(string key)
        {
            return await Db.DeleteAsync<T>(key);
        }

        /// <summary>
        /// 通过主键删除多条数据
        /// </summary>
        /// <param name="keys"></param>
        public int Delete(List<string> keys)
        {
            return Db.Delete<T>(keys);
        }

        /// <summary>
        /// 通过主键删除多条数据
        /// </summary>
        /// <param name="keys"></param>
        public async Task<int> DeleteAsync(List<string> keys)
        {
            return await Db.DeleteAsync<T>(keys);
        }

        /// <summary>
        /// 删除单条数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        public int Delete(T entity)
        {
            return Db.Delete<T>(entity);
        }

        /// <summary>
        /// 删除单条数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        public async Task<int> DeleteAsync(T entity)
        {
            return await Db.DeleteAsync(entity);
        }

        /// <summary>
        /// 删除多条数据
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        public int Delete(List<T> entities)
        {
            return Db.Delete<T>(entities);
        }

        /// <summary>
        /// 删除多条数据
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        public async Task<int> DeleteAsync(List<T> entities)
        {
            return await Db.DeleteAsync<T>(entities);
        }

        /// <summary>
        /// 删除指定条件数据
        /// </summary>
        /// <param name="condition">筛选条件</param>
        public int Delete(Expression<Func<T, bool>> condition)
        {
            return Db.Delete(condition);
        }

        /// <summary>
        /// 删除指定条件数据
        /// </summary>
        /// <param name="condition">筛选条件</param>
        public async Task<int> DeleteAsync(Expression<Func<T, bool>> condition)
        {
            return await Db.DeleteAsync(condition);
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
            return Db.Delete_Sql(where);
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
            return await Db.Delete_SqlAsync(where);
        }

        #endregion

        #region 更新数据

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        public int Update(T entity)
        {
            return Db.Update(entity);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        public async Task<int> UpdateAsync(T entity)
        {
            return await Db.UpdateAsync(entity);
        }

        /// <summary>
        /// 更新多条数据
        /// </summary>
        /// <param name="entities">数据列表</param>
        public int Update(List<T> entities)
        {
            return Db.Update(entities);
        }

        /// <summary>
        /// 更新多条数据
        /// </summary>
        /// <param name="entities">数据列表</param>
        public async Task<int> UpdateAsync(List<T> entities)
        {
            return await Db.UpdateAsync(entities);
        }

        /// <summary>
        /// 指定条件更新
        /// </summary>
        /// <param name="whereExpre">筛选表达式</param>
        /// <param name="set">更改属性回调</param>
        public int UpdateWhere(Expression<Func<T, bool>> whereExpre, Action<T> set)
        {
            return Db.UpdateWhere(whereExpre, set);
        }

        /// <summary>
        /// 指定条件更新
        /// </summary>
        /// <param name="whereExpre">筛选表达式</param>
        /// <param name="set">更改属性回调</param>
        public async Task<int> UpdateWhereAsync(Expression<Func<T, bool>> whereExpre, Action<T> set)
        {
            return await Db.UpdateWhereAsync(whereExpre, set);
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
            return Db.UpdateWhere_Sql(where, values);
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
            return await Db.UpdateWhere_SqlAsync(where, values);
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
            return Db.GetEntity<T>(keyValue);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public async Task<T> GetEntityAsync(params object[] keyValue)
        {
            return await Db.GetEntityAsync<T>(keyValue);
        }

        /// <summary>
        /// 获取表的所有数据，当数据量很大时不要使用！
        /// </summary>
        /// <returns></returns>
        public List<T> GetList()
        {
            return Db.GetList<T>();
        }

        /// <summary>
        /// 获取表的所有数据，当数据量很大时不要使用！
        /// </summary>
        /// <returns></returns>
        public async Task<List<T>> GetListAsync()
        {
            return await Db.GetListAsync<T>();
        }

        /// <summary>
        /// 获取实体对应的表，延迟加载，主要用于支持Linq查询操作
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<T> GetIQueryable()
        {
            return Db.GetIQueryable<T>();
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
        /// <param name="input">查询参数</param>
        /// <returns></returns>
        public async Task<List<SelectOption>> GetOptionListAsync(OptionListInputDTO input)
        {
            return await GetOptionListAsync(input, _textField, _valueField, null);
        }

        /// <summary>
        /// 构建前端Select远程搜索数据
        /// </summary>
        /// <param name="input">查询参数</param>
        /// <param name="textFiled">文本字段</param>
        /// <param name="valueField">值字段</param>
        /// <param name="source">指定数据源</param>
        /// <returns></returns>
        public async Task<List<SelectOption>> GetOptionListAsync(OptionListInputDTO input, string textFiled, string valueField, IQueryable<T> source = null)
        {
            PageInput pageInput = new PageInput
            {
                PageRows = 10
            };

            List<T> selectedList = new List<T>();
            string where = " 1=1 ";
            List<string> ids = input.selectedValues;
            if (ids.Count > 0)
            {
                selectedList = await GetNewQ().Where($"@0.Contains({valueField})", ids).ToListAsync();

                where += $" && !@0.Contains({valueField})";
            }

            if (!input.q.IsNullOrEmpty())
            {
                where += $" && it.{textFiled}.Contains(@1)";
            }
            List<T> newQList = await GetNewQ().Where(where, ids, input.q).GetPageListAsync(pageInput);

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
    }
}
