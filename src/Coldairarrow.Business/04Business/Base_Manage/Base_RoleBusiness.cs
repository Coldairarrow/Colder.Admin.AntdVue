using AutoMapper;
using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Coldairarrow.Entity.Base_Manage.EnumType;

namespace Coldairarrow.Business.Base_Manage
{
    public class Base_RoleBusiness : BaseBusiness<Base_Role>, IBase_RoleBusiness, IDependency
    {
        #region 外部接口

        public async Task<List<Base_RoleDTO>> GetDataListAsync(Pagination pagination, string roleId = null, string roleName = null)
        {
            var where = LinqHelper.True<Base_Role>();
            if (!roleId.IsNullOrEmpty())
                where = where.And(x => x.Id == roleId);
            if (!roleName.IsNullOrEmpty())
                where = where.And(x => x.RoleName.Contains(roleName));

            var list = (await GetIQueryable()
                .Where(where)
                .GetPagination(pagination)
                .ToListAsync())
                .Select(x => Mapper.Map<Base_RoleDTO>(x))
                .ToList();

            await SetProperty(list);

            return list;

            async Task SetProperty(List<Base_RoleDTO> _list)
            {
                var allActionIds = await Service.GetIQueryable<Base_Action>().Select(x => x.Id).ToListAsync();

                var ids = _list.Select(x => x.Id).ToList();
                var roleActions = await Service.GetIQueryable<Base_RoleAction>()
                    .Where(x => ids.Contains(x.RoleId))
                    .ToListAsync();
                _list.ForEach(aData =>
                {
                    if (aData.RoleName == RoleTypeEnum.超级管理员.ToString())
                        aData.Actions = allActionIds;
                    else
                        aData.Actions = roleActions.Where(x => x.RoleId == aData.Id).Select(x => x.ActionId).ToList();
                });
            }
        }

        public async Task<Base_RoleDTO> GetTheDataAsync(string id)
        {
            return (await GetDataListAsync(new Pagination(), id)).FirstOrDefault();
        }

        [DataAddLog(LogType.系统角色管理, "RoleName", "角色")]
        [DataRepeatValidate(new string[] { "RoleName" }, new string[] { "角色名" })]
        public async Task AddDataAsync(Base_Role newData, List<string> actions)
        {
            var res = await RunTransactionAsync(async () =>
            {
                await InsertAsync(newData);
                await SetRoleActionAsync(newData.Id, actions);
            });
            if (!res.Success)
                throw new Exception("系统异常,请重试", res.ex);
        }

        [DataEditLog(LogType.系统角色管理, "RoleName", "角色")]
        [DataRepeatValidate(new string[] { "RoleName" }, new string[] { "角色名" })]
        public async Task UpdateDataAsync(Base_Role theData, List<string> actions)
        {
            var res = await RunTransactionAsync(async () =>
            {
                await UpdateAsync(theData);
                await SetRoleActionAsync(theData.Id, actions);
            });
            if (!res.Success)
                throw new Exception("系统异常,请重试", res.ex);
        }

        [DataDeleteLog(LogType.系统角色管理, "RoleName", "角色")]
        public async Task DeleteDataAsync(List<string> ids)
        {
            var res = await RunTransactionAsync(async () =>
            {
                await DeleteAsync(ids);
                await Service.Delete_SqlAsync<Base_RoleAction>(x => ids.Contains(x.Id));
            });
            if (!res.Success)
                throw new Exception("系统异常,请重试", res.ex);
        }

        #endregion

        #region 私有成员

        private async Task SetRoleActionAsync(string roleId, List<string> actions)
        {
            var roleActions = (actions ?? new List<string>())
                .Select(x => new Base_RoleAction
                {
                    Id = IdHelper.GetId(),
                    ActionId = x,
                    CreateTime = DateTime.Now,
                    RoleId = roleId
                }).ToList();
            await Service.Delete_SqlAsync<Base_RoleAction>(x => x.RoleId == roleId);
            await Service.InsertAsync(roleActions);
        }

        #endregion

        #region 数据模型

        #endregion
    }
}