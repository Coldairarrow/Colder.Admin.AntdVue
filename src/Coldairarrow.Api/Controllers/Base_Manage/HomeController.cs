using Coldairarrow.Business.Base_Manage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Coldairarrow.Api.Controllers.Base_Manage
{
    /// <summary>
    /// 首页控制器
    /// </summary>
    [Route("/Base_Manage/[controller]/[action]")]
    public class HomeController : BaseApiController
    {
        public HomeController(IHomeBusiness homeBus, IPermissionBusiness permissionBus)
        {
            _homeBus = homeBus;
            _permissionBus = permissionBus;
        }
        IHomeBusiness _homeBus { get; }
        IPermissionBusiness _permissionBus { get; }

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
        public ActionResult<List<Base_ActionDTO>> GetUserMenuList()
        {
            var dataList = _permissionBus.GetUserMenuList(Operator.UserId);

            return Success(dataList);
        }
    }
}