using Coldairarrow.Business.Base_Manage;
using Coldairarrow.IBusiness;
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
        /// <returns></returns>
        [HttpPost]
        [NoCheckJWT]
        public async Task<string> SubmitLogin(LoginInputDTO input)
        {
            return await _homeBus.SubmitLoginAsync(input);
        }

        [HttpPost]
        public async Task ChangePwd(ChangePwdInputDTO input)
        {
            await _homeBus.ChangePwdAsync(input);
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