using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Coldairarrow.Api
{
    public class GlobalExceptionFilter : BaseActionFilter, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            ILogger logger = AutofacHelper.GetScopeService<ILogger>();

            var ex = context.Exception;
            if (ex is BusException busEx)
            {
                logger.Info(LogType.系统跟踪, busEx.Message);
                context.Result = Error(busEx.Message, busEx.ErrorCode);
            }
            else
            {
                logger.Error(ex);
                context.Result = Error(ex.Message);
            }
        }
    }
}
