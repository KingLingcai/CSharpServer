using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SongyuanCloudServer.Models
{
    /// <summary>
    /// 将小程入园意向界面提交的数据保存到数据库中
    /// </summary>
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
    public class KanyuanData
    {

        public int? id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string gender { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string birth { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string relateName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string relation { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string phoneNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string isYoueryuan { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string desire { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string joinLottery { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ruyuanDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string isAppointment { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string appointmentDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string relateGender { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string haveReceiver { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string receiverName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string needSchoolBus { get; set; }

    }
}