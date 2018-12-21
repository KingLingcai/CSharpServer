using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SongyuanUtils;
using Senparc.Weixin.WxOpen.AdvancedAPIs.Sns;
using Senparc.Weixin.WxOpen.Containers;
using SongyuanServer.Business;
using System.IO;

namespace SongyuanServer.Controllers
{
    public class WXPayController : Controller
    {
        [HttpPost]
        public ActionResult OnUnifiedOrder(string kindergartenName, string fromPage, string sessionId, double totalCharge, string dataBag)
        {
            StatusReport sr = new StatusReport();
            SessionBag sessionBag = null;
            sessionBag = SessionContainer.GetSession(sessionId);
            if (sessionBag == null)
            {
                sr.status = "Fail";
                sr.result = "session已失效";
                return Json(sr);
            }
            string openId = sessionBag.OpenId;
            sr = WXPayDal.UnifiedOrder(kindergartenName, fromPage, openId, totalCharge, dataBag);
            return Json(sr);
        }

        [HttpPost]
        public ActionResult OnSetCharge(string sessionId, string tradeNumber,string totalCharge, string datetime, string fromPage, string kindergartenName, string id)
        {
            StatusReport sr = new StatusReport();
            if (string.IsNullOrEmpty(kindergartenName))
            {
                sr.status = "Fail";
                sr.result = "未指定幼儿园";
                return Json(sr);
            }
            //if (string.IsNullOrEmpty(sessionId))
            //{
            //    sr.status = "Fail";
            //    sr.result = "sessionId不存在";
            //    sr.parameters = sessionId;
            //    return Json(sr);
            //}
            //SessionBag sessionBag = null;
            //sessionBag = SessionContainer.GetSession(sessionId);
            //if (sessionBag == null)
            //{
            //    sr.status = "Fail";
            //    sr.result = "session已失效";
            //    return Json(sr);
            //}
            //string openId = sessionBag.OpenId;
            if (string.IsNullOrEmpty(tradeNumber) || string.IsNullOrEmpty(totalCharge) || string.IsNullOrEmpty(datetime))
            {
                sr.status = "Fail";
                sr.result = "信息不完整";
                return Json(sr);
            }
            sr = WXPayDal.SetCharge(id, tradeNumber, totalCharge, datetime,fromPage,kindergartenName);
            return Json(sr);
        }

        [HttpPost]
        public ActionResult OnGetChargeInfo(string kindergartenName, string feeName)
        {
            StatusReport sr = new StatusReport();
            if ( string.IsNullOrEmpty(kindergartenName) || string.IsNullOrEmpty(feeName))
            {
                sr.status = "Fail";
                sr.result = "信息不完整";
                return Json(sr);
            }
            sr = WXPayDal.GetChargeInfo(kindergartenName, feeName);
            return Json(sr);
        }

        [HttpPost]
        public ActionResult OnGetSignupChargeList(string kindergartenName, string sessionId)
        {
            StatusReport sr = new StatusReport();
            if (string.IsNullOrEmpty(kindergartenName))
            {
                sr.status = "Fail";
                sr.result = "未指定幼儿园";
                return Json(sr);
            }
            if (string.IsNullOrEmpty(sessionId))
            {
                sr.status = "Fail";
                sr.result = "sessionId不存在";
                sr.parameters = sessionId;
                return Json(sr);
            }
            SessionBag sessionBag = null;
            sessionBag = SessionContainer.GetSession(sessionId);
            if (sessionBag == null)
            {
                sr.status = "Fail";
                sr.result = "session已失效";
                return Json(sr);
            }
            string openId = sessionBag.OpenId;
            sr = WXPayDal.GetSignupChargeList(kindergartenName, openId);
            return Json(sr);
        }


        [HttpPost]
        public bool OnSetWXPayInfo()
        {
            StatusReport sr = new StatusReport();
            StreamReader reader = new StreamReader(Request.InputStream);
            string value = reader.ReadToEnd();
            using (StreamWriter sw = new StreamWriter("C:\\1_importTemp\\TestFile.txt"))
            {
                sw.WriteLine(value);
            }
            sr = WXPayDal.SetWXPayInfo(value);
            
            return true;
        }
    }
}