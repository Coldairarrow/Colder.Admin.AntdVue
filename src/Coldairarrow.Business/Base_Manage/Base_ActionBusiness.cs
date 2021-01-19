using AutoMapper;
using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using EFCore.Sharding;
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
        public Base_ActionBusiness(IDbAccessor db, IMapper mapper)
            : base(db)
        {
            _mapper = mapper;
        }

        #region 外部接口

        public async Task<List<Base_Action>> GetDataListAsync(Base_ActionsInputDTO input)
        {
            var q = GetIQueryable();
            q = q
                .WhereIf(!input.parentId.IsNullOrEmpty(), x => x.ParentId == input.parentId)
                .WhereIf(input.types?.Length > 0, x => input.types.Contains(x.Type))
                .WhereIf(input.ActionIds?.Length > 0, x => input.ActionIds.Contains(x.Id))
                ;

            return await q.OrderBy(x => x.Sort).ToListAsync();
        }

        public async Task<List<Base_ActionDTO>> GetTreeDataListAsync(Base_ActionsInputDTO input)
        {
            var qList = await GetDataListAsync(input);

            var treeList = qList.Select(x => new Base_ActionDTO
            {
                Id = x.Id,
                NeedAction = x.NeedAction,
                Text = x.Name,
                ParentId = x.ParentId,
                Type = x.Type,
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
            await DeleteAsync(x => ids.Contains(x.ParentId));
        }

        /// <summary>
        /// 生成菜单
        /// </summary>
        /// <param name="desc">表描述</param>
        /// <param name="areaName">区域名称</param>
        /// <param name="entityName">实体名称</param>
        /// <returns></returns>
        public async Task AddBuildMenu(string desc, string areaName, string entityName)
        {
            List<Base_Action> permissionList = new List<Base_Action>();
            // 生成页面的
            Base_Action dTOTable = new Base_Action()
            {
                Id = IdHelper.GetId(),
                CreateTime = DateTime.Now,
                Name = desc, //拿描述作为菜单
                NeedAction = true,
                Type = Entity.ActionType.页面,
                ParentId = "1178957405992521728",
                Url = "/" + areaName + "/" + entityName + "/List"
            };
            permissionList.Add(dTOTable);
            // 增
            Base_Action dTOPermissionAdd = new Base_Action()
            {
                Id = IdHelper.GetId(),
                CreateTime = DateTime.Now,
                Name = "增",
                NeedAction = true,
                Type = Entity.ActionType.权限,
                ParentId = dTOTable.Id,
                Value = entityName + ".Add"
            };
            permissionList.Add(dTOPermissionAdd);
            // 改
            Base_Action dTOPermissionEdit = new Base_Action()
            {
                Id = IdHelper.GetId(),
                CreateTime = DateTime.Now,
                Name = "改",
                NeedAction = true,
                Type = Entity.ActionType.权限,
                ParentId = dTOTable.Id,
                Value = entityName + ".Edit"
            };
            permissionList.Add(dTOPermissionEdit);
            // 删
            Base_Action dTOPermissionDelete = new Base_Action()
            {
                Id = IdHelper.GetId(),
                CreateTime = DateTime.Now,
                Name = "删",
                NeedAction = true,
                Type = Entity.ActionType.权限,
                ParentId = dTOTable.Id,
                Value = entityName + ".Delete"
            };
            permissionList.Add(dTOPermissionDelete);
            await InsertAsync(permissionList);
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