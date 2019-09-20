using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Text.RegularExpressions;

namespace Coldairarrow.Api
{
    /// <summary>
    /// JWT校验
    /// </summary>
    public class CheckJWTAttribute : BaseActionFilter, IActionFilter
    {
        private static readonly int _errorCode = 401;

        /// <summary>
        /// Action执行之前执行
        /// </summary>
        /// <param name="context">过滤器上下文</param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ContainsFilter<NoCheckJWTAttribute>() || GlobalSwitch.RunModel == RunModel.LocalTest)
                return;

            string tokenHeader = context.HttpContext.Request.Headers["Authorization"].ToString();
            if (tokenHeader.IsNullOrEmpty())
            {
                context.Result = Error("缺少token!", _errorCode);
                return;
            }

            string pattern = "^Bearer (.*?)$";
            if (!Regex.IsMatch(tokenHeader, pattern))
            {
                context.Result = Error("token格式不对!格式为:Bearer {token}", _errorCode);
                return;
            }

            string token = Regex.Match(tokenHeader, pattern).Groups[1]?.ToString();
            if (!JWTHelper.CheckToken(token, JWTHelper.JWTSecret))
            {
                context.Result = Error("token校验失败!", _errorCode);
                return;
            }

            var payload = JWTHelper.GetPayload<JWTPayload>(token);
            if (payload.Expire < DateTime.Now)
            {
                context.Result = Error("token过期!", _errorCode);
                return;
            }
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