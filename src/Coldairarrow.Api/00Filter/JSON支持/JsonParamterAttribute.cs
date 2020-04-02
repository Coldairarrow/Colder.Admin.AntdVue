using Coldairarrow.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace Coldairarrow.Api
{
    /// <summary>
    /// Json参数支持
    /// </summary>
    public class JsonParamterAttribute : BaseActionFilterAsync
    {
        public async override Task OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ContainsFilter<NoJsonParamterAttribute>())
                return;

            var request = context.HttpContext.Request;

            //参数映射：支持application/json
            string contentType = context.HttpContext.Request.ContentType;
            if (!contentType.IsNullOrEmpty() && contentType.Contains("application/json"))
            {
                var actionParameters = context.ActionDescriptor.Parameters;
                request.EnableBuffering();

                var allParamters = await HttpHelper.GetAllRequestParamsAsync(context.HttpContext);
                var actionArguments = context.ActionArguments;
                actionParameters.ForEach(aParamter =>
                {
                    string key = aParamter.Name;
                    if (allParamters.ContainsKey(key))
                    {
                        actionArguments[key] = allParamters[key]?.ToString()?.ChangeType_ByConvert(aParamter.ParameterType);
                    }
                    else
                    {
                        try
                        {
                            actionArguments[key] = allParamters.ToJson().ToObject(aParamter.ParameterType);
                        }
                        catch
                        {

                        }
                    }
                });
            }
        }
    }
}