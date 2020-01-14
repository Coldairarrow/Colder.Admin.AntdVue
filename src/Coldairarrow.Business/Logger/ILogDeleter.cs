using System;
using System.Threading.Tasks;

namespace Coldairarrow.Business
{
    public interface ILogDeleter
    {
        Task DeleteLogAsync(
            string logContent,
            string logType,
            string level,
            string opUserName,
            DateTime? startTime,
            DateTime? endTime);
    }
}
