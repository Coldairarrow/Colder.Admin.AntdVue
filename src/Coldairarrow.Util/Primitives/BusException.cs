using System;

namespace Coldairarrow.Util
{
    /// <summary>
    /// 业务异常
    /// 注:并不会当作真正的异常处理,仅为方便返回前端错误提示信息
    /// </summary>
    public class BusException : Exception
    {
        /// <summary>
        /// 错误代码
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public BusException()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">错误信息</param>
        public BusException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">错误信息</param>
        /// <param name="errorCode">错误代码</param>
        public BusException(string message, int errorCode)
            : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
