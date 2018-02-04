using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WanboServer.Models
{
    public class WatchResult
    {
        public int OfficeId { get; set; }//分类记录Id
        public string WorkDate { get; set; }//工作日期
        public int FrequencyId { get; set; }//班次Id
        public int PointId { get; set; }//巡更点Id
        public double arriveLa { get; set; }//到达经度
        public double arriveLo { get; set; }//到达纬度
        public string arriveTime { get; set; }//到达时间
        public int watchTimes { get; set; }//巡更序次
        public int usedTime { get; set; }//单次实际实长_分

    }
}