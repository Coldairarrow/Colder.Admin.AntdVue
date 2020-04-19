using AutoMapper;
using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using EFCore.Sharding;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coldairarrow.Business.Base_Manage
{
    public class Base_RoleBusiness : BaseBusiness<Base_Role>, IBase_RoleBusiness, ITransientDependency
    {
        readonly IMapper _mapper;
        public Base_RoleBusiness(IRepository repository, IMapper mapper)
            : base(repository)
        {
            _mapper = mapper;
        }

        #region 外部接口

        public async Task<PageResult<Base_RoleOutputDTO>> GetDataListAsync(PageInput<RolesInputDTO> input)
        {
            var where = LinqHelper.True<Base_Role>();
            var search = input.Search;
            if (!search.roleId.IsNullOrEmpty())
                where = where.And(x => x.Id == search.roleId);
            if (!search.roleName.IsNullOrEmpty())
                where = where.And(x => x.RoleName.Contains(search.roleName));

            var list = await GetIQueryable().Where(where).GetPageResultAsync(input);
            var dtoList = list.Data.Select(x => _mapper.Map<Base_RoleOutputDTO>(x)).ToList();

            await SetProperty(dtoList);

            return new PageResult<Base_RoleOutputDTO> { Data = dtoList, Total = list.Total };

            async Task SetProperty(List<Base_RoleOutputDTO> _list)
            {
                var allActionIds = await Service.GetIQueryable<Base_Action>().Select(x => x.Id).ToListAsync();

                var ids = _list.Select(x => x.Id).ToList();
                var roleActions = await Service.GetIQueryable<Base_RoleAction>()
                    .Where(x => ids.Contains(x.RoleId))
                    .ToListAsync();
                _list.ForEach(aData =>
                {
                    if (aData.RoleName == RoleTypes.超级管理员.ToString())
                        aData.Actions = allActionIds;
                    else
                        aData.Actions = roleActions.Where(x => x.RoleId == aData.Id).Select(x => x.ActionId).ToList();
                });
            }
        }

        public async Task<Base_RoleOutputDTO> GetTheDataAsync(string id)
        {
            return (await GetDataListAsync(new PageInput<RolesInputDTO> { Search = new RolesInputDTO { roleId = id } })).Data.FirstOrDefault();
        }

        [DataAddLog(UserLogType.系统角色管理, "RoleName", "角色")]
        [DataRepeatValidate(new string[] { "RoleName" }, new string[] { "角色名" })]
        public async Task AddDataAsync(RoleEditInputDTO input)
        {
            await InsertAsync(_mapper.Map<Base_Role>(input));
            await SetRoleActionAsync(input.Id, input.actions);
        }

        [DataEditLog(UserLogType.系统角色管理, "RoleName", "角色")]
        [DataRepeatValidate(new string[] { "RoleName" }, new string[] { "角色名" })]
        [Transactional]
        public async Task UpdateDataAsync(RoleEditInputDTO input)
        {
            await UpdateAsync(_mapper.Map<Base_Role>(input));
            await SetRoleActionAsync(input.Id, input.actions);
        }

        [DataDeleteLog(UserLogType.系统角色管理, "RoleName", "角色")]
        [Transactional]
        public async Task DeleteDataAsync(List<string> ids)
        {
            await DeleteAsync(ids);
            await Service.DeleteAsync<Base_RoleAction>(x => ids.Contains(x.RoleId));
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
            await Service.DeleteAsync<Base_RoleAction>(x => x.RoleId == roleId);
            await Service.InsertAsync(roleActions);
        }

        #endregion

        #region 数据模型

        #endregion
    }
}