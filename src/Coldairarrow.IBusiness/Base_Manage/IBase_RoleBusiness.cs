using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.Base_Manage
{
    public interface IBase_RoleBusiness
    {
        Task<PageResult<Base_RoleOutputDTO>> GetDataListAsync(PageInput<RolesInputDTO> input);
        Task<Base_RoleOutputDTO> GetTheDataAsync(string id);
        Task AddDataAsync(RoleEditInputDTO input);
        Task UpdateDataAsync(RoleEditInputDTO input);
        Task DeleteDataAsync(List<string> ids);
    }

    [Map(typeof(Base_Role))]
    public class RoleEditInputDTO : Base_Role
    {
        public List<string> actions { get; set; }
    }

    public class RolesInputDTO
    {
        public string roleId { get; set; }
        public string roleName { get; set; }
    }

    [Map(typeof(Base_Role))]
    public class Base_RoleOutputDTO : Base_Role
    {
        public RoleTypes? RoleType { get => RoleName?.ToEnum<RoleTypes>(); }
        public List<string> Actions { get; set; } = new List<string>();
    }
}