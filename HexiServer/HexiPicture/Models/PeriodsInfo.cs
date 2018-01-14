using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HexiPicture.Models
{
    public class PeriodsInfo
    {
        /// <summary>
        /// ID
        /// </summary>
        public int? id { get; set; }
        /// <summary>
        /// 期数
        /// </summary>
        public string periods { get; set; }
        /// <summary>
        /// 主题
        /// </summary>
        public string theme { get; set; }
        /// <summary>
        /// 主题描述
        /// </summary>
        public string themeContent { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public string startTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string endTime { get; set; }

    }
}