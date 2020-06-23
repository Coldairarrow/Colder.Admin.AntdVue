using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using System.Collections.Generic;

namespace Coldairarrow.Entity
{
    [Map(typeof(Base_User))]
    public class Base_UserDTO : Base_User
    {
        public string RoleNames { get => string.Join(",", RoleNameList ?? new List<string>()); }
        public List<string> RoleIdList { get; set; }
        public List<string> RoleNameList { get; set; }
        public RoleTypes RoleType
        {
            get
            {
                int type = 0;

                var values = typeof(RoleTypes).GetEnumValues();
                foreach (var aValue in values)
                {
                    if (RoleNames.Contains(aValue.ToString()))
                        type += (int)aValue;
                }

                return (RoleTypes)type;
            }
        }
        public string DepartmentName { get; set; }
        public string SexText { get => Sex.GetDescription(); }
        public string BirthdayText { get => Birthday?.ToString("yyyy-MM-dd"); }
    }
}
