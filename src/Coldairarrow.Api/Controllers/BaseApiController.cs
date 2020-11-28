using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Coldairarrow.Api
{
    /// <summary>
    /// 对外接口基控制器
    /// </summary>
    [Authorize]
    [ApiController]
    [ApiLog]
    public class BaseApiController : BaseController
    {

    }
}