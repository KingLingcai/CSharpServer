using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WanboServer.Models
{
    public class Watch
    {
        public Point[] route { get; set; }
        public Frequency[] frequencies { get; set; }
    }
}