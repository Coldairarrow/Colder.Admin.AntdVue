using Coldairarrow.Util;
using System;
using System.Collections.Concurrent;
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
        private List<(string targetTableName, IRepository targetDb)> GetTargetDb(List<(string tableName, string conString, DatabaseType dbType)> configs)
        {
            SetRepositories(configs.Select(x => (x.conString, x.dbType)).ToList());
            return configs.Select(x => (x.tableName, _repositories[GetDbId(x.conString, x.dbType)])).ToList();
        }
        private List<(object targetObj, IRepository targetDb)> GetMapConfigs<T>(List<T> entities)
        {
            var configs = entities.Select(x => ShardingConfig.Instance.GetTheWriteTable(typeof(T).Name, x)).ToList();
            var targetDbs = GetTargetDb(configs);
            List<(object targetObj, IRepository targetDb)> resList = new List<(object targetObj, IRepository targetDb)>();
            entities.ForEach(aEntity =>
            {
                (string tableName, string conString, DatabaseType dbType) = ShardingConfig.Instance.GetTheWriteTable(typeof(T).Name, aEntity);
                var targetDb = _repositories[GetDbId(conString, dbType)];
                var targetObj = aEntity.ChangeType(MapTable(tableName));
                resList.Add((targetObj, targetDb));
            });

            return resList;
        }
        private string GetDbId(string conString, DatabaseType dbType)
        {
            return $"{conString}{dbType.ToString()}";
        }
        private void SetRepositories(List<(string conString, DatabaseType dbType)> physicDbs)
        {
            physicDbs.ForEach(aConfig =>
            {
                var dbId = GetDbId(aConfig.conString, aConfig.dbType);
                if (!_repositories.ContainsKey(dbId))
                    _repositories[dbId] = DbFactory.GetRepository(aConfig.conString, aConfig.dbType);
            });
        }
        private int PackAccessData(Func<int> access)
        {
            var dbs = _repositories.Values.ToArray();
            int count = 0;
            if (!_openedTransaction)
            {
                using (var transaction = DistributedTransactionFactory.GetDistributedTransaction(dbs))
                {
                    var (Success, ex) = transaction.RunTransaction(() =>
                    {
                        count = access();
                    });
                    if (!Success)
                        throw ex;
                }
                ClearRepositories();
                return count;
            }
            else
            {
                _transaction.AddRepository(dbs);
                count = access();
            }

            return count;
        }
        private async Task<int> PackAccessDataAsync(Func<Task<int>> access)
        {
            var dbs = _repositories.Values.ToArray();

            int count = 0;
            if (!_openedTransaction)
            {
                using (var transaction = DistributedTransactionFactory.GetDistributedTransaction(dbs))
                {
                    var (Success, ex) = transaction.RunTransaction(async () =>
                    {
                        count = await access();
                    });
                    if (!Success)
                        throw ex;
                }
                ClearRepositories();
                return count;
            }
            else
            {
                _transaction.AddRepository(dbs);
                count = await access();
            }

            return count;
        }
        private int WriteTable<T>(List<T> entities, Func<object, IRepository, int> accessData)
        {
            var mapConfigs = GetMapConfigs(entities);

            return PackAccessData(() =>
            {
                int tmpCount = 0;

                mapConfigs.ForEach(aConfig =>
                {
                    tmpCount += accessData(aConfig.targetObj, aConfig.targetDb);
                });

                return tmpCount;
            });
        }
        private async Task<int> WriteTableAsync<T>(List<T> entities, Func<object, IRepository, Task<int>> accessDataAsync)
        {
            var mapConfigs = GetMapConfigs(entities);

            return await PackAccessDataAsync(async () =>
            {
                var tasks = mapConfigs.Select(aConfig => accessDataAsync(aConfig.targetObj, aConfig.targetDb));
                return (await Task.WhenAll(tasks.ToArray())).Sum();
            });
        }
        private bool _openedTransaction { get; set; } = false;
        private DistributedTransaction _transaction { get; set; }
        private ConcurrentDictionary<string, IRepository> _repositories { get; }
            = new ConcurrentDictionary<string, IRepository>();
        private void ClearRepositories()
        {
            _repositories.ForEach(x => x.Value.Dispose());
            _repositories.Clear();
        }

        #endregion

        #region 外部接口
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
            return WriteTable(entities, (targetObj, targetDb) => targetDb.Insert(targetObj));
        }
        public async Task<int> InsertAsync<T>(List<T> entities) where T : class, new()
        {
            return await WriteTableAsync(entities, (targetObj, targetDb) => targetDb.InsertAsync(targetObj));
        }
        public int DeleteAll<T>() where T : class, new()
        {
            var configs = ShardingConfig.Instance.GetAllWriteTables(typeof(T).Name);
            var targetDbs = GetTargetDb(configs);
            return PackAccessData(() =>
            {
                int count = 0;

                targetDbs.ForEach(x =>
                {
                    count += x.targetDb.DeleteAll(MapTable(x.targetTableName));
                });

                return count;
            });
        }
        public async Task<int> DeleteAllAsync<T>() where T : class, new()
        {
            var configs = ShardingConfig.Instance.GetAllWriteTables(typeof(T).Name);
            var targetDbs = GetTargetDb(configs);
            return await PackAccessDataAsync(async () =>
            {
                var tasks = targetDbs.Select(x => x.targetDb.DeleteAllAsync(MapTable(x.targetTableName)));
                return (await Task.WhenAll(tasks.ToArray())).Sum();
            });
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
            return WriteTable(entities, (targetObj, targetDb) => targetDb.Delete(targetObj));
        }
        public async Task<int> DeleteAsync<T>(List<T> entities) where T : class, new()
        {
            return await WriteTableAsync(entities, (targetObj, targetDb) => targetDb.DeleteAsync(targetObj));
        }
        public int Delete<T>(Expression<Func<T, bool>> condition) where T : class, new()
        {
            var deleteList = GetIShardingQueryable<T>().Where(condition).ToList();

            return Delete(deleteList);
        }
        public async Task<int> DeleteAsync<T>(Expression<Func<T, bool>> condition) where T : class, new()
        {
            var deleteList = GetIShardingQueryable<T>().Where(condition).ToList();

            return await DeleteAsync(deleteList);
        }
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
            return WriteTable(entities, (targetObj, targetDb) => targetDb.Update(targetObj));
        }
        public async Task<int> UpdateAsync<T>(List<T> entities) where T : class, new()
        {
            return await WriteTableAsync(entities, (targetObj, targetDb) => targetDb.UpdateAsync(targetObj));
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
            return WriteTable(entities, (targetObj, targetDb) => targetDb.UpdateAny(targetObj, properties));
        }
        public async Task<int> UpdateAnyAsync<T>(List<T> entities, List<string> properties) where T : class, new()
        {
            return await WriteTableAsync(entities, (targetObj, targetDb) => targetDb.UpdateAnyAsync(targetObj, properties));
        }
        public int UpdateWhere<T>(Expression<Func<T, bool>> whereExpre, Action<T> set) where T : class, new()
        {
            var list = GetIShardingQueryable<T>().Where(whereExpre).ToList();
            list.ForEach(aData => set(aData));
            return Update(list);
        }
        public async Task<int> UpdateWhereAsync<T>(Expression<Func<T, bool>> whereExpre, Action<T> set) where T : class, new()
        {
            var list = GetIShardingQueryable<T>().Where(whereExpre).ToList();
            list.ForEach(aData => set(aData));
            return await UpdateAsync(list);
        }
        public IShardingQueryable<T> GetIShardingQueryable<T>() where T : class, new()
        {
            return new ShardingQueryable<T>(_db.GetIQueryable<T>(), _transaction);
        }
        public List<T> GetList<T>() where T : class, new()
        {
            return GetIShardingQueryable<T>().ToList();
        }
        public async Task<List<T>> GetListAsync<T>() where T : class, new()
        {
            return await GetIShardingQueryable<T>().ToListAsync();
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
        public async Task<(bool Success, Exception ex)> RunTransactionAsync(Func<Task> action, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            bool isOK = true;
            Exception resEx = null;
            try
            {
                await BeginTransactionAsync(isolationLevel);

                await action();

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
        public async Task BeginTransactionAsync(IsolationLevel isolationLevel)
        {
            _openedTransaction = true;
            _transaction = new DistributedTransaction();
            await _transaction.BeginTransactionAsync(isolationLevel);
        }
        public void CommitTransaction()
        {
            _transaction.CommitTransaction();
        }
        public void RollbackTransaction()
        {
            _transaction.RollbackTransaction();
        }
        public void DisposeTransaction()
        {
            _openedTransaction = false;
            _transaction.DisposeTransaction();
            ClearRepositories();
        }

        #endregion

        #region Dispose

        private bool _disposed = false;
        public virtual void Dispose()
        {
            if (_disposed)
                return;

            _disposed = true;
            _transaction?.Dispose();
            ClearRepositories();
        }

        #endregion
    }
}
