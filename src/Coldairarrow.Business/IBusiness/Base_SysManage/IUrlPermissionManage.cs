using System.Collections.Generic;

namespace Coldairarrow.Business.Base_SysManage
{
    public interface IUrlPermissionManage
    {
        #region 外部接口

        /// <summary>
        /// 获取所有URL需要的权限
        /// </summary>
        /// <returns></returns>
        List<ActionPermission> GetAllUrlPermissions();

        #endregion
    }

    #region 数据模型

    /// <summary>
    /// URL接口权限
    /// </summary>
    public class ActionPermission
    {
        public string Url { get; set; }
        public string PermissionValue { get; set; }
    }

    #endregion
}