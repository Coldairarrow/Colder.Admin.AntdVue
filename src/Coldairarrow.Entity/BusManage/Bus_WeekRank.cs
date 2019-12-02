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
    /// 每周排行榜
    /// </summary>
    [Table("Bus_WeekRank")]
    public class Bus_WeekRank
    {

        /// <summary>
        /// 主键
        /// </summary>
        [Key, Column(Order = 1)]
        public String Id { get; set; }

        /// <summary>
        /// 微信用户Id
        /// </summary>
        public String CustomerId { get; set; }

        /// <summary>
        /// 最大分数
        /// </summary>
        public Int32 MaxScore { get; set; }

        /// <summary>
        /// 奖品
        /// </summary>
        public String Prize { get; set; }

        /// <summary>
        /// 周数
        /// </summary>
        public Int32 Week { get; set; }

        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime Time { get; set; }
    }
}