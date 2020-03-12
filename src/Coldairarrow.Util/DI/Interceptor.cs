using Castle.DynamicProxy;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Coldairarrow.Util
{
    public class Interceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            var allFilers = invocation.MethodInvocationTarget.GetCustomAttributes<BaseFilterAttribute>(true)
                .Concat(invocation.InvocationTarget.GetType().GetCustomAttributes<BaseFilterAttribute>(true))
                .Where(x => x is IFilter)
                .Select(x => (IFilter)x)
                .ToList();

            //执行前
            foreach (var aFiler in allFilers)
            {
                aFiler.OnActionExecuting(invocation);

                if (!invocation.ReturnValue.IsNullOrEmpty())
                    return;
            }

            //执行
            invocation.Proceed();

            //若异常则不执行后面的
            if (invocation.ReturnValue is Task resTask && resTask.Status == TaskStatus.Faulted)
                return;

            //执行后
            allFilers.ForEach(aFiler =>
            {
                aFiler.OnActionExecuted(invocation);
            });
        }
    }
}
