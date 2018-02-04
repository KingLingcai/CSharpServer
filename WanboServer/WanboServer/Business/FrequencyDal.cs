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
    public class FrequencyDal
    {
        public static StatusReport GetAllFrequency(string officeId)
        {
            StatusReport sr = new StatusReport();
            string sqlString = string.Format("select ID,班次名称,巡更次数,开始时间,结束时间,单次规定时长_分 from 基础_巡更设置_班次 where pid in (select MIN(ID) as ID from 基础_巡更设置 where 组织ID = {0})", officeId);
            DataTable dt = SQLHelper.ExecuteQuery("wyt",sqlString);
            if (dt.Rows.Count == 0)
            {
                sr.status = "Fail";
                sr.result = "未查询到任何数据";
                return null;
            }
            List<Frequency> frequencies = new List<Frequency>();
            foreach (DataRow row in dt.Rows)
            {
                Frequency f = new Frequency();
                f.frequencyId = DataTypeHelper.GetIntValue(row["ID"]);
                f.frequencyName = DataTypeHelper.GetStringValue(row["班次名称"]);
                f.times = DataTypeHelper.GetIntValue(row["巡更次数"]);
                f.startTime = DataTypeHelper.GetStringValue(row["开始时间"]);
                f.endTime = DataTypeHelper.GetStringValue(row["结束时间"]);
                f.minutes = DataTypeHelper.GetIntValue(row["单次规定时长_分"]);
                frequencies.Add(f);
            }
            Frequency[] frequencyArray = frequencies.ToArray();
            sr.status = "Success";
            sr.result = "成功";
            sr.data = frequencyArray;
            return sr;
        }
    }
}