using Castle.DynamicProxy;

namespace Coldairarrow.Util
{
    /// <summary>
    /// 过滤器
    /// </summary>
    public interface IFilter
    {
        /// <summary>
        /// 执行前
        /// </summary>
        /// <param name="invocation">执行信息</param>
        void OnActionExecuting(IInvocation invocation);

        /// <summary>
        /// 执行后
        /// </summary>
        /// <param name="invocation">执行信息</param>
        void OnActionExecuted(IInvocation invocation);
    }
}
