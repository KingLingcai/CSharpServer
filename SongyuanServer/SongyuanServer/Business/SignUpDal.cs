using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HexiUtils;

namespace SongyuanServer.Business
{
    public class SignUpDal
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
        /// <returns></returns>
        public static StatusReport SetSignUpData(string kindergartenName, string name, string gender, string bagPhone, string birth,
            string address, string source, string guardianName, string relation, string guardianPhone, string guardianCredentialType,
            string guardianIdNumber, string occupation, string job, string guardianName2, string relation2, string guardianPhone2,
            string guardianCredentialType2, string guardianIDNumber2, string occupation2, string job2, string guardianName3,
            string relation3, string guardianPhone3, string guardianCredentialType3, string guardianIDNumber3, string occupation3,
            string job3, string guardianName4, string relation4, string guardianPhone4, string guardianCredentialType4,
            string guardianIDNumber4, string occupation4, string job4, string healthRemarks, string foodDragRemarks, string healthCareNote,
            string examination, string vaccineNote, string kidCredentialType, string kidIDNumber, string kidNation, string kidNationality,
            string gangaotai, string area, string areaDetail, string residenceNature, string nonagricultureType, string disabled,
            string disabledType, string leftChild, string onlyChild, string migrantWorkerChild, string orphan, string healthCondition,
            string bloodType, string teacherName, string patriarchName, string websiteName, string examDate)
        {
            StatusReport sr = new StatusReport();

            return sr;
        }
    }
}