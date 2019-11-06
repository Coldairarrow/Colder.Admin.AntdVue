using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using System.Collections.Generic;

namespace Coldairarrow.Business.Base_Manage
{
    public interface IBase_UserBusiness
    {
        List<Base_UserDTO> GetDataList(Pagination pagination, bool all, string userId = null, string keyword = null);
        List<SelectOption> GetOptionList(string selectedValueJson, string q);
        Base_UserDTO GetTheData(string id);
        AjaxResult AddData(Base_User newData, List<string> roleIds);
        AjaxResult UpdateData(Base_User theData, List<string> roleIds);
        AjaxResult DeleteData(List<string> ids);
    }
}