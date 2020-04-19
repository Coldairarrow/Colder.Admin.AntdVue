using AspectCore.DynamicProxy;
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
    [AttributeUsage(AttributeTargets.Method)]
    public class TransactionalAttribute : AbstractInterceptorAttribute
    {
        readonly IsolationLevel _isolationLevel;
        public TransactionalAttribute(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            _isolationLevel = isolationLevel;
        }
        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            var container = context.ServiceProvider.GetService<TransactionContainer>();
            if (!container.TransactionOpened)
            {
                container.TransactionOpened = true;
                var res = await container.RunTransactionAsync(async () =>
                {
                    await next(context);
                }, _isolationLevel);
                container.TransactionOpened = false;

                if (!res.Success)
                    throw new Exception("系统异常", res.ex);
            }
            else
                await next(context);
        }
    }

    public class TransactionContainer : IScopeDependency, IDisposable
    {
        public TransactionContainer(IServiceProvider serviceProvider)
        {
            _distributedTransaction = DistributedTransactionFactory.GetDistributedTransaction();

            var repositories = GlobalData.AllFxTypes.Where(x =>
                  typeof(IRepository).IsAssignableFrom(x)
                  && x.IsInterface
                ).Select(x => serviceProvider.GetService(x) as IRepository)
                .ToArray();

            _distributedTransaction.AddRepository(repositories);
        }
        private IDistributedTransaction _distributedTransaction;
        public bool TransactionOpened { get; set; }

        public void Dispose()
        {
            _distributedTransaction.Dispose();
        }

        public Task<(bool Success, Exception ex)> RunTransactionAsync(Func<Task> action, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            return _distributedTransaction.RunTransactionAsync(action, isolationLevel);
        }
    }
}
