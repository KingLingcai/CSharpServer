using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using HexiUtils;
using Schedule.Models;

namespace Schedule.Business
{
    public class ScheduleDal
    {
        public static StatusReport SearchSchedule(string name)
        {
            string sqlString = " select BJ,CouTime,CouKC,CouAddr,CouJS from tb_BaseCourse " +
                                " where BJ in (select BJ from tb_BaseStudent where stuName + stuXH = @value) ";
            //string sqlString = " select BJ,CouTime,CouKC,CouAddr,CouJS from tb_BaseCourse ";
            //string sqlString = " select * f"
            DataTable dt = SQLHelper.ExecuteQuery(sqlString, new SqlParameter("@value", name));
            if(dt.Rows.Count == 0)
            {
                return new StatusReport().SetFail();
            }
            else
            {
                List<ScheduleModel> schedules = new List<ScheduleModel>();
                foreach (DataRow dr in dt.Rows) 
                {
                    ScheduleModel model = new ScheduleModel()
                    {
                        className = DataTypeHelper.GetStringValue(dr["BJ"]),
                        courseTime = DataTypeHelper.GetStringValue(dr["CouTime"]),
                        course = DataTypeHelper.GetStringValue(dr["CouKC"]),
                        address = DataTypeHelper.GetStringValue(dr["CouAddr"]),
                        classroom = DataTypeHelper.GetStringValue(dr["CouJS"])
                    };
                    switch (model.course)
                    {
                        case "数学":
                            model.color = "math";
                            break;
                        case "语文":
                            model.color = "chinese";
                            break;
                        case "英语":
                            model.color = "english";
                            break;
                        default:
                            model.color = "other";
                            break;
                    }
                    schedules.Add(model);
                }
                return new StatusReport(schedules.ToArray());
            }
        }
    }
}