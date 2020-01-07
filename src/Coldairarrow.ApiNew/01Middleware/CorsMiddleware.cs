using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Coldairarrow.Api
{
    /// <summary>
    /// 跨域中间件
    /// </summary>
    public class CorsMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// 管道执行到该中间件时候下一个中间件的RequestDelegate请求委托，如果有其它参数，也同样通过注入的方式获得
        /// </summary>
        /// <param name="next"></param>
        public CorsMiddleware(RequestDelegate next)
        {
            //通过注入方式获得对象
            _next = next;
        }

        /// <summary>
        /// 自定义中间件要执行的逻辑
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            context.Response.Headers.Add("Access-Control-Allow-Headers", context.Request.Headers["Access-Control-Request-Headers"]);
            context.Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");

            //若为OPTIONS跨域请求则直接返回,不进入后续管道
            if (context.Request.Method.ToUpper() != "OPTIONS")
                await _next(context);//把context传进去执行下一个中间件
        }
    }
}
