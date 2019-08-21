using Coldairarrow.Business.Base_SysManage;
using Coldairarrow.Entity.Base_SysManage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;

namespace Coldairarrow.Web.Areas.Base_SysManage.Controllers
{
    [Area("Base_SysManage")]
    public class Base_AppSecretController : BaseMvcController
    {
        #region DI

        public Base_AppSecretController(IBase_AppSecretBusiness appSecretBus, IPermissionManage permissionManage)
        {
            _appSecretBus = appSecretBus;
            _permissionManage = permissionManage;
        }

        IBase_AppSecretBusiness _appSecretBus { get; }
        IPermissionManage _permissionManage { get; set; }

        #endregion

        #region 视图

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Form(string id)
        {
            var theData = id.IsNullOrEmpty() ? new Base_AppSecret() : _appSecretBus.GetTheData(id);

            return View(theData);
        }

        public ActionResult PermissionForm(string appId)
        {
            ViewData["appId"] = appId;

            return View();
        }

        #endregion

        #region 获取

        public ActionResult GetDataList(Pagination pagination, string keyword)
        {
            var dataList = _appSecretBus.GetDataList(pagination, keyword);

            return Content(pagination.BuildTableResult_DataGrid(dataList).ToJson());
        }

        #endregion

        #region 提交

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="theData">保存的数据</param>
        public ActionResult SaveData(Base_AppSecret theData)
        {
            AjaxResult res;
            if (theData.Id.IsNullOrEmpty())
            {
                theData.Id = IdHelper.GetId();

                res = _appSecretBus.AddData(theData);
            }
            else
            {
                res = _appSecretBus.UpdateData(theData);
            }

            return JsonContent(res.ToJson());
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="theData">删除的数据</param>
        public ActionResult DeleteData(string ids)
        {
            var res = _appSecretBus.DeleteData(ids.ToList<string>());

            return JsonContent(res.ToJson());
        }

        public ActionResult SavePermission(string appId, string permissions)
        {
            _permissionManage.SetAppIdPermission(appId, permissions.ToList<string>());

            return Success();
        }

        #endregion
    }
}