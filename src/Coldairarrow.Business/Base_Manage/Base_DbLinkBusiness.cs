using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using EFCore.Sharding;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.Base_Manage
{
    public class Base_DbLinkBusiness : BaseBusiness<Base_DbLink>, IBase_DbLinkBusiness, ITransientDependency
    {
        public Base_DbLinkBusiness(IRepository repository) : base(repository)
        {
        }

        #region 外部接口

        public async Task<List<Base_DbLink>> GetDataListAsync(Pagination pagination)
        {
            return await GetIQueryable().GetPagination(pagination).ToListAsync();
        }

        public async Task<Base_DbLink> GetTheDataAsync(string id)
        {
            return await GetEntityAsync(id);
        }

        [InitEntity]
        public async Task AddDataAsync(Base_DbLink newData)
        {
            await InsertAsync(newData);
        }

        public async Task UpdateDataAsync(Base_DbLink theData)
        {
            await UpdateAsync(theData);
        }

        public async Task DeleteDataAsync(List<string> ids)
        {
            await DeleteAsync(ids);
        }

        #endregion

        #region 私有成员

        #endregion

        #region 数据模型

        #endregion
    }
}