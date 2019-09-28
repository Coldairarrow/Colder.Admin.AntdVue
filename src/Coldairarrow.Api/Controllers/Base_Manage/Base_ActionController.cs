using Coldairarrow.Business.Base_Manage;
using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Coldairarrow.Api.Controllers.Base_Manage
{
    /// <summary>
    /// 系统权限
    /// </summary>
    /// <seealso cref="Coldairarrow.Api.BaseApiController" />
    [Route("/Base_Manage/[controller]/[action]")]
    public class Base_ActionController : BaseApiController
    {
        #region DI

        public Base_ActionController(IBase_ActionBusiness actionBus)
        {
            _actionBus = actionBus;
        }

        IBase_ActionBusiness _actionBus { get; }

        #endregion

        #region 获取

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="id">id主键</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<AjaxResult<Base_Action>> GetTheData(string id)
        {
            var theData = _actionBus.GetTheData(id) ?? new Base_Action();

            return Success(theData);
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="parentId">父级Id</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<AjaxResult<List<Base_Action>>> GetPermissionList(string parentId)
        {
            var dataList = _actionBus.GetDataList(new Pagination(), null, parentId, new List<int> { 2 });

            return Success(dataList);
        }

        /// <summary>
        /// 获取菜单树列表
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<AjaxResult<List<Base_ActionDTO>>> GetMenuTreeList(string keyword)
        {
            var dataList = _actionBus.GetTreeDataList(keyword, new List<int> { 0, 1 });

            return Success(dataList);
        }

        #endregion

        #region 提交

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="theData">保存的数据</param>
        [HttpPost]
        public ActionResult<AjaxResult> SaveData(Base_Action theData)
        {
            AjaxResult res;
            if (theData.Id.IsNullOrEmpty())
            {
                theData.Id = IdHelper.GetId();
                theData.CreateTime = DateTime.Now;
                theData.CreatorId = Operator.UserId;
                //theData.CreatorRealName = Operator.Property.RealName;

                res = _actionBus.AddData(theData);
            }
            else
            {
                res = _actionBus.UpdateData(theData);
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
            var res = _actionBus.DeleteData(ids.ToList<string>());

            return JsonContent(res.ToJson());
        }

        #endregion
    }
}