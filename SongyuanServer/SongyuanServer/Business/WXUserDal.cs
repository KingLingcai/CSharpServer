using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using HexiUtils;
using SongyuanServer.Models;

namespace SongyuanServer.Business
{
    public class WXUserDal
    {
        public static StatusReport SetNewUser (string openId, string kindergartenName)
        {
            StatusReport sr = new StatusReport();
            string dbName = kindergartenName == "松园幼儿园" ? "wyt" : "ydal";
            string sqlString = " if not exists (select ID from 基础_小程序用户 where openid = @openid) " +
                " begin " +
                " insert into 基础_小程序用户 (openid) select @openid " +
                " select @@identity " +
                " end " +
                " else " +
                " begin" +
                " select ID from 基础_小程序用户 where openid = @openid " +
                " end ";
            sr = SQLHelper.Insert(dbName, sqlString, new SqlParameter("@openid", openId));
            if (sr.data != null)
            {
                string id = sr.data.ToString();
                string sqlStr = "select 对应员工,用户身份 from 基础_小程序用户 where ID = " + id;
                DataTable dt = SQLHelper.ExecuteQuery(dbName, sqlStr);
                DataRow dr = dt.Rows[0];
                User user = new User();
                user.id = id;
                user.name = DataTypeHelper.GetStringValue(dr["对应员工"]);
                user.identity = DataTypeHelper.GetStringValue(dr["用户身份"]);
                sr.status = "Success";
                sr.data = user;
                //sr.data = new { ID = id, name = DataTypeHelper.GetStringValue(dr["对应员工"]), identity = DataTypeHelper.GetStringValue(dr["用户身份"]) };
            }
            return sr;
        }

        public static StatusReport SetWXInfo(string openId, string nickName, int gender, string kindergartenName)
        {
            StatusReport sr = new StatusReport();
            string dbName = kindergartenName == "松园幼儿园" ? "wyt" : "ydal";
            string sqlString = "update 基础_小程序用户 set 微信昵称 = @微信昵称,性别 = @性别 where openid = @openid " +
                " select @@identity";
            sr = SQLHelper.Update(dbName, sqlString,
                new SqlParameter("@微信昵称", nickName),
                new SqlParameter("@性别", gender == 1 ? "男" : "女"),
                new SqlParameter("@openid", openId));
            return sr;
        }

        public static StatusReport SetUserInfo(string openId, string relateName, string phoneNumber, string kindergartenName, string name, string relation)
        {
            StatusReport sr = new StatusReport();
            string dbName = kindergartenName == "松园幼儿园" ? "wyt" : "ydal";
            string sqlString = "update 基础_小程序用户 set 姓名 = @姓名, 手机号 = @手机号, 用户说明 = @用户说明 where openid = @openid " +
                " select @@identity";
            sr = SQLHelper.Update(dbName, sqlString,
                new SqlParameter("@姓名", relateName),
                new SqlParameter("@手机号", phoneNumber),
                new SqlParameter("@用户说明", name + "的" + relation + relateName),
                new SqlParameter("@openid", openId));
            return sr;
        }


        public static StatusReport GetMyInfo(string kindergartenName, string openid)
        {
            StatusReport sr = new StatusReport();
            string dbName = kindergartenName == "松园幼儿园" ? "wyt" : "ydal";
            string sqlString = "select 对应员工,员工联系方式 from 基础_小程序用户 where openid = @openid and 用户身份 = '老师'";
            DataTable dt = SQLHelper.ExecuteQuery(dbName, sqlString, new SqlParameter("@openid", openid));
            if (dt.Rows.Count == 0)
            {
                sr.status = "Fail";
                sr.result = "未找到";
                return sr;
            }
            DataRow dr = dt.Rows[0];
            sr.status = "Success";
            sr.result = "成功";
            sr.data = new { name = DataTypeHelper.GetStringValue(dr["对应员工"]), phone = DataTypeHelper.GetStringValue(dr["员工联系方式"]) };
            return sr;
        }
    }
}