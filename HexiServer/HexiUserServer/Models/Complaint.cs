using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HexiUserServer.Models
{
    public class Complaint
    {
        /// <summary>
        /// ID
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// 投诉接待日期
        /// </summary>
        public string ReceptionDate { get; set; }
        /// <summary>
        /// 投诉方式
        /// </summary>
        public string Way { get; set; }
        /// <summary>
        /// 投诉人姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 投诉内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public string Classify { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 投诉处理单编号
        /// </summary>
        public string Number { get; set; }
        /// <summary>
        /// 处理完成日期
        /// </summary>
        public string FinishDate { get; set; }
        /// <summary>
        /// 处理完成情况
        /// </summary>
        public string FinishStatus { get; set; }
        /// <summary>
        /// 登记人
        /// </summary>
        public string Registrant { get; set; }
        /// <summary>
        /// 责任部门
        /// </summary>
        public string Department { get; set; }
    }
}