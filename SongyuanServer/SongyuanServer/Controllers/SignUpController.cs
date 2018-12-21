using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Senparc.Weixin.WxOpen.AdvancedAPIs.Sns;
using Senparc.Weixin.WxOpen.Containers;
using SongyuanUtils;
using SongyuanServer.Business;

namespace SongyuanServer.Controllers
{
    public class SignUpController : Controller
    {
        /// <summary>
        /// 接收小程序入园报名界面提交的数据，并将其保存到数据库中
        /// </summary>
        /// <param name="kindergartenName">幼儿园</param>
        /// <param name="name">姓名</param>
        /// <param name="gender">性别</param>
        /// <param name="bagPhone">书包电话</param>
        /// <param name="birth">出生日期</param>
        /// <param name="address">家庭住址</param>
        /// <param name="source">学生来源</param>
        /// <param name="guardianName">监护人</param>
        /// <param name="relation">与幼儿关系</param>
        /// <param name="guardianPhone">监护人电话</param>
        /// <param name="guardianCredentialType">监护人身份证件类型</param>
        /// <param name="guardianIdNumber">监护人证件号码</param>
        /// <param name="occupation">监护人职业</param>
        /// <param name="job">监护人工作单位及职位</param>
        /// <param name="guardianName2">监护人2</param>
        /// <param name="relation2">与幼儿关系2</param>
        /// <param name="guardianPhone2">监护人电话2</param>
        /// <param name="guardianCredentialType2">监护人身份证件类型2</param>
        /// <param name="guardianIDNumber2">监护人证件号码2</param>
        /// <param name="occupation2">监护人职业2</param>
        /// <param name="job2">监护人工作单位及职位2</param>
        /// <param name="guardianName3">监护人3</param>
        /// <param name="relation3">与幼儿关系3</param>
        /// <param name="guardianPhone3">监护人电话3</param>
        /// <param name="guardianCredentialType3">监护人身份证件类型3</param>
        /// <param name="guardianIDNumber3">监护人证件号码3</param>
        /// <param name="occupation3">监护人职业3</param>
        /// <param name="job3">监护人工作单位及职位3</param>
        /// <param name="guardianName4">监护人4</param>
        /// <param name="relation4">与幼儿关系4</param>
        /// <param name="guardianPhone4">监护人电话4</param>
        /// <param name="guardianCredentialType4">监护人身份证件类型4</param>
        /// <param name="guardianIDNumber4">监护人证件号码4</param>
        /// <param name="occupation4">监护人职业4</param>
        /// <param name="job4">监护人工作单位及职位4</param>
        /// <param name="healthRemarks">健康备注</param>
        /// <param name="foodDragRemarks">食品药品备注</param>
        /// <param name="healthCareNote">保健手册</param>
        /// <param name="examination">体检手册</param>
        /// <param name="vaccineNote">预防针本</param>
        /// <param name="kidCredentialType">幼儿身份证件类型</param>
        /// <param name="kidIDNumber">幼儿身份证件号码</param>
        /// <param name="kidNation">民族</param>
        /// <param name="kidNationality">国籍地区</param>
        /// <param name="gangaotai">港澳台侨外</param>
        /// <param name="area">户籍地区</param>
        /// <param name="areaDetail">户籍地区详情</param>
        /// <param name="residenceNature">户口性质</param>
        /// <param name="nonagricultureType">非农业户口类型</param>
        /// <param name="disabled">是否残疾</param>
        /// <param name="disabledType">残疾类型</param>
        /// <param name="leftChild">是否留守儿童</param>
        /// <param name="onlyChild">是否独生子女</param>
        /// <param name="migrantWorkerChild">是否进城务工人员子女</param>
        /// <param name="orphan">是否孤儿</param>
        /// <param name="healthCondition">健康状况</param>
        /// <param name="bloodType">血型</param>
        /// <param name="teacherName">老师姓名</param>
        /// <param name="patriarchName">家长姓名</param>
        /// <param name="websiteName">网站名称</param>
        /// <param name="examDate">体检时间</param>
        /// <param name="kyId">看园ID</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult OnSetSignUpData(string kindergartenName, string name, string gender, string bagPhone, string birth,
            string address, string source, string guardianName, string relation, string guardianPhone, string guardianCredentialType,
            string guardianIdNumber, string occupation, string job, string guardianName2, string relation2, string guardianPhone2,
            string guardianCredentialType2, string guardianIDNumber2, string occupation2, string job2, string guardianName3,
            string relation3, string guardianPhone3, string guardianCredentialType3, string guardianIDNumber3, string occupation3,
            string job3, string guardianName4, string relation4, string guardianPhone4, string guardianCredentialType4, 
            string guardianIDNumber4, string occupation4, string job4, string healthRemarks, string foodDragRemarks, string healthCareNote,
            string examination, string vaccineNote, string kidCredentialType, string kidIDNumber, string kidNation, string kidNationality,
            string gangaotai, string area, string areaDetail, string residenceNature, string nonagricultureType, string disabled,
            string disabledType, string leftChild, string onlyChild, string migrantWorkerChild, string orphan, string healthCondition,
            string bloodType, string teacherName, string patriarchName, string websiteName, string examDate, string kanyuanID, string sessionId,
            string shareNumber)
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
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(bagPhone))
            {
                sr.status = "Fail";
                sr.result = "姓名和书包电话不能为空";
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
            string openid = sessionBag.OpenId;

            sr = WXUserDal.SetUserInfo(openid, guardianName, bagPhone,kindergartenName,name,relation);

            //如果数据满足条件，调用SignUpDal.SetSignUpData方法，将数据存入数据库中
            sr = SignUpDal.SetSignUpData(kindergartenName, name, gender, bagPhone, birth, address, source, 
                guardianName, relation,guardianPhone, guardianCredentialType, guardianIdNumber, occupation, job, 
                guardianName2, relation2, guardianPhone2, guardianCredentialType2, guardianIDNumber2, occupation2, job2, 
                guardianName3, relation3, guardianPhone3, guardianCredentialType3, guardianIDNumber3, occupation3, job3, 
                guardianName4, relation4, guardianPhone4, guardianCredentialType4, guardianIDNumber4, occupation4, job4,
                healthRemarks, foodDragRemarks, healthCareNote, examination, vaccineNote, kidCredentialType, kidIDNumber, 
                kidNation, kidNationality, gangaotai, area, areaDetail, residenceNature, nonagricultureType, disabled, 
                disabledType, leftChild, onlyChild, migrantWorkerChild, orphan,healthCondition, bloodType, teacherName, 
                patriarchName, websiteName, examDate, kanyuanID,openid,shareNumber);
            //if (sr.status == "Success")
            //{
            //    string data = sr.data.ToString();
            //    StatusReport report = new StatusReport();
            //    report = KanyuanDataDal.GetPayInfo(name, bagPhone,kindergartenName);
            //    if (report.status == "Success")
            //    {
            //        string totalFee = report.data.ToString();
            //        report.data = new { totalFee = totalFee, signUpId = data.ToString() };
            //        return Json(report);
            //    }
            //}
            return Json(sr) ;
        }
    }
}