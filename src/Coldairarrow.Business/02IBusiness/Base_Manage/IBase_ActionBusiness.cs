using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using System.Collections.Generic;

namespace Coldairarrow.Business.Base_Manage
{
    public interface IBase_ActionBusiness
    {
        List<Base_Action> GetDataList(Pagination pagination, string keyword = null, string parentId = null, List<int> types = null);
        List<Base_ActionDTO> GetTreeDataList(string keyword, List<int> types);
        Base_Action GetTheData(string id);
        AjaxResult AddData(Base_Action newData);
        AjaxResult UpdateData(Base_Action theData);
        AjaxResult DeleteData(List<string> ids);
    }
}