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
}