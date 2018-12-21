using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HexiUtils;
using Schedule.Business;

namespace Schedule.Controllers
{
    public class ScheduleController : Controller
    {
        [HttpPost]
        public ActionResult OnSearchSchedule(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return Json(new StatusReport().SetFail("数据不完整"));
            }
            else
            {
                return Json(ScheduleDal.SearchSchedule(value));
            }
        }
    }
}