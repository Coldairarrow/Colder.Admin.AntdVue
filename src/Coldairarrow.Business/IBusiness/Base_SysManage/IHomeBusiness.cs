using Coldairarrow.Util;

namespace Coldairarrow.Business.Base_SysManage
{
    public interface IHomeBusiness
    {
        AjaxResult SubmitLogin(string userName, string password);
    }
}
