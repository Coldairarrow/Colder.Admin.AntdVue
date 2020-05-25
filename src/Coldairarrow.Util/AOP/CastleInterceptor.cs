using Castle.DynamicProxy;
using System.Threading.Tasks;

namespace Coldairarrow.Util
{
    internal class CastleInterceptor : IInterceptor
    {
        private async Task Befor(IInvocation invocation)
        {
            await Task.CompletedTask;
        }
        private async Task After(IInvocation invocation)
        {
            await Task.CompletedTask;
        }
        private async Task InternalInterceptAsync(IInvocation invocation)
        {
            await Befor(invocation);
            invocation.Proceed();
            await After(invocation);
        }

        public void Intercept(IInvocation invocation)
        {
            bool isAsync = typeof(Task).IsAssignableFrom(invocation.MethodInvocationTarget.ReturnType);
            if (!isAsync)
            {
                AsyncHelper.RunSync(() => Befor(invocation));
            }
            invocation.Proceed();
            if (!isAsync)
            {
                AsyncHelper.RunSync(() => After(invocation));
            }

            if (isAsync)
            {
                invocation.ReturnValue = InterceptAsync((dynamic)invocation.ReturnValue, invocation);
            }
        }

        private async Task InterceptAsync(Task task, IInvocation invocation)
        {
            await Befor(invocation);

            await task;

            await After(invocation);
        }

        private async Task<T> InterceptAsync<T>(Task<T> task, IInvocation invocation)
        {
            await Befor(invocation);

            T result = await task;

            await After(invocation);

            return result;
        }
    }
}
