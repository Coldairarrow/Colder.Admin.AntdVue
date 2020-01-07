using Coldairarrow.Business;
using Coldairarrow.Util;

namespace Coldairarrow.Api
{
    /// <summary>
    /// Mvc对外接口基控制器
    /// </summary>
    [CheckJWT]
    public class BaseApiController : BaseController
    {
        public IOperator Operator { get => AutofacHelper.GetScopeService<IOperator>(); }
    }
}