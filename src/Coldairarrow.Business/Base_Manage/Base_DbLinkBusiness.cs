using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using EFCore.Sharding;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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

        /// <summary>
        /// 获取指定的单条数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public async Task<Base_DbLink> GetTheDataAsync(string id)
        {
            return await GetEntityAsync(id);
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="newData">数据</param>
        public async Task AddDataAsync(Base_DbLink newData)
        {
            await InsertAsync(newData);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
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