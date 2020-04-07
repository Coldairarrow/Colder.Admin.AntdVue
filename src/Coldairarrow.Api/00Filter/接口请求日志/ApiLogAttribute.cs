using Coldairarrow.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coldairarrow.Api
{
    public class ApiLogAttribute : BaseActionFilterAsync
    {
        static ConcurrentDictionary<HttpContext, DateTime> _requesTime { get; }
            = new ConcurrentDictionary<HttpContext, DateTime>();

        public override async Task OnActionExecuting(ActionExecutingContext context)
        {
            _requesTime[context.HttpContext] = DateTime.Now;

            await Task.CompletedTask;
        }

        public override async Task OnActionExecuted(ActionExecutedContext context)
        {
            var time = DateTime.Now - _requesTime[context.HttpContext];
            _requesTime.TryRemove(context.HttpContext, out _);

            var request = context.HttpContext.Request;
            string resContent = string.Empty;
            if (context.Result is ContentResult result)
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
            var logger = context.HttpContext.RequestServices.GetService<ILogger<ApiLogAttribute>>();
            logger.LogInformation(log);

            await Task.CompletedTask;
        }
    }
}