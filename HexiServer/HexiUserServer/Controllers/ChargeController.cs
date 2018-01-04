using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Senparc.Weixin.WxOpen.AdvancedAPIs.Sns;
using Senparc.Weixin.WxOpen.Containers;
using HexiUserServer.Business;
using HexiUtils;
using System.Collections.Specialized;
using System.IO;

namespace HexiUserServer.Controllers
{
    public class ChargeController : Controller
    {
        [HttpPost]
        public ActionResult OnGetCharges(string ztCode, string roomNumber, string userName )
        {
            return Json(ChargeDal.GetCharges(ztCode, roomNumber, userName));
        }

        [HttpPost]
        public ActionResult OnSetCharges(string datetime, string proprietorName, string[] chargeIds)
        {
            //return Json(new { datetime = datetime, proprietorName = proprietorName, chargeIds = chargeIds });
            return Json(ChargeDal.SetCharges(datetime, proprietorName, chargeIds));
        }

        [HttpPost]
        public ActionResult OnUnifiedOrder(string sessionId, double totalCharge, string dataBag)
        {
            StatusReport sr = new StatusReport();
            SessionBag sessionBag = null;
            sessionBag = SessionContainer.GetSession(sessionId);
            if (sessionBag == null)
            {
                sr.status = "Fail";
                sr.result = "session已失效";
                return Json(sr);
            }
            string openId = sessionBag.OpenId;
            sr = WXPayDal.UnifiedOrder(openId, totalCharge, dataBag);
            return Json(sr);
        }


        public ActionResult OnTest()
        {
            
            StreamReader reader = new StreamReader(Request.InputStream);
            string value = reader.ReadToEnd();
            using (StreamWriter sw = new StreamWriter("D:\\1_importTemp\\TestFile.txt"))
            {
                NameValueCollection headers = Request.Headers;
                //string headerstring = "";
                string[] headerKeys = headers.AllKeys;
                for (int i = 0; i < headerKeys.Length; i++)
                {
                    sw.WriteLine(headerKeys[i] + ":" + headers.Get(headerKeys[i]));
                }
                sw.WriteLine("-------------------");
                sw.WriteLine("-------------------");
                sw.WriteLine("-------------------");
                sw.WriteLine("-------------------");
                sw.WriteLine("-------------------");
                sw.WriteLine("-------------------");
                sw.WriteLine("-------------------");
                sw.WriteLine(value);
                NameValueCollection nvc = Request.QueryString;
                //string value = "";
                string[] nvcKeys = nvc.AllKeys;
                for (int i = 0; i < nvcKeys.Length; i++)
                {
                    sw.WriteLine(nvcKeys[i] + ":" + nvc.Get(nvcKeys[i]));
                }

                //sw.WriteLine(headerstring);
                //sw.WriteLine(value);
                // Add some text to the file.
                //sw.Write("This is the ");
                //sw.WriteLine("header for the file.");
                sw.WriteLine("-------------------");
                // Arbitrary objects can also be written to the file.
                sw.Write("The date is: ");
                sw.WriteLine(DateTime.Now);
            }
            return null;
        }

        //public ActionResult OnGetPrepayId(string xmlResult)
        //{
        //    return Json(WXPayDal.GetOrderInfo(xmlResult));
        //}
    }
}