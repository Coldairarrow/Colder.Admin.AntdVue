using System;

namespace Coldairarrow.Util
{
    /// <summary>
    /// Session帮助类,自定义Session,解决原Session并发问题
    /// </summary>
    public class SessionHelper
    {
        #region 私有成员

        private static string CacheModuleName { get; } = "Session";
        private static string _sessionId { get => HttpContextCore.Current.Request.Cookies[SessionCookieName]; }
        private static string BuildCacheKey(string sessionKey)
        {
            return $"{GlobalSwitch.ProjectName}_{CacheModuleName}_{_sessionId}_{sessionKey}";
        }

        #endregion

        #region 外部成员

        /// <summary>
        /// 存放Session标志的Cookie名
        /// </summary>
        public static string SessionCookieName { get; } = "Coldairarrow.Framework.ASP.NETCore_Session_Id";

        /// <summary>
        /// 当前Session
        /// </summary>
        public static _Session Session { get; } = new _Session();

        /// <summary>
        /// 自定义_Session类
        /// </summary>
        public class _Session
        {
            public object this[string index]
            {
                get
                {
                    string cacheKey = BuildCacheKey(index);
                    return CacheHelper.Cache.GetCache(cacheKey);
                }
                set
                {
                    string cacheKey = BuildCacheKey(index);
                    if (value.IsNullOrEmpty())
                        CacheHelper.Cache.RemoveCache(cacheKey);
                    else
                        CacheHelper.Cache.SetCache(cacheKey, value);
                }
            }
        }

        /// <summary>
        /// 清除Session的Cookie
        /// </summary>
        public static void RemoveSessionCookie()
        {
            HttpContextCore.Current.Response.Cookies.Delete(SessionCookieName);
        }

        #endregion
    }
}
