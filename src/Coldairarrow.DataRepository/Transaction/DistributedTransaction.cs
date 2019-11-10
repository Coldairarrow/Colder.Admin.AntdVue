using Coldairarrow.Util;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace Coldairarrow.DataRepository
{
    /// <summary>
    /// 数据库分布式事务,跨库事务
    /// </summary>
    internal class DistributedTransaction : IInternalTransaction, IDisposable
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repositories">其它数据仓储</param>
        public DistributedTransaction(params IRepository[] repositories)
        {
            if (repositories == null)
                throw new Exception("repositories不能为NULL");

            if (repositories.Length > 0)
            {
                repositories.ForEach(aRepository =>
                {
                    if (!_repositorys.Contains(aRepository))
                        _repositorys.Add(aRepository);
                });
            }
        }

        #endregion

        #region 内部成员

        private IsolationLevel _isolationLevel { get; set; }
        private ConcurrentDictionary<string, DbTransaction> _transactionMap { get; }
            = new ConcurrentDictionary<string, DbTransaction>();
        private string GetRepositoryId(IRepository repository)
        {
            return $"{repository.DbType.ToString()}{repository.ConnectionString}";
        }
        private SynchronizedCollection<IRepository> _repositorys { get; set; }
            = new SynchronizedCollection<IRepository>();
        private object _lock { get; } = new object();
        private void _BeginTransaction(params IRepository[] repositorys)
        {
            repositorys.ForEach(x =>
            {
                Begin(x);
            });

            void Begin(IRepository db)
            {
                lock (_lock)
                {
                    //同一个数据库共享同一个事物
                    string id = GetRepositoryId(db);
                    if (_transactionMap.ContainsKey(id))
                        db.UseTransaction(_transactionMap[id]);
                    else
                    {
                        (db as IInternalTransaction).BeginTransaction(_isolationLevel);

                        _transactionMap[id] = db.GetTransaction();
                    }
                }
            }
        }

        #endregion

        #region 外部接口

        public void AddRepository(params IRepository[] repositories)
        {
            List<IRepository> needBeginList = new List<IRepository>();
            if (repositories.Length > 0)
            {
                repositories.ForEach(aRepository =>
                {
                    if (!_repositorys.Contains(aRepository))
                    {
                        _repositorys.Add(aRepository);
                        needBeginList.Add(aRepository);
                    }
                });
            }

            _BeginTransaction(needBeginList.ToArray());
        }

        public void BeginTransaction(IsolationLevel isolationLevel)
        {
            _isolationLevel = isolationLevel;
            _BeginTransaction(_repositorys.ToArray());
        }

        public (bool Success, Exception ex) RunTransaction(Action action, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            if (_repositorys.Count == 0)
                throw new Exception("IRepository数量不能为0");

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
            }

            return (isOK, resEx);
        }

        /// <summary>
        /// 提交事物
        /// </summary>
        public void CommitTransaction()
        {
            _transactionMap.Values.ForEach(x => x.Commit());
        }

        /// <summary>
        /// 回滚事物
        /// </summary>
        public void RollbackTransaction()
        {
            _transactionMap.Values.ForEach(x => x.Rollback());
        }

        #endregion

        #region Dispose

        public bool Disposed { get; set; } = false;

        protected virtual void Dispose(bool disposing)
        {
            if (Disposed)
                return;

            if (disposing)
            {
                _repositorys.ForEach(x => x.Dispose());
            }

            Disposed = true;
        }

        ~DistributedTransaction()
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
