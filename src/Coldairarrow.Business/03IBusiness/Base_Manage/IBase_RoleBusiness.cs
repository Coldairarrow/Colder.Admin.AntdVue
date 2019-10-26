using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using System.Collections.Generic;
using static Coldairarrow.Entity.Base_Manage.EnumType;

namespace Coldairarrow.Business.Base_Manage
{
    public interface IBase_RoleBusiness
    {
        List<Base_RoleDTO> GetDataList(Pagination pagination, string roleId = null, string roleName = null);
        Base_RoleDTO GetTheData(string id);
        AjaxResult AddData(Base_Role newData, List<string> actions);
        AjaxResult UpdateData(Base_Role theData, List<string> actions);
        AjaxResult DeleteData(List<string> ids);
    }

    [MapFrom(typeof(Base_Role))]
    public class Base_RoleDTO : Base_Role
    {
        public RoleTypeEnum? RoleType { get => RoleName?.ToEnum<RoleTypeEnum>(); }
        public List<string> Actions { get; set; } = new List<string>();
    }
}