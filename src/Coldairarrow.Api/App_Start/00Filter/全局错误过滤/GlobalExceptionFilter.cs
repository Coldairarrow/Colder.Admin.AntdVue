using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Coldairarrow.Web
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            ILogger logger = AutofacHelper.GetScopeService<ILogger>();

            var ex = context.Exception;
            logger.Error(ex);

            context.Result = new ContentResult
            {
                Content = new AjaxResult { Success = false, Msg = ex.Message }.ToJson(),
                ContentType = "application/json; charset=utf-8",
            };
        }
    }
}
