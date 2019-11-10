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
        private Type MapTable(Type absTable, string targetTableName)
        {
            return ShardingHelper.MapTable(absTable, targetTableName);
        }
        private List<(object targetObj, IRepository targetDb)> GetMapConfigs<T>(List<T> entities)
        {
            List<(object targetObj, IRepository targetDb)> resList = new List<(object targetObj, IRepository targetDb)>();
            entities.ForEach(aEntity =>
            {
                (string tableName, string conString, DatabaseType dbType) = ShardingConfig.Instance.GetTheWriteTable(typeof(T).Name, aEntity);

                resList.Add((aEntity.ChangeType(MapTable(typeof(T), tableName)), DbFactory.GetRepository(conString, dbType)));
            });

            return resList;
        }
        private void WriteTable<T>(List<T> entities, Action<object, IRepository> accessData)
        {
            var mapConfigs = GetMapConfigs(entities);

            var dbs = mapConfigs.Select(x => x.targetDb).ToArray();
            if (!_openedTransaction)
            {
                DistributedTransaction transaction = new DistributedTransaction(dbs);

                var (Success, ex) = transaction.RunTransaction(Run);
                if (!Success)
                    throw ex;
            }
            else
            {
                _transaction.AddRepository(dbs);
                Run();
            }

            void Run()
            {
                mapConfigs.ForEach(aConfig =>
                {
                    accessData(aConfig.targetObj, aConfig.targetDb);
                });
            }
        }
        private bool _openedTransaction { get; set; } = false;
        private DistributedTransaction _transaction { get; set; }

        #endregion

        #region 外部接口

        /// <summary>
        /// 添加单条记录
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <param name="entity">实体对象</param>
        public void Insert<T>(T entity) where T : class, new()
        {
            Insert(new List<T> { entity });
        }

        /// <summary>
        /// 添加多条记录
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <param name="entities">实体对象集合</param>
        public void Insert<T>(List<T> entities) where T : class, new()
        {
            WriteTable(entities, (targetObj, targetDb) => targetDb.Insert(targetObj));
        }

        /// <summary>
        /// 删除所有记录
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        public void DeleteAll<T>() where T : class, new()
        {
            var configs = ShardingConfig.Instance.GetAllWriteTables(typeof(T).Name);
            var allDbs = configs.Select(x => new
            {
                Db = DbFactory.GetRepository(x.conString, x.dbType),
                TargetType = MapTable(typeof(T), x.tableName)
            }).ToList();

            var dbs = allDbs.Select(x => x.Db).ToArray();

            if (!_openedTransaction)
            {
                var transaction = DistributedTransactionFactory.GetDistributedTransaction(dbs);
                var (Success, ex) = transaction.RunTransaction(Run);
                if (!Success)
                    throw ex;
            }
            else
            {
                _transaction.AddRepository(dbs);
                Run();
            }

            void Run()
            {
                allDbs.ForEach(x =>
                {
                    x.Db.DeleteAll(x.TargetType);
                });
            }
        }

        /// <summary>
        /// 删除单条记录
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <param name="entity">实体对象</param>
        public void Delete<T>(T entity) where T : class, new()
        {
            Delete(new List<T> { entity });
        }

        /// <summary>
        /// 删除多条记录
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <param name="entities">实体对象集合</param>
        public void Delete<T>(List<T> entities) where T : class, new()
        {
            WriteTable(entities, (targetObj, targetDb) => targetDb.Delete(targetObj));
        }

        /// <summary>
        /// 按条件删除记录
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <param name="condition">筛选条件</param>
        public void Delete<T>(Expression<Func<T, bool>> condition) where T : class, new()
        {
            var deleteList = GetIShardingQueryable<T>().Where(condition).ToList();

            Delete(deleteList);
        }

        /// <summary>
        /// 更新单条记录
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <param name="entity">实体对象</param>
        public void Update<T>(T entity) where T : class, new()
        {
            Update(new List<T> { entity });
        }

        /// <summary>
        /// 更新多条记录
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <param name="entities">实体对象集合</param>
        public void Update<T>(List<T> entities) where T : class, new()
        {
            WriteTable(entities, (targetObj, targetDb) => targetDb.Update(targetObj));
        }

        /// <summary>
        /// 更新单条记录的某些属性
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <param name="entity">实体对象</param>
        /// <param name="properties">属性</param>
        public void UpdateAny<T>(T entity, List<string> properties) where T : class, new()
        {
            UpdateAny(new List<T> { entity }, properties);
        }

        /// <summary>
        /// 更新多条记录的某些属性
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <param name="entities">实体对象集合</param>
        /// <param name="properties">属性</param>
        public void UpdateAny<T>(List<T> entities, List<string> properties) where T : class, new()
        {
            WriteTable(entities, (targetObj, targetDb) => targetDb.UpdateAny(targetObj, properties));
        }

        /// <summary>
        /// 按照条件更新记录
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <param name="whereExpre">筛选条件</param>
        /// <param name="set">更新操作</param>
        public void UpdateWhere<T>(Expression<Func<T, bool>> whereExpre, Action<T> set) where T : class, new()
        {
            var list = GetIShardingQueryable<T>().Where(whereExpre).ToList();
            list.ForEach(aData => set(aData));
            Update(list);
        }

        /// <summary>
        /// 获取IShardingQueryable
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <returns></returns>
        public IShardingQueryable<T> GetIShardingQueryable<T>() where T : class, new()
        {
            return new ShardingQueryable<T>(_db.GetIQueryable<T>(), _transaction);
        }

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <returns></returns>
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
                Dispose();
                _openedTransaction = false;
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

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (disposedValue)
                return;

            if (disposing)
            {
                _transaction.Dispose();
            }

            _openedTransaction = false;

            disposedValue = true;
        }

        ~ShardingRepository()
        {
            Dispose(false);
        }

        /// <summary>
        /// 执行与释放或重置非托管资源关联的应用程序定义的任务。
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        #endregion
    }
}
