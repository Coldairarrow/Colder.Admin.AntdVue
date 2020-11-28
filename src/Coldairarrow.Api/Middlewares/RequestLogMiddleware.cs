using Coldairarrow.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Coldairarrow.Api
{
    public class RequestLogMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public RequestLogMiddleware(RequestDelegate next, ILogger<RequestLogMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            Stopwatch watch = Stopwatch.StartNew();
            string resContent = string.Empty;

            //返回Body需特殊处理
            Stream originalResponseBody = context.Response.Body;
            using var memStream = new MemoryStream();
            context.Response.Body = memStream;

            try
            {
                await _next(context);

                memStream.Position = 0;
                resContent = new StreamReader(memStream).ReadToEnd();

                memStream.Position = 0;
                await memStream.CopyToAsync(originalResponseBody);
            }
            catch
            {
                throw;
            }
            finally
            {
                context.Response.Body = originalResponseBody;

                watch.Stop();

                if (resContent?.Length > 1000)
                {
                    resContent = new string(resContent.Copy(0, 1000).ToArray());
                    resContent += "......内容太长已忽略";
                }

                string log =
            @"方向:请求本系统
Url:{Url}
Time:{Time}ms
Method:{Method}
ContentType:{ContentType}
Body:{Body}
StatusCode:{StatusCode}

Response:{Response}
";
                _logger.LogInformation(
                    log,
                    context.Request.Path,
                    (int)watch.ElapsedMilliseconds,
                    context.Request.Method,
                    context.Request.ContentType,
                    context.RequestServices.GetService<RequestBody>().Body,
                    context.Response.StatusCode,
                    resContent
                    );
            }
        }
    }
}
