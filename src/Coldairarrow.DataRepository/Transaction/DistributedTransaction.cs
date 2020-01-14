using Coldairarrow.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

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
                    if (!_repositories.Contains(aRepository))
                        _repositories.Add(aRepository);
                });
            }
        }

        #endregion

        #region 内部成员
        private IsolationLevel _isolationLevel { get; set; }
        private SynchronizedCollection<IRepository> _repositories { get; set; }
            = new SynchronizedCollection<IRepository>();

        #endregion

        #region 外部接口

        public bool OpenTransaction { get; set; }

        public void AddRepository(params IRepository[] repositories)
        {
            repositories.ForEach(aRepositroy =>
            {
                if (!_repositories.Contains(aRepositroy))
                {
                    if (OpenTransaction)
                        (aRepositroy as IInternalTransaction).BeginTransaction(_isolationLevel);

                    _repositories.Add(aRepositroy);
                }
            });
        }

        public void BeginTransaction(IsolationLevel isolationLevel)
        {
            OpenTransaction = true;
            _isolationLevel = isolationLevel;
            _repositories.ForEach(aRepository => (aRepository as IInternalTransaction).BeginTransaction(isolationLevel));
        }

        public async Task BeginTransactionAsync(IsolationLevel isolationLevel)
        {
            OpenTransaction = true;
            _isolationLevel = isolationLevel;
            foreach (var aRepository in _repositories)
            {
                await (aRepository as IInternalTransaction).BeginTransactionAsync(isolationLevel);
            }
        }

        public (bool Success, Exception ex) RunTransaction(Action action, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            if (_repositories.Count == 0)
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
                DisposeTransaction();
            }

            return (isOK, resEx);
        }

        public void CommitTransaction()
        {
            _repositories.ForEach(x => (x as IInternalTransaction).CommitTransaction());
        }

        public void RollbackTransaction()
        {
            _repositories.ForEach(x => (x as IInternalTransaction).RollbackTransaction());
        }

        public void DisposeTransaction()
        {
            OpenTransaction = false;
            _repositories.ForEach(x => (x as IInternalTransaction).DisposeTransaction());
        }

        public async Task<(bool Success, Exception ex)> RunTransactionAsync(Func<Task> action, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            if (_repositories.Count == 0)
                throw new Exception("IRepository数量不能为0");

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

        #endregion

        #region Dispose

        private bool _disposed = false;
        public virtual void Dispose()
        {
            if (_disposed)
                return;

            _disposed = true;
            DisposeTransaction();
            _repositories = null;
        }

        #endregion
    }
}
