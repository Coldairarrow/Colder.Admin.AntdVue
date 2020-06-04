using Coldairarrow.Util;
using Microsoft.AspNetCore.Http;
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

            var request = context.HttpContext.Request;
            request.EnableBuffering();
            string body = await request.Body?.ReadToStringAsync(Encoding.UTF8);
            Console.WriteLine(body);

            await Task.CompletedTask;
        }

        public override async Task OnActionExecuted(ActionExecutedContext context)
        {
            var time = DateTime.Now - _requesTime[context.HttpContext];
            _requesTime.TryRemove(context.HttpContext, out _);

            var request = context.HttpContext.Request;
            request.EnableBuffering();
            string body = await request.Body?.ReadToStringAsync(Encoding.UTF8);
            string resContent = string.Empty;
            if (context.Result is ContentResult result)
                resContent = result.Content;

            if (resContent?.Length > 1000)
            {
                resContent = new string(resContent.Copy(0, 1000).ToArray());
                resContent += "......";
            }

            string log =
@"方向:请求本系统
Url:{Url}
Time:{Time}ms
Method:{Method}
ContentType:{ContentType}
Body:{Body}

Response:{Response}
";
            var logger = context.HttpContext.RequestServices.GetService<ILogger<ApiLogAttribute>>();
            logger.LogInformation(
                log,
                request.Path,
                (int)time.TotalMilliseconds,
                request.Method,
                request.ContentType,
                body,
                resContent
                );

            await Task.CompletedTask;
        }
    }
}