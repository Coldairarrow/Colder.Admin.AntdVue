using Coldairarrow.Business.Base_Manage;
using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.Base_Manage
{
    [Route("/Base_Manage/[controller]/[action]")]
    public class Base_LogController : BaseApiController
    {
        #region DI

        public Base_LogController(IBase_LogBusiness logBus)
        {
            _logBus = logBus;
        }

        IBase_LogBusiness _logBus { get; }

        #endregion

        #region 获取

        [HttpPost]
        public async Task<PageResult<Base_Log>> GetLogList(PageInput<LogsInputDTO> input)
        {
            input.SortField = "CreateTime";
            input.SortType = "desc";

            return await _logBus.GetLogListAsync(input);
        }

        [HttpPost]
        public List<SelectOption> GetLogLevelList()
        {
            return EnumHelper.ToOptionList(typeof(LogLevel));
        }

        #endregion

        #region 提交

        #endregion
    }
}