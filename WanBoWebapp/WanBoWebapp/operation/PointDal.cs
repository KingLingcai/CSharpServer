using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using WanBoWebapp.model;
using WanBoWebapp.util;

namespace WanBoWebapp.operation
{
    public class PointDal
    {
        public static Point[] GetAllPoints(int officeId)
        {
            //officeId = 9;
            string sqlString = string.Format("select ID,位置名称,经度,纬度,排序编号 from 基础_巡更设置_巡更点 where pid in (select MIN(ID) as ID from 基础_巡更设置 where 组织ID = {0}) order by 排序编号", officeId);
            DataTable dt = SqlHelper.ExecuteQuery(sqlString);
            List<Point> points = new List<Point>();
            foreach (DataRow row in dt.Rows)
            {
                Point p = new Point();
                p.pointId = (int)row["ID"];
                p.locationName = (string)row["位置名称"];
                p.longitude = Convert.ToDouble(row["经度"]);
                p.latitude = Convert.ToDouble(row["纬度"]);
                p.no = (int)row["排序编号"];
                points.Add(p);
            }
            Point[] pointArray = points.ToArray();
            Point[] sortedArray = SortPoints(pointArray);
            return pointArray;
        }

        //该方法对数据库中取出的点，按no字段排序
        private static Point[] SortPoints(Point[] points)
        {

            for(int i = 0; i < points.Length - 1; i++)
            {
                for(int j = 0; j < points.Length - 1 - i; j++)
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
 //for (int i= 0; i<arr.Length - 1; i++)
 //           {
 //              // #region将大的数字移到数组的arr.Length-1-i
 //               for (int j= 0; j<arr.Length - 1 - i; j++)
 //               {
 //                   if (arr[j]> arr[j + 1])
 //                   {
 //                       temp = arr[j + 1];
 //                       arr[j + 1] = arr[j];
 //                       arr[j] = temp;
 //                   }
 //               }
 //           #endregion
 //           }