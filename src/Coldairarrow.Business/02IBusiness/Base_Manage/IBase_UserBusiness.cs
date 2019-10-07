using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using System.Collections.Generic;

namespace Coldairarrow.Business.Base_Manage
{
    public interface IBase_UserBusiness
    {
        List<Base_UserDTO> GetDataList(Pagination pagination, bool all, string userId = null, string keyword = null);
        Base_User GetTheData(string id);
        Base_UserDTO GetTheInfo(string userId);
        AjaxResult AddData(Base_User newData);
        AjaxResult UpdateData(Base_User theData);
        AjaxResult DeleteData(List<string> ids);
    }
}