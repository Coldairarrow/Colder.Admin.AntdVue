using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.Base_Manage
{
    /// <summary>
    /// 系统日志表
    /// </summary>
    [Table("Base_SysLog")]
    public class Base_SysLog
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
        /// 日志级别
        /// </summary>
        public String Level { get; set; }

        /// <summary>
        /// 日志类型
        /// </summary>
        public String LogType { get; set; }

        /// <summary>
        /// 日志内容
        /// </summary>
        public String LogContent { get; set; }

        /// <summary>
        /// 操作员用户名
        /// </summary>
        public String OpUserName { get; set; }

        /// <summary>
        /// 日志记录时间
        /// </summary>
        public DateTime? OpTime { get; set; }

        /// <summary>
        /// 数据备份（转为JSON字符串）
        /// </summary>
        public String Data { get; set; }

    }
}