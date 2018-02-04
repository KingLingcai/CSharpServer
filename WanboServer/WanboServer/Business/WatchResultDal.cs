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
    public class WatchResultDal
    {
        public static StatusReport SetWatchResult(string officeId, string frequencyId, string pointId, string workDate, string watchTimes, string usedTime, string arriveLa, string arriveLo, string arriveTime)
        {
            StatusReport sr = new StatusReport();
            string sqlString = "insert into 基础_微信巡更 (当前组织ID,工作日期,班次ID,巡更点ID,到达经度,到达纬度,到达时间,巡更序次,单次实际时长_分)" +
                "select @当前组织ID,@工作日期,@班次ID,@巡更点ID,@到达经度,@到达纬度,@到达时间,@巡更序次,@单次实际时长_分";

            sr = SQLHelper.Insert("wyt",sqlString,
                new SqlParameter("@当前组织ID", officeId),
                new SqlParameter("@工作日期", GetDBValue(workDate)),
                new SqlParameter("@班次ID", frequencyId),
                new SqlParameter("@巡更点ID", pointId),
                new SqlParameter("@到达纬度", arriveLa),
                new SqlParameter("@到达经度", arriveLo),
                new SqlParameter("@到达时间", GetDBValue(arriveTime)),
                new SqlParameter("@巡更序次", watchTimes),
                new SqlParameter("@单次实际时长_分", usedTime)
                );
            if (sr.status == "Success")
            {
                int id = SQLHelper.ExecuteScalar("wyt","select max(ID) from 基础_微信巡更");
                sr.data = id.ToString();
            }
            //sr.parameters = JsonConvert.SerializeObject(result);
            return sr;
        }

        public static StatusReport SetWatchImage(string id, string func, string imagePath)
        {
            StatusReport sr = new StatusReport();
            string image = func == "1" ? "巡更图片1" : "巡更图片2";
            string sqlString = "update 基础_微信巡更 set "  + image + "= @巡更图片 where ID = @ID";
            sr = SQLHelper.Update("wyt", sqlString, new SqlParameter("@巡更图片", imagePath), new SqlParameter("@ID", id));
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