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
    /// 获奖记录表
    /// </summary>
    [Table("Bus_WinningPrize")]
    public class Bus_WinningPrize
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
        /// 奖品Id
        /// </summary>
        public String PrizeId { get; set; }

        /// <summary>
        /// 是否已使用
        /// </summary>
        public Boolean IsUsed { get; set; }

        /// <summary>
        /// 中奖时间
        /// </summary>
        public DateTime? WinningTime { get; set; }

        /// <summary>
        /// 红包额度
        /// </summary>
        public decimal Money { get; set; }
    }
}