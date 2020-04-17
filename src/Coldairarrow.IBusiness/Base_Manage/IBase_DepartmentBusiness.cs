using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.Base_Manage
{
    public interface IBase_DepartmentBusiness
    {
        Task<List<Base_DepartmentTreeDTO>> GetTreeDataListAsync(DepartmentsTreeInputDTO input);
        Task<Base_Department> GetTheDataAsync(string id);
        Task<List<string>> GetChildrenIdsAsync(string departmentId);
        Task AddDataAsync(Base_Department newData);
        Task UpdateDataAsync(Base_Department theData);
        Task DeleteDataAsync(List<string> ids);
    }

    public class DepartmentsTreeInputDTO
    {
        public string parentId { get; set; }
    }

    public class Base_DepartmentTreeDTO : TreeModel
    {
        public object children { get => Children; }
        public string title { get => Text; }
        public string value { get => Id; }
        public string key { get => Id; }
    }
}