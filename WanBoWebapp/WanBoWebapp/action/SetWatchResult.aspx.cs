using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WanBoWebapp.model;
using WanBoWebapp.operation;
using Newtonsoft.Json;

namespace WanBoWebapp.action
{
    public partial class SetWatchResult : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            WatchResult wr = new WatchResult()
            {
                OfficeId = Convert.ToInt32(Request["officeId"]),//管理处Id
                FrequencyId = Convert.ToInt32(Request["frequencyId"]),//班次Id
                PointId = Convert.ToInt32(Request["pointId"]),//巡更点Id
                WorkDate = Request["workDate"],//工作日期
                watchTimes = Convert.ToInt32(Request["watchTimes"]),//当次序次
                usedTime = Convert.ToInt32(Request["usedTime"]),//已用时间
                arriveLa = Convert.ToDouble(Request["arriveLa"]),//扫码时经度
                arriveLo = Convert.ToDouble(Request["arriveLo"]),//扫码时纬度
                arriveTime = Request["arriveTime"]//到达时间
            };
            StatusReport sr =  WatchResultDal.SetWatchResult(wr);
            string srString = JsonConvert.SerializeObject(sr);
            Response.Write(srString);
        }

        private Object GetDBValue(string value)
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