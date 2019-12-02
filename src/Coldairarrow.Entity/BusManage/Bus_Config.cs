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
    /// 业务配置表
    /// </summary>
    [Table("Bus_Config")]
    public class Bus_Config
    {

        /// <summary>
        /// 主键
        /// </summary>
        [Key, Column(Order = 1)]
        public String Id { get; set; }

        /// <summary>
        /// 开启红包限制
        /// </summary>
        public Boolean OpenReadPackLimit { get; set; }

        /// <summary>
        /// 正式开始游戏
        /// </summary>
        public Boolean StartGame { get; set; }

    }
}