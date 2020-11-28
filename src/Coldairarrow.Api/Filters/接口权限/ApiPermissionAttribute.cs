using Coldairarrow.Business.Base_Manage;
using Coldairarrow.IBusiness;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Coldairarrow.Api
{
    /// <summary>
    /// 接口权限校验
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class ApiPermissionAttribute : BaseActionFilterAsync
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
        public async override Task OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ContainsFilter<NoApiPermissionAttribute>())
                return;
            IServiceProvider serviceProvider = context.HttpContext.RequestServices;
            IPermissionBusiness _permissionBus = serviceProvider.GetService<IPermissionBusiness>();
            IOperator _operator = serviceProvider.GetService<IOperator>();

            var permissions = await _permissionBus.GetUserPermissionValuesAsync(_operator.UserId);
            if (!permissions.Contains(_permissionValue))
                context.Result = Error("权限不足!");
        }
    }
}