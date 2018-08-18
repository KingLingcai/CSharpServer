using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SongyuanServer.Business;
using HexiUtils;

namespace SongyuanServer.Controllers
{
    public class WXShareController : Controller
    {
        //[HttpPost]
        //public ActionResult OnSetShareInfo(string sessionId, string userid, string shareNumber)
        //{
        //    StatusReport sr = new StatusReport();
        //    if (string.IsNullOrEmpty(userid) || string.IsNullOrEmpty(shareNumber))
        //    {
        //        sr.status = "Fail";
        //        sr.result = "信息不完整";
        //        return Json(sr);
        //    }
        //    sr = WXShareDal.SetShareInfo(userid, shareNumber);
        //    return Json(sr);
        //}
        [HttpPost]
        public ActionResult OnGetShareInfoList(string kindergartenName, string userName)
        {
            StatusReport sr = new StatusReport();
            if (string.IsNullOrEmpty(kindergartenName))
            {
                sr.status = "Fail";
                sr.result = "信息不完整";
                return Json(sr);
            }
            sr = WXShareDal.GetShareInfoList(kindergartenName,userName);
            return Json(sr);
        }


        public ActionResult OnGetShareDetail(string kindergartenName, string id)
        {
            StatusReport sr = new StatusReport();
            if (string.IsNullOrEmpty(kindergartenName) || string.IsNullOrEmpty(id))
            {
                sr.status = "Fail";
                sr.result = "信息不完整";
                return Json(sr);
            }
            sr = WXShareDal.GetShareDetail(kindergartenName,id);
            return Json(sr);
        }


        public ActionResult OnGetSignupDetail(string kindergartenName, string shareId, string receiverId)
        {
            StatusReport sr = new StatusReport();
            if (string.IsNullOrEmpty(kindergartenName) || string.IsNullOrEmpty(shareId) || string.IsNullOrEmpty(receiverId))
            {
                sr.status = "Fail";
                sr.result = "信息不完整";
                return Json(sr);
            }
            sr = WXShareDal.GetSignupDetail(kindergartenName, shareId,receiverId);
            return Json(sr);
        }
    }
}