using Coldairarrow.Business.Base_SysManage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace Coldairarrow.Web
{
    /// <summary>
    /// 校验AppId接口权限
    /// </summary>
    public class CheckAppIdPermissionAttribute : Attribute, IActionFilter
    {
        /// <summary>
        /// Action执行之前执行
        /// </summary>
        /// <param name="filterContext">过滤器上下文</param>
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            IPermissionManage PermissionManage = AutofacHelper.GetScopeService<IPermissionManage>();
            IUrlPermissionManage UrlPermissionManage = AutofacHelper.GetScopeService<IUrlPermissionManage>();

            //若为本地测试，则不需要校验
            if (GlobalSwitch.RunModel == RunModel.LocalTest)
            {
                return;
            }
            AjaxResult res = new AjaxResult();
            //判断是否需要校验
            if (filterContext.ContainsFilter<IgnoreAppIdPermissionAttribute>())
                return;

            var allRequestParams = HttpHelper.GetAllRequestParams(filterContext.HttpContext);
            if (!allRequestParams.ContainsKey("appId"))
            {
                res.Success = false;
                res.Msg = "缺少appId参数！";
                filterContext.Result = new ContentResult { Content = res.ToJson() };

                return;
            }
            string appId = allRequestParams["appId"]?.ToString();
            var allUrlPermissions = UrlPermissionManage.GetAllUrlPermissions();
            string requestUrl = filterContext.HttpContext.Request.Path;
            var thePermission = allUrlPermissions.Where(x => requestUrl.Contains(x.Url.ToLower())).FirstOrDefault();
            if (thePermission == null)
                return;
            string needPermission = thePermission.PermissionValue;
            bool hasPermission = PermissionManage.GetAppIdPermissionValues(appId).Any(x => x.ToLower() == needPermission.ToLower());
            if (hasPermission)
                return;
            else
            {
                res.Success = false;
                res.Msg = "权限不足！访问失败！";
                filterContext.Result = new ContentResult { Content = res.ToJson() };
            }
        }

        /// <summary>
        /// Action执行完毕之后执行
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {

        }
    }
}