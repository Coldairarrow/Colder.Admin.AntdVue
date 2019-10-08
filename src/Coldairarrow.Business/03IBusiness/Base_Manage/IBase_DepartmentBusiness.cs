using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using System.Collections.Generic;

namespace Coldairarrow.Business.Base_Manage
{
    public interface IBase_DepartmentBusiness
    {
        List<Base_DepartmentTreeDTO> GetTreeDataList(string parentId = null);
        Base_Department GetTheData(string id);
        List<string> GetChildrenIds(string departmentId);
        AjaxResult AddData(Base_Department newData);
        AjaxResult UpdateData(Base_Department theData);
        AjaxResult DeleteData(List<string> ids);
    }
}