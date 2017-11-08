using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HexiServer.Business;

namespace HexiServer.Controllers
{
    public class ChargeController : Controller
    {
        // GET: Charge
        [HttpPost]
        public ActionResult OnGetChargedList(string homeNumber, string name, string ztcode, string startMonth, string endMonth)
        {
            return Json(ChargeDal.GetChargedList(homeNumber,name,ztcode,startMonth,endMonth));
        }

        //[HttpPost]
        public ActionResult OnGetChargedDetail(string RoomNumber, string Name, string ZTCode, string startMonth, string endMonth)
        {
            return Json(ChargeDal.GetChargedDetail(RoomNumber, Name, ZTCode, startMonth, endMonth),JsonRequestBehavior.AllowGet);
        }
    }
}