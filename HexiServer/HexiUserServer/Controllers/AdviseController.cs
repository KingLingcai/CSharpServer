using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HexiUtils;
using HexiUserServer.Business;

namespace HexiUserServer.Controllers
{
    public class AdviseController : Controller
    {
        [HttpPost]
        public ActionResult OnSetAdvise(string ztName, string name, string phone, string roomNumber, string type, string content, string submitDate)
        {
            StatusReport sr = new StatusReport();
            if (string.IsNullOrEmpty(ztName) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(roomNumber) || string.IsNullOrEmpty(type) || string.IsNullOrEmpty(content) || string.IsNullOrEmpty(submitDate))
            {
                sr.status = "Fail";
                sr.result = "信息不完整";
                return Json(sr);
            }
            sr = AdviseDal.SetAdvise(ztName, name, phone, roomNumber, type, content, submitDate);
            return Json(sr);
        }


        public ActionResult OnGetAdvise(string ztName, string phone)
        {
            StatusReport sr = new StatusReport();
            if (string.IsNullOrEmpty(ztName) || string.IsNullOrEmpty(phone))
            {
                sr.status = "Fail";
                sr.result = "信息不完整";
                return Json(sr);
            }
            sr = AdviseDal.GetAdvise(ztName, phone);
            return Json(sr);
        }
    }
}