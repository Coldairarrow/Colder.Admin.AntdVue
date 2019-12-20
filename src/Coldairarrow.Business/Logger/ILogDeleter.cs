using System;

namespace Coldairarrow.Business
{
    public interface ILogDeleter
    {
        void DeleteLog(
            string logContent,
            string logType,
            string level,
            string opUserName,
            DateTime? startTime,
            DateTime? endTime);
    }
}
