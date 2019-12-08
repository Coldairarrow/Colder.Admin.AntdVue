using Coldairarrow.Util;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
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
        private SynchronizedCollection<IRepository> _repositorys { get; set; }
            = new SynchronizedCollection<IRepository>();

        #endregion

        #region 外部接口

        public bool OpenTransaction { get; set; }

        public void AddRepository(params IRepository[] repositories)
        {
            repositories.ForEach(aRepositroy =>
            {
                if (!_repositorys.Contains(aRepositroy))
                {
                    if (OpenTransaction)
                        (aRepositroy as IInternalTransaction).BeginTransaction(_isolationLevel);

                    _repositorys.Add(aRepositroy);
                }
            });
        }

        public void BeginTransaction(IsolationLevel isolationLevel)
        {
            OpenTransaction = true;
            _isolationLevel = isolationLevel;
            _repositorys.ForEach(aRepository => (aRepository as IInternalTransaction).BeginTransaction(isolationLevel));
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
                DisposeTransaction();
            }

            return (isOK, resEx);
        }

        public void CommitTransaction()
        {
            _repositorys.ForEach(x => (x as IInternalTransaction).CommitTransaction());
        }

        public void RollbackTransaction()
        {
            _repositorys.ForEach(x => (x as IInternalTransaction).RollbackTransaction());
        }

        public void DisposeTransaction()
        {
            OpenTransaction = false;
            _repositorys.ForEach(x => (x as IInternalTransaction).DisposeTransaction());
        }

        #endregion

        #region Dispose

        private bool _disposed { get; set; } = false;
        public virtual void Dispose()
        {
            if (_disposed)
                return;

            _disposed = true;
            DisposeTransaction();
            _repositorys = null;
        }

        #endregion
    }
}
