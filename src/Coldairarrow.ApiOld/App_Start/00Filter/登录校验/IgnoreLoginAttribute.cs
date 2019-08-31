using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Coldairarrow.Web
{
    /// <summary>
    /// 忽略登录校验
    /// </summary>
    public class IgnoreLoginAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

        }
    }
}