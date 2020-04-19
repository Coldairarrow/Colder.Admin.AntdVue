using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System;
using System.Text.RegularExpressions;

namespace Coldairarrow.Util
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

        /// <summary>
        /// 获取Token
        /// </summary>
        /// <param name="req">请求</param>
        /// <returns></returns>
        public static string GetToken(this HttpRequest req)
        {
            string tokenHeader = req.Headers["Authorization"].ToString();
            if (tokenHeader.IsNullOrEmpty())
                return null;

            string pattern = "^Bearer (.*?)$";
            if (!Regex.IsMatch(tokenHeader, pattern))
                throw new Exception("token格式不对!格式为:Bearer {token}");

            string token = Regex.Match(tokenHeader, pattern).Groups[1]?.ToString();
            if (token.IsNullOrEmpty())
                throw new Exception("token不能为空!");

            return token;
        }

        /// <summary>
        /// 获取Token中的Payload
        /// </summary>
        /// <param name="req">请求</param>
        /// <returns></returns>
        public static JWTPayload GetJWTPayload(this HttpRequest req)
        {
            string token = req.GetToken();
            var payload = JWTHelper.GetPayload<JWTPayload>(token);

            return payload;
        }
    }
}
