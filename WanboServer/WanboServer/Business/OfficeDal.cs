using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using HexiUtils;
using WanboServer.Models;

namespace WanboServer.Business
{
    public class OfficeDal
    {
        public static StatusReport GetAllOffice()
        {
            StatusReport sr = new StatusReport();
            string sqlString = "select ID,名称 from 组织结构 where ID in (select distinct 组织ID from 基础_巡更设置)";
            DataTable dt = SQLHelper.ExecuteQuery("wyt",sqlString);
            if (dt.Rows.Count == 0)
            {
                sr.status = "Fail";
                sr.result = "未查询到任何数据";
                return null;
            }
            List<Office> offices = new List<Office>();
            foreach (DataRow row in dt.Rows)
            {
                Office o = new Office();
                o.officeName = DataTypeHelper.GetStringValue(row["名称"]);
                o.officeId = DataTypeHelper.GetIntValue(row["ID"]);
                offices.Add(o);
            }
            Office[] officeArray = offices.ToArray();
            sr.status = "Success";
            sr.result = "成功";
            sr.data = officeArray;
            return sr;
        }

    }
}