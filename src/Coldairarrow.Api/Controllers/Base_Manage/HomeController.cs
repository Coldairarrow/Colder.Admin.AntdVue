using Coldairarrow.Business;
using Coldairarrow.Business.Base_Manage;
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
        readonly IHomeBusiness _homeBus;
        readonly IPermissionBusiness _permissionBus;
        readonly IBase_UserBusiness _userBus;
        readonly IOperator _operator;
        public HomeController(
            IHomeBusiness homeBus,
            IPermissionBusiness permissionBus,
            IBase_UserBusiness userBus,
            IOperator @operator
            )
        {
            _homeBus = homeBus;
            _permissionBus = permissionBus;
            _userBus = userBus;
            _operator = @operator;
        }

        /// <summary>
        /// 用户登录(获取token)
        /// </summary>
        /// <param name="userName">账号</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        [HttpPost]
        [CheckParamNotEmpty("userName", "password")]
        [NoCheckJWT]
        public async Task<string> SubmitLogin(string userName, string password)
        {
            return await _homeBus.SubmitLoginAsync(userName, password);
        }

        [HttpPost]
        [CheckParamNotEmpty("oldPwd", "newPwd")]
        public async Task ChangePwd(string oldPwd, string newPwd)
        {
            await _homeBus.ChangePwdAsync(oldPwd, newPwd);
        }

        [HttpPost]
        public async Task<object> GetOperatorInfo()
        {
            var theInfo = await _userBus.GetTheDataAsync(_operator.UserId);
            var permissions = await _permissionBus.GetUserPermissionValuesAsync(_operator.UserId);
            var resObj = new
            {
                UserInfo = theInfo,
                Permissions = permissions
            };

            return resObj;
        }

        [HttpPost]
        public async Task<List<Base_ActionDTO>> GetOperatorMenuList()
        {
            return await _permissionBus.GetUserMenuListAsync(_operator.UserId);
        }
    }
}