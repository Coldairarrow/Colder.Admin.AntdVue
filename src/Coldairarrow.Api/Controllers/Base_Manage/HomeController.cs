using Coldairarrow.Business.Base_Manage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult<AjaxResult> SubmitLogin(string userName, string password)
        {
            AjaxResult res = _homeBus.SubmitLogin(userName, password);

            return JsonContent(res.ToJson());
        }

        [HttpPost]
        [CheckParamNotEmpty("oldPwd", "newPwd")]
        public IActionResult ChangePwd(string oldPwd, string newPwd)
        {
            var res = _homeBus.ChangePwd(oldPwd, newPwd);

            return JsonContent(res.ToJson());
        }

        [HttpPost]
        public IActionResult GetOperatorInfo()
        {
            var theInfo = _userBus.GetTheData(Operator.UserId);
            var permissions = _permissionBus.GetUserPermissionValues(Operator.UserId);
            var resObj = new
            {
                UserInfo = theInfo,
                Permissions = permissions
            };

            return Success(resObj);
        }

        [HttpPost]
        public IActionResult GetOperatorMenuList()
        {
            var dataList = _permissionBus.GetUserMenuList(Operator.UserId);

            return Success(dataList);
        }
    }
}