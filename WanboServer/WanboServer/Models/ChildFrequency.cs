using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WanboServer.Models
{
    public class ChildFrequency
    {
        public int time { get; set; }
        public bool? isDone { get; set; }
        public Point[] route { get; set; }
        public string startTime { get; set; }
    }
}