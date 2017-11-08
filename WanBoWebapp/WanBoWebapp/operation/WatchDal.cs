using System;
using System.Collections.Generic;
using System.Web;
using WanBoWebapp.model;

namespace WanBoWebapp.operation
{
    public class WatchDal
    {
        public static Watch getWatchInfo(int officeId)
        {
            Frequency[] frequencies = FrequencyDal.GetAllFrequency(officeId);
            Point[] points = PointDal.GetAllPoints(officeId);
            Watch w = new Watch();
            w.frequencies = frequencies;
            w.route = points;
            return w;
        }
    }
}