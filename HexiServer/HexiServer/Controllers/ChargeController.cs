using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HexiServer.Business;
using HexiServer.Models;

namespace HexiServer.Controllers
{
    public class ChargeController : Controller
    {
        // GET: Charge
        [HttpPost]
        public ActionResult OnGetChargedList(string homeNumber, string name, string ztcode, string startMonth, string endMonth)
        {
            if (string.IsNullOrEmpty(ztcode) || string.IsNullOrEmpty(startMonth) || string.IsNullOrEmpty(endMonth))
            {
                return Json(new {status = "Fail" , result = "信息不完整" });
            }
            //Status s = new Status();
            //Charged[] c = ChargeDal.GetChargedList(homeNumber, name, ztcode, startMonth, endMonth);
            //s.data = c[0];
            ////s.da/*ta = "hello world";*/
            //s.result = "Success";
            //return Json(s);
            return Json(ChargeDal.GetChargedList(homeNumber, name, ztcode, startMonth, endMonth));
        }

        //[HttpPost]
        public ActionResult OnGetChargedDetail(string RoomNumber, string Name, string ZTCode, string startMonth, string endMonth)
        {
            if (string.IsNullOrEmpty(RoomNumber) || string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(ZTCode) || string.IsNullOrEmpty(startMonth) || string.IsNullOrEmpty(endMonth))
            {
                return Json(new { status = "Fail", result = "信息不完整" });
            }
            return Json(ChargeDal.GetChargedDetail(RoomNumber, Name, ZTCode, startMonth, endMonth),JsonRequestBehavior.AllowGet);
        }
    }
}