using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SongyuanUtils;
using SongyuanServer.Business;

namespace SongyuanServer.Controllers
{
    public class EmployeeController : Controller
    {
        /// <summary>
        /// 接收小程序职工管理界面发送的数据，并将其保存到数据库中
        /// </summary>
        /// <param name="kindergartenName">幼儿园名称</param>
        /// <param name="name">姓名</param>
        /// <param name="birth">出生日期</param>
        /// <param name="IDNumber">身份证号码</param>
        /// <param name="hiredate">入职时间</param>
        /// <param name="address">住址</param>
        /// <param name="socialSecurityNumber">社保号</param>
        /// <param name="HAFA">住房公积金帐号</param>
        /// <param name="salaryWay">工资发放方式</param>
        /// <param name="payCheckAccount">工资卡帐号</param>
        /// <param name="bank">所属银行</param>
        /// <param name="noSocialSecurityReason">无社保原因</param>
        /// <param name="payAmount">工资金额</param>
        /// <param name="socialSecurityAmount">社保金额</param>
        /// <param name="phoneNumber">手机</param>
        /// <param name="phoneAddress">手机地址</param>
        /// <param name="hardwareAddress">电脑硬件地址</param>
        /// <param name="contact">员工联系人</param>
        /// <param name="contactPhoneNumber">联系人手机</param>
        /// <param name="record">学历</param>
        /// <param name="title">专业技术职称</param>
        /// <param name="department">部门</param>
        /// <param name="position">职务</param>
        /// <param name="qualification">资格</param>
        /// <param name="isHealthCertificate">是否有健康证</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult OnSetEmployeeData(string kindergartenName, string name, string birth, string IDNumber, string hiredate,
            string address, string socialSecurityNumber, string HAFA, string salaryWay, string payCheckAccount, string bank,
            string noSocialSecurityReason, string payAmount, string socialSecurityAmount, string phoneNumber, string phoneAddress,
            string hardwareAddress, string contact, string contactPhoneNumber, string record, string title, string department, 
            string position, string qualification, string isHealthCertificate, string college, string profession,
            string speciality, string college1, string college2, string college3, string eduStartTime1, string eduStartTime2,
            string eduStartTime3, string eduEndTime1, string eduEndTime2, string eduEndTime3, string work1, string work2,
            string work3, string workstartTime1, string workstartTime2, string workstartTime3, string workEndTime1,
            string workEndTime2, string workEndTime3, string gender)
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

            //如果提交的数据满足条件，调用EmployeeDal.SetEmployeeData方法将职工信息保存到数据库中
            sr = EmployeeDal.SetEmployeeData(kindergartenName, name, birth, IDNumber, hiredate, address, socialSecurityNumber, HAFA, salaryWay,
                payCheckAccount, bank, noSocialSecurityReason, payAmount, socialSecurityAmount, phoneNumber, phoneAddress, hardwareAddress,
                contact, contactPhoneNumber, record, title, department, position, qualification, isHealthCertificate,college,profession,
                speciality,college1,college2,college3,eduStartTime1,eduStartTime2,eduStartTime3,eduEndTime1,eduEndTime2,eduEndTime3,
                work1,work2,work3,workstartTime1,workstartTime2,workstartTime3,workEndTime1,workEndTime2,workEndTime3,gender);

            return Json(sr);
        }
    }
}