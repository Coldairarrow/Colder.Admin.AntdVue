using System;
using System.Data;
using System.Threading.Tasks;

namespace Coldairarrow.DataRepository
{
    public interface ITransaction : IDisposable
    {
        /// <summary>
        /// 执行事务,具体执行操作包括在action中
        /// 注:支持自定义事务级别,默认为ReadCommitted
        /// </summary>
        /// <param name="action">执行操作</param>
        /// <param name="isolationLevel">事务级别,默认为ReadCommitted</param>
        /// <returns></returns>
        (bool Success, Exception ex) RunTransaction(Action action, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);

        /// <summary>
        /// 执行事务,具体执行操作包括在action中
        /// 注:支持自定义事务级别,默认为ReadCommitted
        /// </summary>
        /// <param name="action">执行操作</param>
        /// <param name="isolationLevel">事务级别,默认为ReadCommitted</param>
        /// <returns></returns>
        Task<(bool Success, Exception ex)> RunTransactionAsync(Func<Task> action, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
    }
}
