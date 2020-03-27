using Autofac;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;

namespace Coldairarrow.Api
{
    public class ApiLogAttribute : Attribute, IActionFilter
    {
        static ConcurrentDictionary<HttpContext, DateTime> _requesTime { get; }
            = new ConcurrentDictionary<HttpContext, DateTime>();

        /// <summary>
        /// Action执行之前执行
        /// </summary>
        /// <param name="filterContext">过滤器上下文</param>
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _requesTime[HttpContextCore.Current] = DateTime.Now;
        }

        /// <summary>
        /// Action执行完毕之后执行
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var time = DateTime.Now - _requesTime[HttpContextCore.Current];
            _requesTime.TryRemove(HttpContextCore.Current, out _);

            var request = filterContext.HttpContext.Request;
            string contentType = request.ContentType ?? string.Empty;
            if (!contentType.Contains("json"))
                return;
            string resContent = string.Empty;
            if (filterContext.Result is ContentResult result)
                resContent = result.Content;

            if (resContent?.Length > 1000)
            {
                resContent = new string(resContent.Copy(0, 1000).ToArray());
                resContent += "......";
            }

            string log =
$@"方向:请求本系统
url:{request.GetDisplayUrl()}
method:{request.Method}
contentType:{request.ContentType}
body:{request.Body?.ReadToString(Encoding.UTF8)}
耗时:{(int)time.TotalMilliseconds}ms

返回:{resContent}
";
            //接口日志
            using (var lifescope = AutofacHelper.Container.BeginLifetimeScope())
            {
                lifescope.Resolve<ILogger>().Info(LogType.系统跟踪, log);
            }
        }
    }
}