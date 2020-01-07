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
            return _userBus.GetDataList(new Pagination(), true, key).FirstOrDefault();
        }
    }
}
