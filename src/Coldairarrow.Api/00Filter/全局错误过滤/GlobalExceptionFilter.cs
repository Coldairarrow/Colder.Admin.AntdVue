using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Coldairarrow.Api
{
    public class GlobalExceptionFilter : BaseActionFilter, IExceptionFilter
    {
        readonly IMyLogger _myLogger;
        public void OnException(ExceptionContext context)
        {
            var ex = context.Exception;
            if (ex is BusException busEx)
            {
                _myLogger.Info(LogType.系统跟踪, busEx.Message);
                context.Result = Error(busEx.Message, busEx.ErrorCode);
            }
            else
            {
                _myLogger.Error(ex);
                context.Result = Error(ex.Message);
            }
        }
    }
}
