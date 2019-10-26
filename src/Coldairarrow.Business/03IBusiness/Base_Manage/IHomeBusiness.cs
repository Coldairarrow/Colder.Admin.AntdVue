using Coldairarrow.Util;

namespace Coldairarrow.Business.Base_Manage
{
    public interface IHomeBusiness
    {
        AjaxResult SubmitLogin(string userName, string password);
        AjaxResult ChangePwd(string oldPwd, string newPwd);
    }
}
