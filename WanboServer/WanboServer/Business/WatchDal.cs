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
        public static StatusReport getWatchInfo(string officeId,string workDate)
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
                //watch.frequencies = frequencies;
            }
            sr = PointDal.GetAllPoints(officeId);
            if (sr.status == "Fail")
            {
                return sr;
            }
            else
            {
                points = (Point[])sr.data;
                //watch.route = points;
            }
            

            List<Frequency> fList = new List<Frequency>();
            for (int i = 0; i < frequencies.Length; i++)
            {
                List<ChildFrequency> cfList = new List<ChildFrequency>();
                for (int j = 0; j < frequencies[i].times; j++)
                {
                    ChildFrequency cf = new ChildFrequency()
                    {
                        isDone = false,
                        //route = (Point[])points.Reverse<Point>().Reverse<Point>(),
                        //route = new Point[points.Length],
                        //route = (Point[])points.Clone(),
                        //route = points.Reverse<Point>();
                        time = j + 1,
                        startTime = ""
                    };
                    List<Point> pl = new List<Point>();
                    for (int k = 0; k < points.Length; k++)
                    {
                        Point p = new Point();
                        p.pointId = points[k].pointId;
                        p.locationName = points[k].locationName;
                        p.longitude = points[k].longitude;
                        p.latitude = points[k].latitude;
                        p.no = points[k].no;
                        p.isScan = points[k].isScan;
                        pl.Add(p);
                    }
                    cf.route = pl.ToArray();
                    cfList.Add(cf);
                }
                frequencies[i].cFreq = cfList.ToArray();
            }
            watch.frequencies = frequencies;


            sr = WatchResultDal.GetWatchInfo(officeId, workDate);
            if (sr.status == "Success")
            {
                WatchInfo[] wis = (WatchInfo[])sr.data;
                for(int i = 0; i < wis.Length; i++)
                {
                    int? frequencyId = wis[i].frequencyId;
                    int? pointId = wis[i].pointId;
                    int? time = wis[i].watchTimes;
                    string startTime = wis[i].startTime;
                    for(int j = 0; j < watch.frequencies.Length; j++)
                    {
                        for(int k = 0; k < watch.frequencies[j].cFreq.Length; k++)
                        {
                            for(int l = 0; l < watch.frequencies[j].cFreq[k].route.Length; l++)
                            {
                                if (watch.frequencies[j].frequencyId == frequencyId &&
                                    watch.frequencies[j].cFreq[k].time == time &&
                                    watch.frequencies[j].cFreq[k].route[l].pointId == pointId)
                                {
                                    watch.frequencies[j].cFreq[k].route[l].isScan = true;
                                    watch.frequencies[j].cFreq[k].startTime = startTime;
                                }
                                
                            }
                        }
                    }
                }

                bool totalDone = true;
                for (int i = 0; i < watch.frequencies.Length; i++)
                {
                    //bool[] cFreqIsDone = new bool[watch.frequencies[i].cFreq.Length];
                    //bool isDone = true;
                    watch.frequencies[i].isDone = false;
                    bool freqIsDone = true;
                    for (int j = 0; j < watch.frequencies[i].cFreq.Length; j++)
                    {
                        watch.frequencies[i].cFreq[j].isDone = false;
                        bool cFreqIsDone = true;
                        for (int k = 0; k < watch.frequencies[i].cFreq[j].route.Length; k++)
                        {
                            if (watch.frequencies[i].cFreq[j].route[k].isScan == false)
                            {
                                cFreqIsDone = false;
                            }
                        }
                        if (cFreqIsDone == true)
                        {
                            watch.frequencies[i].cFreq[j].isDone = true;
                        }
                        if (watch.frequencies[i].cFreq[j].isDone == false)
                        {
                            freqIsDone = false;
                        }
                    }
                    if (freqIsDone == true )
                    {
                        watch.frequencies[i].isDone = true ;
                        //break;
                    }
                    if (watch.frequencies[i].isDone == false)
                    {
                        totalDone = false;
                    }
                }
                if (totalDone == true)
                {
                    watch.isDone = true;
                }
            }


            sr.status = "Success";
            sr.result = "成功获取到巡更信息";
            sr.data = watch;
            return sr;
        }
    }
}




/**
 * public static StatusReport getWatchInfo(string officeId,string workDate)
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

            sr = WatchResultDal.GetWatchInfo(officeId, workDate);
            if (sr.status == "Fail")
            {

            }
            else
            {

            }
            

            

            sr.data = watch;
            return sr;
        }
*/