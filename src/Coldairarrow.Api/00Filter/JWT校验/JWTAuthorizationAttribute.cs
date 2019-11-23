using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;

namespace Coldairarrow.Api
{
    public class JWTAuthorizationAttribute : Attribute, IAuthorizationHandler
    {
        public Task HandleAsync(AuthorizationHandlerContext context)
        {
            return Task.Run(() => { });
        }
    }
}
