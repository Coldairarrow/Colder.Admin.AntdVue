using Castle.DynamicProxy;
using System;
using System.Reflection;

namespace Coldairarrow.Util
{
    public class CastleAOPContext : IAOPContext
    {
        private readonly IInvocation _invocation;
        public CastleAOPContext(IInvocation invocation, IServiceProvider serviceProvider)
        {
            _invocation = invocation;
            ServiceProvider = serviceProvider;
        }
        public IServiceProvider ServiceProvider { get; }

        public object[] Arguments => _invocation.Arguments;

        public Type[] GenericArguments => _invocation.GenericArguments;

        public MethodInfo Method => _invocation.Method;

        public MethodInfo MethodInvocationTarget => _invocation.MethodInvocationTarget;

        public object Proxy => _invocation.Proxy;

        public object ReturnValue { get => _invocation.ReturnValue; set => _invocation.ReturnValue = value; }

        public Type TargetType => _invocation.TargetType;

        public object InvocationTarget => _invocation.InvocationTarget;
    }
}
