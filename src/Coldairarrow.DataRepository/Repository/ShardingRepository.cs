using Coldairarrow.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Coldairarrow.DataRepository
{
    internal class ShardingRepository : IShardingRepository, IInternalTransaction
    {
        #region 构造函数

        public ShardingRepository(IRepository db)
        {
            _db = db;
        }

        #endregion

        #region 私有成员

        private IRepository _db { get; }
        private Type MapTable(string targetTableName)
        {
            return DbModelFactory.GetEntityType(targetTableName);
        }
        private List<(object targetObj, IRepository targetDb)> GetMapConfigs<T>(List<T> entities)
        {
            var configs = entities.Select(x => ShardingConfig.Instance.GetTheWriteTable(typeof(T).Name, x)).ToList();
            Dictionary<string, IRepository> targetDbs = new Dictionary<string, IRepository>();
            configs.GroupBy(x => GetDbId(x.conString, x.dbType)).Select(x => x.Key).ForEach(aDb =>
            {
                var theConfig = configs.Where(x => GetDbId(x.conString, x.dbType) == aDb).FirstOrDefault();
                targetDbs[aDb] = DbFactory.GetRepository(theConfig.conString, theConfig.dbType);
            });
            List<(object targetObj, IRepository targetDb)> resList = new List<(object targetObj, IRepository targetDb)>();
            entities.ForEach(aEntity =>
            {
                (string tableName, string conString, DatabaseType dbType) = ShardingConfig.Instance.GetTheWriteTable(typeof(T).Name, aEntity);
                var targetDb = targetDbs[GetDbId(conString, dbType)];
                var targetObj = aEntity.ChangeType(MapTable(tableName));
                resList.Add((targetObj, targetDb));
            });

            return resList;

            string GetDbId(string conString, DatabaseType dbType)
            {
                return $"{conString}{dbType.ToString()}";
            }
        }
        private int WriteTable<T>(List<T> entities, Func<object, IRepository, int> accessData)
        {
            var mapConfigs = GetMapConfigs(entities);

            var dbs = mapConfigs.Select(x => x.targetDb).Distinct().ToArray();
            if (!_openedTransaction)
            {
                using (var transaction = DistributedTransactionFactory.GetDistributedTransaction(dbs))
                {
                    int count = 0;
                    var (Success, ex) = transaction.RunTransaction(() =>
                    {
                        count = Run();
                    });
                    if (!Success)
                        throw ex;
                    else
                        return count;
                }
            }
            else
            {
                _transaction.AddRepository(dbs);
                return Run();
            }

            int Run()
            {
                int count = 0;

                mapConfigs.ForEach(aConfig =>
                {
                    count += accessData(aConfig.targetObj, aConfig.targetDb);
                });

                dbs.ForEach(x => x.Dispose());
                return count;
            }
        }
        private bool _openedTransaction { get; set; } = false;
        private DistributedTransaction _transaction { get; set; }

        #endregion

        #region 外部接口
        public int Insert<T>(T entity) where T : class, new()
        {
            return Insert(new List<T> { entity });
        }
        public int Insert<T>(List<T> entities) where T : class, new()
        {
            return WriteTable(entities, (targetObj, targetDb) => targetDb.Insert(targetObj));
        }
        public int DeleteAll<T>() where T : class, new()
        {
            var configs = ShardingConfig.Instance.GetAllWriteTables(typeof(T).Name);
            var allDbs = configs.Select(x => new
            {
                Db = DbFactory.GetRepository(x.conString, x.dbType),
                TargetType = MapTable(x.tableName)
            }).ToList();

            var dbs = allDbs.Select(x => x.Db).ToArray();

            if (!_openedTransaction)
            {
                int count = 0;
                using (var transaction = DistributedTransactionFactory.GetDistributedTransaction(dbs))
                {
                    var (Success, ex) = transaction.RunTransaction(() =>
                    {
                        count = Run();
                    });
                    if (!Success)
                        throw ex;
                    else
                        return count;
                }
            }
            else
            {
                _transaction.AddRepository(dbs);
                return Run();
            }

            int Run()
            {
                int count = 0;

                allDbs.ForEach(x =>
                {
                    count += x.Db.DeleteAll(x.TargetType);
                });

                return count;
            }
        }
        public int Delete<T>(T entity) where T : class, new()
        {
            return Delete(new List<T> { entity });
        }
        public int Delete<T>(List<T> entities) where T : class, new()
        {
            return WriteTable(entities, (targetObj, targetDb) => targetDb.Delete(targetObj));
        }
        public int Delete<T>(Expression<Func<T, bool>> condition) where T : class, new()
        {
            var deleteList = GetIShardingQueryable<T>().Where(condition).ToList();

            return Delete(deleteList);
        }
        public int Update<T>(T entity) where T : class, new()
        {
            return Update(new List<T> { entity });
        }
        public int Update<T>(List<T> entities) where T : class, new()
        {
            return WriteTable(entities, (targetObj, targetDb) => targetDb.Update(targetObj));
        }
        public int UpdateAny<T>(T entity, List<string> properties) where T : class, new()
        {
            return UpdateAny(new List<T> { entity }, properties);
        }
        public int UpdateAny<T>(List<T> entities, List<string> properties) where T : class, new()
        {
            return WriteTable(entities, (targetObj, targetDb) => targetDb.UpdateAny(targetObj, properties));
        }
        public int UpdateWhere<T>(Expression<Func<T, bool>> whereExpre, Action<T> set) where T : class, new()
        {
            var list = GetIShardingQueryable<T>().Where(whereExpre).ToList();
            list.ForEach(aData => set(aData));
            return Update(list);
        }
        public IShardingQueryable<T> GetIShardingQueryable<T>() where T : class, new()
        {
            return new ShardingQueryable<T>(_db.GetIQueryable<T>(), _transaction);
        }
        public List<T> GetList<T>() where T : class, new()
        {
            return GetIShardingQueryable<T>().ToList();
        }

        #endregion

        #region 事物处理

        public (bool Success, Exception ex) RunTransaction(Action action, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            bool isOK = true;
            Exception resEx = null;
            try
            {
                BeginTransaction(isolationLevel);

                action();

                CommitTransaction();
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                isOK = false;
                resEx = ex;
            }
            finally
            {
                DisposeTransaction();
            }

            return (isOK, resEx);
        }

        public void BeginTransaction(IsolationLevel isolationLevel)
        {
            _openedTransaction = true;
            _transaction = new DistributedTransaction();
            _transaction.BeginTransaction(isolationLevel);
        }

        public void CommitTransaction()
        {
            _transaction.CommitTransaction();
        }

        public void RollbackTransaction()
        {
            _transaction.RollbackTransaction();
        }

        #endregion

        #region Dispose

        private bool _disposed { get; set; } = false;
        public virtual void Dispose()
        {
            if (_disposed)
                return;

            _disposed = true;
            _transaction?.Dispose();
        }

        public Task<int> InsertAsync<T>(T entity) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertAsync<T>(List<T> entities) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAllAsync<T>() where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync<T>(T entity) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync<T>(List<T> entities) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync<T>(Expression<Func<T, bool>> condition) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync<T>(T entity) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync<T>(List<T> entities) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAnyAsync<T>(T entity, List<string> properties) where T : class, new()
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

        public void DisposeTransaction()
        {
            _openedTransaction = false;
            _transaction.DisposeTransaction();
            ((IInternalTransaction)_transaction).DisposeTransaction();
        }

        #endregion
    }
}
