using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.IO;
using System.Xml.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using SongyuanUtils;
using SongyuanCloudServer.Models;
using System.Collections.Specialized;

namespace SongyuanCloudServer.Business
{
    public class WXPayDal
    {

        public static StatusReport GetChargeInfo(string kindergartenName, string feeName)
        {
            StatusReport sr = new StatusReport();
            //string databaseName = "";
            string dbName = kindergartenName == "松园幼儿园" ? "localsy" : "localyd";
            string sqlString = "select 费用种类,费用名称,应收金额 from 基础_小程序收费设置 where 费用种类 = @费用种类";
            DataTable dt = SQLHelper.ExecuteQuery(dbName, sqlString, new SqlParameter("@费用种类", feeName));
            if (dt.Rows.Count == 0)
            {
                sr.status = "Fail";
                sr.result = "未查询到任何记录";
                return sr;
            }
            DataRow dr = dt.Rows[0];
            ChargeInfo info = new ChargeInfo()
            {
                chareType = DataTypeHelper.GetStringValue(dr["费用种类"]),
                chargeName = DataTypeHelper.GetStringValue(dr["费用名称"]),
                charge = DataTypeHelper.GetDoubleValue(dr["应收金额"])
            };
            sr.status = "Success";
            sr.result = "成功";
            sr.data = info;
            return sr;
        }


        /// <summary>
        /// 统一下单
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="totalCharge"></param>
        /// <returns>StatusReport</returns>
        public static StatusReport UnifiedOrder(string kindergartenName, string fromPage, string openId, double totalCharge, string dataBag)//统一下单
        {
            StatusReport sr = new StatusReport();
            string outTradeNo = GetOutTradeNumber();
            string body = kindergartenName + "-收费";
            string detail = fromPage == "kanyuan" ? "看园定金" : "报名费";
            WXUnifiedOrder order = new WXUnifiedOrder()
            {
                appid = Comman.Appid,
                mch_id = Comman.Mchid,
                attach = dataBag,
                body = body,
                detail = detail,
                nonce_str = GetNonceStr(),
                notify_url = "http://16y7e12590.iask.in/SYServer/WXPay/OnSetWXPayInfo",
                openid = openId,
                out_trade_no = outTradeNo,
                spbill_create_ip = "115.159.93.120",
                total_fee = Convert.ToInt32(totalCharge * 100).ToString(),
                trade_type = "JSAPI",
            };
            string xmlParam = GetXmlParam(order);
            //sr.parameters = xmlParam;
            string url = "https://api.mch.weixin.qq.com/pay/unifiedorder";
            sr = RequestHelper.PostRequest(url, xmlParam);
            if (sr.status == "fail")
            {
                return sr;
            }
            string xmlResult = (string)sr.data;
            sr = GetOrderResult(xmlResult);
            //sr.data = xmlResult;
            sr.parameters = order.out_trade_no;
            //sr.status = "success";
            return sr;
        }

        public static StatusReport SetCharge(string id, string tradeNumber, string totalCharge, string datetime, string fromPage, string kindergartenName)
        {
            StatusReport sr = new StatusReport();
            string dbName = kindergartenName == "松园幼儿园" ? "cloudsy" : "cloudyd";
            string sqlString = "";
            if (fromPage == "kanyuan")
            {
                sqlString = " update 基础_看园管理 set 是否交定金 = @是否交定金, 交费金额 = @交费金额, 交费时间 = @交费时间, 订单编号 = @订单编号 " +
                " where ID = @id ";
            }
            else
            {
                sqlString = " update 基础_小程序报名 set 是否已交费 = @是否已交费, 交费金额 = @交费金额, 交费时间 = @交费时间, 订单编号 = @订单编号 " +
                " where ID = @id ";
            }
            sr = SQLHelper.Update(dbName, sqlString,
                new SqlParameter("@是否已交费", "是"),
                new SqlParameter("@是否交定金", "是"),
                new SqlParameter("@交费金额", totalCharge),
                new SqlParameter("@交费时间", datetime),
                new SqlParameter("@订单编号", tradeNumber),
                new SqlParameter("@id", id));
            return sr;
        }

        public static StatusReport GetSignupChargeList(string kindergartenName, string openid)
        {

            /**
             * SELECT   dbo.基础_小程序报名.姓名, dbo.基础_小程序报名.书包电话, dbo.基础_小程序报名.openid, 
                dbo.基础_小程序报名.保教费, dbo.基础_小程序报名.伙食费, dbo.基础_小程序报名.入园杂费, 
                dbo.基础_小程序报名.其他费用, dbo.基础_小程序报名.是否已交费, dbo.基础_小程序报名.交费金额, 
                dbo.基础_小程序报名.交费时间, dbo.基础_小程序报名.订单编号, dbo.基础_看园管理.交费金额 AS 入园定金
FROM      dbo.基础_小程序报名 LEFT OUTER JOIN
                dbo.基础_看园管理 ON dbo.基础_小程序报名.看园ID = dbo.基础_看园管理.ID 
             * 
             * */



            StatusReport sr = new StatusReport();
            string dbName = kindergartenName == "松园幼儿园" ? "localsy" : "localyd";
            string sqlString = " select top 1 基础_小程序报名.ID,基础_小程序报名.姓名, 基础_小程序报名.保教费, 基础_小程序报名.伙食费, 基础_小程序报名.入园杂费, 基础_小程序报名.其他费用, 基础_看园管理.交费金额 as 看园定金" +
                " from 基础_小程序报名 " +
                " LEFT OUTER JOIN dbo.基础_看园管理 ON 基础_小程序报名.看园ID = 基础_看园管理.ID " +
                " where 基础_小程序报名.openid = @openid and (是否已交费 is null or 是否已交费 = '否')" +
                " order by 基础_小程序报名.ID desc ";
            DataTable dt = SQLHelper.ExecuteQuery(dbName, sqlString, new SqlParameter("@openid", openid));
            if (dt.Rows.Count == 0)
            {
                sr.status = "Fail";
                sr.result = "未查询到任何记录";
                return sr;
            }
            DataRow dr = dt.Rows[0];
            SignupCharge sc = new SignupCharge()
            {
                signupId = DataTypeHelper.GetIntValue(dr["ID"]),
                name = DataTypeHelper.GetStringValue(dr["姓名"]),
                careFee = DataTypeHelper.GetDecimalValue(dr["保教费"]),
                mealFee = DataTypeHelper.GetDecimalValue(dr["伙食费"]),
                pettyFee = DataTypeHelper.GetDecimalValue(dr["入园杂费"]),
                otherFee = DataTypeHelper.GetDecimalValue(dr["其他费用"]),
                kanyuanFee = DataTypeHelper.GetDecimalValue(dr["看园定金"])
            };
            sc.careFee = sc.careFee == null ? 0 : sc.careFee;
            sc.mealFee = sc.mealFee == null ? 0 : sc.mealFee;
            sc.pettyFee = sc.pettyFee == null ? 0 : sc.pettyFee;
            sc.otherFee = sc.otherFee == null ? 0 : sc.otherFee;
            sc.kanyuanFee = sc.kanyuanFee == null ? 0 : sc.kanyuanFee;
            sc.totalFee = sc.careFee + sc.mealFee + sc.pettyFee + sc.otherFee;
            sr.status = "Success";
            sr.result = "成功";
            sr.data = sc;
            return sr;
        }

        public static StatusReport SetWXPayInfo(string xmlValue)
        {
            StatusReport sr = new StatusReport();
            WXPayResult payResult = new WXPayResult();
            payResult = GetWXPayResult(xmlValue);
            string sqlString = " if not exists (select ID from 基础_小程序收费情况 where 微信支付单号 = @微信支付单号) " +
                " insert into 基础_小程序收费情况 (openid,费用名称,收费说明,付款银行,订单金额,现金支付金额,货币种类,商户号,订单编号,支付完成时间,微信支付单号) " +
                " select @openid,@费用名称,@收费说明,@付款银行,@订单金额,@现金支付金额,@货币种类,@商户号,@订单编号,@支付完成时间,@微信支付单号" +
                " select @@identity ";
            sr = SQLHelper.Insert("cloudsy", sqlString,
                new SqlParameter("openid", payResult.openid),
                new SqlParameter("费用名称", payResult.fee_name),
                new SqlParameter("收费说明", payResult.attach),
                new SqlParameter("付款银行", payResult.bank_type),
                new SqlParameter("订单金额", payResult.total_fee),
                new SqlParameter("现金支付金额", payResult.cash_fee),
                new SqlParameter("货币种类", payResult.fee_type),
                new SqlParameter("商户号", payResult.mch_id),
                new SqlParameter("订单编号", payResult.out_trade_no),
                new SqlParameter("支付完成时间", payResult.time_end),
                new SqlParameter("微信支付单号", payResult.transaction_id));
            using (StreamWriter sw = new StreamWriter("D:\\1_importTemp\\TestFile1.txt"))
            {
                sw.WriteLine(sr.result.ToString());
            }
            return sr;
        }

        private static StatusReport GetOrderInfo(string xmlResult)
        {
            StatusReport sr = new StatusReport();
            //xmlResult = "<xml><return_code><![CDATA[SUCCESS]]></return_code><return_msg><![CDATA[OK]]></return_msg><appid><![CDATA[wx3bfed4fd2abe0355]]></appid><mch_id><![CDATA[1492807892]]></mch_id><nonce_str><![CDATA[0mYmAa79azHOQfsT]]></nonce_str><sign><![CDATA[972EBB9552267F6A06426A2667BB7715]]></sign><result_code><![CDATA[SUCCESS]]></result_code><prepay_id>wx20171129080541ec6fa2b7c60713319480</prepay_id><trade_type><![CDATA[JSAPI]]></trade_type></xml>";
            try
            {
                byte[] array = Encoding.ASCII.GetBytes(xmlResult);
                Stream stream = new MemoryStream(array);

                XDocument xDocument = new XDocument();
                xDocument = XDocument.Load(stream);
                XElement root = xDocument.Root;
                XElement prepayid = root.Element("prepay_id");
                string prepayId = prepayid.Value;
                if (!string.IsNullOrEmpty(prepayId))
                {
                    sr.status = "Success";
                    sr.result = "成功获取prepay_id";
                    sr.parameters = xmlResult;
                    sr.data = prepayId;
                }
                else
                {
                    sr.status = "Fail";
                    sr.result = "获取失败";
                }
            }
            catch (Exception exp)
            {
                sr.status = "Fail";
                sr.result = exp.Message;
            }

            return sr;

        }

        private static StatusReport GetOrderResult(string xmlResult)
        {
            StatusReport sr = new StatusReport();
            sr.status = "Success";
            sr.result = "成功";
            WXUnifiedOrderResult orderResult = new WXUnifiedOrderResult();
            //xmlResult = "<xml><return_code><![CDATA[SUCCESS]]></return_code><return_msg><![CDATA[OK]]></return_msg><appid><![CDATA[wx3bfed4fd2abe0355]]></appid><mch_id><![CDATA[1492807892]]></mch_id><nonce_str><![CDATA[0mYmAa79azHOQfsT]]></nonce_str><sign><![CDATA[972EBB9552267F6A06426A2667BB7715]]></sign><result_code><![CDATA[SUCCESS]]></result_code><prepay_id>wx20171129080541ec6fa2b7c60713319480</prepay_id><trade_type><![CDATA[JSAPI]]></trade_type></xml>";
            try
            {
                byte[] array = Encoding.UTF8.GetBytes(xmlResult);
                Stream stream = new MemoryStream(array);
                XDocument xDocument = new XDocument();
                xDocument = XDocument.Load(stream);
                XElement root = xDocument.Root;
                orderResult.return_code = root.Element("return_code").Value;
                orderResult.return_msg = root.Element("return_msg").Value;
                if (orderResult.return_code == "SUCCESS")
                {
                    orderResult.appid = root.Element("appid").Value;
                    orderResult.mch_id = root.Element("mch_id").Value;
                    //orderResult.device_info = root.Element("device_info").Value;
                    orderResult.nonce_str = root.Element("nonce_str").Value;
                    orderResult.sign = root.Element("sign").Value;
                    orderResult.result_code = root.Element("result_code").Value;
                    if (orderResult.result_code == "SUCCESS")
                    {
                        orderResult.trade_type = root.Element("trade_type").Value;
                        orderResult.prepay_id = root.Element("prepay_id").Value;
                    }
                    else
                    {
                        orderResult.err_code = root.Element("err_code").Value;
                        orderResult.err_code_des = root.Element("err_code_des").Value;
                        sr.status = orderResult.err_code;
                        sr.result = orderResult.err_code_des;
                    }
                }
                else
                {
                    sr.status = orderResult.return_code;
                    sr.result = orderResult.return_msg;
                }
            }
            catch (Exception exp)
            {
                sr.status = "Fail";
                sr.result = "Error:" + exp.Message;
            }

            sr.data = JsonConvert.SerializeObject(orderResult);
            return sr;

        }

        private static WXPayResult GetWXPayResult(string xmlResult)
        {
            WXPayResult payResult = new WXPayResult();
            try
            {
                byte[] array = Encoding.UTF8.GetBytes(xmlResult);
                Stream stream = new MemoryStream(array);
                XDocument xDocument = new XDocument();
                xDocument = XDocument.Load(stream);
                XElement root = xDocument.Root;
                string attach = root.Element("attach").Value;
                AttachObject attachObject = new AttachObject();
                attachObject = JsonConvert.DeserializeObject<AttachObject>(attach);
                payResult.fee_name = attachObject.fn;
                payResult.mch_id = root.Element("mch_id").Value;
                payResult.bank_type = root.Element("bank_type").Value;
                payResult.cash_fee = Convert.ToDouble(root.Element("cash_fee").Value) / 100;
                payResult.fee_type = root.Element("fee_type").Value;
                payResult.openid = root.Element("openid").Value;
                payResult.out_trade_no = root.Element("out_trade_no").Value;
                string time = root.Element("time_end").Value;
                payResult.time_end = time.Substring(0, 4) + "-" + time.Substring(4, 2) + "-" + time.Substring(6, 2) + " " + time.Substring(8, 2) + ":" + time.Substring(10, 2) + ":" + time.Substring(12, 2);
                payResult.total_fee = Convert.ToDouble(root.Element("total_fee").Value) / 100;
                payResult.transaction_id = root.Element("transaction_id").Value;
                if (attachObject.fn == "看园定金")
                {
                    payResult.attach = attachObject.fn + ":" + attachObject.name + "的家长" + attachObject.rn + "交费" + attachObject.tc + "元";
                }
                else
                {
                    SignUp s = SignUpDal.GetSignUpInfo(attachObject.kn, attachObject.id);
                    payResult.attach = attachObject.fn + ":" + s.name + "的家长" + s.relateName + "交费" + attachObject.tc + "元";
                }
            }
            catch (Exception exp)
            {
                using (StreamWriter sw = new StreamWriter("D:\\1_importTemp\\TestFile5.txt"))
                {
                    sw.WriteLine("error:" + exp.Message);
                }
            }
            return payResult;
        }


        /// <summary>
        /// 获取统一下单需要的xml参数
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private static string GetXmlParam(WXUnifiedOrder order)

        {
            Dictionary<string, string> dic = ToDictionary(order);
            dic = dic.OrderBy(m => m.Key).ToDictionary(m => m.Key, m => m.Value);
            StringBuilder sb = new StringBuilder();
            StringBuilder sbXml = new StringBuilder();
            sbXml.Append("<xml>");
            foreach (var item in dic)
            {
                sbXml.Append("<" + item.Key + ">" + item.Value + "</" + item.Key + ">");
                sb.Append(item.Key + "=" + item.Value + "&");
            }
            sb.Append("key=" + Comman.Mchkey);
            string str = sb.ToString().Replace(" ", "");
            string sign = EncryptionHelper.MD5Encryption(str).ToUpper();
            sbXml.Append("<sign>" + sign + "</sign></xml>");
            return sbXml.ToString().Replace(" ", "");

        }

        /// <summary>
        /// 根据形成订单的系统时间生成订单号
        /// </summary>
        /// <returns></returns>
        private static string GetOutTradeNumber()
        {
            string dt = DateTime.Now.ToString("yyyyMMddHHmmssffff");
            Random random = new Random();
            int randomNumber = random.Next(1000, 9999);
            return dt + randomNumber.ToString();
        }

        /// <summary>
        /// 将对象转换为字典
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        private static Dictionary<string, string> ToDictionary(Object o)
        {
            Dictionary<string, string> map = new Dictionary<string, string>();
            Type t = o.GetType();
            PropertyInfo[] pi = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo p in pi)
            {
                MethodInfo mi = p.GetGetMethod();

                if (mi != null && mi.IsPublic)
                {
                    map.Add(p.Name, (string)(mi.Invoke(o, new Object[] { })));
                }
            }
            return map;
        }

        private static string GetNonceStr()
        {
            string nonceStr = Guid.NewGuid().ToString("N");
            nonceStr = nonceStr.Substring(0, 32).ToUpper();
            return nonceStr;
        }
    }
}