using System;
using System.Collections.Generic;
using System.Web;
using WanBoWebapp.model;
using WanBoWebapp.util;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace WanBoWebapp.operation
{
    public class WatchResultDal
    {
        public static StatusReport SetWatchResult(WatchResult result)
        {
            string sqlString = "insert into 基础_微信巡更 (当前组织ID,工作日期,班次ID,巡更点ID,到达经度,到达纬度,到达时间,巡更序次,单次实际时长_分)" +
                "select @当前组织ID,@工作日期,@班次ID,@巡更点ID,@到达经度,@到达纬度,@到达时间,@巡更序次,@单次实际时长_分";

            StatusReport sr = SqlHelper.Insert(sqlString,
                new SqlParameter("@当前组织ID", result.OfficeId),
                new SqlParameter("@工作日期", GetDBValue(result.WorkDate)),
                new SqlParameter("@班次ID", result.FrequencyId),
                new SqlParameter("@巡更点ID", result.PointId),
                new SqlParameter("@到达经度", result.arriveLa),
                new SqlParameter("@到达纬度", result.arriveLo),
                new SqlParameter("@到达时间", GetDBValue(result.arriveTime)),
                new SqlParameter("@巡更序次", result.watchTimes),
                new SqlParameter("@单次实际时长_分", result.usedTime)
                );
            if (sr.status == "Success")
            {
                int id = SqlHelper.ExecuteScalar("select max(ID) from 基础_微信巡更");
                sr.data = id.ToString();
            }
            sr.parameters = JsonConvert.SerializeObject(result);
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