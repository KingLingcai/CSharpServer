using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HexiServer.Business;
using HexiServer.Models;

namespace HexiServer.Controllers
{
    public class ProprietorController : Controller
    {
        // GET: Proprietor
        public ActionResult OnGetProprietorList(string ztCode, string homeNumber, string name, string licensePlateNumber)
        {
            Proprietor[] proprietorList = ProprietorDal.GetProprietorList(ztCode, homeNumber, name, licensePlateNumber);

            return Json(proprietorList);
        }
    }
}