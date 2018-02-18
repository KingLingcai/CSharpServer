using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WanboServer.Models
{
    public class Point
    {
        public int? pointId { get; set; }
        public double? longitude { get; set; }
        public double? latitude { get; set; }
        public string locationName { get; set; }
        public int? no { get; set; }
        public bool? isScan { get; set; }
    }
}