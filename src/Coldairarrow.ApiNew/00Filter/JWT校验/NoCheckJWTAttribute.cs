using Microsoft.AspNetCore.Mvc.Filters;

namespace Coldairarrow.Api
{
    /// <summary>
    /// 忽略JWT校验
    /// </summary>
    public class NoCheckJWTAttribute : BaseActionFilter, IActionFilter
    {
        /// <summary>
        /// Action执行之前执行
        /// </summary>
        /// <param name="context">过滤器上下文</param>
        public void OnActionExecuting(ActionExecutingContext context)
        {

        }

        /// <summary>
        /// Action执行完毕之后执行
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}