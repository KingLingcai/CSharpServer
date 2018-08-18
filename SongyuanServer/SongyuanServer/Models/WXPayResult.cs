using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SongyuanServer.Models
{

    public class AttachObject
    {
        public string tc { get; set; }
        public string fn { get; set; }
        public string name { get; set; }
        public string rn { get; set; }
        public string kn { get; set; }
        public string id { get; set; }
    }
    public class WXPayResult
    {
        /// <summary>
        /// 收费说明
        /// </summary>
        public string attach { get ; set ; }
        /// <summary>
        /// 费用名称
        /// </summary>
        public string fee_name { get; set; }
        /// <summary>
        /// 付款银行
        /// </summary>
        public string bank_type { get; set; }
        /// <summary>
        /// 现金支付金额
        /// </summary>
        public double? cash_fee { get; set; }
        /// <summary>
        /// 货币种类
        /// </summary>
        public string fee_type { get; set; }
        /// <summary>
        /// 商户号
        /// </summary>
        public string mch_id { get; set; }
        /// <summary>
        /// openid
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string out_trade_no { get; set; }
        /// <summary>
        /// 支付完成时间
        /// </summary>
        public string time_end { get; set; }
        /// <summary>
        /// 订单金额
        /// </summary>
        public double? total_fee { get; set; }
        /// <summary>
        /// 微信支付订单号
        /// </summary>
        public string transaction_id { get; set; }
    }

}