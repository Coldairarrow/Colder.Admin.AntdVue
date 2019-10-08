using AutoMapper;
using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Coldairarrow.Business.Base_Manage
{
    public class Base_RoleBusiness : BaseBusiness<Base_Role>, IBase_RoleBusiness, IDependency
    {
        #region 外部接口

        public List<Base_RoleDTO> GetDataList(Pagination pagination, string roldId = null, string roleName = null)
        {
            var where = LinqHelper.True<Base_Role>();
            if (!roldId.IsNullOrEmpty())
                where = where.And(x => x.Id == roldId);
            if (!roleName.IsNullOrEmpty())
                where = where.And(x => x.RoleName.Contains(roleName));

            var list = GetIQueryable()
                .Where(where)
                .GetPagination(pagination)
                .ToList()
                .Select(x => Mapper.Map<Base_RoleDTO>(x))
                .ToList();

            SetProperty(list);

            return list;

            void SetProperty(List<Base_RoleDTO> _list)
            {
                var ids = _list.Select(x => x.Id).ToList();
                var roleActions = Service.GetIQueryable<Base_RoleAction>()
                    .Where(x => ids.Contains(x.RoleId))
                    .ToList();
                _list.ForEach(aData =>
                {
                    aData.Actions = roleActions.Where(x => x.RoleId == aData.Id).Select(x => x.ActionId).ToList();
                });
            }
        }

        public Base_RoleDTO GetTheData(string id)
        {
            return GetDataList(new Pagination(), id).FirstOrDefault();
        }

        [DataAddLog(LogType.系统角色管理, "RoleName", "角色")]
        [DataRepeatValidate(new string[] { "RoleName" }, new string[] { "角色名" })]
        public AjaxResult AddData(Base_Role newData, List<string> actions)
        {
            using (var transaction = BeginTransaction())
            {
                Insert(newData);
                SetRoleAction(newData.Id, actions);

                var res = EndTransaction();
                if (!res.Success)
                    throw new Exception("系统异常,请重试", res.ex);
            }

            return Success();
        }

        [DataEditLog(LogType.系统角色管理, "RoleName", "角色")]
        [DataRepeatValidate(new string[] { "RoleName" }, new string[] { "角色名" })]
        public AjaxResult UpdateData(Base_Role theData, List<string> actions)
        {
            using (var transaction = BeginTransaction())
            {
                Update(theData);
                SetRoleAction(theData.Id, actions);

                var res = EndTransaction();
                if (!res.Success)
                    throw new Exception("系统异常,请重试", res.ex);
            }

            return Success();
        }

        [DataDeleteLog(LogType.系统角色管理, "RoleName", "角色")]
        public AjaxResult DeleteData(List<string> ids)
        {
            using (var transaction = BeginTransaction())
            {
                Delete(ids);
                Service.Delete_Sql<Base_RoleAction>(x => ids.Contains(x.Id));

                var res = EndTransaction();
                if (!res.Success)
                    throw new Exception("系统异常,请重试", res.ex);
            }

            return Success();
        }

        #endregion

        #region 私有成员

        private void SetRoleAction(string roleId, List<string> actions)
        {
            var roleActions = (actions ?? new List<string>())
                .Select(x => new Base_RoleAction
                {
                    Id = IdHelper.GetId(),
                    ActionId = x,
                    CreateTime = DateTime.Now,
                    RoleId = roleId
                }).ToList();
            Service.Delete_Sql<Base_RoleAction>(x => x.RoleId == roleId);
            Service.Insert(roleActions);
        }

        #endregion

        #region 数据模型

        #endregion
    }
}