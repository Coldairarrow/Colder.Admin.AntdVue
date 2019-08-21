using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.Base_SysManage
{
    /// <summary>
    /// 数据库连接
    /// </summary>
    [Table("Base_DatabaseLink")]
    public class Base_DatabaseLink
    {

        /// <summary>
        /// 代理主键
        /// </summary>
        [Key]
        public String Id { get; set; }

        /// <summary>
        /// 连接名
        /// </summary>
        public String LinkName { get; set; }

        /// <summary>
        /// 连接字符串
        /// </summary>
        public String ConnectionStr { get; set; }

        /// <summary>
        /// 数据库类型
        /// </summary>
        public String DbType { get; set; }

        /// <summary>
        /// 排序编号
        /// </summary>
        public String SortNum { get; set; }

    }
}