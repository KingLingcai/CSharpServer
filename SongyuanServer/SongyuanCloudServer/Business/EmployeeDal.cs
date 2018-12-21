using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using SongyuanUtils;

namespace SongyuanCloudServer.Business
{
    public class EmployeeDal
    {
        /// <summary>
        /// 将职工信息保存到数据库中
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
        public static StatusReport SetEmployeeData(string kindergartenName, string name, string birth, string IDNumber, string hiredate,
            string address, string socialSecurityNumber, string HAFA, string salaryWay, string payCheckAccount, string bank,
            string noSocialSecurityReason, string payAmount, string socialSecurityAmount, string phoneNumber, string phoneAddress,
            string hardwareAddress, string contact, string contactPhoneNumber, string record, string title, string department,
            string position, string qualification, string isHealthCertificate, string college, string profession,
            string speciality, string college1, string college2, string college3, string eduStartTime1, string eduStartTime2,
            string eduStartTime3, string eduEndTime1, string eduEndTime2, string eduEndTime3, string work1, string work2,
            string work3, string workstartTime1, string workstartTime2, string workstartTime3, string workEndTime1,
            string workEndTime2, string workEndTime3)
        {
            StatusReport sr = new StatusReport();

            //设置数据库名
            string dbName = kindergartenName == "松园幼儿园" ? "cloudsy" : "cloudyd";
            //设置sql语句
            string sqlString = " if not exists(select * from 基础_小程序职工登记 where 姓名=@姓名 and 手机= @手机) " +
                               " insert into 基础_小程序职工登记 (姓名,出生日期,身份证号码,入职时间,住址,社保号,住房公积金帐号, " +
                               " 工资发放方式,工资卡帐号,所属银行,无社保原因,工资金额,社保金额,手机,手机地址,电脑硬件地址,员工联系人, " +
                               " 联系人手机,学历,专业技术职称,部门,职务,资格,是否有健康证,审核状态,毕业院校,专业,特长, " +
                               " 入学时间1,毕业时间1,所在院校1,入学时间2,毕业时间2,所在院校2,入学时间3,毕业时间3,所在院校3, " +
                               " 入职时间1,离职时间1,所在单位1,入职时间2,离职时间2,所在单位2,入职时间3,离职时间3,所在单位3) " +
                               " select @姓名,@出生日期,@身份证号码,@入职时间,@住址,@社保号,@住房公积金帐号, " +
                               " @工资发放方式,@工资卡帐号,@所属银行,@无社保原因,@工资金额,@社保金额,@手机,@手机地址,@电脑硬件地址,@员工联系人," +
                               " @联系人手机,@学历,@专业技术职称,@部门,@职务,@资格,@是否有健康证,@审核状态,@毕业院校,@专业,@特长, " +
                               " @入学时间1,@毕业时间1,@所在院校1,@入学时间2,@毕业时间2,@所在院校2,@入学时间3,@毕业时间3,@所在院校3, " +
                               " @入职时间1,@离职时间1,@所在单位1,@入职时间2,@离职时间2,@所在单位2,@入职时间3,@离职时间3,@所在单位3 " +
                               " select @@identity ";
            sr = SQLHelper.Insert(dbName, sqlString, new SqlParameter("@姓名", GetDBValue(name)),
                                                     new SqlParameter("@出生日期", GetDBValue(birth)),
                                                     new SqlParameter("@身份证号码", GetDBValue(IDNumber)),
                                                     new SqlParameter("@入职时间", GetDBValue(hiredate)),
                                                     new SqlParameter("@住址", GetDBValue(address)),
                                                     new SqlParameter("@社保号", GetDBValue(socialSecurityNumber)),
                                                     new SqlParameter("@住房公积金帐号", GetDBValue(HAFA)),
                                                     new SqlParameter("@工资发放方式", GetDBValue(salaryWay)),
                                                     new SqlParameter("@工资卡帐号", GetDBValue(payCheckAccount)),
                                                     new SqlParameter("@所属银行", GetDBValue(bank)),
                                                     new SqlParameter("@无社保原因", GetDBValue(noSocialSecurityReason)),
                                                     new SqlParameter("@工资金额", GetDBValue(payAmount)),
                                                     new SqlParameter("@社保金额", GetDBValue(socialSecurityAmount)),
                                                     new SqlParameter("@手机", GetDBValue(phoneNumber)),
                                                     new SqlParameter("@手机地址", GetDBValue(phoneAddress)),
                                                     new SqlParameter("@电脑硬件地址", GetDBValue(hardwareAddress)),
                                                     new SqlParameter("@员工联系人", GetDBValue(contact)),
                                                     new SqlParameter("@联系人手机", GetDBValue(contactPhoneNumber)),
                                                     new SqlParameter("@学历", GetDBValue(record)),
                                                     new SqlParameter("@专业技术职称", GetDBValue(title)),
                                                     new SqlParameter("@部门", GetDBValue(department)),
                                                     new SqlParameter("@职务", GetDBValue(position)),
                                                     new SqlParameter("@资格", GetDBValue(qualification)),
                                                     new SqlParameter("@是否有健康证", GetDBValue(isHealthCertificate)),
                                                     new SqlParameter("@审核状态", "未审"),
                                                     new SqlParameter("@毕业院校", GetDBValue(college)),
                                                     new SqlParameter("@专业", GetDBValue(profession)),
                                                     new SqlParameter("@特长", GetDBValue(speciality)),
                                                     new SqlParameter("@入学时间1", GetDBValue(eduStartTime1)),
                                                     new SqlParameter("@毕业时间1", GetDBValue(eduEndTime1)),
                                                     new SqlParameter("@所在院校1", GetDBValue(college1)),
                                                     new SqlParameter("@入学时间2", GetDBValue(eduStartTime2)),
                                                     new SqlParameter("@毕业时间2", GetDBValue(eduEndTime2)),
                                                     new SqlParameter("@所在院校2", GetDBValue(college2)),
                                                     new SqlParameter("@入学时间3", GetDBValue(eduStartTime3)),
                                                     new SqlParameter("@毕业时间3", GetDBValue(eduEndTime3)),
                                                     new SqlParameter("@所在院校3", GetDBValue(college3)),
                                                     new SqlParameter("@入职时间1", GetDBValue(workstartTime1)),
                                                     new SqlParameter("@离职时间1", GetDBValue(workEndTime1)),
                                                     new SqlParameter("@所在单位1", GetDBValue(work1)),
                                                     new SqlParameter("@入职时间2", GetDBValue(workstartTime2)),
                                                     new SqlParameter("@离职时间2", GetDBValue(workEndTime2)),
                                                     new SqlParameter("@所在单位2", GetDBValue(work2)),
                                                     new SqlParameter("@入职时间3", GetDBValue(workstartTime3)),
                                                     new SqlParameter("@离职时间3", GetDBValue(workEndTime3)),
                                                     new SqlParameter("@所在单位3", GetDBValue(work3)));
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