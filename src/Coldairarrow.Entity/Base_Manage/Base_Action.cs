using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.Base_Manage
{
    /// <summary>
    /// 系统权限表
    /// </summary>
    [Table("Base_Action")]
    public class Base_Action
    {

        /// <summary>
        /// 主键
        /// </summary>
        [Key, Column(Order = 1)]
        public String Id { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建人Id
        /// </summary>
        public String CreatorId { get; set; }

        /// <summary>
        /// 创建人姓名
        /// </summary>
        public String CreatorRealName { get; set; }

        /// <summary>
        /// 否已删除
        /// </summary>
        public Boolean Deleted { get; set; }

        /// <summary>
        /// 父级Id
        /// </summary>
        public String ParentId { get; set; }

        /// <summary>
        /// 类型,菜单=0,权限=1
        /// </summary>
        public Int32 Type { get; set; }

        /// <summary>
        /// 权限名/菜单名
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// 菜单地址
        /// </summary>
        public String Url { get; set; }

        /// <summary>
        /// 权限值
        /// </summary>
        public String Value { get; set; }

    }
}