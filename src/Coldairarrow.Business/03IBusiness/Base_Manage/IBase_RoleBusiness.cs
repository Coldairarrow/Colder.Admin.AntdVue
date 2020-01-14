using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Coldairarrow.Entity.Base_Manage.EnumType;

namespace Coldairarrow.Business.Base_Manage
{
    public interface IBase_RoleBusiness
    {
        Task<List<Base_RoleDTO>> GetDataListAsync(Pagination pagination, string roleId = null, string roleName = null);
        Task<Base_RoleDTO> GetTheDataAsync(string id);
        Task AddDataAsync(Base_Role newData, List<string> actions);
        Task UpdateDataAsync(Base_Role theData, List<string> actions);
        Task DeleteDataAsync(List<string> ids);
    }

    [MapFrom(typeof(Base_Role))]
    public class Base_RoleDTO : Base_Role
    {
        public RoleTypeEnum? RoleType { get => RoleName?.ToEnum<RoleTypeEnum>(); }
        public List<string> Actions { get; set; } = new List<string>();
    }
}