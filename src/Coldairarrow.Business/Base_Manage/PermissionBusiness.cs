using Coldairarrow.Entity;
using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using EFCore.Sharding;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coldairarrow.Business.Base_Manage
{
    class PermissionBusiness : BaseBusiness<Base_Action>, IPermissionBusiness, ITransientDependency
    {
        public PermissionBusiness(IDbAccessor db, IBase_ActionBusiness actionBus, IBase_UserBusiness userBus)
            : base(db)
        {
            _actionBus = actionBus;
            _userBus = userBus;
        }
        IBase_ActionBusiness _actionBus { get; }
        IBase_UserBusiness _userBus { get; }

        async Task<string[]> GetUserActionIds(string userId)
        {
            var where = LinqHelper.False<Base_Action>();
            var theUser = await _userBus.GetTheDataAsync(userId);

            //不需要权限的菜单
            where = where.Or(x => x.NeedAction == false);

            if (userId == GlobalData.ADMINID || theUser.RoleType.HasFlag(RoleTypes.超级管理员))
                where = where.Or(x => true);
            else
            {
                var actionIds = from a in Db.GetIQueryable<Base_UserRole>()
                                join b in Db.GetIQueryable<Base_RoleAction>() on a.RoleId equals b.RoleId
                                where a.UserId == userId
                                select b.ActionId;

                where = where.Or(x => actionIds.Contains(x.Id));
            }

            return await GetIQueryable().Where(where).Select(x => x.Id).ToArrayAsync();
        }

        public async Task<List<Base_ActionDTO>> GetUserMenuListAsync(string userId)
        {
            var actionIds = await GetUserActionIds(userId);
            return await _actionBus.GetTreeDataListAsync(new Base_ActionsInputDTO
            {
                types = new ActionType[] { ActionType.菜单, ActionType.页面 },
                ActionIds = actionIds,
                checkEmptyChildren = true
            });
        }

        public async Task<List<string>> GetUserPermissionValuesAsync(string userId)
        {
            var actionIds = await GetUserActionIds(userId);
            return (await _actionBus
                .GetDataListAsync(new Base_ActionsInputDTO
                {
                    types = new ActionType[] { ActionType.权限 },
                    ActionIds = actionIds
                }))
                .Select(x => x.Value)
                .ToList();
        }
    }
}
