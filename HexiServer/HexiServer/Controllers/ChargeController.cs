using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HexiServer.Business;
using HexiUtils;

namespace HexiServer.Controllers
{
    public class ChargeController : Controller
    {
        // GET: Charge
        [HttpPost]
        public ActionResult OnGetChargedList(string homeNumber, string name, string ztcode, string startMonth, string endMonth, string isCharge)
        {
            StatusReport sr = new StatusReport();
            if (string.IsNullOrEmpty(ztcode) || string.IsNullOrEmpty(startMonth) || string.IsNullOrEmpty(endMonth) || string.IsNullOrEmpty(isCharge))
            {
                return Json(new {status = "Fail" , result = "信息不完整" });
            }
            if (isCharge == "已收")
            {
                return Json(ChargeDal.GetChargedList(homeNumber, name, ztcode, startMonth, endMonth));
            }
            else
            {
                return Json(ChargeDal.GetChargeList(homeNumber, name, ztcode, startMonth, endMonth));
            }
        }

        [HttpPost]
        public ActionResult OnGetChargedDetail(string RoomNumber, string Name, string ZTCode, string startMonth, string endMonth)
        {
            if (string.IsNullOrEmpty(RoomNumber) || string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(ZTCode) || string.IsNullOrEmpty(startMonth) || string.IsNullOrEmpty(endMonth))
            {
                return Json(new { status = "Fail", result = "信息不完整" });
            }
            return Json(ChargeDal.GetChargedDetail(RoomNumber, Name, ZTCode, startMonth, endMonth));
        }

        [HttpPost]
        public ActionResult OnGetChargeDetail(string ZTCode, string RoomNumber, string Name, string startMonth, string endMonth)
        {
            if (string.IsNullOrEmpty(RoomNumber) || string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(ZTCode) || string.IsNullOrEmpty(startMonth) || string.IsNullOrEmpty(endMonth))
            {
                return Json(new { status = "Fail", result = "信息不完整" });
            }
            return Json(ChargeDal.GetCharges(ZTCode, RoomNumber, Name, startMonth, endMonth));
        }
    }
}