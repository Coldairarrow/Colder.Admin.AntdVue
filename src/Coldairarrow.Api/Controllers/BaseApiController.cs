using Coldairarrow.Business;
using Coldairarrow.Util;

namespace Coldairarrow.Api
{
    /// <summary>
    /// Mvc对外接口基控制器
    /// </summary>
    //[ApiController]
    //[CheckSign]
    public class BaseApiController : BaseController
    {
        public IOperator Operator { get => AutofacHelper.GetScopeService<IOperator>(); }
    }
}