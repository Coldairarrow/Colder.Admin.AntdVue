using Coldairarrow.Business.Base_Manage;
using Coldairarrow.Business.Cache;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Http;
using static Coldairarrow.Entity.Base_Manage.EnumType;

namespace Coldairarrow.Business
{
    /// <summary>
    /// 操作者
    /// </summary>
    public class Operator : IOperator, IScopeDependency
    {
        readonly IBase_UserCache _userCache;
        public Operator(IBase_UserCache userCache, IHttpContextAccessor httpContextAccessor)
        {
            _userCache = userCache;
            UserId = httpContextAccessor?.HttpContext?.Request.GetJWTPayload()?.UserId;
        }

        private Base_UserDTO _property;
        private object _lockObj = new object();

        /// <summary>
        /// 当前操作者UserId
        /// </summary>
        public string UserId { get; }

        /// <summary>
        /// 属性
        /// </summary>
        public Base_UserDTO Property
        {
            get
            {
                if (UserId.IsNullOrEmpty())
                    return default;

                if (_property == null)
                {
                    lock (_lockObj)
                    {
                        if (_property == null)
                        {
                            _property = _userCache.GetCache(UserId);
                        }
                    }
                }

                return _property;
            }
        }

        /// <summary>
        /// 判断是否为超级管理员
        /// </summary>
        /// <returns></returns>
        public bool IsAdmin()
        {
            var role = Property.RoleType;
            if (UserId == GlobalSwitch.AdminId || role.HasFlag(RoleTypeEnum.超级管理员))
                return true;
            else
                return false;
        }
    }
}
