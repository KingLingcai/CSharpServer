using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Senparc.Weixin.WxOpen.AdvancedAPIs.Sns;
using Senparc.Weixin.WxOpen.Containers;
using SongyuanUtils;
using SongyuanCloudServer.Business;

namespace SongyuanCloudServer.Controllers
{
    public class KanyuanController : Controller
    {
        /// <summary>
        /// 接收小程入园意向界面提交的数据，并将其保存到数据库中
        /// </summary>
        /// /// <param name="kindergartenName">幼儿园</param>
        /// <param name="name">姓名</param>
        /// <param name="gender">性别</param>
        /// <param name="birth">出生日期</param>
        /// <param name="relateName">家长姓名</param>
        /// <param name="relation">家长与幼儿关系</param>
        /// <param name="phoneNumber">联系电话</param>
        /// <param name="address">家庭住址</param>
        /// <param name="isYoueryuan">是否上过幼儿园</param>
        /// <param name="desire">入托意愿</param>
        /// <param name="joinLottery">参加政府入园摇号</param>
        /// <param name="ruyuanDate">计划入学时间</param>
        /// <param name="isAppointment">是否预约看园</param>
        /// <param name="appointmentDate">预约看园时间</param>
        /// <param name="relateGender">家长性别</param>
        /// <param name="haveReceiver">是否有接待人</param>
        /// <param name="receiverName">接待人姓名</param>
        /// <param name="needSchoolBus">是否需要校车</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult OnSetKanyuanData(string kindergartenName, string name, string gender, string birth,
            string relateName, string relation, string phoneNumber, string address, string isYoueryuan,
            string desire, string joinLottery, string ruyuanDate, string isAppointment, string appointmentDate,
            string relateGender, string haveReceiver, string receiverName, string needSchoolBus, string sessionId)
        {
            StatusReport sr = new StatusReport();

            //如果未指定幼儿园，返回错误信息
            if (string.IsNullOrEmpty(kindergartenName))
            {
                sr.status = "Fail";
                sr.result = "未指定幼儿园";
                return Json(sr);
            }

            //如果姓名或联系方式为空，返回错误信息
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(phoneNumber))
            {
                sr.status = "Fail";
                sr.result = "姓名和联系电话不能为空";
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

            sr = WXUserDal.SetUserInfo(openId, relateName, phoneNumber, kindergartenName, name, relation);

            //如果提交的数据满足条件，调用KanyuanDataDal.SetKanyuanData方法，将数据存入数据库中
            sr = KanyuanDataDal.SetKanyuanData(kindergartenName, name, gender, birth, relateName, relation, phoneNumber,
                address, isYoueryuan, desire, joinLottery, ruyuanDate, isAppointment, appointmentDate, relateGender,
                haveReceiver, receiverName, needSchoolBus, openId);


            return Json(sr);
        }

        /// <summary>
        /// 通过姓名和联系电话查找看园信息
        /// </summary>
        /// <param name="kindergartenName">幼儿园名称</param>
        /// <param name="name">姓名</param>
        /// <param name="phoneNumber">联系电话</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult OnGetKanyuanData(string kindergartenName, string sessionId)
        {
            StatusReport sr = new StatusReport();
            //如果未指定幼儿园，返回错误信息
            if (string.IsNullOrEmpty(kindergartenName))
            {
                sr.status = "Fail";
                sr.result = "未指定幼儿园";
                return Json(sr);
            }

            //如果姓名或联系方式为空，返回错误信息
            //if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(phoneNumber))
            //{
            //    sr.status = "Fail";
            //    sr.result = "姓名和联系电话不能为空";
            //    return Json(sr);
            //}

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

            //如果提交的数据满足条件，调用KanyuanDataDal.GetKanyuanData方法，在数据库中获取满足条件的数据
            sr = KanyuanDataDal.GetKanyuanData(kindergartenName, openId);

            return Json(sr);
        }
    }
}