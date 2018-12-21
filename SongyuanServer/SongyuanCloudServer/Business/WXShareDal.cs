using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using SongyuanUtils;
using SongyuanCloudServer.Models;
using System.Collections.Specialized;
using System.Collections;

namespace SongyuanCloudServer.Business
{
    public class WXShareDal
    {
        //sharePerson person = new sharePerson();
        //public static StatusReport SetShareInfo (string userid, string shareNumber)
        //{
        //    StatusReport sr = new StatusReport();
        //    string sqlString = "insert into 基础_分享 (分享单编号) " +
        //        " select @分享单编号 " +
        //        " select @@identity ";
        //    sr = SQLHelper.Insert("wyt", sqlString, new SqlParameter("@分享单编号", shareNumber));
        //    if (!string.IsNullOrEmpty(sr.data.ToString()))
        //    {
        //        string PID = sr.data.ToString();
        //        sqlString = "insert into 基础_分享_分享情况 (PID,发单人ID,发单时间) " +
        //            " select @PID,@发单人ID,@发单时间 " +
        //            " select @@identity ";
        //        sr = SQLHelper.Insert("wyt", sqlString, new SqlParameter("@PID", PID),
        //            new SqlParameter("@发单人ID", userid),
        //            new SqlParameter("@发单时间", DateTime.Now));
        //        return sr;
        //    }
        //    else
        //    {
        //        return sr;
        //    }
        //}

        public static StatusReport SetShareInfo(string receiverId, string shareNumber, string userId, string userName, string shareTime, string kindergartenName)
        {
            StatusReport sr = new StatusReport();
            string dbName = kindergartenName == "松园幼儿园" ? "cloudsy" : "cloudyd";
            string sqlString =
                "if not exists (select ID from 基础_分享 where 分享单编号 = @分享单编号) " +
                " begin " +
                " insert into 基础_分享(分享单编号, 分享人姓名) " +
                " select @分享单编号,@分享人姓名 " +
                " select @@identity " +
                " end " +
                " else " +
                " begin " +
                " select ID from 基础_分享 where 分享单编号 = @分享单编号" +
                " end ";
            sr = SQLHelper.Insert(dbName, sqlString, new SqlParameter("@分享单编号", shareNumber), new SqlParameter("@分享人姓名", userName));
            if (!string.IsNullOrEmpty(sr.data.ToString()))
            {
                StatusReport report = new StatusReport();
                /**
                 * 如果存在发单人 = @发单人 ，层数为发单人的层数
                 * 如果不存在发单人 = @发单人，判断接单人 = @发单人，如果存在，层数为接单人层数 + 1 ，如果不存在，层数为1。
                 **/
                string PID = sr.data.ToString();
                string sqlStr = "if not exists (select ID from 基础_分享_分享情况 where (PID = @PID) and ((发单人ID = @发单人ID and 接单人ID = @接单人ID) or ( 发单人ID = @接单人ID))) " +
                                    " begin " +
                                        " if not exists (select 层数 from 基础_分享_分享情况 where PID = @PID and 发单人ID = @发单人ID) " +
                                            " begin " +
                                                " if not exists (select 层数 from 基础_分享_分享情况 where PID = @PID and 接单人ID = @发单人ID) " +
                                                    " begin " +
                                                        " select 1 as 层数" +
                                                    " end " +
                                                " else " +
                                                    " begin " +
                                                        " select 层数 + 1 as 层数 from 基础_分享_分享情况 where PID = @PID and 接单人ID = @发单人ID " +
                                                    " end " +
                                            " end " +
                                        " else " +
                                            " begin " +
                                                " select 层数 from 基础_分享_分享情况 where PID = @PID and 发单人ID = @发单人ID " +
                                            " end " +
                                    " end ";
                DataTable dt = SQLHelper.ExecuteQuery(dbName, sqlStr,
                    new SqlParameter("@PID", PID),
                    new SqlParameter("@发单人ID", userId),
                    new SqlParameter("@接单人ID", receiverId));
                if (dt.Rows.Count == 0)
                {
                    report.status = "Fail";
                    report.result = "数据已存在";
                    return report;
                }
                DataRow dr = dt.Rows[0];
                object value = dr["层数"];
                string sqlStr1 = "insert into 基础_分享_分享情况 (PID,发单人ID,发单时间,接单人ID,接单时间,层数) " +
                    " select @PID,@发单人ID,@发单时间,@接单人ID,@接单时间,@层数 " +
                    " select @@identity ";
                report = SQLHelper.Insert(dbName, sqlStr1,
                    new SqlParameter("@PID", PID),
                    new SqlParameter("@发单人ID", userId),
                    new SqlParameter("@发单时间", shareTime),
                    new SqlParameter("@接单人ID", receiverId),
                    new SqlParameter("@接单时间", DateTime.Now),
                    new SqlParameter("@层数", value));
                return report;
            }
            return sr;
        }

        public static StatusReport GetShareInfoList(string kindergartenName, string userName)
        {
            StatusReport sr = new StatusReport();
            string dbName = kindergartenName == "松园幼儿园" ? "localsy" : "localyd";
            string sqlString = "";
            if (userName == "崔道远")
            {
                sqlString = "select 发单人ID, 发单人姓名, count(distinct 接单人ID) as 接单人数, count(distinct 姓名 + 书包电话) as 报名人数, sum(case when 是否已交费 is null or 是否已交费 = '否' then 0 else 1 end) as 已交费人数 " +
                "from 小程序_分享情况 where 发单人ID is not null " +
                "group by 发单人ID,发单人姓名 " +
                "order by 发单人ID ";
            }
            else
            {
                sqlString = "select 发单人ID, 发单人姓名, count(distinct 接单人ID) as 接单人数, count(distinct 姓名 + 书包电话) as 报名人数, sum(case when 是否已交费 is null or 是否已交费 = '否' then 0 else 1 end) as 已交费人数 " +
               " from 小程序_分享情况 where 发单人ID is not null and 发单人姓名 = @发单人姓名" +
               " group by 发单人ID,发单人姓名 " +
               " order by 发单人ID ";
            }

            //string sqlString = " select 分享单编号,count(发单人ID) as 分享次数 , max(层数) as 层数 from 小程序_分享情况 group by ID, 分享单编号 order by ID desc ";
            DataTable dt = SQLHelper.ExecuteQuery(dbName, sqlString, new SqlParameter("@发单人姓名", userName));
            if (dt.Rows.Count == 0)
            {
                sr.status = "Fail";
                sr.result = "未查询到分享记录";
                return sr;
            }
            else
            {
                List<ShareInfoIntro> list = new List<ShareInfoIntro>();
                foreach (DataRow dr in dt.Rows)
                {
                    ShareInfoIntro intro = new ShareInfoIntro()
                    {
                        id = DataTypeHelper.GetIntValue(dr["发单人ID"]),
                        name = DataTypeHelper.GetStringValue(dr["发单人姓名"]),
                        shareTimes = DataTypeHelper.GetIntValue(dr["接单人数"]),
                        signUpTimes = DataTypeHelper.GetIntValue(dr["报名人数"]),
                        payTimes = DataTypeHelper.GetIntValue(dr["已交费人数"])
                    };
                    list.Add(intro);
                }
                sr.status = "Success";
                sr.result = "成功";
                sr.data = list.ToArray();
                return sr;
            }
        }


        public static StatusReport GetShareDetail(string kindergartenName, string id)
        {
            StatusReport sr = new StatusReport();
            string dbName = kindergartenName == "松园幼儿园" ? "localsy" : "localyd";
            string sqlString = "select 发单人ID,接单人ID,接单人姓名,接单人手机号,接单人微信昵称,接单人性别,count(姓名) as 报名人数 " +
                "from 小程序_分享情况 " +
                "where 发单人ID = " + id +
                "group by 发单人ID,接单人ID,接单人姓名,接单人手机号,接单人微信昵称,接单人性别";
            DataTable dt = SQLHelper.ExecuteQuery(dbName, sqlString);
            if (dt.Rows.Count == 0)
            {
                sr.status = "Fail";
                sr.result = "未查询到分享详情记录";
                return sr;
            }
            List<shareDetailTemp> tempList = new List<shareDetailTemp>();
            foreach (DataRow dr in dt.Rows)
            {
                shareDetailTemp detailTemp = new shareDetailTemp()
                {
                    shareID = id,
                    receiverID = DataTypeHelper.GetIntValue(dr["接单人ID"]).ToString(),
                    receiverName = DataTypeHelper.GetStringValue(dr["接单人姓名"]),
                    receiverPhone = DataTypeHelper.GetStringValue(dr["接单人手机号"]),
                    receiverNickName = DataTypeHelper.GetStringValue(dr["接单人微信昵称"]),
                    receiverGender = DataTypeHelper.GetStringValue(dr["接单人性别"]),
                    signupTimes = DataTypeHelper.GetIntValue(dr["报名人数"]),
                };
                tempList.Add(detailTemp);
            }
            shareDetailTemp[] tempArr = tempList.ToArray();
            sr.status = "Success";
            sr.result = "成功";
            sr.data = tempArr;
            return sr;
        }

        public static StatusReport GetSignupDetail(string kindergartenName, string shareId, string receiverId)
        {
            StatusReport sr = new StatusReport();
            string dbName = kindergartenName == "松园幼儿园" ? "localsy" : "localyd";
            string sqlString = " select 姓名,监护人,与幼儿关系,书包电话,报名日期,是否已交费 " +
                " from 小程序_分享情况 " +
                " where 姓名 is not null and 发单人ID = " + shareId + "and 接单人ID = " + receiverId;
            DataTable dt = SQLHelper.ExecuteQuery(dbName, sqlString);
            if (dt.Rows.Count == 0)
            {
                sr.status = "Fail";
                sr.result = "未查询到分享详情记录";
                return sr;
            }
            List<signupDetail> tempList = new List<signupDetail>();
            foreach (DataRow dr in dt.Rows)
            {
                signupDetail detailTemp = new signupDetail()
                {
                    name = DataTypeHelper.GetStringValue(dr["姓名"]).ToString(),
                    phone = DataTypeHelper.GetStringValue(dr["书包电话"]),
                    relateName = DataTypeHelper.GetStringValue(dr["监护人"]),
                    relation = DataTypeHelper.GetStringValue(dr["与幼儿关系"]),
                    signuptime = DataTypeHelper.GetDateStringValue(dr["报名日期"]),
                    isCharge = DataTypeHelper.GetStringValue(dr["是否已交费"]),
                };
                tempList.Add(detailTemp);
            }
            signupDetail[] tempArr = tempList.ToArray();
            sr.status = "Success";
            sr.result = "成功";
            sr.data = tempArr;
            return sr;
        }





        //public static StatusReport GetShareDetail(string kindergartenName, string id)
        //{
        //    StatusReport sr = new StatusReport();
        //    string dbName = kindergartenName == "松园幼儿园" ? "wyt" : "ydal";
        //    string sqlString = " select 发单人ID,发单人姓名,发单人手机号,发单人微信昵称,发单人性别,接单人ID,接单人姓名,接单人手机号,接单人微信昵称,接单人性别,层数 " +
        //        " from 小程序_分享情况 " +
        //        " where 发单人ID = @发单人ID " +
        //        " order by 层数 ";



        //    DataTable dt = SQLHelper.ExecuteQuery(dbName, sqlString, new SqlParameter("@发单人ID", id));
        //    if (dt.Rows.Count == 0)
        //    {
        //        sr.status = "Fail";
        //        sr.result = "未查询到分享详情记录";
        //        return sr;
        //    }
        //    List<shareDetailTemp> tempList = new List<shareDetailTemp>();
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        shareDetailTemp detailTemp = new shareDetailTemp()
        //        {
        //            shareID = DataTypeHelper.GetIntValue(dr["发单人ID"]).ToString(),
        //            shareName = DataTypeHelper.GetStringValue(dr["发单人姓名"]),
        //            sharePhone = DataTypeHelper.GetStringValue(dr["发单人手机号"]),
        //            shareNickName = DataTypeHelper.GetStringValue(dr["发单人微信昵称"]),
        //            shareGender = DataTypeHelper.GetStringValue(dr["发单人性别"]),
        //            receiverID = DataTypeHelper.GetIntValue(dr["接单人ID"]).ToString(),
        //            receiverName = DataTypeHelper.GetStringValue(dr["接单人姓名"]),
        //            receiverPhone = DataTypeHelper.GetStringValue(dr["接单人手机号"]),
        //            receiverNickName = DataTypeHelper.GetStringValue(dr["接单人微信昵称"]),
        //            receiverGender = DataTypeHelper.GetStringValue(dr["接单人性别"]),
        //            floor = DataTypeHelper.GetIntValue(dr["层数"]).ToString(),
        //        };
        //        tempList.Add(detailTemp);
        //    }
        //    shareDetailTemp[] tempArr = tempList.ToArray();

        //    int floors = Convert.ToInt32(tempArr[tempArr.Length - 1].floor);
        //    List<shareDetailTemp[]> list = new List<shareDetailTemp[]>();
        //    for (int i = 0; i < floors; i++)
        //    {
        //        List<shareDetailTemp> tList = new List<shareDetailTemp>();
        //        for (int j = 0; j < tempArr.Length; j++)
        //        {
        //            if (Convert.ToInt32(tempArr[j].floor) == i + 1)
        //            {
        //                tList.Add(tempArr[j]);
        //            }
        //        }
        //        list.Add(tList.ToArray());
        //    }
        //    shareDetailTemp[][] detailTempArr = list.ToArray();//有几层，每一层有多少人
        //                                                       /**
        //                                                        * 第一层： 取发单人(可能有重复，需要保证唯一）
        //                                                        * 第二层： 取第一层的接单人，或第二层的发单人(可能有重复，需要保证唯一）
        //                                                        * 第三层： 取第二层的接单人，或第三层的发单人(可能有重复，需要保证唯一）
        //                                                        */

        //    shareDetailTemp temp = detailTempArr[0][0];
        //    sharePerson person = new sharePerson(temp.floor,temp.shareID,temp.shareName,temp.sharePhone,temp.shareNickName,temp.shareGender);
        //    foreach(shareDetailTemp t in detailTempArr[0])
        //    {
        //        person.children.Add(new sharePerson(t.floor, t.receiverID, t.receiverName, t.receiverPhone, t.receiverNickName, t.receiverGender));
        //    }
        //    //person.ID = detailTempArr[0][0].shareID;
        //    //person.name = detailTempArr[0][0].shareID;
        //    //person.phone = detailTempArr[0][0].shareID;
        //    //person.nickName = detailTempArr[0][0].shareID;
        //    //person.gender = detailTempArr[0][0].shareID;
        //    //person.floor = detailTempArr[0][0].floor;
        //    for (int i = 1; i < detailTempArr.Length; i++)
        //    {
        //        for (int j = 0; j < detailTempArr[i].Length; j++)
        //        {
        //            person.Find(detailTempArr[i][j]);
        //        }
        //    }
        //    sr.status = "Success";
        //    sr.result = "成功";
        //    sr.data = person;
        //    return sr;
        //}

        //private static void Find (sharePerson person, shareDetailTemp temp)
        //{

        //    if (Convert.ToInt32(temp.floor) == 1)
        //    {
        //        if (string.IsNullOrEmpty(person.ID))
        //        {
        //            person.ID = temp.shareID;
        //            person.name = temp.shareName;
        //            person.phone = temp.sharePhone;
        //            person.nickName = temp.shareNickName;
        //            person.gender = temp.shareGender;
        //            person.floor = temp.floor;
        //            //person.receiverID = temp.receiverID;
        //            person.children = new List<sharePerson>();
        //        }
        //        sharePerson tempPerson = new sharePerson();
        //        tempPerson.floor = (Convert.ToInt32(temp.floor) + 1).ToString();
        //        tempPerson.ID = temp.receiverID;
        //        tempPerson.name = temp.receiverName;
        //        tempPerson.phone = temp.receiverPhone;
        //        tempPerson.nickName = temp.receiverNickName;
        //        tempPerson.gender = temp.receiverGender;
        //        person.children.Add(tempPerson);
        //    }
        //    //else if (Convert.ToInt32(temp.floor) == 2)
        //    //{
        //    //    //sharePerson childPerson = new sharePerson();
        //    //    //childPerson.ID = temp.shareID;
        //    //    //childPerson.name = temp.shareName;
        //    //    //childPerson.phone = temp.sharePhone;
        //    //    //childPerson.nickName = temp.shareNickName;
        //    //    //childPerson.gender = temp.shareGender;
        //    //    //childPerson.floor = temp.floor;
        //    //    //childPerson.receiverID = temp.receiverID;
        //    //    //if (person.children == null)
        //    //    //{
        //    //    //    person.children = new List<sharePerson>();
        //    //    //}
        //    //    //person.children.Add(childPerson);
        //    //}
        //    else
        //    {
        //        foreach (sharePerson childPerson in person.children)

        //        {
        //            if (temp.shareID == childPerson.receiverID)
        //            {
        //                sharePerson tempPerson = new sharePerson();
        //                tempPerson.ID = temp.receiverID;
        //                tempPerson.name = temp.receiverName;
        //                tempPerson.phone = temp.receiverPhone;
        //                tempPerson.nickName = temp.receiverNickName;
        //                tempPerson.gender = temp.receiverGender;
        //                tempPerson.floor = temp.floor;
        //                //tempPerson.receiverID = temp.receiverID;
        //                if (childPerson.children == null)
        //                {
        //                    childPerson.children = new List<sharePerson>();
        //                }
        //                childPerson.children.Add(tempPerson);
        //            }
        //            else
        //            {
        //                Find(childPerson, temp);
        //            }
        //        }
        //    }
        //}



        //public static StatusReport SetReceiveInfo(string receiverId, string shareNumber, string userId, string shareTime)
        //{
        //    StatusReport sr = new StatusReport();
        //    string sqlString = 
        //        "if not exists (select ID from 基础_分享情况 where 分享单编号 = @分享单编号)" +
        //            " begin " +
        //                " insert into 基础_分享情况 (是否父节点,层数,父节点ID,发单人ID,接单人ID,分享单编号,发单时间,接单时间) " +
        //                " select 1,1,NULL,@发单人ID,@接单人ID,@分享单编号,@发单时间,@接单时间 " +
        //            " end " +
        //        " else " +
        //            " begin " +
        //                " insert into 基础_分享情况 (是否父节点,层数,父节点ID,发单人ID,接单人ID,分享单编号,发单时间,接单时间) " +
        //                " select 0,@层数,@父节点ID,@发单人ID,@接单人ID,@分享单编号,@发单时间,@接单时间 from 基础_分享情况 where 分享单编号 = @分享单编号 ";
        //}
    }
}


