using Coldairarrow.Business.Base_Manage;
using Coldairarrow.Util;
using System.Linq;

namespace Coldairarrow.Business.Cache
{
    class Base_UserCache : BaseCache<Base_UserDTO>, IBase_UserCache, IDependency
    {
        IBase_UserBusiness _userBus { get => AutofacHelper.GetScopeService<IBase_UserBusiness>(); }
        protected override Base_UserDTO GetDbData(string key)
        {
            var list = AsyncHelper.RunSync(() => _userBus.GetDataListAsync(new Pagination(), true, key));

            return list.FirstOrDefault();
        }
    }
}
