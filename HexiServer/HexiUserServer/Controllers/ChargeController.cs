using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HexiUserServer.Business;

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
    }
}