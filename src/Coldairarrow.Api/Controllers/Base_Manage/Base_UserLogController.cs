using Coldairarrow.Business.Base_Manage;
using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.Base_Manage
{
    [Route("/Base_Manage/[controller]/[action]")]
    [OpenApiTag("审计日志")]
    public class Base_UserLogController : BaseApiController
    {
        #region DI

        public Base_UserLogController(IBase_UserLogBusiness logBus)
        {
            _logBus = logBus;
        }

        IBase_UserLogBusiness _logBus { get; }

        #endregion

        #region 获取

        [HttpPost]
        public async Task<PageResult<Base_UserLog>> GetLogList(PageInput<UserLogsInputDTO> input)
        {
            input.SortField = "CreateTime";
            input.SortType = "desc";

            return await _logBus.GetLogListAsync(input);
        }

        [HttpPost]
        public List<SelectOption> GetLogTypeList()
        {
            return EnumHelper.ToOptionList(typeof(UserLogType));
        }

        #endregion

        #region 提交

        #endregion
    }
}