using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Coldairarrow.Util
{
    /// <summary>
    /// Random随机数帮助类
    /// </summary>
    public static class RandomHelper
    {
        private static Random _random { get; } = new Random();

        /// <summary>
        /// 下一个随机数
        /// </summary>
        /// <param name="minValue">最小值</param>
        /// <param name="maxValue">最大值</param>
        /// <returns></returns>
        public static int Next(int minValue, int maxValue)
        {
            return _random.Next(minValue, maxValue);
        }

        /// <summary>
        /// 下一个随机值
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="source">值的集合</param>
        /// <returns></returns>
        public static T Next<T>(IEnumerable<T> source)
        {
            return source.ToList()[Next(0, source.Count())];
        }
    }
}