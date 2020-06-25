using System;

namespace Coldairarrow.Entity
{
    /// <summary>
    /// 系统角色类型
    /// </summary>
    [Flags]
    public enum RoleTypes
    {
        超级管理员 = 1,
        部门管理员 = 2
    }
}
