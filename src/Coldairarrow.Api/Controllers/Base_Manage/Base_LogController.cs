using Coldairarrow.Business.Base_Manage;
using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Coldairarrow.Api.Controllers.Base_Manage
{
    /// <summary>
    /// 应用密钥
    /// </summary>
    /// <seealso cref="Coldairarrow.Api.BaseApiController" />
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
        public ActionResult<AjaxResult<List<Base_Log>>> GetLogList(
            Pagination pagination,
            string logContent,
            string logType,
            string level,
            string opUserName,
            DateTime? startTime,
            DateTime? endTime)
        {
            pagination.SortField = "CreateTime";
            pagination.SortType = "desc";
            var list = _logBus.GetLogList(pagination, logContent, logType, level, opUserName, startTime, endTime);

            return JsonContent(pagination.BuildTableResult_AntdVue(list).ToJson());
        }

        [HttpPost]
        public ActionResult GetLogTypeList()
        {
            var list = EnumHelper.ToOptionList(typeof(LogType));

            return Success(list);
        }

        [HttpPost]
        public ActionResult GetLoglevelList()
        {
            var list = EnumHelper.ToOptionList(typeof(LogLevel));

            return Success(list);
        }

        #endregion

        #region 提交

        #endregion
    }
}