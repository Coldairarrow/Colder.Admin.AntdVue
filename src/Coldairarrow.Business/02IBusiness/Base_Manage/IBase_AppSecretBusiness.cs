using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using System.Collections.Generic;

namespace Coldairarrow.Business.Base_Manage
{
    public interface IBase_AppSecretBusiness
    {
        List<Base_AppSecret> GetDataList(Pagination pagination, string keyword);
        Base_AppSecret GetTheData(string id);
        string GetAppSecret(string appId);
        AjaxResult AddData(Base_AppSecret newData);
        AjaxResult UpdateData(Base_AppSecret theData);
        AjaxResult DeleteData(List<string> ids);
    }
}