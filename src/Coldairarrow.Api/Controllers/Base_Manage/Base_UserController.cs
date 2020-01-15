using Coldairarrow.Business.Base_Manage;
using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.Base_Manage
{
    /// <seealso cref="Coldairarrow.Api.BaseApiController" />
    [Route("/Base_Manage/[controller]/[action]")]
    public class Base_UserController : BaseApiController
    {
        #region DI

        public Base_UserController(IBase_UserBusiness userBus)
        {
            _userBus = userBus;
        }

        IBase_UserBusiness _userBus { get; }

        #endregion

        #region 获取

        [HttpPost]
        public async Task<AjaxResult<List<Base_UserDTO>>> GetDataList(Pagination pagination, string keyword)
        {
            var dataList = await _userBus.GetDataListAsync(pagination, false, null, keyword);

            return DataTable(dataList, pagination);
        }

        [HttpPost]
        public async Task<Base_UserDTO> GetTheData(string id)
        {
            return await _userBus.GetTheDataAsync(id) ?? new Base_UserDTO();
        }

        [HttpPost]
        public async Task<List<SelectOption>> GetOptionList(string selectedValueJson, string q)
        {
            return await _userBus.GetOptionListAsync(selectedValueJson, q);
        }

        #endregion

        #region 提交

        [HttpPost]
        public async Task SaveData(Base_User theData, string newPwd, string roleIdsJson)
        {
            if (!newPwd.IsNullOrEmpty())
                theData.Password = newPwd.ToMD5String();
            var roleIds = roleIdsJson?.ToList<string>() ?? new List<string>();
            if (theData.Id.IsNullOrEmpty())
            {
                theData.InitEntity();

                await _userBus.AddDataAsync(theData, roleIds);
            }
            else
            {
                await _userBus.UpdateDataAsync(theData, roleIds);
            }
        }

        [HttpPost]
        public async Task DeleteData(string ids)
        {
            await _userBus.DeleteDataAsync(ids.ToList<string>());
        }

        #endregion
    }
}