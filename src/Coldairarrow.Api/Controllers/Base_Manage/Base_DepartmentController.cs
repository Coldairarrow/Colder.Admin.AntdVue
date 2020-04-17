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
        public async Task<Base_Department> GetTheData(IdInputDTO input)
        {
            return await _departmentBus.GetTheDataAsync(input.id) ?? new Base_Department();
        }

        [HttpPost]
        public async Task<List<Base_DepartmentTreeDTO>> GetTreeDataList(DepartmentsTreeInputDTO input)
        {
            return await _departmentBus.GetTreeDataListAsync(input);
        }

        #endregion

        #region 提交

        [HttpPost]
        public async Task SaveData(Base_Department theData)
        {
            if (theData.Id.IsNullOrEmpty())
            {
                InitEntity(theData);

                await _departmentBus.AddDataAsync(theData);
            }
            else
            {
                await _departmentBus.UpdateDataAsync(theData);
            }
        }

        [HttpPost]
        public async Task DeleteData(List<string> ids)
        {
            await _departmentBus.DeleteDataAsync(ids);
        }

        #endregion
    }
}