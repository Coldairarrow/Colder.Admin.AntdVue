using Coldairarrow.Business;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Authorization;

namespace Coldairarrow.Api
{
    /// <summary>
    /// Mvc对外接口基控制器
    /// </summary>
    //[ApiController]
    //[CheckSign]
    [CheckJWT]
    public class BaseApiController : BaseController
    {
        public IOperator Operator { get => AutofacHelper.GetScopeService<IOperator>(); }
    }
}