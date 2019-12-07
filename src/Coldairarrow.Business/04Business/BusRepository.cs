using Coldairarrow.DataRepository;
using Coldairarrow.Util;
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
    /// 业务仓储类,全局控制业务相关操作
    /// 软删除:查询:获取Deleted=false,删除:更新获取Deleted=true
    /// 其它:按照具体业务修改
    /// </summary>
    public class BusRepository : IRepository
    {
        public BusRepository(IRepository db)
        {
            _db = db;
        }
        const string Deleted = "Deleted";
        const string Id = "Id";
        bool NeedLogicDelete(Type entityType)
        {
            return GlobalSwitch.DeleteMode == DeleteMode.Logic
                && entityType.GetProperties().Any(x => x.Name == Deleted);
        }
        private IRepository _db { get; }
        public Action<string> HandleSqlLog { set => _db.HandleSqlLog = value; }

        public string ConnectionString => _db.ConnectionString;

        public DatabaseType DbType => _db.DbType;

        #region 重写

        public IQueryable<T> GetIQueryable<T>() where T : class, new()
        {
            return GetIQueryable(typeof(T)) as IQueryable<T>;
        }

        public IQueryable GetIQueryable(Type type)
        {
            var q = _db.GetIQueryable(type);
            if (NeedLogicDelete(type))
            {
                q = q.Where($"{Deleted} = @0", false);
            }

            return q;
        }

        public List<object> GetList(Type type)
        {
            return GetIQueryable(type).CastToList<object>();
        }

        public List<T> GetList<T>() where T : class, new()
        {
            return GetIQueryable<T>().ToList();
        }

        public int Delete(Type type, string key)
        {
            return Delete(type, new List<string> { key });
        }

        public int Delete(Type type, List<string> keys)
        {
            if (NeedLogicDelete(type))
                return UpdateWhere_Sql(type, $"@0.Contains({Id})", new object[] { keys }, (Deleted, UpdateType.Equal, true));
            else
                return _db.Delete(type, keys);
        }

        public int Delete<T>(string key) where T : class, new()
        {
            return Delete<T>(new List<string> { key });
        }

        public int Delete<T>(List<string> keys) where T : class, new()
        {
            return Delete(typeof(T), keys);
        }

        public int Delete<T>(T entity) where T : class, new()
        {
            return Delete(new List<T> { entity });
        }

        public int Delete<T>(List<T> entities) where T : class, new()
        {
            if (entities?.Count > 0)
            {
                var keys = entities.Select(x => x.GetPropertyValue(Id) as string).ToList();
                return Delete(typeof(T), keys);
            }
            else
                return 0;
        }

        public int Delete<T>(Expression<Func<T, bool>> condition) where T : class, new()
        {
            return Delete_Sql(condition);
        }

        public int DeleteAll(Type type)
        {
            if (NeedLogicDelete(type))
                return UpdateWhere_Sql(type, "true", null, (Deleted, UpdateType.Equal, true));
            else
                return _db.DeleteAll(type);
        }

        public int DeleteAll<T>() where T : class, new()
        {
            return DeleteAll(typeof(T));
        }

        public int Delete_Sql<T>(Expression<Func<T, bool>> where) where T : class, new()
        {
            if (NeedLogicDelete(typeof(T)))
                return UpdateWhere_Sql(where, (Deleted, UpdateType.Equal, true));
            else
                return _db.Delete_Sql(where);
        }

        public int Delete_Sql(Type entityType, string where, params object[] paramters)
        {
            if (NeedLogicDelete(entityType))
                return UpdateWhere_Sql(entityType, where, paramters, (Deleted, UpdateType.Equal, true));
            else
                return _db.Delete_Sql(entityType, where, paramters);
        }

        #endregion

        #region 忽略

        public void BulkInsert<T>(List<T> entities) where T : class, new()
        {
            _db.BulkInsert(entities);
        }

        public Task<int> DeleteAllAsync(Type type)
        {
            return _db.DeleteAllAsync(type);
        }

        public Task<int> DeleteAsync(Type type, string key)
        {
            return _db.DeleteAsync(type, key);
        }

        public Task<int> DeleteAsync(Type type, List<string> keys)
        {
            return _db.DeleteAsync(type, keys);
        }

        public Task<int> DeleteAsync<T>(string key) where T : class, new()
        {
            return _db.DeleteAsync<T>(key);
        }

        public Task<int> DeleteAsync<T>(List<string> keys) where T : class, new()
        {
            return _db.DeleteAsync<T>(keys);
        }

        public Task<int> Delete_SqlAsync<T>(Expression<Func<T, bool>> where) where T : class, new()
        {
            return _db.Delete_SqlAsync(where);
        }

        public Task<int> Delete_SqlAsync(Type entityType, string where, params object[] paramters)
        {
            return _db.Delete_SqlAsync(entityType, where, paramters);
        }

        public int UpdateWhere_Sql<T>(Expression<Func<T, bool>> where, params (string field, UpdateType updateType, object value)[] values) where T : class, new()
        {
            return _db.UpdateWhere_Sql(where, values);
        }

        public Task<int> UpdateWhere_SqlAsync<T>(Expression<Func<T, bool>> where, params (string field, UpdateType updateType, object value)[] values) where T : class, new()
        {
            return _db.UpdateWhere_SqlAsync(where, values);
        }

        public int UpdateWhere_Sql(Type entityType, string where, object[] paramters, params (string field, UpdateType updateType, object value)[] values)
        {
            return _db.UpdateWhere_Sql(entityType, where, paramters, values);
        }

        public Task<int> UpdateWhere_SqlAsync(Type entityType, string where, object[] paramters, params (string field, UpdateType updateType, object value)[] values)
        {
            return _db.UpdateWhere_SqlAsync(entityType, where, paramters, values);
        }

        public T GetEntity<T>(params object[] keyValue) where T : class, new()
        {
            return _db.GetEntity<T>(keyValue);
        }

        public Task<T> GetEntityAsync<T>(params object[] keyValue) where T : class, new()
        {
            return _db.GetEntityAsync<T>(keyValue);
        }

        public Task<List<object>> GetListAsync(Type type)
        {
            return _db.GetListAsync(type);
        }

        public DataTable GetDataTableWithSql(string sql, params (string paramterName, object value)[] parameters)
        {
            return _db.GetDataTableWithSql(sql, parameters);
        }

        public Task<DataTable> GetDataTableWithSqlAsync(string sql, params (string paramterName, object value)[] parameters)
        {
            return _db.GetDataTableWithSqlAsync(sql, parameters);
        }

        public List<T> GetListBySql<T>(string sqlStr, params (string paramterName, object value)[] parameters) where T : class, new()
        {
            return _db.GetListBySql<T>(sqlStr, parameters);
        }

        public Task<List<T>> GetListBySqlAsync<T>(string sqlStr, params (string paramterName, object value)[] parameters) where T : class, new()
        {
            return _db.GetListBySqlAsync<T>(sqlStr, parameters);
        }

        public int ExecuteSql(string sql, params (string paramterName, object paramterValue)[] paramters)
        {
            return _db.ExecuteSql(sql, paramters);
        }

        public Task<int> ExecuteSqlAsync(string sql, params (string paramterName, object paramterValue)[] paramters)
        {
            return _db.ExecuteSqlAsync(sql, paramters);
        }

        public int Insert<T>(T entity) where T : class, new()
        {
            return _db.Insert(entity);
        }

        public Task<int> InsertAsync<T>(T entity) where T : class, new()
        {
            return _db.InsertAsync(entity);
        }

        public int Insert<T>(List<T> entities) where T : class, new()
        {
            return _db.Insert(entities);
        }

        public Task<int> InsertAsync<T>(List<T> entities) where T : class, new()
        {
            return _db.InsertAsync(entities);
        }

        public Task<int> DeleteAllAsync<T>() where T : class, new()
        {
            return _db.DeleteAllAsync<T>();
        }

        public Task<int> DeleteAsync<T>(T entity) where T : class, new()
        {
            return _db.DeleteAsync(entity);
        }

        public Task<int> DeleteAsync<T>(List<T> entities) where T : class, new()
        {
            return _db.DeleteAsync(entities);
        }

        public Task<int> DeleteAsync<T>(Expression<Func<T, bool>> condition) where T : class, new()
        {
            return _db.DeleteAsync(condition);
        }

        public int Update<T>(T entity) where T : class, new()
        {
            return _db.Update(entity);
        }

        public Task<int> UpdateAsync<T>(T entity) where T : class, new()
        {
            return _db.UpdateAsync(entity);
        }

        public int Update<T>(List<T> entities) where T : class, new()
        {
            return _db.Update(entities);
        }

        public Task<int> UpdateAsync<T>(List<T> entities) where T : class, new()
        {
            return _db.UpdateAsync(entities);
        }

        public int UpdateAny<T>(T entity, List<string> properties) where T : class, new()
        {
            return _db.UpdateAny(entity, properties);
        }

        public Task<int> UpdateAnyAsync<T>(T entity, List<string> properties) where T : class, new()
        {
            return _db.UpdateAnyAsync(entity, properties);
        }

        public int UpdateAny<T>(List<T> entities, List<string> properties) where T : class, new()
        {
            return _db.UpdateAny(entities, properties);
        }

        public Task<int> UpdateAnyAsync<T>(List<T> entities, List<string> properties) where T : class, new()
        {
            return _db.UpdateAnyAsync(entities, properties);
        }

        public int UpdateWhere<T>(Expression<Func<T, bool>> whereExpre, Action<T> set) where T : class, new()
        {
            return _db.UpdateWhere(whereExpre, set);
        }

        public Task<int> UpdateWhereAsync<T>(Expression<Func<T, bool>> whereExpre, Action<T> set) where T : class, new()
        {
            return _db.UpdateWhereAsync(whereExpre, set);
        }

        public Task<List<T>> GetListAsync<T>() where T : class, new()
        {
            return _db.GetListAsync<T>();
        }

        public (bool Success, Exception ex) RunTransaction(Action action, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            return _db.RunTransaction(action, isolationLevel);
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        #endregion
    }
}
