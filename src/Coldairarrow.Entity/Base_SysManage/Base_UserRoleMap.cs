using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.Base_SysManage
{
    /// <summary>
    /// Base_UserRoleMap
    /// </summary>
    [Table("Base_UserRoleMap")]
    public class Base_UserRoleMap
    {

        /// <summary>
        /// Id
        /// </summary>
        [Key]
        public String Id { get; set; }

        /// <summary>
        /// UserId
        /// </summary>
        public String UserId { get; set; }

        /// <summary>
        /// RoleId
        /// </summary>
        public String RoleId { get; set; }

    }
}