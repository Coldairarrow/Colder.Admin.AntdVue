using Coldairarrow.Business.Base_Manage;
using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost]
        public ActionResult<AjaxResult<List<Base_Action>>> GetAllActionList()
        {
            var dataList = _actionBus.GetDataList(new Pagination(), null, null, new List<int> { 0, 1, 2 });

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
            var dataList = _actionBus.GetTreeDataList(keyword, new List<int> { 0, 1 }, true);

            return Success(dataList);
        }

        /// <summary>
        /// 获取全心爱你树列表
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<AjaxResult<List<Base_ActionDTO>>> GetActionTreeList(string keyword)
        {
            var dataList = _actionBus.GetTreeDataList(keyword, null, false);

            return Success(dataList);
        }

        #endregion

        #region 提交

        [HttpPost]
        public ActionResult<AjaxResult> SaveData(Base_Action theData, string permissionListJson)
        {
            AjaxResult res;
            var permissionList = permissionListJson?.ToList<Base_Action>();
            if (theData.Id.IsNullOrEmpty())
            {
                theData.InitEntity();

                res = _actionBus.AddData(theData, permissionList);
            }
            else
            {
                res = _actionBus.UpdateData(theData, permissionList);
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

        ///// <summary>
        ///// 保存权限
        ///// </summary>
        ///// <returns></returns>
        ///// <param name="parentId">父级Id</param>
        ///// <param name="permissionListJson">权限列表JSON数组</param>
        //[HttpPost]
        //public ActionResult<AjaxResult> SavePermission(string parentId, string permissionListJson)
        //{
        //    var res = _actionBus.SavePermission(parentId, permissionListJson?.ToList<Base_Action>());

        //    return JsonContent(res.ToJson());
        //}

        #endregion
    }
}