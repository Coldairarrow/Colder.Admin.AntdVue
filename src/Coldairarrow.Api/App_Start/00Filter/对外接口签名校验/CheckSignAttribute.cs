using Coldairarrow.Business;
using Coldairarrow.Business.Base_SysManage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Coldairarrow.Web
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
HttpHelper.SafeSignRequest
/Demo/ApiSignDemo
    */
    /// <summary>
    /// 校验签名、十分严格
    /// 防抵赖、防伪造、防重复调用
    /// </summary>
    public class CheckSignAttribute : Attribute, IActionFilter
    {
        /// <summary>
        /// Action执行之前执行
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            IBase_AppSecretBusiness appSecretBus = AutofacHelper.GetScopeService<IBase_AppSecretBusiness>();
            ILogger logger= AutofacHelper.GetScopeService<ILogger>();

            //若为本地测试，则不需要校验
            if (GlobalSwitch.RunModel == RunModel.LocalTest)
            {
                return;
            }

            //判断是否需要签名
            if (filterContext.ContainsFilter<IgnoreSignAttribute>())
                return;

            var request = filterContext.HttpContext.Request;
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

            string guidKey = $"{GlobalSwitch.ProjectName}_apiGuid_{guid}";
            if (CacheHelper.Cache.GetCache(guidKey).IsNullOrEmpty())
                CacheHelper.Cache.SetCache(guidKey, "1", new TimeSpan(0, 10, 0));
            else
            {
                ReturnError("禁止重复调用!");
                return;
            }

            string body = request.Body.ReadToString();

            string sign = request.Headers["sign"].ToString();
            if (sign.IsNullOrEmpty())
            {
                ReturnError("缺少header:sign");
                return;
            }

            string appSecret = appSecretBus.GetAppSecret(appId);
            if (appSecret.IsNullOrEmpty())
            {
                ReturnError("header:appId无效");
                return;
            }

            string newSign = HttpHelper.BuildApiSign(appId, appSecret, guid, time.ToDateTime(), body);
            if (sign != newSign)
            {
                string log =
$@"header:sign签名错误!
headers:{request.Headers.ToJson()}
body:{body}
正确sign:{newSign}
";
                logger.Error(LogType.系统异常, log);
                ReturnError("header:sign签名错误");
                return;
            }

            void ReturnError(string msg)
            {
                AjaxResult res = new AjaxResult
                {
                    Success = false,
                    Msg = msg
                };

                filterContext.Result = new ContentResult { Content = res.ToJson(), ContentType = "application/json;charset=utf-8" };
            }
        }

        /// <summary>
        /// Action执行完毕之后执行
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {

        }
    }
}