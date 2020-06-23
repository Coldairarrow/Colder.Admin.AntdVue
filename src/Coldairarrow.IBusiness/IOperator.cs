using Coldairarrow.Entity;
using Coldairarrow.Util;

namespace Coldairarrow.IBusiness
{
    /// <summary>
    /// 操作者
    /// </summary>
    public interface IOperator
    {
        /// <summary>
        /// 当前操作者UserId
        /// </summary>
        string UserId { get; }

        Base_UserDTO Property { get; }

        #region 操作方法

        /// <summary>
        /// 判断是否为超级管理员
        /// </summary>
        /// <returns></returns>
        bool IsAdmin();

        /// <summary>
        /// 记录操作日志
        /// </summary>
        /// <param name="userLogType">用户日志类型</param>
        /// <param name="msg">内容</param>
        void WriteUserLog(UserLogType userLogType, string msg);

        #endregion
    }
}
