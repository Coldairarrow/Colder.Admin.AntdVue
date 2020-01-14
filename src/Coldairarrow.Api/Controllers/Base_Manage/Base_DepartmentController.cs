using Coldairarrow.Business.Base_Manage;
using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.Base_Manage
{
    [Route("/Base_Manage/[controller]/[action]")]
    public class Base_DepartmentController : BaseApiController
    {
        #region DI

        public Base_DepartmentController(IBase_DepartmentBusiness departmentBus)
        {
            _departmentBus = departmentBus;
        }

        IBase_DepartmentBusiness _departmentBus { get; }

        #endregion

        #region 获取

        [HttpPost]
        public async Task<AjaxResult<Base_Department>> GetTheData(string id)
        {
            var theData = await _departmentBus.GetTheDataAsync(id) ?? new Base_Department();

            return Success(theData);
        }

        [HttpPost]
        public async Task<AjaxResult<List<Base_DepartmentTreeDTO>>> GetTreeDataList(string parentId)
        {
            var dataList = await _departmentBus.GetTreeDataListAsync(parentId);

            return Success(dataList);
        }

        #endregion

        #region 提交

        [HttpPost]
        public async Task<AjaxResult> SaveData(Base_Department theData)
        {
            if (theData.Id.IsNullOrEmpty())
            {
                theData.InitEntity();

                await _departmentBus.AddDataAsync(theData);
            }
            else
            {
                await _departmentBus.UpdateDataAsync(theData);
            }

            return Success();
        }

        [HttpPost]
        public async Task<AjaxResult> DeleteData(string ids)
        {
            await _departmentBus.DeleteDataAsync(ids.ToList<string>());

            return Success();
        }

        #endregion
    }
}