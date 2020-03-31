using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.Base_Manage
{
    public interface IBase_AppSecretBusiness
    {
        Task<List<Base_AppSecret>> GetDataListAsync(Pagination pagination, string keyword);
        Task<Base_AppSecret> GetTheDataAsync(string id);
        Task<string> GetAppSecretAsync(string appId);
        Task AddDataAsync(Base_AppSecret newData);
        Task UpdateDataAsync(Base_AppSecret theData);
        Task DeleteDataAsync(List<string> ids);
    }
}