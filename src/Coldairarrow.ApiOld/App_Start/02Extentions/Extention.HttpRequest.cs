using Coldairarrow.Util;
using Microsoft.AspNetCore.Http.Extensions;

namespace Microsoft.AspNetCore.Http
{
    /// <summary>
    /// 拓展类
    /// </summary>
    public static partial class Extention
    {
        /// <summary>
        /// 判断是否为AJAX请求
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public static bool IsAjaxRequest(this HttpRequest req)
        {
            bool result = false;

            var xreq = req.Headers.ContainsKey("x-requested-with");
            if (xreq)
            {
                result = req.Headers["x-requested-with"] == "XMLHttpRequest";
            }

            return result;
        }

        /// <summary>
        /// 获取去掉查询参数的Url
        /// </summary>
        /// <param name="req">请求</param>
        /// <returns></returns>
        public static string GetDisplayUrlNoQuery(this HttpRequest req)
        {
            var queryStr = req.QueryString.ToString();
            var displayUrl = req.GetDisplayUrl();

            return queryStr.IsNullOrEmpty() ? displayUrl : displayUrl.Replace(queryStr, "");
        }
    }
}
