using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HexiUtils;
using WanboServer.Business;

namespace WanboServer.Controllers
{
    public class PatrolController : Controller
    {
        // GET: Patrol
        public ActionResult GetOfficeInfo()
        {
            StatusReport sr = new StatusReport();
            sr = OfficeDal.GetAllOffice();
            return Json(sr);
        }

        public ActionResult GetWatchInfo(string officeId)
        {
            StatusReport sr = new StatusReport();
            if (string.IsNullOrEmpty(officeId))
            {
                sr.status = "Fail";
                sr.result = "数据不完整";
                return Json(sr);
            }
            sr = WatchDal.getWatchInfo(officeId);
            return Json(sr);
        }

        public ActionResult SetWatchResult(string officeId,string frequencyId,string pointId,string workDate,string watchTimes,string usedTime,string arriveLa,string arriveLo,string arriveTime)
        {
            StatusReport sr = new StatusReport();
            if (string.IsNullOrEmpty(officeId) || string.IsNullOrEmpty(frequencyId) || string.IsNullOrEmpty(pointId) || string.IsNullOrEmpty(workDate) || 
                string.IsNullOrEmpty(watchTimes) || string.IsNullOrEmpty(usedTime) || string.IsNullOrEmpty(arriveLa) || string.IsNullOrEmpty(arriveLo) || 
                string.IsNullOrEmpty(arriveTime))
            {
                sr.status = "Fail";
                sr.result = "数据不完整";
                return Json(sr);
            }
            sr = WatchResultDal.SetWatchResult(officeId,frequencyId,pointId,workDate,watchTimes,usedTime,arriveLa,arriveLo,arriveTime);
            return Json(sr);
        }

        public ActionResult SetWatchImages()
        {
            StatusReport sr = new StatusReport();
            if (Request.Files.Count == 0)
            {
                sr.status = "Fail";
                sr.result = "没有图片";
                return Json(sr);
            }
            string imagePath = "D:\\WYYTNET\\wb iis\\wb iis\\wximages\\";
            string sqlImagePath = "\\wximages\\";
            imagePath += Request.Files.AllKeys[0];
            sqlImagePath += Request.Files.AllKeys[0];
            HttpPostedFileBase uploadImage = Request.Files[0];
            uploadImage.SaveAs(imagePath);
            string id = Request.Form["id"];
            string func = Request.Form["func"];
            sr = WatchResultDal.SetWatchImage(id, func, sqlImagePath);
            return Json(sr);
        }
    }
}