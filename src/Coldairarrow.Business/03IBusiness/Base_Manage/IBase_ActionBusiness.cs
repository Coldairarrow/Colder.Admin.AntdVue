using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Linq;

namespace Coldairarrow.Business.Base_Manage
{
    public interface IBase_ActionBusiness
    {
        List<Base_Action> GetDataList(Pagination pagination, string keyword = null, string parentId = null, List<int> types = null, IQueryable<Base_Action> q = null);
        List<Base_ActionDTO> GetTreeDataList(string keyword, List<int> types, bool selectable, IQueryable<Base_Action> q = null, bool checkEmptyChildren = false);
        Base_Action GetTheData(string id);
        AjaxResult AddData(Base_Action newData, List<Base_Action> permissionList);
        AjaxResult UpdateData(Base_Action theData, List<Base_Action> permissionList);
        AjaxResult DeleteData(List<string> ids);
    }
}