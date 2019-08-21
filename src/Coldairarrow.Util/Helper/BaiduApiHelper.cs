using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Coldairarrow.Util
{
    /// <summary>
    /// 百度接口签名帮助类
    /// </summary>
    public class BaiduApiHelper
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="accessKeyId">百度AccessKeyId(AK)</param>
        /// <param name="secretAccessKey">百度SecretAccessKey(SK)</param>
        public BaiduApiHelper(string accessKeyId,string secretAccessKey)
        {
            _accessKeyId = accessKeyId;
            _secretAccessKey = secretAccessKey;
        }

        #endregion

        #region 内部成员 

        private string _accessKeyId { get; }
        private string _secretAccessKey { get; }
        private string UriEncode(string input, bool encodeSlash = false)
        {
            StringBuilder builder = new StringBuilder();
            foreach (byte b in Encoding.UTF8.GetBytes(input))
            {
                if ((b >= 'a' && b <= 'z') || (b >= 'A' && b <= 'Z') || (b >= '0' && b <= '9') || b == '_' || b == '-' || b == '~' || b == '.')
                {
                    builder.Append((char)b);
                }
                else if (b == '/')
                {
                    if (encodeSlash)
                    {
                        builder.Append("%2F");
                    }
                    else
                    {
                        builder.Append((char)b);
                    }
                }
                else
                {
                    builder.Append('%').Append(b.ToString("X2"));
                }
            }
            return builder.ToString();
        }
        private string Hex(byte[] data)
        {
            var sb = new StringBuilder();
            foreach (var b in data)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
        private string CanonicalRequest(HttpWebRequest req)
        {
            Uri uri = req.RequestUri;
            StringBuilder canonicalReq = new StringBuilder();
            canonicalReq.Append(req.Method).Append("\n").Append(UriEncode(Uri.UnescapeDataString(uri.AbsolutePath))).Append("\n");

            var parameters = HttpUtility.ParseQueryString(uri.Query);
            List<string> parameterStrings = new List<string>();
            foreach (KeyValuePair<string, string> entry in parameters)
            {
                parameterStrings.Add(UriEncode(entry.Key) + '=' + UriEncode(entry.Value));
            }
            parameterStrings.Sort();
            canonicalReq.Append(string.Join("&", parameterStrings.ToArray())).Append("\n");

            string host = uri.Host;
            if (!(uri.Scheme == "https" && uri.Port == 443) && !(uri.Scheme == "http" && uri.Port == 80))
            {
                host += ":" + uri.Port;
            }
            canonicalReq.Append("host:" + UriEncode(host));
            return canonicalReq.ToString();
        }

        #endregion

        #region 外部接口

        /// <summary>
        /// 发送POST请求
        /// </summary>
        /// <param name="method">请求方法，需要大写，列如(POST)</param>
        /// <param name="host">主机地址列如(http://sms.bj.baidubce.com)</param>
        /// <param name="url">接口地址列如(/bce/v2/message)</param>
        /// <param name="paramters">参数列表</param>
        /// <returns></returns>
        public string RequestData(string method,string host, string url, Dictionary<string, object> paramters=null)
        {
            string ak = _accessKeyId;
            string sk = _secretAccessKey;
            DateTime now = DateTime.Now.ToCstTime();
            int expirationInSeconds = 1200;

            HttpWebRequest req = WebRequest.Create(host + url) as HttpWebRequest;
            Uri uri = req.RequestUri;
            req.Method = method;
            req.ContentType = "application/json";

            if (paramters != null)
            {
                Stream requestStream = req.GetRequestStream();
                byte[] data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(paramters));
                requestStream.Write(data, 0, data.Length);
            }

            string signDate = now.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ssK");
            string authString = "bce-auth-v1/" + ak + "/" + signDate + "/" + expirationInSeconds;
            string signingKey = Hex(new HMACSHA256(Encoding.UTF8.GetBytes(sk)).ComputeHash(Encoding.UTF8.GetBytes(authString)));

            string canonicalRequestString = CanonicalRequest(req);

            string signature = Hex(new HMACSHA256(Encoding.UTF8.GetBytes(signingKey)).ComputeHash(Encoding.UTF8.GetBytes(canonicalRequestString)));
            string authorization = authString + "/host/" + signature;

            req.Headers.Add("x-bce-date", signDate);
            req.Headers.Add(HttpRequestHeader.Authorization, authorization);

            HttpWebResponse res;
            string message = "";
            try
            {
                res = req.GetResponse() as HttpWebResponse;
            }
            catch (WebException e)
            {
                res = e.Response as HttpWebResponse;
            }
            message = new StreamReader(res.GetResponseStream()).ReadToEnd();

            return message;
        }

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="phoneNum">手机号码</param>
        /// <param name="code">验证码</param>
        /// <returns></returns>
        public static bool SendMsg(string phoneNum,string code)
        {
            try
            {
                BaiduApiHelper baiduApiHelper = new BaiduApiHelper("", "");

                string host = "http://sms.bj.baidubce.com";
                string url = "/bce/v2/message";
                Dictionary<string, object> paramters = new Dictionary<string, object>();
                paramters.Add("invokeId", "SGf96VPF-WBBx-qbJK");
                paramters.Add("phoneNumber", phoneNum);
                paramters.Add("templateCode", "smsTpl:e7476122a1c24e37b3b0de19d04ae900");
                paramters.Add("contentVar", new { others = "你好", code = code });

                string resJsonStr = baiduApiHelper.RequestData("POST", host, url, paramters);
                var resJson = JsonConvert.DeserializeObject<JObject>(resJsonStr);

                return resJson["code"]?.ToString() == "1000";
            }
            catch
            {
                return false;
            }
        }

        #endregion
    }
}
