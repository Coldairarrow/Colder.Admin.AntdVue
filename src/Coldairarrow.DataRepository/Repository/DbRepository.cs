using Coldairarrow.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Coldairarrow.DataRepository
{
    /// <summary>
    /// 描述：数据库仓储基类类
    /// 作者：Coldairarrow
    /// </summary>
    /// <seealso cref="IRepository" />
    internal abstract class DbRepository : IRepository, IInternalTransaction
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="conString">构造参数，可以为数据库连接字符串或者DbContext</param>
        /// <param name="dbType">数据库类型</param>
        public DbRepository(string conString, DatabaseType dbType)
        {
            ConnectionString = conString;
            DbType = dbType;
            _db = DbFactory.GetDbContext1(conString, dbType);
        }

        #endregion

        #region 私有成员

        protected BaseDbContext _db { get; }
        protected DbTransaction _transaction { get; set; }
        protected static PropertyInfo GetKeyProperty(Type type)
        {
            return GetKeyPropertys(type).FirstOrDefault();
        }
        protected static List<PropertyInfo> GetKeyPropertys(Type type)
        {
            var properties = type
                .GetProperties()
                .Where(x => x.GetCustomAttributes(true).Select(o => o.GetType().FullName).Contains(typeof(KeyAttribute).FullName))
                .ToList();

            return properties;
        }
        protected string GetDbTableName(Type type)
        {
            string tableName = string.Empty;
            var tableAttribute = type.GetCustomAttribute<TableAttribute>();
            if (tableAttribute != null)
                tableName = tableAttribute.Name;
            else
                tableName = type.Name;

            return tableName;
        }
        protected bool _openedTransaction { get; set; } = false;
        protected virtual string FormatFieldName(string name)
        {
            throw new NotImplementedException("请在子类实现!");
        }
        protected virtual string FormatParamterName(string name)
        {
            return $"@{name}";
        }
        private (string sql, List<(string paramterName, object paramterValue)> paramters) GetWhereSql(IQueryable query)
        {
            List<(string paramterName, object paramterValue)> paramters =
                new List<(string paramterName, object paramterValue)>();
            var querySql = query.ToSql();
            string theQSql = querySql.sql.Replace("\r\n", "\n").Replace("\n", " ");
            //无筛选
            if (!theQSql.Contains("WHERE"))
                return (" 1=1 ", paramters);

            string pattern1 = "^SELECT.*?FROM.*? AS (.*?) WHERE .*?$";
            string pattern2 = "^SELECT.*?FROM .*? (.*?) WHERE .*?$";
            string asTmp = string.Empty;
            if (Regex.IsMatch(theQSql, pattern1))
            {
                var match = Regex.Match(theQSql, pattern1);
                asTmp = match.Groups[1]?.ToString();
            }
            else if (Regex.IsMatch(theQSql, pattern2))
            {
                var match = Regex.Match(theQSql, pattern2);
                asTmp = match.Groups[1]?.ToString();
            }
            if (asTmp.IsNullOrEmpty())
                throw new Exception("SQL解析失败!");

            string whereSql = querySql.sql.Split(new string[] { "WHERE" }, StringSplitOptions.None)[1].Replace($"{asTmp}.", "");

            querySql.parameters.ForEach(aData =>
            {
                if (whereSql.Contains(aData.Key))
                    paramters.Add((aData.Key, aData.Value));
            });

            return (whereSql, paramters);
        }

        #endregion

        #region 事物相关

        public void BeginTransaction(IsolationLevel isolationLevel)
        {
            _openedTransaction = true;
            _transaction = _db.Database.BeginTransaction().GetDbTransaction();
        }

        public (bool Success, Exception ex) RunTransaction(Action action, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            bool success = true;
            Exception resEx = null;
            try
            {
                BeginTransaction(isolationLevel);

                action();

                CommitTransaction();
            }
            catch (Exception ex)
            {
                success = false;
                resEx = ex;
                RollbackTransaction();
            }
            finally
            {
                _openedTransaction = false;
                _transaction.Dispose();
            }

            return (success, resEx);
        }

        #endregion

        #region 数据库相关

        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString { get; }

        /// <summary>
        /// 数据库类型
        /// </summary>
        public DatabaseType DbType { get; }

        /// <summary>
        /// 提交事物
        /// </summary>
        public void CommitTransaction()
        {
            _transaction?.Commit();
        }

        /// <summary>
        /// 回滚事物
        /// </summary>
        public void RollbackTransaction()
        {
            _transaction?.Rollback();
        }

        /// <summary>
        /// SQL日志处理方法
        /// </summary>
        /// <value>
        /// The handle SQL log.
        /// </value>
        public Action<string> HandleSqlLog { set => EFCoreSqlLogeerProvider.HandleSqlLog = value; }

        /// <summary>
        /// 使用已存在的事物
        /// </summary>
        /// <param name="transaction">事物对象</param>
        public void UseTransaction(DbTransaction transaction)
        {
            //if (_transaction != null)
            //    _transaction.Dispose();

            //_openedTransaction = true;
            //_transaction = transaction;
            //Db.UseTransaction(transaction);
        }

        /// <summary>
        /// 获取事物对象
        /// </summary>
        /// <returns></returns>
        public DbTransaction GetTransaction()
        {
            return _transaction;
        }

        #endregion

        #region 增加数据

        public int Insert<T>(T entity) where T : class, new()
        {
            return Insert(new List<object> { entity });
        }
        public async Task<int> InsertAsync<T>(T entity) where T : class, new()
        {
            return await InsertAsync(new List<T> { entity });
        }
        public int Insert<T>(List<T> entities) where T : class, new()
        {
            _db.AddRange(entities);

            return _db.SaveChanges();
        }
        public async Task<int> InsertAsync<T>(List<T> entities) where T : class, new()
        {
            await _db.AddRangeAsync(entities);

            return await _db.SaveChangesAsync();
        }
        public abstract void BulkInsert<T>(List<T> entities) where T : class, new();

        #endregion

        #region 删除数据

        public int DeleteAll<T>() where T : class, new()
        {
            return DeleteAll(typeof(T));
        }
        public async Task<int> DeleteAllAsync<T>() where T : class, new()
        {
            return await DeleteAllAsync(typeof(T));
        }
        public int DeleteAll(Type type)
        {
            return Delete_Sql(type, "true");
        }
        public async Task<int> DeleteAllAsync(Type type)
        {
            return await Delete_SqlAsync(type, "true");
        }
        public int Delete<T>(T entity) where T : class, new()
        {
            return Delete(new List<T> { entity });
        }
        public async Task<int> DeleteAsync<T>(T entity) where T : class, new()
        {
            return await DeleteAsync(new List<T> { entity });
        }
        public int Delete<T>(List<T> entities) where T : class, new()
        {
            _db.RemoveRange(entities);

            return _db.SaveChanges();
        }
        public async Task<int> DeleteAsync<T>(List<T> entities) where T : class, new()
        {
            _db.RemoveRange(entities);

            return await _db.SaveChanges();
        }
        public int Delete<T>(Expression<Func<T, bool>> condition) where T : class, new()
        {
            var deleteList = GetIQueryable<T>().Where(condition).ToList();
            return Delete(deleteList);
        }
        public async Task<int> DeleteAsync<T>(Expression<Func<T, bool>> condition) where T : class, new()
        {
            var deleteList = await GetIQueryable<T>().Where(condition).ToListAsync();
            return await DeleteAsync(deleteList);
        }
        public int Delete<T>(string key) where T : class, new()
        {
            return Delete<T>(new List<string> { key });
        }
        public async Task<int> DeleteAsync<T>(string key) where T : class, new()
        {
            return await DeleteAsync<T>(new List<string> { key });
        }
        public int Delete<T>(List<string> keys) where T : class, new()
        {
            return Delete(typeof(T), keys);
        }
        public int Delete(Type type, string key)
        {
            return Delete(type, new List<string> { key });
        }
        public async Task<int> DeleteAsync(Type type, string key)
        {
            return await DeleteAsync(type, new List<string> { key });
        }
        public int Delete(Type type, List<string> keys)
        {
            var theProperty = GetKeyProperty(type);
            if (theProperty == null)
                throw new Exception("该实体没有主键标识！请使用[Key]标识主键！");

            List<object> deleteList = new List<object>();
            keys.ForEach(aKey =>
            {
                object newData = Activator.CreateInstance(type);
                var value = aKey.ChangeType(theProperty.PropertyType);
                theProperty.SetValue(newData, value);
                deleteList.Add(newData);
            });

            return Delete(deleteList);
        }
        public int Delete_Sql<T>(Expression<Func<T, bool>> where) where T : class, new()
        {
            var iq = GetIQueryable<T>().Where(where);

            return _Delete_Sql(iq);
        }
        public int Delete_Sql(Type entityType, string where, params object[] paramters)
        {
            var iq = GetIQueryable(entityType).Where(where, paramters);

            return _Delete_Sql(iq);
        }
        public async Task<int> DeleteAsync(Type type, List<string> keys)
        {
            throw new NotImplementedException();
        }


        public async Task<int> DeleteAsync<T>(List<string> keys) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public async Task<int> Delete_SqlAsync<T>(Expression<Func<T, bool>> where) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public async Task<int> Delete_SqlAsync(Type entityType, string where, params object[] paramters)
        {
            throw new NotImplementedException();
        }

        private (string sql, List<(string paramterName, object paramterValue)> paramters) GetDeleteSql(IQueryable iq)
        {


        }

        private int _Delete_Sql(IQueryable iq)
        {
            string tableName = iq.ElementType.Name;
            DbProviderFactory dbProviderFactory = DbProviderFactoryHelper.GetDbProviderFactory(DbType);
            var whereSql = GetWhereSql(iq);
            var paramters = whereSql.paramters.Select(x =>
            {
                var newParamter = dbProviderFactory.CreateParameter();
                newParamter.ParameterName = x.Key;
                newParamter.Value = x.Value;

                return newParamter;
            }).ToList();

            string sql = $"DELETE FROM {FormatFieldName(tableName)} WHERE {whereSql.sql}";

            return ExecuteSql(sql, paramters);
        }

        #endregion

        #region 更新数据

        /// <summary>
        /// 更新单条记录
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <param name="entity">实体对象</param>
        public void Update<T>(T entity) where T : class, new()
        {
            Update(new List<object> { entity });
        }

        /// <summary>
        /// 更新多条记录
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <param name="entities">实体对象集合</param>
        public void Update<T>(List<T> entities) where T : class, new()
        {
            Update(entities.CastToList<object>());
        }

        /// <summary>
        /// 更新多条记录
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        public void Update(List<object> entities)
        {
            PackWork(entities.Select(x => x.GetType()), () =>
            {
                entities.ForEach(x => Db.Entry(x).State = EntityState.Modified);
            });
        }

        /// <summary>
        /// 更新单条记录的某些属性
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <param name="entity">实体对象</param>
        /// <param name="properties">属性</param>
        public void UpdateAny<T>(T entity, List<string> properties) where T : class, new()
        {
            UpdateAny(new List<object> { entity }, properties);
        }

        /// <summary>
        /// 更新多条记录的某些属性
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <param name="entities">实体对象集合</param>
        /// <param name="properties">属性</param>
        public void UpdateAny<T>(List<T> entities, List<string> properties) where T : class, new()
        {
            UpdateAny(entities.CastToList<object>(), properties);
        }

        /// <summary>
        /// 更新多条记录的某些属性
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        /// <param name="properties">属性</param>
        public void UpdateAny(List<object> entities, List<string> properties)
        {
            PackWork(entities.Select(x => x.GetType()), () =>
            {
                entities.ForEach(aEntity =>
                {
                    var targetObj = aEntity.ChangeType(Db.CheckEntityType(aEntity.GetType()));
                    Db.Attach(targetObj);
                    properties.ForEach(aProperty =>
                    {
                        Db.Entry(targetObj).Property(aProperty).IsModified = true;
                    });
                });
            });
        }

        /// <summary>
        /// 按照条件更新记录
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <param name="whereExpre">筛选条件</param>
        /// <param name="set">更新操作</param>
        public void UpdateWhere<T>(Expression<Func<T, bool>> whereExpre, Action<T> set) where T : class, new()
        {
            var list = GetIQueryable<T>().Where(whereExpre).ToList();
            list.ForEach(aData => set(aData));
            Update(list);
        }

        /// <summary>
        /// 使用SQL语句按照条件更新
        /// 用法:UpdateWhere_Sql"Base_User"(x=&gt;x.Id == "Admin",("Name","小明"))
        /// 注：生成的SQL类似于UPDATE [TABLE] SET [Name] = 'xxx' WHERE [Id] = 'Admin'
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="where">筛选条件</param>
        /// <param name="values">字段值设置</param>
        /// <returns>
        /// 影响条数
        /// </returns>
        public int UpdateWhere_Sql<T>(Expression<Func<T, bool>> where, params (string field, object value)[] values) where T : class, new()
        {
            var iq = GetIQueryable<T>().Where(where);

            return _UpdateWhere_Sql(iq, values);
        }

        public int UpdateWhere_Sql(Type entityType, string where, object[] paramters, params (string field, object value)[] values)
        {
            var iq = GetIQueryable(entityType).Where(where, paramters);

            return _UpdateWhere_Sql(iq, values);
        }

        private int _UpdateWhere_Sql(IQueryable iq, params (string field, object value)[] values)
        {
            string tableName = iq.ElementType.Name;
            DbProviderFactory dbProviderFactory = DbProviderFactoryHelper.GetDbProviderFactory(DbType);

            List<KeyValuePair<string, object>> parameterList = new List<KeyValuePair<string, object>>();
            var whereSql = GetWhereSql(iq);

            parameterList.AddRange(whereSql.paramters.ToArray());

            List<string> propertySetStr = new List<string>();

            values.ToList().ForEach(aProperty =>
            {
                var paramterName = FormatParamterName($"_p_{aProperty.field}");
                parameterList.Add(new KeyValuePair<string, object>(paramterName, aProperty.value));
                propertySetStr.Add($" {FormatFieldName(aProperty.field)} = {paramterName} ");
            });

            var paramters = parameterList.Select(x =>
            {
                var newParamter = dbProviderFactory.CreateParameter();
                newParamter.ParameterName = x.Key;
                newParamter.Value = x.Value;

                return newParamter;
            }).ToList();

            string sql = $"UPDATE {FormatFieldName(tableName)} SET {string.Join(",", propertySetStr)} WHERE {whereSql.sql}";

            return ExecuteSql(sql, paramters);
        }

        #endregion

        #region 查询数据

        /// <summary>
        /// 获取单条记录
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public T GetEntity<T>(params object[] keyValue) where T : class, new()
        {
            var obj = Db.Set<T>().Find(keyValue);
            if (!obj.IsNullOrEmpty())
                Db.Entry(obj).State = EntityState.Detached;

            return obj;
        }

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <returns></returns>
        public List<T> GetList<T>() where T : class, new()
        {
            return GetIQueryable<T>().ToList();
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="type">实体类型</param>
        /// <returns></returns>
        public List<object> GetList(Type type)
        {
            return GetIQueryable(type).CastToList<object>();
        }

        /// <summary>
        /// 获取IQueryable
        /// 注:默认取消实体追踪
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <returns></returns>
        public IQueryable<T> GetIQueryable<T>() where T : class, new()
        {
            return GetIQueryable(typeof(T)) as IQueryable<T>;
        }

        /// <summary>
        /// 获取IQueryable
        /// 注:默认取消实体追踪
        /// </summary>
        /// <param name="type">实体泛型</param>
        /// <returns></returns>
        public IQueryable GetIQueryable(Type type)
        {
            return Db.GetIQueryable(type);
        }

        /// <summary>
        /// 通过SQL获取DataTable
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns></returns>
        public DataTable GetDataTableWithSql(string sql)
        {
            return GetDataTableWithSql(sql, null);
        }

        /// <summary>
        /// 通过SQL获取DataTable
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">SQL参数</param>
        /// <returns></returns>
        public DataTable GetDataTableWithSql(string sql, List<DbParameter> parameters)
        {
            DbProviderFactory dbProviderFactory = DbProviderFactoryHelper.GetDbProviderFactory(DbType);
            using (DbConnection conn = dbProviderFactory.CreateConnection())
            {
                conn.ConnectionString = ConnectionString;
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    cmd.CommandTimeout = 5 * 60;

                    if (parameters != null && parameters?.Count > 0)
                        cmd.Parameters.AddRange(parameters.ToArray());

                    DbDataAdapter adapter = dbProviderFactory.CreateDataAdapter();
                    adapter.SelectCommand = cmd;
                    DataSet table = new DataSet();
                    adapter.Fill(table);
                    cmd.Parameters.Clear();

                    return table.Tables[0];
                }
            }
        }

        /// <summary>
        /// 通过SQL获取List
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <param name="sqlStr">SQL语句</param>
        /// <returns></returns>
        public List<T> GetListBySql<T>(string sqlStr) where T : class, new()
        {
            return GetListBySql<T>(sqlStr, new List<DbParameter>());
        }

        /// <summary>
        /// 通过SQL获取List
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <param name="sqlStr">SQL语句</param>
        /// <param name="parameters">SQL参数</param>
        /// <returns></returns>
        public List<T> GetListBySql<T>(string sqlStr, List<DbParameter> parameters) where T : class, new()
        {
            return Db.Set<T>().FromSql(sqlStr, parameters.ToArray()).ToList();
        }

        #endregion

        #region 执行Sql语句

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="sql">SQL语句</param>
        public int ExecuteSql(string sql)
        {
            int count = Db.Database.ExecuteSqlCommand(sql);

            if (!_openedTransaction)
                Dispose();

            return count;
        }

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">SQL参数</param>
        public int ExecuteSql(string sql, List<DbParameter> parameters)
        {
            int count = Db.Database.ExecuteSqlCommand(sql, parameters.ToArray());

            if (!_openedTransaction)
                Dispose();

            return count;
        }

        #endregion

        #region Dispose
        private bool _disposed { get; set; }
        public virtual void Dispose()
        {
            if (_disposed)
                return;

            _db.Dispose();
        }

        int IRepository.UpdateAny(List<object> entities, List<string> properties)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAnyAsync(List<object> entities, List<string> properties)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateWhere_SqlAsync<T>(Expression<Func<T, bool>> where, params (string field, object value)[] values) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public int UpdateWhere_Sql<T>(Expression<Func<T, bool>> where, params (string field, UpdateType updateType, object value)[] values) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateWhere_SqlAsync(Type entityType, string where, object[] paramters, params (string field, UpdateType updateType, object value)[] values)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetEntityAsync<T>(params object[] keyValue) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<List<object>> GetListAsync(Type type)
        {
            throw new NotImplementedException();
        }

        public DataTable GetDataTableWithSql(string sql, params (string paramterName, object value)[] parameters)
        {
            throw new NotImplementedException();
        }

        public Task<DataTable> GetDataTableWithSqlAsync(string sql, params (string paramterName, object value)[] parameters)
        {
            throw new NotImplementedException();
        }

        public List<T> GetListBySql<T>(string sqlStr, params (string paramterName, object value)[] parameters) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetListBySqlAsync<T>(string sqlStr, params (string paramterName, object value)[] parameters) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public int ExecuteSql(string sql, params (string paramterName, object paramterValue)[] paramters)
        {
            throw new NotImplementedException();
        }

        public Task<int> ExecuteSqlAsync(string sql, params (string paramterName, object paramterValue)[] paramters)
        {
            throw new NotImplementedException();
        }

        int IBaseRepository.Update<T>(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync<T>(T entity) where T : class, new()
        {
            throw new NotImplementedException();
        }

        int IBaseRepository.Update<T>(List<T> entities)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync<T>(List<T> entities) where T : class, new()
        {
            throw new NotImplementedException();
        }

        int IBaseRepository.UpdateAny<T>(T entity, List<string> properties)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAnyAsync<T>(T entity, List<string> properties) where T : class, new()
        {
            throw new NotImplementedException();
        }

        int IBaseRepository.UpdateAny<T>(List<T> entities, List<string> properties)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAnyAsync<T>(List<T> entities, List<string> properties) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateWhereAsync<T>(Expression<Func<T, bool>> whereExpre, Action<T> set) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetListAsync<T>() where T : class, new()
        {
            throw new NotImplementedException();
        }

        public void CommitDb()
        {
            throw new NotImplementedException();
        }

        int IRepository.BulkInsert<T>(List<T> entities)
        {
            throw new NotImplementedException();
        }


        #endregion
    }
}
