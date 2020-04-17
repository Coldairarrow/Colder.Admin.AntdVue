using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using System;
using System.Threading.Tasks;

namespace Coldairarrow.Business.Base_Manage
{
    public interface IBase_LogBusiness
    {
        Task<PageResult<Base_Log>> GetLogListAsync(PageInput<LogsInputDTO> input);
    }

    public class LogsInputDTO
    {
        public int? level { get; set; }
        public string logContent { get; set; }
        public DateTime? startTime { get; set; }
        public DateTime? endTime { get; set; }
    }
}