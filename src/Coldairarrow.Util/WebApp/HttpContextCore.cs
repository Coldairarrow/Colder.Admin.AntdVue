using Microsoft.AspNetCore.Http;

namespace Coldairarrow.Util
{
    public static class HttpContextCore
    {
        public static HttpContext Current { get => AutofacHelper.GetService<IHttpContextAccessor>().HttpContext; }
    }
}
