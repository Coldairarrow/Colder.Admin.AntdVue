using AutoMapper;
using Coldairarrow.Business.Cache;
using Coldairarrow.Entity.Base_Manage;
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
            IRepository repository,
            IBase_UserCache userCache,
            IOperator @operator,
            IMapper mapper
            )
            : base(repository)
        {
            _userCache = userCache;
            _operator = @operator;
            _mapper = mapper;
        }
        IBase_UserCache _userCache { get; }
        protected override string _textField => "RealName";

        #region 外部接口

        public async Task<PageResult<Base_UserDTO>> GetDataListAsync(Base_UsersInputDTO input)
        {
            Expression<Func<Base_User, Base_Department, Base_UserDTO>> select = (a, b) => new Base_UserDTO
            {
                DepartmentName = b.Name
            };
            select = select.BuildExtendSelectExpre();
            var q_User = input.all ? Service.GetIQueryable<Base_User>() : GetIQueryable();
            var q = from a in q_User.AsExpandable()
                    join b in Service.GetIQueryable<Base_Department>() on a.DepartmentId equals b.Id into ab
                    from b in ab.DefaultIfEmpty()
                    select @select.Invoke(a, b);

            var where = LinqHelper.True<Base_UserDTO>();
            if (!input.userId.IsNullOrEmpty())
                where = PredicateBuilder.And(where, x => x.Id == input.userId);
            if (!input.keyword.IsNullOrEmpty())
            {
                where = where.And(x =>
                    EF.Functions.Like(x.UserName, input.keyword)
                    || EF.Functions.Like(x.RealName, input.keyword));
            }

            var list = await q.Where(where).GetPageResultAsync(input);

            await SetProperty(list.Data);

            return list;

            async Task SetProperty(List<Base_UserDTO> users)
            {
                //补充用户角色属性
                List<string> userIds = users.Select(x => x.Id).ToList();
                var userRoles = await (from a in Service.GetIQueryable<Base_UserRole>()
                                       join b in Service.GetIQueryable<Base_Role>() on a.RoleId equals b.Id
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
                return (await GetDataListAsync(new Base_UsersInputDTO { all = true, userId = id })).Data.FirstOrDefault();
        }

        [DataAddLog(UserLogType.系统用户管理, "RealName", "用户")]
        [DataRepeatValidate(
            new string[] { "UserName" },
            new string[] { "用户名" })]
        [Transactional]
        public async Task AddDataAsync(UserEditInputDTO input)
        {
            await InsertAsync(_mapper.Map<Base_User>(input));
            await SetUserRoleAsync(input.Id, input.roleIds);
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
            await SetUserRoleAsync(input.Id, input.roleIds);
            await _userCache.UpdateCacheAsync(input.Id);
        }

        [DataDeleteLog(UserLogType.系统用户管理, "RealName", "用户")]
        [Transactional]
        public async Task DeleteDataAsync(List<string> ids)
        {
            if (ids.Contains(GlobalData.ADMINID))
                throw new BusException("超级管理员是内置账号,禁止删除！");
            var userIds = await GetIQueryable().Where(x => ids.Contains(x.Id)).Select(x => x.Id).ToListAsync();

            await DeleteAsync(ids);

            await _userCache.UpdateCacheAsync(ids);
        }

        #endregion

        #region 私有成员

        private async Task SetUserRoleAsync(string userId, List<string> roleIds)
        {
            var userRoleList = roleIds.Select(x => new Base_UserRole
            {
                Id = IdHelper.GetId(),
                CreateTime = DateTime.Now,
                UserId = userId,
                RoleId = x
            }).ToList();
            await Service.Delete_SqlAsync<Base_UserRole>(x => x.UserId == userId);
            await Service.InsertAsync(userRoleList);
        }

        #endregion
    }
}