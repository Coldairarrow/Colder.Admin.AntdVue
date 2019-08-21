using Coldairarrow.Util;
using System.Collections.Generic;

namespace Coldairarrow.Business.Base_SysManage
{
    /// <summary>
    /// 权限管理接口
    /// </summary>
    public interface IPermissionManage
    {
        #region 所有权限

        /// <summary>
        /// 获取所有权限值
        /// </summary>
        /// <returns></returns>
        List<string> GetAllPermissionValues();

        #endregion

        #region 角色权限

        /// <summary>
        /// 获取角色权限模块
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        List<PermissionModule> GetRolePermissionModules(string roleId);

        #endregion

        #region AppId权限

        /// <summary>
        /// 获取AppId权限模块
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        List<PermissionModule> GetAppIdPermissionModules(string appId);

        /// <summary>
        /// 获取AppId权限值
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        List<string> GetAppIdPermissionValues(string appId);

        /// <summary>
        /// 设置AppId权限
        /// </summary>
        /// <param name="appId">AppId</param>
        /// <param name="permissions">权限值列表</param>
        void SetAppIdPermission(string appId, List<string> permissions);

        #endregion

        #region 用户权限

        /// <summary>
        /// 获取用户权限模块
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<PermissionModule> GetUserPermissionModules(string userId);

        /// <summary>
        /// 获取用户拥有的所有权限值
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        List<string> GetUserPermissionValues(string userId);

        /// <summary>
        /// 设置用户权限
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="permissions">权限值列表</param>
        void SetUserPermission(string userId, List<string> permissions);

        /// <summary>
        /// 清除所有用户权限缓存
        /// </summary>
        void ClearUserPermissionCache();

        /// <summary>
        /// 更新用户权限缓存
        /// </summary>
        /// <param name="userId"><用户Id/param>
        void UpdateUserPermissionCache(string userId);

        #endregion

        #region 当前操作用户权限

        /// <summary>
        /// 获取当前操作者拥有的所有权限值
        /// </summary>
        /// <returns></returns>
        List<string> GetOperatorPermissionValues();

        /// <summary>
        /// 判断当前操作者是否拥有某项权限值
        /// </summary>
        /// <param name="value">权限值</param>
        /// <returns></returns>
        bool OperatorHasPermissionValue(string value);

        #endregion
    }

    #region 数据模型

    public class PermissionModule
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public List<PermissionItem> Items { get; set; }
    }

    public class PermissionItem
    {
        public string Id { get; set; } = IdHelper.GetId();
        public string Name { get; set; }
        public string Value { get; set; }
        public bool IsChecked { get; set; }
    }

    #endregion
}