using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using System.Collections.Generic;

namespace Coldairarrow.Business.Base_Manage
{
    public interface IBase_BuildTestBusiness
    {
        List<Base_BuildTest> GetDataList(Pagination pagination, string condition, string keyword);
        Base_BuildTest GetTheData(string id);
        AjaxResult AddData(Base_BuildTest data);
        AjaxResult UpdateData(Base_BuildTest data);
        AjaxResult DeleteData(List<string> ids);
    }
}