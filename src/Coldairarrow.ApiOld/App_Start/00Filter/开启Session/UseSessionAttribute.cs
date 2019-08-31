using Coldairarrow.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Coldairarrow.Web
{
    /// <summary>
    /// 使用Session
    /// </summary>
    public class UseSessionAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var sessionCookie = context.HttpContext.Request.Cookies[SessionHelper.SessionCookieName];
            if (sessionCookie.IsNullOrEmpty())
            {
                string sessionId = Guid.NewGuid().ToString();
                context.HttpContext.Response.Cookies.Append(SessionHelper.SessionCookieName, sessionId, new CookieOptions { Expires = DateTime.MaxValue, SameSite = SameSiteMode.None });

                context.Result = new RedirectResult(context.HttpContext.Request.GetDisplayUrl());
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}
