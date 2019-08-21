using Coldairarrow.Entity.Base_SysManage;
using Coldairarrow.Util;
using System.Collections.Generic;
using static Coldairarrow.Entity.Base_SysManage.EnumType;

namespace Coldairarrow.Business.Base_SysManage
{
    public interface IBase_SysRoleBusiness
    {
        List<Base_SysRoleDTO> GetDataList(Pagination pagination, string roldId = null, string roleName = null);
        Base_SysRole GetTheData(string id);
        Base_SysRoleDTO GetTheInfo(string id);
        AjaxResult AddData(Base_SysRole newData);
        AjaxResult UpdateData(Base_SysRole theData);
        AjaxResult DeleteData(List<string> ids);
        AjaxResult SavePermission(string roleId, List<string> permissions);
    }

    public class Base_SysRoleDTO : Base_SysRole
    {
        public RoleType? RoleType { get => RoleName?.ToEnum<RoleType>(); }
    }
}