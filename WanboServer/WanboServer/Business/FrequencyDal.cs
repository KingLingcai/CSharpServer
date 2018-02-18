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
            //Frequency freq = frequencyArray[0];
            //if (Convert.ToInt32(freq.startTime.Split(':')[0]) > DateTime.Now.Hour)
            //{
            //    foreach (Frequency f in frequencyArray)
            //    {
            //        f.workDate = GetDate();
            //    }
            //}
            //else
            //{
            //    for (int i = 0; i < frequencyArray.Length; i++)
            //    {
            //        Frequency f = frequencyArray[i];
            //        int startHour = Convert.ToInt32(f.startTime.Split(':'));
            //        int endHour = Convert.ToInt32(f.endTime.Split(':'));
            //        if (startHour < endHour)
            //        {
            //            f.workDate = GetDate();
            //        }
            //        else
            //        {
            //            f.workDate = GetEarlyDate(1);
            //        }
            //    }
            //}

            sr.status = "Success";
            sr.result = "成功";
            sr.data = frequencyArray;
            return sr;
        }

        private static string GetDate()
        {
            return DateTime.Now.ToString("yyyy-MM-dd");
        }

        private static string GetEarlyDate(int day)
        {
            DateTime today = DateTime.Now;
            DateTime earlyDay = today.AddDays(-day);
            return earlyDay.ToString("yyyy-MM-dd");
        }

        private static int GetHour()
        {
            return DateTime.Now.Hour;
        }
    }
}




/**
 设置班次的工作日期
        如果第一个班次的开始时间大于当前时间，设置工作日期为当天
        if (parseInt(frequencies[0].startTime.split(":")[0]) > getHour()) {
          for (var i = 0; i < frequencies.length; i++) {
            frequencies[i].workDate = getDate();
          }
        }
        else {
          for (var i = 0; i < frequencies.length; i++) {
            var start = parseInt(frequencies[i].startTime.split(":")[0]);
            var end = parseInt(frequencies[i].endTime.split(":")[0]);
            //如果开始时间小于结束时间，设置工作日期为当天
            if (start < end) {
              frequencies[i].workDate = getDate();
            }
            //如果开始时间大于结束时间，设置工作日期为前一天
            if (start > end) {
              frequencies[i].workDate = getEarlyDate(1);
            }
          }
        }
*/