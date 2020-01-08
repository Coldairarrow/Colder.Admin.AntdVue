using Nest;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.Base_Manage
{
    /// <summary>
    /// 系统日志表
    /// </summary>
    [Table("Base_Log")]
    public class Base_Log
    {

        /// <summary>
        /// 自然主键
        /// </summary>
        [Key, Column(Order = 1)]
        [Keyword]
        public String Id { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Keyword]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建人Id
        /// </summary>
        [Keyword]
        public String CreatorId { get; set; }

        /// <summary>
        /// 创建人姓名
        /// </summary>
        [Keyword]
        public String CreatorRealName { get; set; }

        /// <summary>
        /// 否已删除
        /// </summary>
        [Keyword]
        public Boolean Deleted { get; set; }

        /// <summary>
        /// 日志级别
        /// </summary>
        [Keyword]
        public String Level { get; set; }

        /// <summary>
        /// 日志类型
        /// </summary>
        [Keyword]
        public String LogType { get; set; }

        /// <summary>
        /// 日志内容
        /// </summary>
        [Text(Analyzer = "ik_max_word")]
        public String LogContent { get; set; }

        /// <summary>
        /// 数据备份（转为JSON字符串）
        /// </summary>
        [Text(Index = false)]
        public String Data { get; set; }

    }
}