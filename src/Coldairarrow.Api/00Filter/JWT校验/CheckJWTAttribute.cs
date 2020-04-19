using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace Coldairarrow.Api
{
    /// <summary>
    /// JWT校验
    /// </summary>
    public class CheckJWTAttribute : BaseActionFilterAsync
    {
        private static readonly int _errorCode = 401;

        public override async Task OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ContainsFilter<NoCheckJWTAttribute>())
                return;

            try
            {
                var req = context.HttpContext.Request;

                string token = req.GetToken();
                if (token.IsNullOrEmpty())
                {
                    context.Result = Error("缺少token", _errorCode);
                    return;
                }

                if (!JWTHelper.CheckToken(token, JWTHelper.JWTSecret))
                {
                    context.Result = Error("token校验失败!", _errorCode);
                    return;
                }

                var payload = JWTHelper.GetPayload<JWTPayload>(token);
                if (payload.Expire < DateTime.Now)
                {
                    context.Result = Error("token过期!", _errorCode);
                    return;
                }
            }
            catch (Exception ex)
            {
                context.Result = Error(ex.Message, _errorCode);
            }

            await Task.CompletedTask;
        }
    }
}