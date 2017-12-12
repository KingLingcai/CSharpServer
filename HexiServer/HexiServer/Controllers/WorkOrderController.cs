using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Senparc.Weixin.WxOpen.AdvancedAPIs.Sns;
using Senparc.Weixin.WxOpen.Containers;
using HexiServer.Business;
using HexiServer.Models;

namespace HexiServer.Controllers
{
    public class WorkOrderController : Controller
    {
      
        /// <summary>
        /// 获取某接单人的工单列表
        /// </summary>
        /// <param name="userCode"></param>
        /// <param name="ztCode"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult OnGetRepairList(string userCode, string ztCode, string orderType,int page)
        {
            StatusReport sr = new StatusReport();
            //string openid = GetOpenId(sessionId);
            Repair[] repairList = null;
            //if (openid != null)
            //{
                repairList = RepairDal.GetRepairOrder(userCode, ztCode,orderType,page);
            //}
            
            return Json(repairList);
        }

        /// <summary>
        /// 将工单的处理详情写入数据库
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="id"></param>
        /// <param name="arriveTime"></param>
        /// <param name="completeTime"></param>
        /// <param name="completeStatus"></param>
        /// <param name="laborExpense"></param>
        /// <param name="materialExpense"></param>
        /// <returns></returns>
        public ActionResult OnSetRepairOrder(string sessionId, string id, string arriveTime, string completeTime, string completeStatus, string laborExpense, string materialExpense, string status)
        {
            StatusReport sr = new StatusReport();
            sr = RepairDal.SetRepairOrder(id, arriveTime, completeTime, completeStatus, laborExpense, materialExpense,status);
            return Json(sr);
        }

        public ActionResult OnSetOrderIsRead(string id)
        {
            StatusReport sr = new StatusReport();
            sr = RepairDal.SetOrderIsRead(id);
            return Json(sr);
        }

        private string GetOpenId (string sessionId)
        {
            SessionBag sessionbag = null;
            sessionbag = SessionContainer.GetSession(sessionId);
            if (sessionbag != null)
            {
                return sessionbag.OpenId;
            }
            else
            {
                return null;
            }
        }
    }
}