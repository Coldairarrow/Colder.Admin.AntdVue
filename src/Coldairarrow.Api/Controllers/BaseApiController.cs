using Microsoft.AspNetCore.Mvc;

namespace Coldairarrow.Api
{
    /// <summary>
    /// Mvc对外接口基控制器
    /// </summary>
    [CheckJWT]
    [ApiController]
    public class BaseApiController : BaseController
    {

    }
}