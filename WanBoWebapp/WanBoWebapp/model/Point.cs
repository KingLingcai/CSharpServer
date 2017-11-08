using System;
using System.Collections.Generic;
using System.Web;

namespace WanBoWebapp.model
{
    public class Point
    {
        public int pointId { get; set; }
        public double longitude { get; set; }
        public double latitude { get; set; }
        public string locationName { get; set; }
        public int no { get; set; } 

    }
}