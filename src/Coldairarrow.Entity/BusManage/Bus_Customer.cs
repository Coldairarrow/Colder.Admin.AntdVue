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
    /// 微信用户表
    /// </summary>
    [Table("Bus_Customer")]
    public class Bus_Customer
    {

        /// <summary>
        /// 主键
        /// </summary>
        [Key, Column(Order = 1)]
        public String Id { get; set; }

        /// <summary>
        /// 微信OpenID
        /// </summary>
        public String OpenId { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public String NickName { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public String HeadImg { get; set; }

        /// <summary>
        /// 是否为会员
        /// </summary>
        public Boolean IsVIP { get; set; }

        /// <summary>
        /// 游戏总分
        /// </summary>
        public Int32 ScoreSum { get; set; }

        /// <summary>
        /// 总星星数
        /// </summary>
        public Int32 StarSum { get; set; }

        /// <summary>
        /// 省份
        /// </summary>
        public String Province { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        public String City { get; set; }

        /// <summary>
        /// 三江集OpenId
        /// </summary>
        public String SJJOpneId { get; set; }

        /// <summary>
        /// 会员系统OpenId
        /// </summary>
        public String GaoxinVIPOpenUserId { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public String PhoneNum { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public String RealName { get; set; }

        /// <summary>
        /// 从游戏获取星星数
        /// </summary>
        public Int32 GameStar { get; set; }

        /// <summary>
        /// 普通抽奖总次数
        /// </summary>
        public Int32 NormalChanceSum { get; set; }

        /// <summary>
        /// 普通抽奖已使用次数
        /// </summary>
        public Int32 NormalChanceUsed { get; set; }

        /// <summary>
        /// 当前助跑距离
        /// </summary>
        public Int32 HelpRunDistance { get; set; }

        /// <summary>
        /// 已使用星星数
        /// </summary>
        public int StarUsed { get; set; }

        /// <summary>
        /// 东论OpenId
        /// </summary>
        public string BBSOpenId { get; set; }

        /// <summary>
        /// 水街OpenId
        /// </summary>
        public string ShuijieOpenId { get; set; }

        /// <summary>
        /// 泛太OpenId
        /// </summary>
        public string PanPacificOpenId { get; set; }

        /// <summary>
        /// 已关注公众号
        /// </summary>
        public bool Subscribed { get; set; }

        /// <summary>
        /// 阳明OpenId
        /// </summary>
        public string YangmingOpenId { get; set; }

        /// <summary>
        /// 水街MemberNum
        /// </summary>
        public string ShuijieMemberNum { get; set; }
        
        /// <summary>
        /// 区域Id
        /// </summary>
        public String AreaId { get; set; }
    }
}