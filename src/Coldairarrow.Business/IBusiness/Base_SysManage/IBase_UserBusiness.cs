using Coldairarrow.Entity.Base_SysManage;
using Coldairarrow.Util;
using System.Collections.Generic;

namespace Coldairarrow.Business.Base_SysManage
{
    public interface IBase_UserBusiness
    {
        List<Base_UserDTO> GetDataList(Pagination pagination, bool all, string userId = null, string keyword = null);
        Base_User GetTheData(string id);
        Base_UserDTO GetTheInfo(string userId);
        List<Base_User> BuildSelectResult(string selectedValueJson, string q, string textFiled, string valueField);
        List<string> GetUserRoleIds(string userId);
        AjaxResult AddData(Base_User newData);
        AjaxResult UpdateData(Base_User theData);
        AjaxResult DeleteData(List<string> ids);
        AjaxResult SetUserRole(string userId, List<string> roleIds);
        AjaxResult ChangePwd(string oldPwd, string newPwd);
    }

    public class Base_UserDTO : Base_User
    {
        public string RoleNames { get => string.Join(",", RoleNameList); }

        public List<string> RoleIdList { get; set; }

        public List<string> RoleNameList { get; set; }

        public EnumType.RoleType RoleType
        {
            get
            {
                int type = 0;

                var values = typeof(EnumType.RoleType).GetEnumValues();
                foreach (var aValue in values)
                {
                    if (RoleNames.Contains(aValue.ToString()))
                        type += (int)aValue;
                }

                return (EnumType.RoleType)type;
            }
        }

        public string DepartmentName { get; set; }
    }
}