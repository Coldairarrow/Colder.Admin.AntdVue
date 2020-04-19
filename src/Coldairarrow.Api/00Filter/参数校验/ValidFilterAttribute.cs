using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Threading.Tasks;

namespace Coldairarrow.Api
{
    public class ValidFilterAttribute : BaseActionFilterAsync
    {
        public override async Task OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var msgList = context.ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);

                context.Result = Error(string.Join(",", msgList));
            }

            await Task.CompletedTask;
        }
    }
}
