using Coldairarrow.Business.Base_Manage;
using Coldairarrow.Util;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Coldairarrow.Business.Cache
{
    class Base_UserCache : BaseCache<Base_UserDTO>, IBase_UserCache, ITransientDependency
    {
        readonly IServiceProvider _serviceProvider;
        public Base_UserCache(IServiceProvider serviceProvider, IDistributedCache cache)
            : base(cache)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task<Base_UserDTO> GetDbDataAsync(string key)
        {
            var list = await _serviceProvider.GetService<IBase_UserBusiness>().GetDataListAsync(new Base_UsersInputDTO { all = true, userId = key });

            return list.Data.FirstOrDefault();
        }
    }
}
