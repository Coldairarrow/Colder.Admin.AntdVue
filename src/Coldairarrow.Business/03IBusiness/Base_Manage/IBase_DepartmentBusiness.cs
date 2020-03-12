using Coldairarrow.Entity.Base_Manage;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.Base_Manage
{
    public interface IBase_DepartmentBusiness
    {
        Task<List<Base_DepartmentTreeDTO>> GetTreeDataListAsync(string parentId = null);
        Task<Base_Department> GetTheDataAsync(string id);
        Task<List<string>> GetChildrenIdsAsync(string departmentId);
        Task AddDataAsync(Base_Department newData);
        Task UpdateDataAsync(Base_Department theData);
        Task DeleteDataAsync(List<string> ids);
    }
}