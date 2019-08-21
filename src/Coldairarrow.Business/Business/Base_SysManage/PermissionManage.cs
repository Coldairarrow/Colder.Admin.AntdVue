using Coldairarrow.Entity.Base_SysManage;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using static Coldairarrow.Entity.Base_SysManage.EnumType;

namespace Coldairarrow.Business.Base_SysManage
{
    /// <summary>
    /// 权限管理静态类
    /// </summary>
    public class PermissionManage : IPermissionManage, IDependency
    {
        #region DI

        public IBase_UserBusiness _sysUserBus { get => AutofacHelper.GetScopeService<IBase_UserBusiness>(); }
        public IOperator _operator { get => AutofacHelper.GetScopeService<IOperator>(); }
        public IBase_SysRoleBusiness RoleBus { get => AutofacHelper.GetScopeService<IBase_SysRoleBusiness>(); }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        static PermissionManage()
        {
            InitAllPermissionModules();
            InitAllPermissionValues();
        }

        #endregion

        #region 内部成员

        private static string _permissionConfigFile { get; } = "~/Config/Permission.config";
        private static List<PermissionModule> _allPermissionModules { get; set; }
        private static List<string> _allPermissionValues { get; set; }
        private static void InitAllPermissionModules()
        {
            List<PermissionModule> resList = new List<PermissionModule>();
            string filePath = PathHelper.GetAbsolutePath(_permissionConfigFile);
            XElement xe = XElement.Load(filePath);
            xe.Elements("module")?.ForEach(aModule =>
            {
                PermissionModule newModule = new PermissionModule();
                resList.Add(newModule);

                newModule.Name = aModule.Attribute("name")?.Value;
                newModule.Value = aModule.Attribute("value")?.Value;
                newModule.Items = new List<PermissionItem>();
                aModule?.Elements("permission")?.ForEach(aItem =>
                {
                    PermissionItem newItem = new PermissionItem();
                    newModule.Items.Add(newItem);

                    newItem.Name = aItem?.Attribute("name")?.Value;
                    newItem.Value = $"{newModule.Value}.{aItem?.Attribute("value")?.Value}";
                });
            });

            _allPermissionModules = resList;
        }
        private static void InitAllPermissionValues()
        {
            List<string> resList = new List<string>();

            GetAllPermissionModules()?.ForEach(aModule =>
            {
                aModule.Items?.ForEach(aItem =>
                {
                    resList.Add(aItem.Value);
                });
            });

            _allPermissionValues = resList;
        }
        private static List<PermissionModule> GetPermissionModules(List<string> hasPermissions)
        {
            var permissionModules = GetAllPermissionModules();
            permissionModules.ForEach(aModule =>
            {
                aModule.Items?.ForEach(aItem =>
                {
                    aItem.IsChecked = hasPermissions.Contains(aItem.Value);
                });
            });

            return permissionModules;
        }
        private static List<PermissionModule> GetAllPermissionModules()
        {
            return _allPermissionModules.DeepClone();
        }
        private static string _cacheKey { get; } = "Permission";
        private static string BuildCacheKey(string key)
        {
            return $"{GlobalSwitch.ProjectName}_{_cacheKey}_{key}";
        }

        #endregion

        #region 所有权限

        /// <summary>
        /// 获取所有权限值
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllPermissionValues()
        {
            return _allPermissionValues.DeepClone();
        }

        #endregion

        #region 角色权限

        /// <summary>
        /// 获取角色权限模块
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public List<PermissionModule> GetRolePermissionModules(string roleId)
        {
            BaseBusiness<Base_PermissionRole> _db = new BaseBusiness<Base_PermissionRole>();
            List<string> permissions = new List<string>();
            var theRoleInfo = RoleBus.GetTheInfo(roleId);
            if (theRoleInfo.RoleType == EnumType.RoleType.超级管理员)
                permissions = _allPermissionValues.DeepClone();
            else
                permissions = _db.GetIQueryable().Where(x => x.RoleId == roleId).Select(x => x.PermissionValue).ToList();

            return GetPermissionModules(permissions);
        }

        #endregion

        #region AppId权限

        /// <summary>
        /// 获取AppId权限模块
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        public List<PermissionModule> GetAppIdPermissionModules(string appId)
        {
            var hasPermissions = GetAppIdPermissionValues(appId);

            return GetPermissionModules(hasPermissions);
        }

        /// <summary>
        /// 获取AppId权限值
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        public List<string> GetAppIdPermissionValues(string appId)
        {
            string cacheKey = BuildCacheKey(appId);
            var permissions = CacheHelper.Cache.GetCache<List<string>>(cacheKey);
            if (permissions == null)
            {
                BaseBusiness<Base_PermissionAppId> _db = new BaseBusiness<Base_PermissionAppId>();
                permissions = _db.GetIQueryable().Where(x => x.AppId == appId).Select(x => x.PermissionValue).ToList();

                CacheHelper.Cache.SetCache(cacheKey, permissions);
            }

            return permissions.DeepClone();
        }

        /// <summary>
        /// 设置AppId权限
        /// </summary>
        /// <param name="appId">AppId</param>
        /// <param name="permissions">权限值列表</param>
        public void SetAppIdPermission(string appId, List<string> permissions)
        {
            //更新缓存
            string cacheKey = BuildCacheKey(appId);
            CacheHelper.Cache.SetCache(cacheKey, permissions);

            //更新数据库
            BaseBusiness<Base_UnitTest> _db = new BaseBusiness<Base_UnitTest>();
            var Service = _db.Service;

            Service.Delete<Base_PermissionAppId>(x => x.AppId == appId);

            List<Base_PermissionAppId> insertList = new List<Base_PermissionAppId>();
            permissions.ForEach(newPermission =>
            {
                insertList.Add(new Base_PermissionAppId
                {
                    Id = IdHelper.GetId(),
                    AppId = appId,
                    PermissionValue = newPermission
                });
            });

            Service.Insert(insertList);
        }

        #endregion

        #region 用户权限

        /// <summary>
        /// 获取用户权限模块
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<PermissionModule> GetUserPermissionModules(string userId)
        {
            var userInfo = _sysUserBus.GetTheInfo(userId);
            List<string> hasPermissions = new List<string>();
            if (userInfo.RoleType.HasFlag(RoleType.超级管理员) || userId == "Admin")
                hasPermissions = _allPermissionValues.DeepClone();
            else
                hasPermissions = GetUserPermissionValues(userId);

            return GetPermissionModules(hasPermissions);
        }

        /// <summary>
        /// 获取用户拥有的所有权限值
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public List<string> GetUserPermissionValues(string userId)
        {
            string cacheKey = BuildCacheKey(userId);
            var permissions = CacheHelper.Cache.GetCache<List<string>>(cacheKey)?.DeepClone();

            if (permissions == null)
            {
                UpdateUserPermissionCache(userId);
                permissions = CacheHelper.Cache.GetCache<List<string>>(cacheKey)?.DeepClone();
            }

            return permissions;
        }

        /// <summary>
        /// 设置用户权限
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="permissions">权限值列表</param>
        public void SetUserPermission(string userId, List<string> permissions)
        {
            //更新数据库
            BaseBusiness<Base_UnitTest> _db = new BaseBusiness<Base_UnitTest>();
            var Service = _db.Service;

            Service.Delete<Base_PermissionUser>(x => x.UserId == userId);
            var roleIdList = _db.Service.GetIQueryable<Base_UserRoleMap>().Where(x => x.UserId == userId).Select(x => x.RoleId).ToList();
            var existsPermissions = Service.GetIQueryable<Base_PermissionRole>()
                .Where(x => roleIdList.Contains(x.RoleId) && permissions.Contains(x.PermissionValue))
                .GroupBy(x => x.PermissionValue)
                .Select(x => x.Key)
                .ToList();
            permissions.RemoveAll(x => existsPermissions.Contains(x));

            List<Base_PermissionUser> insertList = new List<Base_PermissionUser>();
            permissions.ForEach(newPermission =>
            {
                insertList.Add(new Base_PermissionUser
                {
                    Id = IdHelper.GetId(),
                    UserId = userId,
                    PermissionValue = newPermission
                });
            });

            Service.Insert(insertList);

            //更新缓存
            UpdateUserPermissionCache(userId);
        }

        /// <summary>
        /// 清除所有用户权限缓存
        /// </summary>
        public void ClearUserPermissionCache()
        {
            BaseBusiness<Base_UnitTest> _db = new BaseBusiness<Base_UnitTest>();
            var userIdList = _db.Service.GetIQueryable<Base_User>().Select(x => x.Id).ToList();
            userIdList.ForEach(aUserId =>
            {
                CacheHelper.Cache.RemoveCache(BuildCacheKey(aUserId));
            });
        }

        /// <summary>
        /// 更新用户权限缓存
        /// </summary>
        /// <param name="userId"><用户Id/param>
        public void UpdateUserPermissionCache(string userId)
        {
            string cacheKey = BuildCacheKey(userId);
            List<string> permissions = new List<string>();

            BaseBusiness<Base_PermissionUser> _db = new BaseBusiness<Base_PermissionUser>();
            var userPermissions = _db.GetIQueryable().Where(x => x.UserId == userId).Select(x => x.PermissionValue).ToList();
            var theUser = _db.Service.GetIQueryable<Base_User>().Where(x => x.Id == userId).FirstOrDefault();
            var roleIdList = _sysUserBus.GetUserRoleIds(userId);
            var rolePermissions = _db.Service.GetIQueryable<Base_PermissionRole>().Where(x => roleIdList.Contains(x.RoleId)).GroupBy(x => x.PermissionValue).Select(x => x.Key).ToList();
            var existsPermissions = userPermissions.Concat(rolePermissions).Distinct();

            permissions = existsPermissions.ToList();
            CacheHelper.Cache.SetCache(cacheKey, permissions);
        }

        #endregion

        #region 当前操作用户权限

        /// <summary>
        /// 获取当前操作者拥有的所有权限值
        /// </summary>
        /// <returns></returns>
        public List<string> GetOperatorPermissionValues()
        {
            if (_operator.IsAdmin())
                return GetAllPermissionValues();
            else
                return GetUserPermissionValues(_operator.UserId);
        }

        /// <summary>
        /// 判断当前操作者是否拥有某项权限值
        /// </summary>
        /// <param name="value">权限值</param>
        /// <returns></returns>
        public bool OperatorHasPermissionValue(string value)
        {
            return GetOperatorPermissionValues().Exists(x => x.ToLower() == value.ToLower());
        }

        #endregion
    }
}