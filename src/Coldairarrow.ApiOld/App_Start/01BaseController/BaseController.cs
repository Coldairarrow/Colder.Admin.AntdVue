using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Text;

namespace Coldairarrow.Web
{
    /// <summary>
    /// Mvc基控制器
    /// </summary>
    public class BaseController : Controller
    {
        /// <summary>
        /// 执行前调用
        /// </summary>
        /// <param name="context">请求上下文</param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            //参数映射：支持application/json
            string contentType = context.HttpContext.Request.ContentType;
            if (!contentType.IsNullOrEmpty() && contentType.Contains("application/json"))
            {
                var actionParameters = context.ActionDescriptor.Parameters;
                var allParamters = HttpHelper.GetAllRequestParams(context.HttpContext);
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

        /// <summary>
        /// 在调用操作方法后调用
        /// </summary>
        /// <param name="filterContext">请求上下文</param>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            context.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            context.HttpContext.Response.Headers.Add("Access-Control-Allow-Headers", "*");
            context.HttpContext.Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE");
        }

        /// <summary>
        /// 默认返回JSON
        /// </summary>
        /// <param name="content">JSON字符串</param>
        /// <returns></returns>
        public override ContentResult Content(string content)
        {
            return JsonContent(content);
        }

        public ContentResult JsonContent(string jsonStr)
        {
            return base.Content(jsonStr, "application/json", Encoding.UTF8);
        }

        public ContentResult HtmlContent(string body)
        {
            return base.Content(body);
        }

        /// <summary>
        /// 返回成功
        /// </summary>
        /// <returns></returns>
        public ContentResult Success()
        {
            AjaxResult res = new AjaxResult
            {
                Success = true,
                Msg = "请求成功！",
                Data = null
            };

            return Content(res.ToJson());
        }

        /// <summary>
        /// 返回成功
        /// </summary>
        /// <param name="msg">消息</param>
        /// <returns></returns>
        public ContentResult Success(string msg)
        {
            AjaxResult res = new AjaxResult
            {
                Success = true,
                Msg = msg,
                Data = null
            };

            return Content(res.ToJson());
        }

        /// <summary>
        /// 返回成功
        /// </summary>
        /// <param name="data">返回的数据</param>
        /// <returns></returns>
        public ContentResult Success(object data)
        {
            AjaxResult res = new AjaxResult
            {
                Success = true,
                Msg = "请求成功！",
                Data = data
            };

            return Content(res.ToJson());
        }

        /// <summary>
        /// 返回成功
        /// </summary>
        /// <param name="msg">返回的消息</param>
        /// <param name="data">返回的数据</param>
        /// <returns></returns>
        public ContentResult Success(string msg, object data)
        {
            AjaxResult res = new AjaxResult
            {
                Success = true,
                Msg = msg,
                Data = data
            };

            return Content(res.ToJson());
        }

        /// <summary>
        /// 返回错误
        /// </summary>
        /// <returns></returns>
        public ContentResult Error()
        {
            AjaxResult res = new AjaxResult
            {
                Success = false,
                Msg = "请求失败！",
                Data = null
            };

            return Content(res.ToJson());
        }

        /// <summary>
        /// 返回错误
        /// </summary>
        /// <param name="msg">错误提示</param>
        /// <returns></returns>
        public ContentResult Error(string msg)
        {
            AjaxResult res = new AjaxResult
            {
                Success = false,
                Msg = msg,
                Data = null
            };

            return Content(res.ToJson());
        }

        /// <summary>
        /// 返回数据表格数据
        /// 注：BootstrapTable格式
        /// </summary>
        /// <param name="dataList">数据列表</param>
        /// <param name="pagination">分页参数</param>
        /// <returns></returns>
        public ActionResult DataTable_Bootstrap(object dataList, Pagination pagination)
        {
            return Content(pagination.BuildTableResult_BootstrapTable(dataList).ToJson());
        }

        /// <summary>
        /// 当前URL是否包含某字符串
        /// 注：忽略大小写
        /// </summary>
        /// <param name="subUrl">包含的字符串</param>
        /// <returns></returns>
        public bool UrlContains(string subUrl)
        {
            return Request.Path.ToString().ToLower().Contains(subUrl.ToLower());
        }
    }
}