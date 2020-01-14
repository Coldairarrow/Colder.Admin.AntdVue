using Coldairarrow.Business.Base_Manage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.Base_Manage
{
    /// <summary>
    /// 首页控制器
    /// </summary>
    [Route("/Base_Manage/[controller]/[action]")]
    public class HomeController : BaseApiController
    {
        public HomeController(IHomeBusiness homeBus, IPermissionBusiness permissionBus, IBase_UserBusiness userBus)
        {
            _homeBus = homeBus;
            _permissionBus = permissionBus;
            _userBus = userBus;
        }
        IHomeBusiness _homeBus { get; }
        IPermissionBusiness _permissionBus { get; }
        IBase_UserBusiness _userBus { get; }

        /// <summary>
        /// 用户登录(获取token)
        /// </summary>
        /// <param name="userName">账号</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        [HttpPost]
        [CheckParamNotEmpty("userName", "password")]
        [NoCheckJWT]
        public async Task<AjaxResult<string>> SubmitLogin(string userName, string password)
        {
            return await _homeBus.SubmitLoginAsync(userName, password);
        }

        [HttpPost]
        [CheckParamNotEmpty("oldPwd", "newPwd")]
        public async Task<AjaxResult> ChangePwd(string oldPwd, string newPwd)
        {
            await _homeBus.ChangePwdAsync(oldPwd, newPwd);

            return Success();
        }

        [HttpPost]
        public async Task<AjaxResult> GetOperatorInfo()
        {
            var theInfo = await _userBus.GetTheDataAsync(Operator.UserId);
            var permissions = await _permissionBus.GetUserPermissionValuesAsync(Operator.UserId);
            var resObj = new
            {
                UserInfo = theInfo,
                Permissions = permissions
            };

            return Success(resObj);
        }

        [HttpPost]
        public async Task<AjaxResult<List<Base_ActionDTO>>> GetOperatorMenuList()
        {
            var list = await _permissionBus.GetUserMenuListAsync(Operator.UserId);

            return Success(list);
        }
    }
}