using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.Base_SysManage
{
    /// <summary>
    /// 系统角色
    /// </summary>
    [Table("Base_SysRole")]
    public class Base_SysRole
    {
        /// <summary>
        /// 角色Id主键
        /// </summary>
        [Key]
        public String Id { get; set; }

        /// <summary>
        /// 角色名
        /// </summary>
        public String RoleName { get; set; }
    }
}