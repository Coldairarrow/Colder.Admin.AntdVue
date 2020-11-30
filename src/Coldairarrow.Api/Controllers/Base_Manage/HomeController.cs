using Coldairarrow.Business.Base_Manage;
using Coldairarrow.IBusiness;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NSwag.Annotations;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.Base_Manage
{
    /// <summary>
    /// 首页控制器
    /// </summary>
    [Route("/Base_Manage/[controller]/[action]")]
    [OpenApiTag("主页")]
    public class HomeController : BaseApiController
    {
        readonly IHomeBusiness _homeBus;
        readonly IPermissionBusiness _permissionBus;
        readonly IBase_UserBusiness _userBus;
        readonly IOperator _operator;
        private readonly JwtOptions _jwtOptions;
        public HomeController(
            IHomeBusiness homeBus,
            IPermissionBusiness permissionBus,
            IBase_UserBusiness userBus,
            IOperator @operator,
            IOptions<JwtOptions> jwtOptions
            )
        {
            _homeBus = homeBus;
            _permissionBus = permissionBus;
            _userBus = userBus;
            _operator = @operator;
            _jwtOptions = jwtOptions.Value;
        }

        /// <summary>
        /// 用户登录(获取token)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<string> SubmitLogin(LoginInputDTO input)
        {
            var userId = await _homeBus.SubmitLoginAsync(input);

            var claims = new[]
            {
                new Claim("userId",userId)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken(
                string.Empty,
                string.Empty,
                claims,
                expires: DateTime.Now.AddHours(_jwtOptions.AccessExpireHours),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
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