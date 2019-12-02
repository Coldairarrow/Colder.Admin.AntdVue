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
    /// 活动记录表
    /// </summary>
    [Table("Bus_Activity")]
    public class Bus_Activity
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
        /// 活动类型，0：冲浪，1：沙滩跑酷，2：助跑自己跑，3:助跑分享，4:助跑别人助力，5：每日游戏签到，6：大抽奖，101~103：扫线下二维码1~3
        /// </summary>
        public Int32 ActivityType { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public DateTime? ActivityTime { get; set; }

        /// <summary>
        /// 获得游戏分数
        /// </summary>
        public Int32 GameScore { get; set; }

        /// <summary>
        /// 获得星星数
        /// </summary>
        public Int32 GetStarNum { get; set; }

        /// <summary>
        /// 消耗星星数
        /// </summary>
        public Int32 ConsumeStarNum { get; set; }

        /// <summary>
        /// 助力人Id
        /// </summary>
        public string HelperCustomerId { get; set; }

        /// <summary>
        /// 投票项Id
        /// </summary>
        public string VoteOptionId { get; set; }

        /// <summary>
        /// 周数
        /// </summary>
        public int Week { get; set; }
    }
}