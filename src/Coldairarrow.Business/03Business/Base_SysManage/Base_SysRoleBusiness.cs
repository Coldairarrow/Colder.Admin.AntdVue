using AutoMapper;
using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Linq;

namespace Coldairarrow.Business.Base_Manage
{
    public class Base_SysRoleBusiness : BaseBusiness<Base_Role>, IBase_SysRoleBusiness, IDependency
    {
        #region DI

        #endregion

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

            return list;
        }

        /// <summary>
        /// 获取指定的单条数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public Base_Role GetTheData(string id)
        {
            return GetEntity(id);
        }

        public Base_RoleDTO GetTheInfo(string id)
        {
            return GetDataList(new Pagination(), id).FirstOrDefault();
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="newData">数据</param>
        [DataAddLog(LogType.系统角色管理, "RoleName", "角色")]
        [DataRepeatValidate(new string[] { "RoleName" }, new string[] { "角色名" })]
        public AjaxResult AddData(Base_Role newData)
        {
            Insert(newData);

            return Success();
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        [DataEditLog(LogType.系统角色管理, "RoleName", "角色")]
        [DataRepeatValidate(new string[] { "RoleName" }, new string[] { "角色名" })]
        public AjaxResult UpdateData(Base_Role theData)
        {
            Update(theData);

            return Success();
        }

        [DataDeleteLog(LogType.系统角色管理, "RoleName", "角色")]
        public AjaxResult DeleteData(List<string> ids)
        {
            Delete(ids);

            return Success();
        }

        #endregion

        #region 私有成员

        #endregion

        #region 数据模型

        #endregion
    }
}