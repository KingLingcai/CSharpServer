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
    public class WatchDal
    {
        public static StatusReport getWatchInfo(string officeId)
        {
            StatusReport sr = new StatusReport();
            Watch watch = new Watch();
            Frequency[] frequencies = null;
            Point[] points = null;
            sr = FrequencyDal.GetAllFrequency(officeId);
            if (sr.status == "Fail")
            {
                return sr;
            }
            else
            {
                frequencies = (Frequency[])sr.data;
                watch.frequencies = frequencies;
            }
            sr = PointDal.GetAllPoints(officeId);
            if (sr.status == "Fail")
            {
                return sr;
            }
            else
            {
                points = (Point[])sr.data;
                watch.route = points;
            }

 

            sr.data = watch;
            return sr;
        }
    }
}