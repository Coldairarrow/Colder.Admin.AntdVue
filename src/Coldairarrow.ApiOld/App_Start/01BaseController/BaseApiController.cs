namespace Coldairarrow.Web
{
    /// <summary>
    /// Mvc对外接口基控制器
    /// </summary>
    [CheckSign]
    [CheckAppIdPermission]
    public class BaseApiController : BaseController
    {

    }
}