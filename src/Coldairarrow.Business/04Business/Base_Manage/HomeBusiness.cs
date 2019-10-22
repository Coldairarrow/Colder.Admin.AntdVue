using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using System;
using System.Linq;

namespace Coldairarrow.Business.Base_Manage
{
    public class HomeBusiness : BaseBusiness<Base_User>, IHomeBusiness, IDependency
    {
        public HomeBusiness(IPermissionBusiness permissionBus)
        {
            _permissionBus = permissionBus;
        }
        IPermissionBusiness _permissionBus { get; }

        public AjaxResult SubmitLogin(string userName, string password)
        {
            if (userName.IsNullOrEmpty() || password.IsNullOrEmpty())
                return Error("账号或密码不能为空！");
            password = password.ToMD5String();
            var theUser = GetIQueryable().Where(x => x.UserName == userName && x.Password == password).FirstOrDefault();
            if (theUser != null)
            {
                //生成token,有效期一天
                JWTPayload jWTPayload = new JWTPayload
                {
                    UserId = theUser.Id,
                    Expire = DateTime.Now.AddDays(1)
                };
                string token = JWTHelper.GetToken(jWTPayload.ToJson(), JWTHelper.JWTSecret);
                var permissions = _permissionBus.GetUserPermissionValues(theUser.Id);

                var res = new
                {
                    Token = token,
                    UserInfo = theUser,
                    Permissions = permissions
                };

                return Success(res);
            }
            else
                return Error("账号或密码不正确！");
        }
    }
}
