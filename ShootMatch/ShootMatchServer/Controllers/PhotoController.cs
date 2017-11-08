using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShootMatchServer.Business;
using ShootMatchServer.Models;
using System.Collections.Specialized;

namespace ShootMatchServer.Controllers
{
    public class PhotoController : Controller
    {
        // GET: Photo
        [HttpPost]
        public ActionResult SetPhotoInfo()
        {
            NameValueCollection photoNvc = Request.Form;
            Photo photo = new Photo(photoNvc);
            PhotoDal.InsertPhotoInfo(photo);
            return View();
        }

        [HttpPost]
        public ActionResult SavePhoto()
        {

            return View();
        }
    }
}