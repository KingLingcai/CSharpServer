using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using WanBoWebapp.model;
using WanBoWebapp.util;

namespace WanBoWebapp.operation
{
    public class OfficeDal
    {
        public static Office[] GetAllOffice()
        {
            string sqlString = "select ID,名称 from 组织结构 where ID in (select distinct 组织ID from 基础_巡更设置)";
            DataTable dt = SqlHelper.ExecuteQuery(sqlString);
            List<Office> offices = new List<Office>();
            foreach (DataRow row in dt.Rows)
            {
                Office o = new Office();
                o.officeName = (string)row["名称"];
                o.officeId = Convert.ToInt32(row["ID"]);
                offices.Add(o);
            }
            Office[] officeArray = offices.ToArray();
            return officeArray;
        }

    }
}