using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using SongyuanUtils;
using SongyuanCloudServer.Models;

namespace SongyuanCloudServer.Business
{
    public class KanyuanDataDal
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
        public static StatusReport SetKanyuanData(string kindergartenName, string name, string gender, string birth,
            string relateName, string relation, string phoneNumber, string address, string isYoueryuan, string desire,
            string joinLottery, string ruyuanDate, string isAppointment, string appointmentDate, string relateGender,
            string haveReceiver, string receiverName, string needSchoolBus, string openid)
        {
            StatusReport sr = new StatusReport();

            //根据幼儿园名称选择不同的数据库
            string dbName = kindergartenName == "松园幼儿园" ? "cloudsy" : "cloudyd";

            string sqlString = "if not exists(select * from 基础_看园管理 where 姓名=@姓名 and 联系电话=@联系电话) " +
                          " insert into 基础_看园管理(姓名,性别,出生年月日,家长姓名,家长与幼儿关系,联系电话,家庭住址," +
                          " 是否上过幼儿园,入托意愿,是否政府摇号,计划入学时间,看园日期,家长性别,是否预约看园,预约看园时间," +
                          " 是否有接待人,接待人姓名,是否校车接送,是否交定金,openid) " +
                          " select @姓名, @性别, @出生年月日, @家长姓名, @家长与幼儿关系, @联系电话, @家庭住址, @是否上过幼儿园," +
                          " @入托意愿, @是否政府摇号,@计划入学时间, @看园日期,@家长性别,@是否预约看园,@预约看园时间," +
                          " @是否有接待人,@接待人姓名,@是否校车接送,@是否交定金,@openid " +
                          " select @@identity ";

            //将看园数据插入到表中
            sr = SQLHelper.Insert(dbName, sqlString, new SqlParameter("@姓名", GetDBValue(name)),
                                                     new SqlParameter("@性别", GetDBValue(gender)),
                                                     new SqlParameter("@出生年月日", GetDBValue(birth)),
                                                     new SqlParameter("@家长姓名", GetDBValue(relateName)),
                                                     new SqlParameter("@家长与幼儿关系", GetDBValue(relation)),
                                                     new SqlParameter("@联系电话", GetDBValue(phoneNumber)),
                                                     new SqlParameter("@家庭住址", GetDBValue(address)),
                                                     new SqlParameter("@是否上过幼儿园", GetDBValue(isYoueryuan)),
                                                     new SqlParameter("@入托意愿", GetDBValue(desire)),
                                                     new SqlParameter("@是否政府摇号", GetDBValue(joinLottery)),
                                                     new SqlParameter("@计划入学时间", GetDBValue(ruyuanDate)),
                                                     new SqlParameter("@看园日期", System.DateTime.Now),
                                                     new SqlParameter("@家长性别", GetDBValue(relateGender)),
                                                     new SqlParameter("@是否预约看园", GetDBValue(isAppointment)),
                                                     new SqlParameter("@预约看园时间", GetDBValue(appointmentDate)),
                                                     new SqlParameter("@是否有接待人", GetDBValue(haveReceiver)),
                                                     new SqlParameter("@接待人姓名", GetDBValue(receiverName)),
                                                     new SqlParameter("@是否校车接送", GetDBValue(needSchoolBus)),
                                                     new SqlParameter("@openid", GetDBValue(openid)),
                                                     new SqlParameter("@是否交定金", "否"));


            return sr;
        }

        /// <summary>
        /// 通过姓名和联系电话查找看园信息
        /// </summary>
        /// <param name="kindergartenName">幼儿园名称</param>
        /// <param name="name">姓名</param>
        /// <param name="phoneNumber">联系电话</param>
        /// <returns></returns>
        //public static StatusReport GetKanyuanData(string kindergartenName, string name, string phoneNumber)
        //{
        //    StatusReport sr = new StatusReport();

        //    //根据幼儿园名称选择不同的数据库
        //    string dbName = kindergartenName == "松园幼儿园" ? "wyt" : "ydal";
        //    string sqlString = " select ID,姓名,性别,出生年月日,家长姓名,家长与幼儿关系,联系电话,家庭住址,是否上过幼儿园, " +
        //                       " 家长性别,入托意愿,计划入学时间,是否政府摇号,是否预约看园,预约看园时间," +
        //                       " 是否有接待人,接待人姓名,是否校车接送 " +
        //                       " from 基础_看园管理 where 姓名=@姓名 and 联系电话= @联系电话 ";

        //    //在数据库中查询匹配的看园数据
        //    DataTable dt = SQLHelper.ExecuteQuery(dbName, sqlString, new SqlParameter("@姓名", GetDBValue(name)),
        //                                                             new SqlParameter("@联系电话", GetDBValue(phoneNumber)));

        //    //如果未查询到数据，返回错误信息
        //    if (dt.Rows.Count == 0)
        //    {
        //        sr.status = "Fail";
        //        sr.result = "未查询到任何数据";
        //        return sr;
        //    }

        //    //获取查询到的数据，并用以初始化kyData
        //    DataRow dr = dt.Rows[0];
        //    KanyuanData kyData = new KanyuanData();
        //    kyData.id = DataTypeHelper.GetIntValue(dr["ID"]);
        //    kyData.name = DataTypeHelper.GetStringValue(dr["姓名"]);
        //    kyData.gender = DataTypeHelper.GetStringValue(dr["性别"]);
        //    kyData.birth = DataTypeHelper.GetStringValue(dr["出生年月日"]);
        //    kyData.relateName = DataTypeHelper.GetStringValue(dr["家长姓名"]);
        //    kyData.relation = DataTypeHelper.GetStringValue(dr["家长与幼儿关系"]);
        //    kyData.phoneNumber = DataTypeHelper.GetStringValue(dr["联系电话"]);
        //    kyData.address = DataTypeHelper.GetStringValue(dr["家庭住址"]);
        //    kyData.isYoueryuan = DataTypeHelper.GetStringValue(dr["是否上过幼儿园"]);
        //    kyData.relateGender = DataTypeHelper.GetStringValue(dr["家长性别"]);
        //    kyData.desire = DataTypeHelper.GetStringValue(dr["入托意愿"]);
        //    kyData.ruyuanDate = DataTypeHelper.GetStringValue(dr["计划入学时间"]);
        //    kyData.joinLottery = DataTypeHelper.GetStringValue(dr["是否政府摇号"]);
        //    kyData.isAppointment = DataTypeHelper.GetStringValue(dr["是否预约看园"]);
        //    kyData.appointmentDate = DataTypeHelper.GetStringValue(dr["预约看园时间"]);
        //    kyData.haveReceiver = DataTypeHelper.GetStringValue(dr["是否有接待人"]);
        //    kyData.receiverName = DataTypeHelper.GetStringValue(dr["接待人姓名"]);
        //    kyData.needSchoolBus = DataTypeHelper.GetStringValue(dr["是否校车接送"]);

        //    //返回查询到的数据
        //    sr.status = "Success";
        //    sr.result = "成功";
        //    sr.data = kyData;

        //    return sr;
        //}

        public static StatusReport GetKanyuanData(string kindergartenName, string openid)
        {
            StatusReport sr = new StatusReport();

            //根据幼儿园名称选择不同的数据库
            string dbName = kindergartenName == "松园幼儿园" ? "localsy" : "localyd";
            //string sqlString = " select ID,姓名,性别,出生年月日,家长姓名,家长与幼儿关系,联系电话,家庭住址,是否上过幼儿园, " +
            //                   " 家长性别,入托意愿,计划入学时间,是否政府摇号,是否预约看园,预约看园时间," +
            //                   " 是否有接待人,接待人姓名,是否校车接送 " +
            //                   " from 基础_看园管理 where 姓名=@姓名 and 联系电话= @联系电话 ";

            //在数据库中查询匹配的看园数据
            //DataTable dt = SQLHelper.ExecuteQuery(dbName, sqlString, new SqlParameter("@姓名", GetDBValue(name)),
            //new SqlParameter("@联系电话", GetDBValue(phoneNumber)));

            string sqlString = " select ID,姓名,性别,出生年月日,家长姓名,家长与幼儿关系,联系电话,家庭住址,是否上过幼儿园, " +
                               " 家长性别,入托意愿,计划入学时间,是否政府摇号,是否预约看园,预约看园时间," +
                               " 是否有接待人,接待人姓名,是否校车接送 " +
                               " from 基础_看园管理 where openid = @openid " +
                               " order by ID desc ";

            //在数据库中查询匹配的看园数据
            DataTable dt = SQLHelper.ExecuteQuery(dbName, sqlString, new SqlParameter("@openid", GetDBValue(openid)));

            //如果未查询到数据，返回错误信息
            if (dt.Rows.Count == 0)
            {
                sr.status = "Fail";
                sr.result = "未查询到任何数据";
                return sr;
            }

            //获取查询到的数据，并用以初始化kyData
            DataRow dr = dt.Rows[0];
            KanyuanData kyData = new KanyuanData();
            kyData.id = DataTypeHelper.GetIntValue(dr["ID"]);
            kyData.name = DataTypeHelper.GetStringValue(dr["姓名"]);
            kyData.gender = DataTypeHelper.GetStringValue(dr["性别"]);
            kyData.birth = DataTypeHelper.GetDateStringValue(dr["出生年月日"]);
            kyData.relateName = DataTypeHelper.GetStringValue(dr["家长姓名"]);
            kyData.relation = DataTypeHelper.GetStringValue(dr["家长与幼儿关系"]);
            kyData.phoneNumber = DataTypeHelper.GetStringValue(dr["联系电话"]);
            kyData.address = DataTypeHelper.GetStringValue(dr["家庭住址"]);
            kyData.isYoueryuan = DataTypeHelper.GetStringValue(dr["是否上过幼儿园"]);
            kyData.relateGender = DataTypeHelper.GetStringValue(dr["家长性别"]);
            kyData.desire = DataTypeHelper.GetStringValue(dr["入托意愿"]);
            kyData.ruyuanDate = DataTypeHelper.GetDateStringValue(dr["计划入学时间"]);
            kyData.joinLottery = DataTypeHelper.GetStringValue(dr["是否政府摇号"]);
            kyData.isAppointment = DataTypeHelper.GetStringValue(dr["是否预约看园"]);
            kyData.appointmentDate = DataTypeHelper.GetDateStringValue(dr["预约看园时间"]);
            kyData.haveReceiver = DataTypeHelper.GetStringValue(dr["是否有接待人"]);
            kyData.receiverName = DataTypeHelper.GetStringValue(dr["接待人姓名"]);
            kyData.needSchoolBus = DataTypeHelper.GetStringValue(dr["是否校车接送"]);

            //返回查询到的数据
            sr.status = "Success";
            sr.result = "成功";
            sr.data = kyData;

            return sr;
        }

        public static StatusReport GetPayInfo(string name, string phoneNumber, string kindergartenName)
        {
            StatusReport sr = new StatusReport();
            string dbName = kindergartenName == "松园幼儿园" ? "localsy" : "localyd";
            string sqlString = "select 交费金额 from 基础_看园管理 where 姓名 = @姓名 and 联系电话 = @联系电话";
            DataTable dt = SQLHelper.ExecuteQuery(dbName, sqlString, new SqlParameter("@姓名", name), new SqlParameter("@联系电话", phoneNumber));
            if (dt.Rows.Count == 0)
            {
                sr.status = "Fail";
                sr.result = "无匹配的看园定金数据";
                return sr;
            }
            else
            {
                DataRow dr = dt.Rows[0];
                double? totalFee = DataTypeHelper.GetDoubleValue(dr["交费金额"]);
                sr.status = "Success";
                sr.result = "成功";
                sr.data = totalFee;
                return sr;
            }
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