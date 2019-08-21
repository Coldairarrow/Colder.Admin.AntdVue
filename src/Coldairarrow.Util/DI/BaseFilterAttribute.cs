using Castle.DynamicProxy;
using System;

namespace Coldairarrow.Util
{
    public abstract class BaseFilterAttribute : Attribute, IFilter
    {
        public abstract void OnActionExecuted(IInvocation invocation);
        public abstract void OnActionExecuting(IInvocation invocation);
    }
}
