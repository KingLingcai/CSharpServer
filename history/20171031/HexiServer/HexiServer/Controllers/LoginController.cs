using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HexiServer.Models;
using HexiServer.Business;

namespace HexiServer.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        //[HttpPost]
        [HttpGet]
        public ActionResult OnLogin(string code)
        {
           WXUser user = WXOperator.GetUser(code);
            return Json(user, JsonRequestBehavior.AllowGet);
        }
    }
}