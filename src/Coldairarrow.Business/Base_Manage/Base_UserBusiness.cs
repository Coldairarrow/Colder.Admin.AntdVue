using AutoMapper;
using Coldairarrow.Business.Cache;
using Coldairarrow.Entity;
using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.IBusiness;
using Coldairarrow.Util;
using EFCore.Sharding;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Coldairarrow.Business.Base_Manage
{
    public class Base_UserBusiness : BaseBusiness<Base_User>, IBase_UserBusiness, ITransientDependency
    {
        readonly IOperator _operator;
        readonly IMapper _mapper;
        public Base_UserBusiness(
            IDbAccessor db,
            IBase_UserCache userCache,
            IOperator @operator,
            IMapper mapper
            )
            : base(db)
        {
            _userCache = userCache;
            _operator = @operator;
            _mapper = mapper;
        }
        IBase_UserCache _userCache { get; }
        protected override string _textField => "RealName";

        #region 外部接口

        public async Task<PageResult<Base_UserDTO>> GetDataListAsync(PageInput<Base_UsersInputDTO> input)
        {
            Expression<Func<Base_User, Base_Department, Base_UserDTO>> select = (a, b) => new Base_UserDTO
            {
                DepartmentName = b.Name
            };
            var search = input.Search;
            select = select.BuildExtendSelectExpre();
            var q_User = search.all ? Db.GetIQueryable<Base_User>() : GetIQueryable();
            var q = from a in q_User.AsExpandable()
                    join b in Db.GetIQueryable<Base_Department>() on a.DepartmentId equals b.Id into ab
                    from b in ab.DefaultIfEmpty()
                    select @select.Invoke(a, b);

            q = q.WhereIf(!search.userId.IsNullOrEmpty(), x => x.Id == search.userId);
            if (!search.keyword.IsNullOrEmpty())
            {
                var keyword = $"%{search.keyword}%";
                q = q.Where(x =>
                      EF.Functions.Like(x.UserName, keyword)
                      || EF.Functions.Like(x.RealName, keyword));
            }

            var list = await q.GetPageResultAsync(input);

            await SetProperty(list.Data);

            return list;

            async Task SetProperty(List<Base_UserDTO> users)
            {
                //补充用户角色属性
                List<string> userIds = users.Select(x => x.Id).ToList();
                var userRoles = await (from a in Db.GetIQueryable<Base_UserRole>()
                                       join b in Db.GetIQueryable<Base_Role>() on a.RoleId equals b.Id
                                       where userIds.Contains(a.UserId)
                                       select new
                                       {
                                           a.UserId,
                                           RoleId = b.Id,
                                           b.RoleName
                                       }).ToListAsync();
                users.ForEach(aUser =>
                {
                    var roleList = userRoles.Where(x => x.UserId == aUser.Id);
                    aUser.RoleIdList = roleList.Select(x => x.RoleId).ToList();
                    aUser.RoleNameList = roleList.Select(x => x.RoleName).ToList();
                });
            }
        }

        public async Task<Base_UserDTO> GetTheDataAsync(string id)
        {
            if (id.IsNullOrEmpty())
                return null;
            else
            {
                PageInput<Base_UsersInputDTO> input = new PageInput<Base_UsersInputDTO>
                {
                    Search = new Base_UsersInputDTO
                    {
                        all = true,
                        userId = id
                    }
                };
                return (await GetDataListAsync(input)).Data.FirstOrDefault();
            }
        }

        [DataAddLog(UserLogType.系统用户管理, "RealName", "用户")]
        [DataRepeatValidate(
            new string[] { "UserName" },
            new string[] { "用户名" })]
        [Transactional]
        public async Task AddDataAsync(UserEditInputDTO input)
        {
            await InsertAsync(_mapper.Map<Base_User>(input));
            await SetUserRoleAsync(input.Id, input.RoleIdList);
        }

        [DataEditLog(UserLogType.系统用户管理, "RealName", "用户")]
        [DataRepeatValidate(
            new string[] { "UserName" },
            new string[] { "用户名" })]
        [Transactional]
        public async Task UpdateDataAsync(UserEditInputDTO input)
        {
            if (input.Id == GlobalData.ADMINID && _operator?.UserId != input.Id)
                throw new BusException("禁止更改超级管理员！");

            await UpdateAsync(_mapper.Map<Base_User>(input));
            await SetUserRoleAsync(input.Id, input.RoleIdList);
            await _userCache.UpdateCacheAsync(input.Id);
        }

        [DataDeleteLog(UserLogType.系统用户管理, "RealName", "用户")]
        [Transactional]
        public async Task DeleteDataAsync(List<string> ids)
        {
            if (ids.Contains(GlobalData.ADMINID))
                throw new BusException("超级管理员是内置账号,禁止删除！");

            await DeleteAsync(ids);

            await _userCache.UpdateCacheAsync(ids);
        }

        #endregion

        #region 私有成员

        private async Task SetUserRoleAsync(string userId, List<string> roleIds)
        {
            roleIds = roleIds ?? new List<string>();
            var userRoleList = roleIds.Select(x => new Base_UserRole
            {
                Id = IdHelper.GetId(),
                CreateTime = DateTime.Now,
                UserId = userId,
                RoleId = x
            }).ToList();
            await Db.DeleteAsync<Base_UserRole>(x => x.UserId == userId);
            await Db.InsertAsync(userRoleList);
        }

        #endregion
    }
}