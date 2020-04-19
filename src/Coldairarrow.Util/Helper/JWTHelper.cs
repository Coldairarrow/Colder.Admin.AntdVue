using Newtonsoft.Json.Linq;

namespace Coldairarrow.Util
{
    public static class JWTHelper
    {
        private static readonly string _headerBase64Url = "{\"alg\":\"HS256\",\"typ\":\"JWT\"}".Base64UrlEncode();
        public static readonly string JWTSecret = ConfigHelper.GetValue("JWTSecret");

        /// <summary>
        /// 生成Token
        /// </summary>
        /// <param name="payloadJsonStr">数据JSON字符串</param>
        /// <param name="secret">密钥</param>
        /// <returns></returns>
        public static string GetToken(string payloadJsonStr, string secret)
        {
            string payloadBase64Url = payloadJsonStr.Base64UrlEncode();
            string sign = $"{_headerBase64Url}.{payloadBase64Url}".ToHMACSHA256String(secret);

            return $"{_headerBase64Url}.{payloadBase64Url}.{sign}";
        }

        /// <summary>
        /// 获取Token中的数据
        /// </summary>
        /// <param name="token">token</param>
        /// <returns></returns>
        public static JObject GetPayload(string token)
        {
            return token.Split('.')[1].Base64UrlDecode().ToJObject();
        }

        /// <summary>
        /// 获取Token中的数据
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="token">token</param>
        /// <returns></returns>
        public static T GetPayload<T>(string token)
        {
            if (token.IsNullOrEmpty())
                return default;

            return token.Split('.')[1].Base64UrlDecode().ToObject<T>();
        }

        /// <summary>
        /// 校验Token
        /// </summary>
        /// <param name="token">token</param>
        /// <param name="secret">密钥</param>
        /// <returns></returns>
        public static bool CheckToken(string token, string secret)
        {
            var items = token.Split('.');
            var oldSign = items[2];
            string newSign = $"{items[0]}.{items[1]}".ToHMACSHA256String(secret);

            return oldSign == newSign;
        }
    }
}
