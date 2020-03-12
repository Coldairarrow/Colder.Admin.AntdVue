using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coldairarrow.Business.Base_Manage
{
    public interface IBase_ActionBusiness
    {
        Task<List<Base_Action>> GetDataListAsync(Pagination pagination, string keyword = null, string parentId = null, List<int> types = null, IQueryable<Base_Action> q = null);
        Task<List<Base_ActionDTO>> GetTreeDataListAsync(string keyword, List<int> types, bool selectable, IQueryable<Base_Action> q = null, bool checkEmptyChildren = false);
        Task<Base_Action> GetTheDataAsync(string id);
        Task AddDataAsync(Base_Action newData, List<Base_Action> permissionList);
        Task UpdateDataAsync(Base_Action theData, List<Base_Action> permissionList);
        Task DeleteDataAsync(List<string> ids);
    }
}