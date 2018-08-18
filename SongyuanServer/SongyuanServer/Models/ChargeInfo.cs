using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SongyuanServer.Models
{
    public class ChargeInfo
    {
        public string chareType { get; set; }
        public string chargeName { get; set; }
        public double? charge { get; set; }
        //public Charge[] chargeDetail { get; set; }
    }

    public class Charge
    {
        public string chargeName { get; set; }
        public double? charge { get; set; }
    }


    public class SignupCharge
    {
        /// <summary>
        /// ID
        /// </summary>
        public int? signupId { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 保教费
        /// </summary>
        public decimal? careFee { get; set; }
        /// <summary>
        /// 伙食费
        /// </summary>
        public decimal? mealFee { get; set; }
        /// <summary>
        /// 入园杂费
        /// </summary>
        public decimal? pettyFee { get; set; }
        /// <summary>
        /// 其他费用
        /// </summary>
        public decimal? otherFee { get; set; }
        /// <summary>
        /// 看园定金
        /// </summary>
        public decimal? kanyuanFee { get; set; }
        /// <summary>
        /// 费用总额
        /// </summary>
        public decimal? totalFee { get; set; }
    }
}