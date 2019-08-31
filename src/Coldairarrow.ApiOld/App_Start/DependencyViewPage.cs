using Coldairarrow.Business;
using Coldairarrow.Business.Base_SysManage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc.Razor;

namespace Microsoft.AspNetCore.Mvc
{
    public abstract class DependencyViewPage : RazorPage<dynamic>
    {
        public IPermissionManage PermissionManage { get => AutofacHelper.GetScopeService<IPermissionManage>(); }
        public ISystemMenuManage SystemMenuManage { get => AutofacHelper.GetScopeService<ISystemMenuManage>(); }
        public IBase_UserBusiness SysUserBus { get => AutofacHelper.GetScopeService<IBase_UserBusiness>(); }
        public IOperator Operator { get => AutofacHelper.GetScopeService<IOperator>(); }
    }
}