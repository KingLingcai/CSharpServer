using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Senparc.Weixin.WxOpen.AdvancedAPIs.Sns;
using Senparc.Weixin.WxOpen.Containers;
using HexiUserServer.Business;
using HexiUtils;

namespace HexiUserServer.Controllers
{
    public class ChargeController : Controller
    {
        [HttpPost]
        public ActionResult OnGetCharges(string ztCode, string roomId, string proprietorId )
        {
            return Json(ChargeDal.GetCharges(ztCode, roomId, proprietorId));
        }

        [HttpPost]
        public ActionResult OnSetCharges(string datetime, string totalCharge, string roomName, string proprietorName, string[] chargeIds)
        {

            return Json(ChargeDal.SetCharges(datetime, totalCharge, roomName, proprietorName, chargeIds));
        }

        [HttpPost]
        public ActionResult OnUnifiedOrder(string sessionId, double totalCharge, int[] ids)
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
            sr = WXPayDal.UnifiedOrder(openId, totalCharge, ids);
            return Json(sr);
        }


        //public ActionResult OnTest()
        //{

        //}

        //public ActionResult OnGetPrepayId(string xmlResult)
        //{
        //    return Json(WXPayDal.GetOrderInfo(xmlResult));
        //}
    }
}