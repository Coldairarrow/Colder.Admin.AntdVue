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
    public class Base_ActionBusiness : BaseBusiness<Base_Action>, IBase_ActionBusiness, ITransientDependency
    {
        readonly IMapper _mapper;
        public Base_ActionBusiness(IRepository repository, IMapper mapper)
            : base(repository)
        {
            _mapper = mapper;
        }

        #region 外部接口

        public async Task<List<Base_Action>> GetDataListAsync(Base_ActionsInputDTO input)
        {
            var q = input.q ?? GetIQueryable();
            var where = LinqHelper.True<Base_Action>();
            if (!input.keyword.IsNullOrEmpty())
            {
                where = where.And(x => EF.Functions.Like(x.Name, $"%{input.keyword}%"));
            }
            if (!input.parentId.IsNullOrEmpty())
                where = where.And(x => x.ParentId == input.parentId);
            if (input.types?.Count > 0)
                where = where.And(x => input.types.Contains((int)x.Type));

            return await q.Where(where).OrderBy(x => x.Sort).ToListAsync();
        }

        public async Task<List<Base_ActionDTO>> GetTreeDataListAsync(Base_ActionsTreeInputDTO input)
        {
            var where = LinqHelper.True<Base_Action>();
            if (!input.types.IsNullOrEmpty())
                where = where.And(x => input.types.Contains((int)x.Type));
            var qList = await (input.q ?? GetIQueryable()).Where(where).OrderBy(x => x.Sort).ToListAsync();

            var treeList = qList.Select(x => new Base_ActionDTO
            {
                Id = x.Id,
                NeedAction = x.NeedAction,
                Text = x.Name,
                ParentId = x.ParentId,
                Type = (int)x.Type,
                Url = x.Url,
                Value = x.Id,
                Icon = x.Icon,
                Sort = x.Sort,
                selectable = input.selectable
            }).ToList();

            //菜单节点中,若子节点为空则移除父节点
            if (input.checkEmptyChildren)
                treeList = treeList.Where(x => x.Type != 0 || TreeHelper.GetChildren(treeList, x, false).Count > 0).ToList();

            await SetProperty(treeList);

            return TreeHelper.BuildTree(treeList);

            async Task SetProperty(List<Base_ActionDTO> _list)
            {
                var ids = _list.Select(x => x.Id).ToList();
                var allPermissions = await GetIQueryable()
                    .Where(x => ids.Contains(x.ParentId) && (int)x.Type == 2)
                    .ToListAsync();

                _list.ForEach(aData =>
                {
                    var permissionValues = allPermissions
                        .Where(x => x.ParentId == aData.Id)
                        .Select(x => $"{x.Name}({x.Value})")
                        .ToList();

                    aData.PermissionValues = permissionValues;
                });
            }
        }

        public async Task<Base_Action> GetTheDataAsync(string id)
        {
            return await GetEntityAsync(id);
        }

        [Transactional]
        public async Task AddDataAsync(ActionEditInputDTO input)
        {
            await InsertAsync(_mapper.Map<Base_Action>(input));
            await SavePermissionAsync(input.Id, input.permissionList);
        }

        [Transactional]
        public async Task UpdateDataAsync(ActionEditInputDTO input)
        {
            await UpdateAsync(_mapper.Map<Base_Action>(input));
            await SavePermissionAsync(input.Id, input.permissionList);
        }

        public async Task DeleteDataAsync(List<string> ids)
        {
            await DeleteAsync(ids);
        }

        public async Task SavePermissionAsync(string parentId, List<Base_Action> permissionList)
        {
            permissionList.ForEach(aData =>
            {
                aData.Id = IdHelper.GetId();
                aData.CreateTime = DateTime.Now;
                aData.CreatorId = null;
                aData.ParentId = parentId;
                aData.NeedAction = true;
            });
            //删除原来
            await DeleteAsync(x => x.ParentId == parentId && (int)x.Type == 2);
            //新增
            await InsertAsync(permissionList);

            //权限值必须唯一
            var repeatValues = await GetIQueryable()
                .GroupBy(x => x.Value)
                .Where(x => !string.IsNullOrEmpty(x.Key) && x.Count() > 1)
                .Select(x => x.Key)
                .ToListAsync();
            if (repeatValues.Count > 0)
                throw new Exception($"以下权限值重复:{string.Join(",", repeatValues)}");
        }

        #endregion

        #region 私有成员

        #endregion
    }

}