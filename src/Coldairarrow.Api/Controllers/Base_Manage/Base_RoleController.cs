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
    public class Base_RoleController : BaseApiController
    {
        #region DI

        public Base_RoleController(IBase_RoleBusiness roleBus)
        {
            _roleBus = roleBus;
        }

        IBase_RoleBusiness _roleBus { get; }

        #endregion

        #region 获取

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<AjaxResult<List<Base_RoleDTO>>> GetDataList(Pagination pagination, string keyword)
        {
            var dataList = _roleBus.GetDataList(pagination, keyword);

            return Content(pagination.BuildTableResult_AntdVue(dataList).ToJson());
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="id">id主键</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<AjaxResult<Base_RoleDTO>> GetTheData(string id)
        {
            var theData = _roleBus.GetTheData(id) ?? new Base_RoleDTO();

            return Success(theData);
        }

        #endregion

        #region 提交

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="theData">保存的数据</param>
        /// <param name="actionsJson">权限值JSON</param>
        [HttpPost]
        public ActionResult<AjaxResult> SaveData(Base_Role theData, string actionsJson)
        {
            AjaxResult res;
            var actionList = actionsJson?.ToList<string>();
            if (theData.Id.IsNullOrEmpty())
            {
                theData.Id = IdHelper.GetId();
                theData.CreateTime = DateTime.Now;
                theData.CreatorId = Operator.UserId;
                //theData.CreatorRealName = Operator.Property.RealName;

                res = _roleBus.AddData(theData, actionList);
            }
            else
            {
                res = _roleBus.UpdateData(theData, actionList);
            }

            return JsonContent(res.ToJson());
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="ids">id数组,JSON数组</param>
        [HttpPost]
        public ActionResult<AjaxResult> DeleteData(string ids)
        {
            var res = _roleBus.DeleteData(ids.ToList<string>());

            return JsonContent(res.ToJson());
        }

        #endregion
    }
}