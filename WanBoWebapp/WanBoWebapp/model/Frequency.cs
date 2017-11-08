using System;
using System.Collections.Generic;
using System.Web;

namespace WanBoWebapp.model
{
    public class Frequency
    {
        public int frequencyId { get; set; }
        public string frequencyName { get; set; }
        public int times { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
        public int minutes { get; set; }//单次规定时长
    }
}