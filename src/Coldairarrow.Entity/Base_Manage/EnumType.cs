namespace Coldairarrow.Entity.Base_Manage
{
    /// <summary>
    /// 枚举类型
    /// </summary>
    public class EnumType
    {
        /// <summary>
        /// 系统角色类型
        /// </summary>
        public enum RoleTypes
        {
            超级管理员 = 1,
            部门管理员 = 2
        }

        /// <summary>
        /// 类型,菜单=0,页面=1,权限=2
        /// </summary>
        public enum ActionType
        {
            菜单 = 0,
            页面 = 1,
            权限 = 2
        }
    }
}
