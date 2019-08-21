using System;
using System.Threading;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace Coldairarrow.Util
{
    /// <summary>
    /// 时间帮助类
    /// </summary>
    public class TimerHelper
    {
        /// <summary>
        /// 设置一个时间间隔的循环操作
        /// </summary>
        /// <param name="action">执行的操作</param>
        /// <param name="timeSpan">时间间隔</param>
        public static Timer SetInterval(Action action, TimeSpan timeSpan)
        {
            //var provider = new ServiceCollection()
            //           .AddMemoryCache()
            //           .BuildServiceProvider();

            ////And now?
            //var cache = provider.GetService<IMemoryCache>();
            Timer threadTimer = new Timer((state =>
            {
                action.Invoke();
            }), null, 0, (long)timeSpan.TotalMilliseconds);
            CacheHelper.Cache.SetCache(Guid.NewGuid().ToString(), threadTimer);

            return threadTimer;
        }

        /// <summary>
        /// 设置一个时间间隔的循环操作
        /// </summary>
        /// <param name="action">执行的操作</param>
        /// <param name="timeSpan">时间间隔</param>
        /// <param name="dely">延迟一段时间后再开始循环</param>
        public static Timer SetInterval(Action action, TimeSpan timeSpan, TimeSpan dely)
        {
            Timer threadTimer = new Timer((state =>
            {
                action.Invoke();
            }), null, dely, timeSpan);
            CacheHelper.Cache.SetCache(Guid.NewGuid().ToString(), threadTimer);

            return threadTimer;
        }
    }
}
