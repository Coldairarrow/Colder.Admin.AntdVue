using Coldairarrow.Business.Base_SysManage;
using Coldairarrow.Entity.Base_SysManage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;

namespace Coldairarrow.Web.Areas.Base_SysManage.Controllers
{
    [Area("Base_SysManage")]
    public class Base_SysRoleController : BaseMvcController
    {
        public Base_SysRoleController(IBase_SysRoleBusiness sysRoleBus, IPermissionManage permissionManage)
        {
            _sysRoleBus = sysRoleBus;
            _permissionManage = permissionManage;
        }

        IBase_SysRoleBusiness _sysRoleBus { get; }
        IPermissionManage _permissionManage { get; set; }

        #region 视图功能

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Form(string id)
        {
            var theData = id.IsNullOrEmpty() ? new Base_SysRole() : _sysRoleBus.GetTheData(id);

            return View(theData);
        }

        public ActionResult PermissionForm(string roleId)
        {
            ViewData["roleId"] = roleId;

            return View();
        }

        #endregion

        #region 获取数据

        public ActionResult GetDataList(Pagination pagination, string roleName)
        {
            var dataList = _sysRoleBus.GetDataList(pagination, null, roleName);

            return DataTable_Bootstrap(dataList, pagination);
        }

        /// <summary>
        /// 获取角色列表
        /// 注：无分页
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDataList_NoPagin()
        {
            var dataList = _sysRoleBus.GetDataList(new Pagination());

            return Content(dataList.ToJson());
        }

        #endregion

        #region 提交数据

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="theData">保存的数据</param>
        public ActionResult SaveData(Base_SysRole theData)
        {
            AjaxResult res;
            if (theData.Id.IsNullOrEmpty())
            {
                theData.Id = IdHelper.GetId();

                res = _sysRoleBus.AddData(theData);
            }
            else
            {
                res = _sysRoleBus.UpdateData(theData);
            }

            return JsonContent(res.ToJson());
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="theData">删除的数据</param>
        public ActionResult DeleteData(string ids)
        {
            var res = _sysRoleBus.DeleteData(ids.ToList<string>());

            if (res.Success)
                _permissionManage.ClearUserPermissionCache();

            return JsonContent(res.ToJson());
        }

        /// <summary>
        /// 设置角色权限
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <param name="permissions">权限值</param>
        /// <returns></returns>
        public ActionResult SavePermission(string roleId, string permissions)
        {
            _sysRoleBus.SavePermission(roleId, permissions.ToList<string>());

            return Success();
        }

        #endregion
    }
}