using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using EFCore.Sharding;
using LinqKit;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coldairarrow.Business.Base_Manage
{
    class PermissionBusiness : BaseBusiness<Base_Action>, IPermissionBusiness, ITransientDependency
    {
        public PermissionBusiness(IRepository repository, IBase_ActionBusiness actionBus, IBase_UserBusiness userBus)
            : base(repository)
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

            if (userId == GlobalData.ADMINID || theUser.RoleType.HasFlag(RoleTypes.超级管理员))
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
            return await _actionBus.GetTreeDataListAsync(new Base_ActionsTreeInputDTO
            {
                types = new List<int> { 0, 1 },
                q = q,
                checkEmptyChildren = true
            });
        }

        public async Task<List<string>> GetUserPermissionValuesAsync(string userId)
        {
            var q = await GetIQ(userId);
            return (await _actionBus
                .GetDataListAsync(new Base_ActionsInputDTO
                {
                    types = new List<int> { 2 },
                    q = q
                }))
                .Select(x => x.Value)
                .ToList();
        }
    }
}
