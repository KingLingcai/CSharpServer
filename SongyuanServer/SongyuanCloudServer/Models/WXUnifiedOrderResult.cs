using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SongyuanCloudServer.Models
{
    public class WXUnifiedOrderResult
    {
        /// <summary>
        /// SUCCESS/FAIL 此字段是通信标识，非交易标识，交易是否成功需要查看result_code来判断
        /// </summary>
        public string return_code { get; set; }
        /// <summary>
        /// 返回信息，如非空，为错误原因 签名失败 参数格式校验错误
        /// </summary>
        public string return_msg { get; set; }
        /// <summary>
        /// 调用接口提交的小程序ID
        /// </summary>
        public string appid { get; set; }
        /// <summary>
        /// 调用接口提交的商户号
        /// </summary>
        public string mch_id { get; set; }
        /// <summary>
        /// 调用接口提交的终端设备号
        /// </summary>
        public string device_info { get; set; }
        /// <summary>
        /// 微信返回的随机字符串
        /// </summary>
        public string nonce_str { get; set; }
        /// <summary>
        /// 微信返回的签名
        /// </summary>
        public string sign { get; set; }
        /// <summary>
        /// 业务结果SUCCESS/FAIL
        /// </summary>
        public string result_code { get; set; }
        /// <summary>
        /// 错误代码
        /// </summary>
        public string err_code { get; set; }
        /// <summary>
        /// 错误代码描述
        /// </summary>
        public string err_code_des { get; set; }
        /// <summary>
        /// 交易类型 调用接口提交的交易类型，取值如下：JSAPI
        /// </summary>
        public string trade_type { get; set; }
        /// <summary>
        /// 预支付交易会话标识  微信生成的预支付回话标识，用于后续接口调用中使用，该值有效期为2小时
        /// </summary>
        public string prepay_id { get; set; }
    }
}