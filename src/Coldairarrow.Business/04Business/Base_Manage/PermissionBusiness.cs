using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Coldairarrow.Entity.Base_Manage.EnumType;

namespace Coldairarrow.Business.Base_Manage
{
    class PermissionBusiness : BaseBusiness<Base_Action>, IPermissionBusiness, IDependency
    {
        public PermissionBusiness(IBase_ActionBusiness actionBus, IBase_UserBusiness userBus)
        {
            _actionBus = actionBus;
            _userBus = userBus;
        }
        IBase_ActionBusiness _actionBus { get; }
        IBase_UserBusiness _userBus { get; }

        async Task<IQueryable<Base_Action>> GetIQ(string userId)
        {
            var where = LinqHelper.False<Base_Action>();
            var theUser = await _userBus.GetTheDataAsync(userId);

            //不需要权限的菜单
            where = where.Or(x => x.NeedAction == false);

            if (userId == GlobalSwitch.AdminId || theUser.RoleType.HasFlag(RoleTypeEnum.超级管理员))
                where = where.Or(x => true);
            else
            {
                var actionIds = from a in Service.GetIQueryable<Base_UserRole>()
                                join b in Service.GetIQueryable<Base_RoleAction>() on a.RoleId equals b.RoleId
                                where a.UserId == userId
                                select b.ActionId;

                where = where.Or(x => actionIds.Contains(x.Id));
            }

            return GetIQueryable().Where(where);
        }

        public async Task<List<Base_ActionDTO>> GetUserMenuListAsync(string userId)
        {
            var q = await GetIQ(userId);

            return await _actionBus.GetTreeDataListAsync(null, new List<int> { 0, 1 }, false, q, true);
        }

        public async Task<List<string>> GetUserPermissionValuesAsync(string userId)
        {
            var q = await GetIQ(userId);
            return (await _actionBus
                .GetDataListAsync(new Pagination(), null, null, new List<int> { 2 }, q))
                .Select(x => x.Value)
                .ToList();
        }
    }
}
