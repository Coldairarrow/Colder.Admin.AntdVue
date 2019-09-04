using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using System.Collections.Generic;
using static Coldairarrow.Entity.Base_Manage.EnumType;

namespace Coldairarrow.Business.Base_Manage
{
    public interface IBase_SysRoleBusiness
    {
        List<Base_RoleDTO> GetDataList(Pagination pagination, string roldId = null, string roleName = null);
        Base_Role GetTheData(string id);
        Base_RoleDTO GetTheInfo(string id);
        AjaxResult AddData(Base_Role newData);
        AjaxResult UpdateData(Base_Role theData);
        AjaxResult DeleteData(List<string> ids);
    }

    public class Base_RoleDTO : Base_Role
    {
        public RoleType? RoleType { get => RoleName?.ToEnum<RoleType>(); }
    }
}