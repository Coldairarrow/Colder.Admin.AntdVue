using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coldairarrow.Api
{
    /// <summary>
    /// 参数非空校验
    /// </summary>
    public class CheckParamNotEmptyAttribute : BaseActionFilterAsync
    {
        private List<string> _paramters { get; }
        public CheckParamNotEmptyAttribute(params string[] paramters)
        {
            _paramters = paramters.ToList();
        }

        public override async Task OnActionExecuting(ActionExecutingContext context)
        {
            var allParamters = await HttpHelper.GetAllRequestParamsAsync(context.HttpContext);
            var needParamters = _paramters.Where(x =>
            {
                if (!allParamters.ContainsKey(x))
                    return true;
                else
                    return allParamters[x].IsNullOrEmpty();
            }).ToList();
            if (needParamters.Count != 0)
            {
                AjaxResult res = new AjaxResult
                {
                    Success = false,
                    Msg = $"参数:{string.Join(",", needParamters)}不能为空！"
                };
                context.Result = new ContentResult { Content = res.ToJson(), ContentType = "application/json;charset=utf-8" };
            }
        }
    }
}