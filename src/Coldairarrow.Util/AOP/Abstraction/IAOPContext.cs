using System;
using System.Reflection;

namespace Coldairarrow.Util
{
    public interface IAOPContext
    {
        IServiceProvider ServiceProvider { get; }
        object[] Arguments { get; }
        Type[] GenericArguments { get; }
        MethodInfo Method { get; }
        MethodInfo MethodInvocationTarget { get; }
        object Proxy { get; }
        object ReturnValue { get; set; }
        Type TargetType { get; }
        object InvocationTarget { get; }
    }
}
