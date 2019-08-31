using Coldairarrow.Business.Base_SysManage;
using Coldairarrow.Entity.Base_SysManage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Coldairarrow.Web.Areas.Base_SysManage.Controllers
{
    [Area("Base_SysManage")]
    public class Base_DepartmentController : BaseMvcController
    {
        #region DI

        public Base_DepartmentController(IBase_DepartmentBusiness base_DepartmentBus)
        {
            _base_DepartmentBus = base_DepartmentBus;
        }
        IBase_DepartmentBusiness _base_DepartmentBus { get; }

        #endregion

        #region 视图功能

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Form(string id)
        {
            var theData = id.IsNullOrEmpty() ? new Base_Department() : _base_DepartmentBus.GetTheData(id);

            return View(theData);
        }

        #endregion

        #region 获取数据

        public ActionResult GetDataList(Pagination pagination, string departmentName = null)
        {
            var dataList = _base_DepartmentBus.GetDataList(pagination, departmentName);

            return DataTable_Bootstrap(dataList, pagination);
        }

        public ActionResult GetDataList_ZTree()
        {
            var dataList = _base_DepartmentBus.GetDataList(new Pagination());
            var resList = dataList.Select(x => new
            {
                id = x.Id,
                name = x.Name,
                pId = x.ParentId,
                open = true
            });

            return JsonContent(resList.ToJson());
        }

        #endregion

        #region 提交数据

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="theData">保存的数据</param>
        public ActionResult SaveData(Base_Department theData)
        {
            AjaxResult res;
            if (theData.Id.IsNullOrEmpty())
            {
                theData.Id = IdHelper.GetId();

                res = _base_DepartmentBus.AddData(theData);
            }
            else
            {
                res = _base_DepartmentBus.UpdateData(theData);
            }

            return JsonContent(res.ToJson());
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="theData">删除的数据</param>
        public ActionResult DeleteData(string ids)
        {
            var res = _base_DepartmentBus.DeleteData(ids.ToList<string>());

            return JsonContent(res.ToJson());
        }

        #endregion
    }
}