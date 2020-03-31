using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.Base_Manage
{
    public interface IBase_UserBusiness
    {
        Task<List<Base_UserDTO>> GetDataListAsync(Pagination pagination, bool all, string userId = null, string keyword = null);
        Task<List<SelectOption>> GetOptionListAsync(string selectedValueJson, string q);
        Task<Base_UserDTO> GetTheDataAsync(string id);
        Task AddDataAsync(Base_User newData, List<string> roleIds);
        Task UpdateDataAsync(Base_User theData, List<string> roleIds);
        Task DeleteDataAsync(List<string> ids);
    }

    [MapFrom(typeof(Base_User))]
    [MapTo(typeof(Base_User))]
    public class Base_UserDTO : Base_User
    {
        public string RoleNames { get => string.Join(",", RoleNameList ?? new List<string>()); }
        public List<string> RoleIdList { get; set; }
        public List<string> RoleNameList { get; set; }
        public EnumType.RoleTypeEnum RoleType
        {
            get
            {
                int type = 0;

                var values = typeof(EnumType.RoleTypeEnum).GetEnumValues();
                foreach (var aValue in values)
                {
                    if (RoleNames.Contains(aValue.ToString()))
                        type += (int)aValue;
                }

                return (EnumType.RoleTypeEnum)type;
            }
        }
        public string DepartmentName { get; set; }
        public string SexText { get => Sex == 1 ? "男" : "女"; }
        public string BirthdayText { get => Birthday?.ToString("yyyy-MM-dd"); }
    }
}