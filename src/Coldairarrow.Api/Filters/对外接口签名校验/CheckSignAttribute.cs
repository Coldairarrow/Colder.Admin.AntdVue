using Coldairarrow.Business.Base_Manage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Coldairarrow.Api
{
    /*
==== 签名校验 ====

为保证接口安全，每次请求必带以下header

| header名 | 类型 | 描述 |
| appId | string | 应用Id |
| time | string | 当前时间，格式为：2017-01-01 23:00:00 |
| guid | string | GUID字符串,作为请求唯一标志,防止重复请求 |
| sign| string | 签名,签名算法如下 |

签名算法示例：

令:

appId=xxx

appSecret=xxx

time=2017-01-01 23:00:00

guid=d0595245-60db-495d-9c0e-fea931b8d69a

请求的body={"aaa":"aaa"}

1: 依次拼接appId+time+guid+body+appSecret得到xxx2017-01-01 23:00:00d0595245-60db-495d-9c0e-fea931b8d69a{"aaa":"aaa"}xxx
2: 将上面拼接字符串进行MD5(32位)即可得到签名
sign=MD5(xxx2017-01-01 23:00:00d0595245-60db-495d-9c0e-fea931b8d69a{"aaa":"aaa"}xxx)
    =4e30f1eca521485c208f642a7d927ff0
3: 在header中携带上述的appId、time、guid、sign即可

详细使用Demo请看:
src\Coldairarrow.Web\src\utils\plugin\axios-plugin.js
    */
    /// <summary>
    /// 校验签名、十分严格
    /// 防抵赖、防伪造、防重复调用
    /// </summary>
    public class CheckSignAttribute : BaseActionFilterAsync
    {
        /// <summary>
        /// Action执行之前执行
        /// </summary>
        /// <param name="filterContext"></param>
        public async override Task OnActionExecuting(ActionExecutingContext filterContext)
        {
            //判断是否需要签名
            if (filterContext.ContainsFilter<IgnoreSignAttribute>())
                return;
            var request = filterContext.HttpContext.Request;
            IServiceProvider serviceProvider = filterContext.HttpContext.RequestServices;
            IBase_AppSecretBusiness appSecretBus = serviceProvider.GetService<IBase_AppSecretBusiness>();
            ILogger logger = serviceProvider.GetService<ILogger<CheckSignAttribute>>();
            var cache = serviceProvider.GetService<IDistributedCache>();

            string appId = request.Headers["appId"].ToString();
            if (appId.IsNullOrEmpty())
            {
                ReturnError("缺少header:appId");
                return;
            }
            string time = request.Headers["time"].ToString();
            if (time.IsNullOrEmpty())
            {
                ReturnError("缺少header:time");
                return;
            }
            if (time.ToDateTime() < DateTime.Now.AddMinutes(-5) || time.ToDateTime() > DateTime.Now.AddMinutes(5))
            {
                ReturnError("time过期");
                return;
            }

            string guid = request.Headers["guid"].ToString();
            if (guid.IsNullOrEmpty())
            {
                ReturnError("缺少header:guid");
                return;
            }

            string guidKey = $"ApiGuid_{guid}";
            if (cache.GetString(guidKey).IsNullOrEmpty())
                cache.SetString(guidKey, "1", new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
                });
            else
            {
                ReturnError("禁止重复调用!");
                return;
            }

            request.EnableBuffering();
            string body = await request.Body.ReadToStringAsync();

            string sign = request.Headers["sign"].ToString();
            if (sign.IsNullOrEmpty())
            {
                ReturnError("缺少header:sign");
                return;
            }

            string appSecret = await appSecretBus.GetAppSecretAsync(appId);
            if (appSecret.IsNullOrEmpty())
            {
                ReturnError("header:appId无效");
                return;
            }

            string newSign = BuildApiSign(appId, appSecret, guid, time.ToDateTime(), body);
            if (sign != newSign)
            {
                string log =
$@"sign签名错误!
headers:{request.Headers.ToJson()}
body:{body}
正确sign:{newSign}
";
                logger.LogWarning(log);
                ReturnError("header:sign签名错误");
                return;
            }

            void ReturnError(string msg)
            {
                filterContext.Result = Error(msg);
            }
        }

        /// <summary>
        /// 生成接口签名sign
        /// 注：md5(appId+time+guid+body+appSecret)
        /// </summary>
        /// <param name="appId">应用Id</param>
        /// <param name="appSecret">应用密钥</param>
        /// <param name="guid">唯一GUID</param>
        /// <param name="time">时间</param>
        /// <param name="body">请求体</param>
        /// <returns></returns>
        private string BuildApiSign(string appId, string appSecret, string guid, DateTime time, string body)
        {
            return $"{appId}{time.ToString("yyyy-MM-dd HH:mm:ss")}{guid}{body}{appSecret}".ToMD5String();
        }
    }
}