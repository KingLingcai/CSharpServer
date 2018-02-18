using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WanboServer.Models
{
    public class WatchInfo
    {
        public int? frequencyId { get; set; }//班次Id
        public int? pointId { get; set; }//巡更点Id
        public int? watchTimes { get; set; }//巡更序次
        public string startTime { get; set; }//开始时间
    }
}