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
        public async Task<Base_Department> GetTheData(string id)
        {
            return await _departmentBus.GetTheDataAsync(id) ?? new Base_Department();
        }

        [HttpPost]
        public async Task<List<Base_DepartmentTreeDTO>> GetTreeDataList(string parentId)
        {
            return await _departmentBus.GetTreeDataListAsync(parentId);
        }

        #endregion

        #region 提交

        [HttpPost]
        public async Task SaveData(Base_Department theData)
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
        }

        [HttpPost]
        public async Task DeleteData(string ids)
        {
            await _departmentBus.DeleteDataAsync(ids.ToList<string>());
        }

        #endregion
    }
}