using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.DynamicProxy;

namespace Coldairarrow.Util
{
    internal class CastleInterceptor : AsyncInterceptorBase
    {
        private readonly IServiceProvider _serviceProvider;
        public CastleInterceptor(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        private IAOPContext _aopContext;
        private List<BaseAOPAttribute> _aops;
        private async Task Befor()
        {
            foreach (var aAop in _aops)
            {
                await aAop.Befor(_aopContext);
            }
        }
        private async Task After()
        {
            foreach (var aAop in _aops)
            {
                await aAop.After(_aopContext);
            }
        }
        private void Init(IInvocation invocation)
        {
            _aopContext = new CastleAOPContext(invocation, _serviceProvider);

            _aops = invocation.MethodInvocationTarget.GetCustomAttributes(typeof(BaseAOPAttribute), true)
                .Concat(invocation.InvocationTarget.GetType().GetCustomAttributes(typeof(BaseAOPAttribute), true))
                .Select(x => (BaseAOPAttribute)x)
                .ToList();
        }

        protected override async Task InterceptAsync(IInvocation invocation, Func<IInvocation, Task> proceed)
        {
            Init(invocation);

            await Befor();
            await proceed(invocation);
            await After();
        }

        protected override async Task<TResult> InterceptAsync<TResult>(IInvocation invocation, Func<IInvocation, Task<TResult>> proceed)
        {
            Init(invocation);

            TResult result;

            await Befor();
            result = await proceed(invocation);
            await After();

            return result;
        }
    }
}
