using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NpgsqlTypes;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;

namespace Coldairarrow.Entity.BusManage
{
    /// <summary>
    /// 投票选项表
    /// </summary>
    [Table("Bus_VoteOption")]
    public class Bus_VoteOption
    {

        /// <summary>
        /// 主键
        /// </summary>
        [Key, Column(Order = 1)]
        public String Id { get; set; }

        /// <summary>
        /// 选项名
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public String Img { get; set; }

        /// <summary>
        /// 票数
        /// </summary>
        public Int32 Count { get; set; }

        /// <summary>
        /// 详情
        /// </summary>
        public string Detail { get; set; }
    }
}