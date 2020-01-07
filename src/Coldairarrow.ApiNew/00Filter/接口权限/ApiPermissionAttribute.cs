using Coldairarrow.Business.Base_Manage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Coldairarrow.Api
{
    /// <summary>
    /// 接口权限校验
    /// </summary>
    public class ApiPermissionAttribute : BaseActionFilter, IActionFilter
    {
        public ApiPermissionAttribute(string permissionValue)
        {
            if (permissionValue.IsNullOrEmpty())
                throw new Exception("permissionValue不能为空");

            _permissionValue = permissionValue;
        }

        public string _permissionValue { get; }

        /// <summary>
        /// Action执行之前执行
        /// </summary>
        /// <param name="context">过滤器上下文</param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ContainsFilter<NoApiPermissionAttribute>())
                return;

            IPermissionBusiness permissionBus = AutofacHelper.GetScopeService<IPermissionBusiness>();
            var permissions = permissionBus.GetUserPermissionValues(Operator.UserId);
            if (!permissions.Contains(_permissionValue))
                context.Result = Error("权限不足!");
        }

        /// <summary>
        /// Action执行完毕之后执行
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}