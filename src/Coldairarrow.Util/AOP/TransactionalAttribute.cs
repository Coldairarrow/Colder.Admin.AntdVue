using EFCore.Sharding;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Coldairarrow.Util
{
    /// <summary>
    /// 使用事务包裹
    /// </summary>
    public class TransactionalAttribute : BaseAOPAttribute
    {
        private readonly IsolationLevel _isolationLevel;
        public TransactionalAttribute(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            _isolationLevel = isolationLevel;
        }
        private TransactionContainer _container;
        public override async Task Befor(IAOPContext context)
        {
            _container = context.ServiceProvider.GetService<TransactionContainer>();

            if (!_container.TransactionOpened)
            {
                _container.TransactionOpened = true;
                await _container.BeginTransactionAsync(_isolationLevel);
            }
        }
        public override async Task After(IAOPContext context)
        {
            _container = context.ServiceProvider.GetService<TransactionContainer>();

            try
            {
                if (_container.TransactionOpened)
                {
                    _container.CommitTransaction();
                }
            }
            catch (Exception ex)
            {
                _container.RollbackTransaction();
                throw new Exception("系统异常", ex);
            }

            if (_container.TransactionOpened)
            {
                _container.TransactionOpened = false;
            }

            await Task.CompletedTask;
        }
    }

    public class TransactionContainer : IScopedDependency, IDistributedTransaction
    {
        public TransactionContainer(IServiceProvider serviceProvider)
        {
            _distributedTransaction = DistributedTransactionFactory.GetDistributedTransaction();

            var allRepositoryInterfaces = GlobalData.AllFxTypes.Where(x =>
                    typeof(IDbAccessor).IsAssignableFrom(x)
                    && x.IsInterface
                    && x != typeof(IDbAccessor)
                ).ToList();
            allRepositoryInterfaces.Add(typeof(IDbAccessor));

            var repositories = allRepositoryInterfaces
                .Select(x => serviceProvider.GetService(x) as IDbAccessor)
                .ToArray();

            _distributedTransaction.AddDbAccessor(repositories);
        }
        private readonly IDistributedTransaction _distributedTransaction;
        public bool TransactionOpened { get; set; }

        public void Dispose()
        {
            _distributedTransaction.Dispose();
        }

        public Task<(bool Success, Exception ex)> RunTransactionAsync(Func<Task> action, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            return _distributedTransaction.RunTransactionAsync(action, isolationLevel);
        }

        public (bool Success, Exception ex) RunTransaction(Action action, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            return _distributedTransaction.RunTransaction(action, isolationLevel);
        }

        public void BeginTransaction(IsolationLevel isolationLevel)
        {
            _distributedTransaction.BeginTransaction(isolationLevel);
        }

        public Task BeginTransactionAsync(IsolationLevel isolationLevel)
        {
            return _distributedTransaction.BeginTransactionAsync(isolationLevel);
        }

        public void CommitTransaction()
        {
            _distributedTransaction.CommitTransaction();
        }

        public void RollbackTransaction()
        {
            _distributedTransaction.RollbackTransaction();
        }

        public void DisposeTransaction()
        {
            _distributedTransaction.DisposeTransaction();
        }

        public void AddDbAccessor(params IDbAccessor[] repositories)
        {
            _distributedTransaction.AddDbAccessor(repositories);
        }
    }
}
