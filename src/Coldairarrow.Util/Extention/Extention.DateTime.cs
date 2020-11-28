using System;
using System.Globalization;

namespace Coldairarrow.Util
{
    public static partial class Extention
    {
        ///   <summary> 
        ///  获取某一日期是该年中的第几周
        ///   </summary> 
        ///   <param name="dateTime"> 日期 </param> 
        ///   <returns> 该日期在该年中的周数 </returns> 
        public static int GetWeekOfYear(this DateTime dateTime)
        {
            GregorianCalendar gc = new GregorianCalendar();
            return gc.GetWeekOfYear(dateTime, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }

        /// <summary>
        /// 获取Js格式的timestamp
        /// </summary>
        /// <param name="dateTime">日期</param>
        /// <returns></returns>
        public static long ToJsTimestamp(this DateTime dateTime)
        {
            var startTime = new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime();
            long result = (dateTime.Ticks - startTime.Ticks) / 10000;   //除10000调整为13位
            return result;
        }

        /// <summary>
        /// 获取js中的getTime()
        /// </summary>
        /// <param name="dt">日期</param>
        /// <returns></returns>
        public static Int64 JsGetTime(this DateTime dt)
        {
            Int64 retval = 0;
            var st = new DateTime(1970, 1, 1);
            TimeSpan t = (dt.ToUniversalTime() - st);
            retval = (Int64)(t.TotalMilliseconds + 0.5);
            return retval;
        }

        /// <summary>
        /// 返回默认时间1970-01-01
        /// </summary>
        /// <param name="dt">时间日期</param>
        /// <returns></returns>
        public static DateTime Default(this DateTime dt)
        {
            return DateTime.Parse("1970-01-01");
        }

        /// <summary>
        /// 转为本地时间
        /// </summary>
        /// <param name="time">时间</param>
        /// <returns></returns>
        public static DateTime ToLocalTime(this DateTime time)
        {
            return TimeZoneInfo.ConvertTime(time, TimeZoneInfo.Local);
        }

        /// <summary>
        /// 转为转换为Unix时间戳格式(精确到秒)
        /// </summary>
        /// <param name="time">时间</param>
        /// <returns></returns>
        public static int ToUnixTimeStamp(this DateTime time)
        {
            DateTime startTime = new DateTime(1970, 1, 1).ToLocalTime();
            return (int)(time - startTime).TotalSeconds;
        }
    }
}
