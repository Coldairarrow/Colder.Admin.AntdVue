using Coldairarrow.Entity.Base_SysManage;
using Coldairarrow.Util;
using System.Linq;

namespace Coldairarrow.Business.Base_SysManage
{
    public class HomeBusiness : BaseBusiness<Base_User>, IHomeBusiness, IDependency
    {
        public HomeBusiness(IOperator theOperator)
        {
            _theOperator = theOperator;
        }
        private IOperator _theOperator { get; }

        public AjaxResult SubmitLogin(string userName, string password)
        {
            if (userName.IsNullOrEmpty() || password.IsNullOrEmpty())
                return Error("账号或密码不能为空！");
            password = password.ToMD5String();
            var theUser = GetIQueryable().Where(x => x.UserName == userName && x.Password == password).FirstOrDefault();
            if (theUser != null)
            {
                _theOperator.Login(theUser.Id);
                return Success();
            }
            else
                return Error("账号或密码不正确！");
        }
    }
}
