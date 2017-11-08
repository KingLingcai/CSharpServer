using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using WanBoWebapp.util;
using WanBoWebapp.model;
using System.Diagnostics;

namespace WanBoWebapp.operation
{
    public class FrequencyDal
    {
        public static Frequency[] GetAllFrequency(int officeId)
        {
            Debug.WriteLine(officeId);
            //officeId = 9;
            string sqlString = string.Format("select ID,班次名称,巡更次数,开始时间,结束时间,单次规定时长_分 from 基础_巡更设置_班次 where pid in (select MIN(ID) as ID from 基础_巡更设置 where 分类记录ID = {0})", officeId);
            DataTable dt = SqlHelper.ExecuteQuery(sqlString);
            List<Frequency> frequencies = new List<Frequency>();
            foreach (DataRow row in dt.Rows)
            {
                Frequency f = new Frequency();
                f.frequencyId = (int)row["ID"];
                f.frequencyName = (string)row["班次名称"];
                f.times = (int)row["巡更次数"];
                f.startTime = (string)row["开始时间"];
                f.endTime = (string)row["结束时间"];
                f.minutes = (int)row["单次规定时长_分"];
                frequencies.Add(f);
            }
            Frequency[] frequencyArray = frequencies.ToArray();
            return frequencyArray;
        }
    }
}