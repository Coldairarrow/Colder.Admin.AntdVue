using Coldairarrow.Business.Base_Manage;
using Coldairarrow.Entity;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.Base_Manage
{
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
        public async Task<PageResult<Base_UserDTO>> GetDataList(PageInput<Base_UsersInputDTO> input)
        {
            return await _userBus.GetDataListAsync(input);
        }

        [HttpPost]
        public async Task<Base_UserDTO> GetTheData(IdInputDTO input)
        {
            return await _userBus.GetTheDataAsync(input.id) ?? new Base_UserDTO();
        }

        [HttpPost]
        public async Task<List<SelectOption>> GetOptionList(OptionListInputDTO input)
        {
            return await _userBus.GetOptionListAsync(input);
        }

        #endregion

        #region 提交

        [HttpPost]
        public async Task SaveData(UserEditInputDTO input)
        {
            if (!input.newPwd.IsNullOrEmpty())
                input.Password = input.newPwd.ToMD5String();
            if (input.Id.IsNullOrEmpty())
            {
                InitEntity(input);

                await _userBus.AddDataAsync(input);
            }
            else
            {
                await _userBus.UpdateDataAsync(input);
            }
        }

        [HttpPost]
        public async Task DeleteData(List<string> ids)
        {
            await _userBus.DeleteDataAsync(ids);
        }

        #endregion
    }
}