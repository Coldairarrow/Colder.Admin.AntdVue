using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.Base_SysManage
{
    /// <summary>
    /// 部门表
    /// </summary>
    [Table("Base_Department")]
    public class Base_Department
    {

        /// <summary>
        /// 自然主键
        /// </summary>
        [Key, Column(Order = 1)]
        public String Id { get; set; }

        /// <summary>
        /// 部门名
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// 上级部门Id
        /// </summary>
        public String ParentId { get; set; }

    }
}