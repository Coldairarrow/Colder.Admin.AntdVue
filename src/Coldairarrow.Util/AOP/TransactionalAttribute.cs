using AspectCore.DynamicProxy;
using EFCore.Sharding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Coldairarrow.Util
{
    public class TransactionalAttribute : AbstractInterceptorAttribute
    {
        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            var logger = context.ServiceProvider.GetService<ILogger<TransactionalAttribute>>();
            logger.LogInformation("666666");

            await next(context);
        }
    }

    public class TransactionContainer: IDistributedTransaction
    {
        private IDistributedTransaction _distributedTransaction =
            DistributedTransactionFactory.GetDistributedTransaction();
        public bool TransactionOpened { get; set; }

        public void AddRepository(params IRepository[] repositories)
        {
            _distributedTransaction.AddRepository(repositories);
        }

        public void Dispose()
        {
            _distributedTransaction.Dispose();
        }

        public (bool Success, Exception ex) RunTransaction(Action action, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            return _distributedTransaction.RunTransaction(action, isolationLevel);
        }

        public Task<(bool Success, Exception ex)> RunTransactionAsync(Func<Task> action, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            return _distributedTransaction.RunTransactionAsync(action, isolationLevel);
        }
    }
}
