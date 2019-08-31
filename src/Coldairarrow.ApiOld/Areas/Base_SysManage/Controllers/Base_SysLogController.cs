using Coldairarrow.Business.Base_SysManage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Coldairarrow.Web.Areas.Base_SysManage.Controllers
{
    [Area("Base_SysManage")]
    public class Base_SysLogController : BaseMvcController
    {
        #region DI

        public Base_SysLogController(IBase_SysLogBusiness sysLogBus)
        {
            _sysLogBus = sysLogBus;
        }

        IBase_SysLogBusiness _sysLogBus { get; }

        #endregion

        #region 视图功能

        public ActionResult Index()
        {
            return View();
        }

        #endregion

        #region 获取数据

        /// <summary>
        /// 获取日志列表
        /// </summary>
        /// <param name="logContent">日志内容</param>
        /// <param name="logType">日志类型</param>
        /// <param name="opUserName">操作人用户名</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="pagination">分页参数</param>
        /// <returns></returns>
        public ActionResult GetLogList(
            Pagination pagination,
            string logContent,
            string logType,
            string level,
            string opUserName,
            DateTime? startTime,
            DateTime? endTime)
        {
            var dataList = _sysLogBus.GetLogList(pagination, logContent, logType, level, opUserName, startTime, endTime);

            return Content(pagination.BuildTableResult_DataGrid(dataList).ToJson());
        }

        public ActionResult GetLogTypeList()
        {
            List<object> logTypeList = new List<object>();

            Enum.GetNames(typeof(LogType)).ForEach(aName =>
            {
                logTypeList.Add(new { Name = aName, Value = aName });
            });

            return Content(logTypeList.ToJson());
        }

        #endregion

        #region 提交数据

        #endregion
    }
}