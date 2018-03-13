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
        public static StatusReport SetWatchResult(string officeId, string frequencyId, string pointId, string workDate, string watchTimes, string usedTime, string arriveLa, string arriveLo, string arriveTime,string startTime)
        {
            StatusReport sr = new StatusReport();
            string sqlString = " if not exists(select * from 基础_微信巡更 where 当前组织ID = @当前组织ID and 工作日期 = @工作日期 and 班次ID = @班次ID and 巡更点ID = @巡更点ID and 巡更序次 = @巡更序次) " +
                " insert into 基础_微信巡更 (当前组织ID,工作日期,班次ID,巡更点ID,到达经度,到达纬度,到达时间,巡更序次,单次实际时长_分,开始时间 )" +
                " select @当前组织ID,@工作日期,@班次ID,@巡更点ID,@到达经度,@到达纬度,@到达时间,@巡更序次,@单次实际时长_分,@开始时间 " +
                " select @@identity ";

            sr = SQLHelper.Insert("wyt",sqlString,
                new SqlParameter("@当前组织ID", officeId),
                new SqlParameter("@工作日期", GetDBValue(workDate)),
                new SqlParameter("@班次ID", frequencyId),
                new SqlParameter("@巡更点ID", pointId),
                new SqlParameter("@到达纬度", arriveLa),
                new SqlParameter("@到达经度", arriveLo),
                new SqlParameter("@到达时间", GetDBValue(arriveTime)),
                new SqlParameter("@巡更序次", watchTimes),
                new SqlParameter("@单次实际时长_分", usedTime),
                new SqlParameter("@开始时间", startTime)
                );
            if (sr.status == "Success")
            {
                string id = (string)sr.data;
                //int id = SQLHelper.ExecuteScalar("wyt","select max(ID) from 基础_微信巡更");
                //sr.data = id.ToString();
                sr = WatchDal.getWatchInfo(officeId,workDate);
                sr.parameters = id;
            }
            //sr.parameters = JsonConvert.SerializeObject(result);
            return sr;
        }

        public static StatusReport GetWatchInfo(string officeId, string date)
        {
            StatusReport sr = new StatusReport();
            string sqlString = "select 班次ID,巡更点ID,巡更序次,开始时间 from 基础_微信巡更 " +
                " where 当前组织ID = @当前组织ID and 工作日期 = @工作日期 order by ID desc";
            DataTable dt = SQLHelper.ExecuteQuery("wyt", sqlString, new SqlParameter("@当前组织ID", officeId),
                                                                   new SqlParameter("@工作日期", date));
            if (dt.Rows.Count == 0)
            {
                sr.status = "Fail";
                sr.result = "未查询到任何数据";
                return sr;
            }
            List<WatchInfo> watchInfoList = new List<WatchInfo>();
            foreach(DataRow dr in dt.Rows)
            {
                WatchInfo watchInfo = new WatchInfo();
                watchInfo.frequencyId = DataTypeHelper.GetIntValue(dr["班次ID"]);
                watchInfo.pointId = DataTypeHelper.GetIntValue(dr["巡更点ID"]);
                watchInfo.watchTimes = DataTypeHelper.GetIntValue(dr["巡更序次"]);
                watchInfo.startTime = DataTypeHelper.GetDateStringValue(dr["开始时间"]);
                watchInfoList.Add(watchInfo);
            }
            sr.status = "Success";
            sr.result = "成功";
            sr.data = watchInfoList.ToArray();
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