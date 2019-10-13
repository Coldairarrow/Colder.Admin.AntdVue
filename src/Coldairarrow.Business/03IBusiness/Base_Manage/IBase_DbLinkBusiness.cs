using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using System.Collections.Generic;

namespace Coldairarrow.Business.Base_Manage
{
    public interface IBase_DbLinkBusiness
    {
        List<Base_DbLink> GetDataList(Pagination pagination);
        Base_DbLink GetTheData(string id);
        AjaxResult AddData(Base_DbLink newData);
        AjaxResult UpdateData(Base_DbLink theData);
        AjaxResult DeleteData(List<string> ids);
    }
}