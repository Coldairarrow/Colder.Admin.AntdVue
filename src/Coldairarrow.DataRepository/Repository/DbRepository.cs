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
            _db = DbFactory.GetDbContext(conString, dbType);
        }

        #endregion

        #region 私有成员

        protected BaseDbContext _db { get; }
        protected IDbContextTransaction _transaction { get; set; }
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
        private (string sql, List<(string paramterName, object paramterValue)> paramters) GetDeleteSql(IQueryable iq)
        {
            string tableName = iq.ElementType.Name;
            var whereSql = GetWhereSql(iq);
            string sql = $"DELETE FROM {FormatFieldName(tableName)} WHERE {whereSql.sql}";

            return (sql, whereSql.paramters);
        }
        private List<object> GetDeleteList(Type type, List<string> keys)
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

            return deleteList;
        }
        private (string sql, List<(string paramterName, object paramterValue)> paramters) GetUpdateWhereSql(IQueryable iq, params (string field, UpdateType updateType, object value)[] values)
        {
            string tableName = iq.ElementType.Name;
            DbProviderFactory dbProviderFactory = DbProviderFactoryHelper.GetDbProviderFactory(DbType);
            var whereSql = GetWhereSql(iq);

            List<string> propertySetStr = new List<string>();

            values.ToList().ForEach(aProperty =>
            {
                var paramterName = FormatParamterName($"_p_{aProperty.field}");
                string formatedField = FormatFieldName(aProperty.field);
                whereSql.paramters.Add((paramterName, aProperty.value));

                string setValueBody = string.Empty;
                switch (aProperty.updateType)
                {
                    case UpdateType.Equal: setValueBody = paramterName; break;
                    case UpdateType.Add: setValueBody = $" {formatedField} + {paramterName} "; break;
                    case UpdateType.Minus: setValueBody = $" {formatedField} - {paramterName} "; break;
                    case UpdateType.Multiply: setValueBody = $" {formatedField} * {paramterName} "; break;
                    case UpdateType.Divide: setValueBody = $" {formatedField} / {paramterName} "; break;
                    default: throw new Exception("updateType无效");
                }

                propertySetStr.Add($" {formatedField} = {setValueBody} ");
            });
            string sql = $"UPDATE {FormatFieldName(tableName)} SET {string.Join(",", propertySetStr)} WHERE {whereSql.sql}";

            return (sql, whereSql.paramters);
        }
        private List<DbParameter> CreateDbParamters(List<(string paramterName, object paramterValue)> paramters)
        {
            DbProviderFactory dbProviderFactory = DbProviderFactoryHelper.GetDbProviderFactory(DbType);
            List<DbParameter> dbParamters = new List<DbParameter>();
            paramters.ForEach(aParamter =>
            {
                var newParamter = dbProviderFactory.CreateParameter();
                newParamter.ParameterName = aParamter.paramterName;
                newParamter.Value = aParamter.paramterValue;
                dbParamters.Add(newParamter);
            });

            return dbParamters;
        }

        #endregion

        #region 事物相关

        public void BeginTransaction(IsolationLevel isolationLevel)
        {
            _openedTransaction = true;
            _transaction = _db.Database.BeginTransaction(isolationLevel);
        }

        public async Task BeginTransactionAsync(IsolationLevel isolationLevel)
        {
            _openedTransaction = true;
            _transaction = await _db.Database.BeginTransactionAsync(isolationLevel);
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
                DisposeTransaction();
            }

            return (success, resEx);
        }

        public async Task<(bool Success, Exception ex)> RunTransactionAsync(Func<Task> action, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            bool success = true;
            Exception resEx = null;
            try
            {
                await BeginTransactionAsync(isolationLevel);

                await action();

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
                DisposeTransaction();
            }

            return (success, resEx);
        }

        #endregion

        #region 数据库相关

        public string ConnectionString { get; }
        public DatabaseType DbType { get; }
        public void CommitTransaction()
        {
            _transaction?.Commit();
        }
        public void RollbackTransaction()
        {
            _transaction?.Rollback();
        }
        public void DisposeTransaction()
        {
            _db.Detach();
            _transaction?.Dispose();
            _openedTransaction = false;
        }
        public Action<string> HandleSqlLog { set => EFCoreSqlLogeerProvider.HandleSqlLog = value; }

        #endregion

        #region 增加数据

        public int Insert<T>(T entity) where T : class, new()
        {
            return Insert(new List<T> { entity });
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

            return await _db.SaveChangesAsync();
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
        public async Task<int> DeleteAsync<T>(List<string> keys) where T : class, new()
        {
            return await DeleteAsync(typeof(T), keys);
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
            return Delete(GetDeleteList(type, keys));
        }
        public async Task<int> DeleteAsync(Type type, List<string> keys)
        {
            return await DeleteAsync(GetDeleteList(type, keys));
        }
        public int Delete_Sql<T>(Expression<Func<T, bool>> where) where T : class, new()
        {
            var iq = GetIQueryable<T>().Where(where);

            return Delete_Sql(iq);
        }
        public async Task<int> Delete_SqlAsync<T>(Expression<Func<T, bool>> where) where T : class, new()
        {
            var iq = GetIQueryable<T>().Where(where);

            return await Delete_SqlAsync(iq);
        }
        public int Delete_Sql(Type entityType, string where, params object[] paramters)
        {
            var iq = GetIQueryable(entityType).Where(where, paramters);

            return Delete_Sql(iq);
        }
        public async Task<int> Delete_SqlAsync(Type entityType, string where, params object[] paramters)
        {
            var iq = GetIQueryable(entityType).Where(where, paramters);

            return await Delete_SqlAsync(iq);
        }
        public int Delete_Sql(IQueryable source)
        {
            var sql = GetDeleteSql(source);

            return ExecuteSql(sql.sql, sql.paramters.ToArray());
        }
        public async Task<int> Delete_SqlAsync(IQueryable source)
        {
            var sql = GetDeleteSql(source);

            return await ExecuteSqlAsync(sql.sql, sql.paramters.ToArray());
        }

        #endregion

        #region 更新数据

        public int Update<T>(T entity) where T : class, new()
        {
            return Update(new List<T> { entity });
        }
        public async Task<int> UpdateAsync<T>(T entity) where T : class, new()
        {
            return await UpdateAsync(new List<T> { entity });
        }
        public int Update<T>(List<T> entities) where T : class, new()
        {
            _db.UpdateRange(entities);
            return _db.SaveChanges();
        }
        public async Task<int> UpdateAsync<T>(List<T> entities) where T : class, new()
        {
            _db.UpdateRange(entities);

            return await _db.SaveChangesAsync();
        }
        public int UpdateAny<T>(T entity, List<string> properties) where T : class, new()
        {
            return UpdateAny(new List<T> { entity }, properties);
        }
        public async Task<int> UpdateAnyAsync<T>(T entity, List<string> properties) where T : class, new()
        {
            return await UpdateAnyAsync(new List<T> { entity }, properties);
        }
        public int UpdateAny<T>(List<T> entities, List<string> properties) where T : class, new()
        {
            entities.ForEach(aEntity =>
            {
                properties.ForEach(aProperty =>
                {
                    _db.Entry(aEntity).Property(aProperty).IsModified = true;
                });
            });

            return _db.SaveChanges();
        }
        public async Task<int> UpdateAnyAsync<T>(List<T> entities, List<string> properties) where T : class, new()
        {
            return await UpdateAnyAsync(entities, properties);
        }
        public int UpdateWhere<T>(Expression<Func<T, bool>> whereExpre, Action<T> set) where T : class, new()
        {
            var list = GetIQueryable<T>().Where(whereExpre).ToList();
            list.ForEach(aData => set(aData));
            return Update(list);
        }
        public async Task<int> UpdateWhereAsync<T>(Expression<Func<T, bool>> whereExpre, Action<T> set) where T : class, new()
        {
            var list = GetIQueryable<T>().Where(whereExpre).ToList();
            list.ForEach(aData => set(aData));
            return await UpdateAsync(list);
        }
        public int UpdateWhere_Sql<T>(Expression<Func<T, bool>> where, params (string field, UpdateType updateType, object value)[] values) where T : class, new()
        {
            var iq = GetIQueryable<T>().Where(where);

            return UpdateWhere_Sql(iq, values);
        }
        public async Task<int> UpdateWhere_SqlAsync<T>(Expression<Func<T, bool>> where, params (string field, UpdateType updateType, object value)[] values) where T : class, new()
        {
            var iq = GetIQueryable<T>().Where(where);

            return await UpdateWhere_SqlAsync(iq, values);
        }
        public int UpdateWhere_Sql(Type entityType, string where, object[] paramters, params (string field, UpdateType updateType, object value)[] values)
        {
            var iq = GetIQueryable(entityType).Where(where, paramters);

            return UpdateWhere_Sql(iq, values);
        }
        public async Task<int> UpdateWhere_SqlAsync(Type entityType, string where, object[] paramters, params (string field, UpdateType updateType, object value)[] values)
        {
            var iq = GetIQueryable(entityType).Where(where, paramters);

            return await UpdateWhere_SqlAsync(iq, values);
        }
        public int UpdateWhere_Sql(IQueryable source, params (string field, UpdateType updateType, object value)[] values)
        {
            var sql = GetUpdateWhereSql(source, values);

            return ExecuteSql(sql.sql, sql.paramters.ToArray());
        }
        public async Task<int> UpdateWhere_SqlAsync(IQueryable source, params (string field, UpdateType updateType, object value)[] values)
        {
            var sql = GetUpdateWhereSql(source, values);

            return await ExecuteSqlAsync(sql.sql, sql.paramters.ToArray());
        }

        #endregion

        #region 查询数据

        public T GetEntity<T>(params object[] keyValue) where T : class, new()
        {
            var obj = _db.Set<T>().Find(keyValue);
            if (!obj.IsNullOrEmpty())
                _db.Entry(obj).State = EntityState.Detached;

            return obj;
        }
        public async Task<T> GetEntityAsync<T>(params object[] keyValue) where T : class, new()
        {
            var obj = await _db.Set<T>().FindAsync(keyValue);
            if (!obj.IsNullOrEmpty())
                _db.Entry(obj).State = EntityState.Detached;

            return obj;
        }
        public List<T> GetList<T>() where T : class, new()
        {
            return GetIQueryable<T>().ToList();
        }
        public async Task<List<T>> GetListAsync<T>() where T : class, new()
        {
            return await GetIQueryable<T>().ToListAsync();
        }
        public List<object> GetList(Type type)
        {
            return GetIQueryable(type).CastToList<object>();
        }
        public async Task<List<object>> GetListAsync(Type type)
        {
            return await GetIQueryable(type).Cast<object>().ToListAsync();
        }
        public IQueryable<T> GetIQueryable<T>() where T : class, new()
        {
            return GetIQueryable(typeof(T)) as IQueryable<T>;
        }
        public IQueryable GetIQueryable(Type type)
        {
            return _db.GetIQueryable(type);
        }
        public DataTable GetDataTableWithSql(string sql, params (string paramterName, object value)[] parameters)
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

                    if (parameters != null && parameters.Count() > 0)
                        cmd.Parameters.AddRange(CreateDbParamters(parameters.ToList()).ToArray());

                    DataTable table = new DataTable();
                    var reader = cmd.ExecuteReader();
                    table.Load(reader);

                    return table;
                }
            }
        }
        public async Task<DataTable> GetDataTableWithSqlAsync(string sql, params (string paramterName, object value)[] parameters)
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

                    if (parameters != null && parameters.Count() > 0)
                        cmd.Parameters.AddRange(CreateDbParamters(parameters.ToList()).ToArray());

                    DataTable table = new DataTable();
                    var reader = await cmd.ExecuteReaderAsync();
                    table.Load(reader);

                    return table;
                }
            }
        }
        public List<T> GetListBySql<T>(string sqlStr, params (string paramterName, object value)[] parameters) where T : class, new()
        {
            return _db.Set<T>().FromSql(sqlStr, CreateDbParamters(parameters.ToList()).ToArray()).AsNoTracking().ToList();
        }
        public async Task<List<T>> GetListBySqlAsync<T>(string sqlStr, params (string paramterName, object value)[] parameters) where T : class, new()
        {
            return await _db.Set<T>().FromSql(sqlStr, CreateDbParamters(parameters.ToList()).ToArray()).AsNoTracking().ToListAsync();
        }

        #endregion

        #region 执行Sql语句

        public int ExecuteSql(string sql, params (string paramterName, object paramterValue)[] paramters)
        {
            return _db.Database.ExecuteSqlCommand(sql, CreateDbParamters(paramters.ToList()).ToArray());
        }
        public async Task<int> ExecuteSqlAsync(string sql, params (string paramterName, object paramterValue)[] paramters)
        {
            return await _db.Database.ExecuteSqlCommandAsync(sql, CreateDbParamters(paramters.ToList()).ToArray());
        }

        #endregion

        #region Dispose

        private bool _disposed = false;
        public virtual void Dispose()
        {
            if (_disposed)
                return;

            _disposed = true;
            DisposeTransaction();
            _db.Dispose();
        }

        #endregion
    }
}
