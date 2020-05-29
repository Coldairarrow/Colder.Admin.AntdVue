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
        public IServiceProvider ServiceProvider { get; set; }

        public object[] Arguments { get => _invocation.Arguments; }

        public Type[] GenericArguments { get => _invocation.GenericArguments; }

        public MethodInfo Method { get => _invocation.Method; }

        public MethodInfo MethodInvocationTarget { get => _invocation.MethodInvocationTarget; }

        public object Proxy { get => _invocation.Proxy; }

        public object ReturnValue { get => _invocation.ReturnValue; set => _invocation.ReturnValue = value; }

        public Type TargetType { get => _invocation.TargetType; }

        public object InvocationTarget => _invocation.InvocationTarget;
    }
}
