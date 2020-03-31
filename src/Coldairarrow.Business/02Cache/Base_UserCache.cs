using Coldairarrow.Business.Base_Manage;
using Coldairarrow.Util;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Coldairarrow.Business.Cache
{
    class Base_UserCache : BaseCache<Base_UserDTO>, IBase_UserCache, IDependency
    {
        readonly IServiceProvider _serviceProvider;
        public Base_UserCache(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        protected override Base_UserDTO GetDbData(string key)
        {
            var list = AsyncHelper.RunSync(() => _serviceProvider.GetService<IBase_UserBusiness>().GetDataListAsync(new Pagination(), true, key));

            return list.FirstOrDefault();
        }
    }
}
