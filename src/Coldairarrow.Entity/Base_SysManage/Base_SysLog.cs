using Nest;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.Base_SysManage
{
    /// <summary>
    /// 系统日志表
    /// </summary>
    [Table("Base_SysLog")]
    public class Base_SysLog
    {

        /// <summary>
        /// 代理主键
        /// </summary>
        [Key]
        [Keyword]
        public String Id { get; set; }

        /// <summary>
        /// 日志级别
        /// </summary>
        public string Level { get; set; }

        /// <summary>
        /// 日志类型
        /// </summary>
        [Keyword]
        public String LogType { get; set; }

        /// <summary>
        /// 日志内容
        /// </summary>
        [Keyword]
        public String LogContent { get; set; }

        /// <summary>
        /// 操作员用户名
        /// </summary>
        [Keyword]
        public String OpUserName { get; set; }

        /// <summary>
        /// 日志记录时间
        /// </summary>
        [Keyword]
        public DateTime? OpTime { get; set; }

        /// <summary>
        /// 数据备份（转为JSON字符串）
        /// </summary>
        public string Data { get; set; }
    }
}