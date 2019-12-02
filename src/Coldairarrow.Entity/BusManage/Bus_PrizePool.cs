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
    /// 奖品池表
    /// </summary>
    [Table("Bus_PrizePool")]
    public class Bus_PrizePool
    {

        /// <summary>
        /// 主键
        /// </summary>
        [Key, Column(Order = 1)]
        public String Id { get; set; }

        /// <summary>
        /// 奖品池类型，0：普通奖品池，1：大奖品池
        /// </summary>
        public Int32 PoolType { get; set; }

        /// <summary>
        /// 奖品名
        /// </summary>
        public String PrizeName { get; set; }

        /// <summary>
        /// 奖品类型，0：谢谢惠顾，1：现金红包，2：实物
        /// </summary>
        public Int32 PrizeType { get; set; }

        /// <summary>
        /// 奖品详情
        /// </summary>
        public String PrizeInfo { get; set; }

        /// <summary>
        /// 价值
        /// </summary>
        public decimal Money { get; set; }

        /// <summary>
        /// 库存
        /// </summary>
        public Int32 Stock { get; set; }

        /// <summary>
        /// 中奖权重（概率）
        /// </summary>
        public Int32 WinningWeight { get; set; }

        /// <summary>
        /// 已使用数量
        /// </summary>
        public Int32 UsedStock { get; set; }

        /// <summary>
        /// 红包最大额度
        /// </summary>
        public decimal MaxMoney { get; set; }

        /// <summary>
        /// 红包最小额度
        /// </summary>
        public decimal MinMoney { get; set; }

        /// <summary>
        /// 每日限量
        /// </summary>
        public int DayCountSum { get; set; }

        /// <summary>
        /// 简短名
        /// </summary>
        public string ShortName { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public string Img { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 中奖概率
        /// </summary>
        public double Rate { get; set; }
    }
}