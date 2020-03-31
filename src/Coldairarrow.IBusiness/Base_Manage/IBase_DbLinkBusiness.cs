using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.Base_Manage
{
    public interface IBase_DbLinkBusiness
    {
        Task<List<Base_DbLink>> GetDataListAsync(Pagination pagination);
        Task<Base_DbLink> GetTheDataAsync(string id);
        Task AddDataAsync(Base_DbLink newData);
        Task UpdateDataAsync(Base_DbLink theData);
        Task DeleteDataAsync(List<string> ids);
    }
}