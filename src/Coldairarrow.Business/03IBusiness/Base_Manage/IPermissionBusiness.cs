using System.Collections.Generic;

namespace Coldairarrow.Business.Base_Manage
{
    public interface IPermissionBusiness
    {
        List<string> GetUserPermissionValues(string userId);
        List<Base_ActionDTO> GetUserMenuList(string userId);
    }
}
