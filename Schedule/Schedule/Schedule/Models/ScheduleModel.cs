using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Schedule.Models
{
    public class ScheduleModel
    {
        /// <summary>
        /// 班级，BJ
        /// </summary>
        public string className { get; set; }
        /// <summary>
        /// 上课时间，CouTime
        /// </summary>
        public string courseTime { get; set; }
        /// <summary>
        /// 课程名，CouKC
        /// </summary>
        public string course { get; set; }
        /// <summary>
        /// 地址，CouAddr
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// 教室，CouJS
        /// </summary>
        public string classroom { get; set; }
        /// <summary>
        /// 颜色
        /// </summary>
        public string color { get; set; }
    }
}