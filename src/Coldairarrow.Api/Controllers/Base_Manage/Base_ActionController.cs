using Coldairarrow.Business.Base_Manage;
using Coldairarrow.Entity;
using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        [HttpPost]
        public async Task<Base_Action> GetTheData(IdInputDTO input)
        {
            return (await _actionBus.GetTheDataAsync(input.id)) ?? new Base_Action();
        }

        [HttpPost]
        public async Task<List<Base_Action>> GetPermissionList(Base_ActionsInputDTO input)
        {
            input.types = new ActionType[] { Entity.ActionType.权限 };

            return await _actionBus.GetDataListAsync(input);
        }

        [HttpPost]
        public async Task<List<Base_Action>> GetAllActionList()
        {
            return await _actionBus.GetDataListAsync(new Base_ActionsInputDTO
            {
                types = new ActionType[] { ActionType.菜单, ActionType.页面, ActionType.权限 }
            });
        }

        [HttpPost]
        public async Task<List<Base_ActionDTO>> GetMenuTreeList(Base_ActionsInputDTO input)
        {
            input.selectable = true;
            input.types = new ActionType[] { ActionType.菜单, ActionType.页面 };

            return await _actionBus.GetTreeDataListAsync(input);
        }

        [HttpPost]
        public async Task<List<Base_ActionDTO>> GetActionTreeList(Base_ActionsInputDTO input)
        {
            input.selectable = false;

            return await _actionBus.GetTreeDataListAsync(input);
        }

        #endregion

        #region 提交

        [HttpPost]
        public async Task SaveData(ActionEditInputDTO input)
        {
            if (input.Id.IsNullOrEmpty())
            {
                InitEntity(input);

                await _actionBus.AddDataAsync(input);
            }
            else
            {
                await _actionBus.UpdateDataAsync(input);
            }
        }

        [HttpPost]
        public async Task DeleteData(List<string> ids)
        {
            await _actionBus.DeleteDataAsync(ids);
        }

        #endregion
    }
}