using Coldairarrow.Business.Base_Manage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.Base_Manage
{
    /// <summary>
    /// 应用密钥
    /// </summary>
    /// <seealso cref="Coldairarrow.Api.BaseApiController" />
    [Route("/Base_Manage/[controller]/[action]")]
    public class Base_RoleController : BaseApiController
    {
        #region DI

        public Base_RoleController(IBase_RoleBusiness roleBus)
        {
            _roleBus = roleBus;
        }

        IBase_RoleBusiness _roleBus { get; }

        #endregion

        #region 获取

        [HttpPost]
        public async Task<PageResult<Base_RoleInfoDTO>> GetDataList(PageInput<RolesInputDTO> input)
        {
            return await _roleBus.GetDataListAsync(input);
        }

        [HttpPost]
        public async Task<Base_RoleInfoDTO> GetTheData(IdInputDTO input)
        {
            return await _roleBus.GetTheDataAsync(input.id) ?? new Base_RoleInfoDTO();
        }

        #endregion

        #region 提交

        [HttpPost]
        public async Task SaveData(Base_RoleInfoDTO input)
        {
            if (input.Id.IsNullOrEmpty())
            {
                InitEntity(input);

                await _roleBus.AddDataAsync(input);
            }
            else
            {
                await _roleBus.UpdateDataAsync(input);
            }
        }

        [HttpPost]
        public async Task DeleteData(List<string> ids)
        {
            await _roleBus.DeleteDataAsync(ids);
        }

        #endregion
    }
}