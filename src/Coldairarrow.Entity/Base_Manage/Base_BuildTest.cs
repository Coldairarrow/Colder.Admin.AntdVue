using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.Base_Manage
{
    /// <summary>
    /// 生成测试表
    /// </summary>
    [Table("Base_BuildTest")]
    public class Base_BuildTest
    {

        /// <summary>
        /// 自然主键
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
        /// 列1
        /// </summary>
        public String Column1 { get; set; }

        /// <summary>
        /// 列2
        /// </summary>
        public String Column2 { get; set; }

        /// <summary>
        /// 列3
        /// </summary>
        public String Column3 { get; set; }

        /// <summary>
        /// 列4
        /// </summary>
        public String Column4 { get; set; }

        /// <summary>
        /// 列5
        /// </summary>
        public String Column5 { get; set; }

    }
}