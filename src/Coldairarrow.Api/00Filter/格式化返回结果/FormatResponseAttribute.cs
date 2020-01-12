using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Coldairarrow.Api
{
    /// <summary>
    /// 若Action返回对象为自定义对象,则将其转为JSON
    /// </summary>
    public class FormatResponseAttribute : BaseActionFilter, IActionFilter
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
            if (context.ContainsFilter<NoJsonParamterAttribute>())
                return;

            if (context.Result is ObjectResult res)
                context.Result = JsonContent(res.Value.ToJson());
        }
    }
}