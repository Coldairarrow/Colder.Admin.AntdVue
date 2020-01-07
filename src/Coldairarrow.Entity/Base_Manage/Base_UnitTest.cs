using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.Base_Manage
{
    /// <summary>
    /// 单元测试表
    /// </summary>
    [Table("Base_UnitTest")]
    public class Base_UnitTest
    {

        /// <summary>
        /// 代理主键
        /// </summary>
        [Key]
        public String Id { get; set; }

        /// <summary>
        /// UserId
        /// </summary>
        public String UserId { get; set; }

        /// <summary>
        /// UserName
        /// </summary>
        public String UserName { get; set; }

        /// <summary>
        /// Age
        /// </summary>
        public Int32? Age { get; set; }

    }
}