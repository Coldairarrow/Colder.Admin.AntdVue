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

        public void Delete(Type type, string key)
        {
            Delete(type, new List<string> { key });
        }

        public void Delete(Type type, List<string> keys)
        {
            if (NeedLogicDelete(type))
                UpdateWhere_Sql(type, $"@0.Contains({Id})", new object[] { keys }, (Deleted, true));
            else
                _db.Delete(type, keys);
        }

        public void Delete(List<object> entities)
        {
            if (entities?.Count > 0)
            {
                var keys = entities.Select(x => x.GetPropertyValue(Id) as string).ToList();
                Delete(entities[0].GetType(), keys);
            }
        }

        public void Delete<T>(string key) where T : class, new()
        {
            Delete<T>(new List<string> { key });
        }

        public void Delete<T>(List<string> keys) where T : class, new()
        {
            Delete(typeof(T), keys);
        }

        public void Delete<T>(T entity) where T : class, new()
        {
            Delete(new List<T> { entity });
        }

        public void Delete<T>(List<T> entities) where T : class, new()
        {
            var objList = entities.Select(x => x as object).ToList();

            Delete(objList);
        }

        public void Delete<T>(Expression<Func<T, bool>> condition) where T : class, new()
        {
            Delete_Sql(condition);
        }

        public void DeleteAll(Type type)
        {
            if (NeedLogicDelete(type))
                UpdateWhere_Sql(type, "true", null, (Deleted, true));
            else
                _db.DeleteAll(type);
        }

        public void DeleteAll<T>() where T : class, new()
        {
            DeleteAll(typeof(T));
        }

        public int Delete_Sql<T>(Expression<Func<T, bool>> where) where T : class, new()
        {
            if (NeedLogicDelete(typeof(T)))
                return UpdateWhere_Sql(where, (Deleted, true));
            else
                return _db.Delete_Sql(where);
        }

        public int Delete_Sql(Type entityType, string where, params object[] paramters)
        {
            if (NeedLogicDelete(entityType))
                return UpdateWhere_Sql(entityType, where, paramters, (Deleted, true));
            else
                return _db.Delete_Sql(entityType, where, paramters);
        }

        #endregion

        #region 忽略

        public Action<string> HandleSqlLog { set => _db.HandleSqlLog = value; }

        public string ConnectionString => _db.ConnectionString;

        public DatabaseType DbType => _db.DbType;

        private IRepository _db { get; }

        public void BulkInsert<T>(List<T> entities) where T : class, new()
        {
            _db.BulkInsert(entities);
        }

        public void CommitDb()
        {
            _db.CommitDb();
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public int ExecuteSql(string sql)
        {
            return _db.ExecuteSql(sql);
        }

        public int ExecuteSql(string sql, List<DbParameter> parameters)
        {
            return _db.ExecuteSql(sql, parameters);
        }

        public DataTable GetDataTableWithSql(string sql)
        {
            return _db.GetDataTableWithSql(sql);
        }

        public DataTable GetDataTableWithSql(string sql, List<DbParameter> parameters)
        {
            return _db.GetDataTableWithSql(sql, parameters);
        }

        public T GetEntity<T>(params object[] keyValue) where T : class, new()
        {
            return _db.GetEntity<T>(keyValue);
        }

        public DbTransaction GetTransaction()
        {
            return _db.GetTransaction();
        }

        public void Insert(List<object> entities)
        {
            _db.Insert(entities);
        }

        public void Insert<T>(T entity) where T : class, new()
        {
            _db.Insert(entity);
        }

        public void Insert<T>(List<T> entities) where T : class, new()
        {
            _db.Insert(entities);
        }

        public void Update(List<object> entities)
        {
            _db.Update(entities);
        }

        public void Update<T>(T entity) where T : class, new()
        {
            _db.Update(entity);
        }

        public void Update<T>(List<T> entities) where T : class, new()
        {
            _db.Update(entities);
        }

        public void UpdateAny(List<object> entities, List<string> properties)
        {
            _db.UpdateAny(entities, properties);
        }

        public void UpdateAny<T>(T entity, List<string> properties) where T : class, new()
        {
            _db.UpdateAny(entity, properties);
        }

        public void UpdateAny<T>(List<T> entities, List<string> properties) where T : class, new()
        {
            _db.UpdateAny(entities, properties);
        }

        public void UpdateWhere<T>(Expression<Func<T, bool>> whereExpre, Action<T> set) where T : class, new()
        {
            _db.UpdateWhere(whereExpre, set);
        }

        public int UpdateWhere_Sql<T>(Expression<Func<T, bool>> where, params (string field, object value)[] values) where T : class, new()
        {
            return _db.UpdateWhere_Sql(where, values);
        }

        public void UseTransaction(DbTransaction transaction)
        {
            _db.UseTransaction(transaction);
        }

        public List<T> GetListBySql<T>(string sqlStr) where T : class, new()
        {
            return _db.GetListBySql<T>(sqlStr);
        }

        public List<T> GetListBySql<T>(string sqlStr, List<DbParameter> parameters) where T : class, new()
        {
            return _db.GetListBySql<T>(sqlStr, parameters);
        }

        public int UpdateWhere_Sql(Type entityType, string where, object[] paramters, params (string field, object value)[] values)
        {
            return _db.UpdateWhere_Sql(entityType, where, paramters, values);
        }

        public (bool Success, Exception ex) RunTransaction(Action action, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            return _db.RunTransaction(action, isolationLevel);
        }

        #endregion
    }
}
