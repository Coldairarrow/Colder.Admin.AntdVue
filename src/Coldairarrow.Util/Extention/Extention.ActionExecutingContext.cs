using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace Coldairarrow.Util
{
    /// <summary>
    /// 拓展类
    /// </summary>
    public static partial class Extention
    {
        /// <summary>
        /// 是否拥有某过滤器
        /// </summary>
        /// <typeparam name="T">过滤器类型</typeparam>
        /// <param name="actionExecutingContext">上下文</param>
        /// <returns></returns>
        public static bool ContainsFilter<T>(this ActionExecutingContext actionExecutingContext)
        {
            return actionExecutingContext.Filters.Any(x => x.GetType() == typeof(T));
        }
    }
}
