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
    public class PointDal
    {
        public static StatusReport GetAllPoints(string officeId)
        {
            StatusReport sr = new StatusReport();
            string sqlString = string.Format("select ID,位置名称,经度,纬度,排序编号 from 基础_巡更设置_巡更点 where pid in (select MIN(ID) as ID from 基础_巡更设置 where 组织ID = {0}) order by 排序编号", officeId);
            DataTable dt = SQLHelper.ExecuteQuery("wyt",sqlString);
            if (dt.Rows.Count == 0)
            {
                sr.status = "Fail";
                sr.result = "未查询到任何数据";
                return null;
            }
            List<Point> points = new List<Point>();
            foreach (DataRow row in dt.Rows)
            {
                Point p = new Point();
                p.pointId = DataTypeHelper.GetIntValue(row["ID"]);
                p.locationName = DataTypeHelper.GetStringValue(row["位置名称"]);
                p.longitude = DataTypeHelper.GetDoubleValue(row["经度"]);
                p.latitude = DataTypeHelper.GetDoubleValue(row["纬度"]);
                p.no = DataTypeHelper.GetIntValue(row["排序编号"]);
                p.isScan = false;
                points.Add(p);
            }
            Point[] pointArray = points.ToArray();
            //Point[] sortedArray = SortPoints(pointArray);
            sr.status = "Success";
            sr.result = "成功";
            sr.data = pointArray;
            return sr;
        }

        //该方法对数据库中取出的点，按no字段排序
        private static Point[] SortPoints(Point[] points)
        {

            for (int i = 0; i < points.Length - 1; i++)
            {
                for (int j = 0; j < points.Length - 1 - i; j++)
                {
                    Point pj = points[j];
                    Point pj1 = points[j + 1];
                    if (pj.no > pj1.no)
                    {
                        Point temp = pj;
                        pj = pj1;
                        pj1 = temp;
                    }
                }
            }
            return points;
        }
    }
}