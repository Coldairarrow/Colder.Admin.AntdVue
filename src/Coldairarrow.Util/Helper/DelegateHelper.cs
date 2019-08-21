using System;
using System.Threading.Tasks;

namespace Coldairarrow.Util
{
    /// <summary>
    /// 委托帮助类
    /// </summary>
    public class DelegateHelper
    {
        /// <summary>
        /// 异步执行方法
        /// </summary>
        /// <param name="firstFunc">首先执行的方法</param>
        /// <param name="next">接下来执行的方法</param>
        public static void RunAsync(Action firstFunc, Action next)
        {
            Task firstTask = new Task(() =>
            {
                firstFunc();
            });

            firstTask.Start();
            firstTask.ContinueWith(x => next());
        }

        /// <summary>
        /// 异步执行方法
        /// </summary>
        /// <param name="firstFunc">首先执行的方法</param>
        /// <param name="next">接下来执行的方法</param>
        public static void RunAsync(Func<object> firstFunc, Action<object> next)
        {
            Task<object> firstTask = new Task<object>(() =>
            {
                return firstFunc();
            });

            firstTask.Start();
            firstTask.ContinueWith(x => next(x.Result));
        }

    }
}
