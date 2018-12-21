using System.Web.Mvc;
using SongyuanUtils;
using Senparc.Weixin.WxOpen.AdvancedAPIs.Sns;
using Senparc.Weixin.WxOpen.Containers;
using SongyuanCloudServer.Models;
using SongyuanCloudServer.Business;
using System.IO;

namespace SongyuanCloudServer.Controllers
{
    public class WXUserController : Controller
    {
        /// <summary>
        /// 微信登陆，如成功则返回3th session-key。
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult OnLogin(string code, string shareNumber, string userId, string userName, string shareTime, string kindergartenName)
        {
            StatusReport sr = new StatusReport();
            var jsonResult = SnsApi.JsCode2Json(Comman.Appid, Comman.AppSecret, code);
            if (jsonResult.errcode == Senparc.Weixin.ReturnCode.请求成功)
            {
                //Session["WxOpenUser"] = jsonResult;
                var sessionBag = SessionContainer.UpdateSession(null, jsonResult.openid, jsonResult.session_key);
                Session[sessionBag.Key] = jsonResult;
                Session.Timeout = 60;
                sr = WXUserDal.SetNewUser(jsonResult.openid, kindergartenName);
                if (sr.status == "Success")
                {
                    User user = (User)sr.data;
                    string receiverId = user.id;
                    if (!string.IsNullOrEmpty(shareNumber) && !string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(shareTime))
                    {
                        if (userId != receiverId)
                        {
                            sr = WXShareDal.SetShareInfo(receiverId, shareNumber, userId, userName, shareTime, kindergartenName);
                            using (StreamWriter sw = new StreamWriter("D:\\1_importTemp\\TestFile1.txt"))
                            {
                                sw.WriteLine(sr.result.ToString());
                            }
                        }
                    }
                    sr.data = new { success = true, msg = "OK", sessionId = sessionBag.Key, user = user };
                }
                else
                {

                    sr.data = new { success = true, msg = "OK", sessionId = sessionBag.Key };
                }
                return Json(sr);
            }
            else
            {
                sr.status = "Fail";
                sr.data = new { success = false, mag = jsonResult.errmsg, result = jsonResult };
                return Json(sr);
                //return Json(new { success = false, mag = jsonResult.errmsg, result = jsonResult });
            }
        }


        [HttpPost]
        public ActionResult OnSetWXInfo(string sessionId, string nickName, int gender, string kindergartenName)
        {
            StatusReport sr = new StatusReport();

            if (string.IsNullOrEmpty(sessionId))
            {
                sr.status = "Fail";
                sr.result = "sessionId不存在";
                sr.parameters = sessionId;
                return Json(sr);
            }
            SessionBag sessionBag = null;
            sessionBag = SessionContainer.GetSession(sessionId);
            if (sessionBag == null)
            {
                sr.status = "Fail";
                sr.result = "session已失效";
                return Json(sr);
            }
            string openId = sessionBag.OpenId;
            sr = WXUserDal.SetWXInfo(openId, nickName, gender, kindergartenName);
            return Json(sr);
        }


        [HttpPost]
        public ActionResult OnGetMyInfo(string kindergartenName, string sessionId)
        {
            StatusReport sr = new StatusReport();
            if (string.IsNullOrEmpty(kindergartenName))
            {
                sr.status = "Fail";
                sr.result = "未指定幼儿园";
                return Json(sr);
            }
            if (string.IsNullOrEmpty(sessionId))
            {
                sr.status = "Fail";
                sr.result = "sessionId不存在";
                sr.parameters = sessionId;
                return Json(sr);
            }
            SessionBag sessionBag = null;
            sessionBag = SessionContainer.GetSession(sessionId);
            if (sessionBag == null)
            {
                sr.status = "Fail";
                sr.result = "session已失效";
                return Json(sr);
            }
            string openId = sessionBag.OpenId;
            sr = WXUserDal.GetMyInfo(kindergartenName, openId);
            return Json(sr);
        }

        //[HttpGet]
        /// <summary>
        /// 获取员工信息，使用openId获取员工信息
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        //[HttpPost]
        //public ActionResult OnGetUserInfo(string sessionId)
        //{
        //    StatusReport sr = new StatusReport();

        //    if (string.IsNullOrEmpty(sessionId))//如果sessionId为空，则返回错误信息
        //    {
        //        sr.status = "Fail";
        //        sr.result = "sessionId不存在";
        //        sr.parameters = sessionId;
        //        return Json(sr);
        //    }
        //    SessionBag sessionBag = null;
        //    sessionBag = SessionContainer.GetSession(sessionId);
        //    if (sessionBag == null)
        //    {
        //        sr.status = "Fail";
        //        sr.result = "session已失效";
        //        return Json(sr);
        //    }
        //    string openId = sessionBag.OpenId;
        //    sr = EmployeeDal.CheckOpenIdExist(openId);
        //    //if (sr.data != null)
        //    //{
        //    //    var o = JsonConvert.DeserializeObject(sr.data);
        //    //    return Json(new { status = "Success", result = "成功", data = o });
        //    //}
        //    //else
        //    //{
        //    return Json(sr);
        //    //}
        //}

        /// <summary>
        /// 将用户Id与openId进行绑定
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        //[HttpPost]
        //public ActionResult OnBindUser(string userName, string password, string sessionId)
        //{
        //    StatusReport sr = new StatusReport();
        //    SessionBag sessionBag = null;
        //    sessionBag = SessionContainer.GetSession(sessionId);
        //    if (sessionBag == null)
        //    {
        //        sr.status = "Fail";
        //        sr.result = "session已失效";
        //        return Json(sr);
        //    }
        //    string openId = sessionBag.OpenId;
        //    //string openId = "oTTDy0KN71B2XLMXobrapvhqlHcY";
        //    int id = EmployeeDal.CheckEmployeeExist(userName, password);
        //    string temp = id > 0 ? "存在" : "不存在";
        //    if (id > 0)
        //    {
        //        sr = EmployeeDal.BindEmployee(id, openId);
        //        return Json(sr);
        //    }
        //    else
        //    {
        //        var data = new
        //        {
        //            msg = "hello world",
        //            username = userName,
        //            password = password,
        //            isExist = temp
        //        };
        //        return Json(data);
        //    }
        //}

        //[HttpPost]
        //public ActionResult OnCheckPassword(string userId, string password)
        //{
        //    StatusReport sr = new StatusReport();
        //    if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(password))
        //    {
        //        sr.status = "Fail";
        //        sr.result = "信息不完整";
        //        return Json(sr);
        //    }
        //    sr = EmployeeDal.CheckPassword(userId, password);
        //    return Json(sr);
        //}
    }
}