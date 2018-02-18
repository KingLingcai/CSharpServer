using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
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
        /// <param name="kyId">看园ID</param>
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
            string bloodType, string teacherName, string patriarchName, string websiteName, string examDate, string kyId)
        {
            StatusReport sr = new StatusReport();
            string dbName = kindergartenName == "松园幼儿园" ? "sy" : "ydal";
            string sqlString = " if not exists(select * from 基础_小程序报名 where 姓名=@姓名 and 书包电话= @书包电话) " +
                               " insert into 基础_小程序报名(姓名,性别,书包电话,出生日期,家庭住址,学生来源,监护人,与幼儿关系," +
                               " 监护人电话,监护人身份证件类型,监护人证件号码,监护人职业,监护人工作单位及职位," +
                               " 监护人2,与幼儿关系2,监护人2电话,监护人2身份证件类型,监护人2证件号码,监护人2职业,监护人2工作单位及职位," +
                               " 监护人3,与幼儿关系3,监护人3电话,监护人3身份证件类型,监护人3证件号码,监护人3职业,监护人3工作单位及职位," +
                               " 监护人4,与幼儿关系4,监护人4电话,监护人4身份证件类型,监护人4证件号码,监护人4职业,监护人4工作单位及职位," +
                               " 健康备注,食物药品备注,保健手册,体检手册,预防针本,幼儿身份证件类型,幼儿身份证件号码,民族,国籍地区,港澳台侨外," +
                               " 户籍地区,户籍地区详情,户口性质,非农业户口类型,是否残疾,残疾类型,是否留守儿童,是否独生子女,是否进城务工人员子女," +
                               " 是否孤儿,健康状况,血型,看园ID,报名日期,家长姓名,老师姓名,网站名称,体检时间,审核状态)" +
                               " select @姓名,@性别,@书包电话,@出生日期,@家庭住址,@学生来源," +
                               " @监护人,@与幼儿关系,@监护人电话,@监护人身份证件类型,@监护人证件号码,@监护人职业,@监护人工作单位及职位," +
                               " @监护人2,@与幼儿关系2,@监护人2电话,@监护人2身份证件类型,@监护人2证件号码,@监护人2职业,@监护人2工作单位及职位," +
                               " @监护人3,@与幼儿关系3,@监护人3电话,@监护人3身份证件类型,@监护人3证件号码,@监护人3职业,@监护人3工作单位及职位," +
                               " @监护人4,@与幼儿关系4,@监护人4电话,@监护人4身份证件类型,@监护人4证件号码,@监护人4职业,@监护人4工作单位及职位," +
                               " @健康备注,@食物药品备注,@保健手册,@体检手册,@预防针本,@幼儿身份证件类型,@幼儿身份证件号码,@民族,@国籍地区,@港澳台侨外," +
                               " @户籍地区,@户籍地区详情,@户口性质,@非农业户口类型,@是否残疾,@残疾类型,@是否留守儿童,@是否独生子女,@是否进城务工人员子女," +
                               " @是否孤儿,@健康状况,@血型,@看园ID,@报名日期,@家长姓名,@老师姓名,@网站名称,@体检时间,@审核状态 " +
                               " select @@identity ";

            sr = SQLHelper.Insert(dbName, sqlString, new SqlParameter("@姓名", GetDBValue(name)),
                                                     new SqlParameter("@性别", GetDBValue(gender)),
                                                     new SqlParameter("@书包电话", GetDBValue(bagPhone)),
                                                     new SqlParameter("@出生日期", GetDBValue(birth)),
                                                     new SqlParameter("@家庭住址", GetDBValue(address)),
                                                     new SqlParameter("@学生来源", GetDBValue(source)),
                                                     new SqlParameter("@监护人", GetDBValue(guardianName)),
                                                     new SqlParameter("@与幼儿关系", GetDBValue(relation)),
                                                     new SqlParameter("@监护人电话", GetDBValue(guardianPhone)),
                                                     new SqlParameter("@监护人身份证件类型", GetDBValue(guardianCredentialType)),
                                                     new SqlParameter("@监护人证件号码", GetDBValue(guardianIdNumber)),
                                                     new SqlParameter("@监护人职业", GetDBValue(occupation)),
                                                     new SqlParameter("@监护人工作单位及职位", GetDBValue(job)),
                                                     new SqlParameter("@监护人2", GetDBValue(guardianName2)),
                                                     new SqlParameter("@与幼儿关系2", GetDBValue(relation2)),
                                                     new SqlParameter("@监护人2电话", GetDBValue(guardianPhone2)),
                                                     new SqlParameter("@监护人2身份证件类型", GetDBValue(guardianCredentialType2)),
                                                     new SqlParameter("@监护人2证件号码", GetDBValue(guardianIDNumber2)),
                                                     new SqlParameter("@监护人2职业", GetDBValue(occupation2)),
                                                     new SqlParameter("@监护人2工作单位及职位", GetDBValue(job2)),
                                                     new SqlParameter("@监护人3", GetDBValue(guardianName3)),
                                                     new SqlParameter("@与幼儿关系3", GetDBValue(relation3)),
                                                     new SqlParameter("@监护人3电话", GetDBValue(guardianPhone3)),
                                                     new SqlParameter("@监护人3身份证件类型", GetDBValue(guardianCredentialType3)),
                                                     new SqlParameter("@监护人3证件号码", GetDBValue(guardianIDNumber3)),
                                                     new SqlParameter("@监护人3职业", GetDBValue(occupation3)),
                                                     new SqlParameter("@监护人3工作单位及职位", GetDBValue(job3)),
                                                     new SqlParameter("@监护人4", GetDBValue(guardianName4)),
                                                     new SqlParameter("@与幼儿关系4", GetDBValue(relation4)),
                                                     new SqlParameter("@监护人4电话", GetDBValue(guardianPhone4)),
                                                     new SqlParameter("@监护人4身份证件类型", GetDBValue(guardianCredentialType4)),
                                                     new SqlParameter("@监护人4证件号码", GetDBValue(guardianIDNumber4)),
                                                     new SqlParameter("@监护人4职业", GetDBValue(occupation4)),
                                                     new SqlParameter("@监护人4工作单位及职位", GetDBValue(job4)),
                                                     new SqlParameter("@健康备注", GetDBValue(healthRemarks)),
                                                     new SqlParameter("@食物药品备注", GetDBValue(foodDragRemarks)),
                                                     new SqlParameter("@保健手册", GetDBValue(healthCareNote)),
                                                     new SqlParameter("@体检手册", GetDBValue(examination)),
                                                     new SqlParameter("@预防针本", GetDBValue(vaccineNote)),
                                                     new SqlParameter("@幼儿身份证件类型", GetDBValue(kidCredentialType)),
                                                     new SqlParameter("@幼儿身份证件号码", GetDBValue(kidIDNumber)),
                                                     new SqlParameter("@民族", GetDBValue(kidNation)),
                                                     new SqlParameter("@国籍地区", GetDBValue(kidNationality)),
                                                     new SqlParameter("@港澳台侨外", GetDBValue(gangaotai)),
                                                     new SqlParameter("@户籍地区", GetDBValue(area)),
                                                     new SqlParameter("@户籍地区详情", GetDBValue(areaDetail)),
                                                     new SqlParameter("@户口性质", GetDBValue(residenceNature)),
                                                     new SqlParameter("@非农业户口类型", GetDBValue(nonagricultureType)),
                                                     new SqlParameter("@是否残疾", GetDBValue(disabled)),
                                                     new SqlParameter("@残疾类型", GetDBValue(disabledType)),
                                                     new SqlParameter("@是否留守儿童", GetDBValue(leftChild)),
                                                     new SqlParameter("@是否独生子女", GetDBValue(onlyChild)),
                                                     new SqlParameter("@是否进城务工人员子女", GetDBValue(migrantWorkerChild)),
                                                     new SqlParameter("@是否孤儿", GetDBValue(orphan)),
                                                     new SqlParameter("@健康状况", GetDBValue(healthCondition)),
                                                     new SqlParameter("@血型", GetDBValue(bloodType)),
                                                     new SqlParameter("@看园ID", GetDBValue(kyId)),
                                                     new SqlParameter("@报名日期", System.DateTime.Now),
                                                     new SqlParameter("@家长姓名", GetDBValue(patriarchName)),
                                                     new SqlParameter("@老师姓名", GetDBValue(teacherName)),
                                                     new SqlParameter("@网站名称", GetDBValue(websiteName)),
                                                     new SqlParameter("@体检时间", GetDBValue(examDate)),
                                                     new SqlParameter("@审核状态", "未审"));
            return sr;
        }

        private static Object GetDBValue(string value)
        {
            if (String.IsNullOrEmpty(value))
            {
                return DBNull.Value;
            }
            else
            {
                return value;
            }
        }
    }
}