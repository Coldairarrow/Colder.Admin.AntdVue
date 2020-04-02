using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace Coldairarrow.Api
{
    public class GlobalExceptionFilter : BaseActionFilterAsync, IAsyncExceptionFilter
    {
        readonly IMyLogger _myLogger;
        public GlobalExceptionFilter(IMyLogger myLogger)
        {
            _myLogger = myLogger;
        }

        public async Task OnExceptionAsync(ExceptionContext context)
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

            await Task.CompletedTask;
        }
    }
}
