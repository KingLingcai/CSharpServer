using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HexiServer.Models
{
    public class Decoration
    {
        /// <summary>
        /// ID
        /// </summary>
        public int? id { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public string classify { get; set; }
        /// <summary>
        /// 房产编号
        /// </summary>
        public string roomNumber { get; set; }
        /// <summary>
        /// 业主姓名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 业主电话
        /// </summary>
        public string phone { get; set; }
        /// <summary>
        /// 客服专员
        /// </summary>
        public string attache { get; set; }
        /// <summary>
        /// 装修类型
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 施工队
        /// </summary>
        public string constructionTeam { get; set; }
        /// <summary>
        /// 施工队负责人
        /// </summary>
        public string teamLeader { get; set; }
        /// <summary>
        /// 施工队负责人电话
        /// </summary>
        public string teamLeaderPhone { get; set; }
        /// <summary>
        /// 施工队成员
        /// </summary>
        public TeamMember[] teamMembers { get; set; }
        /// <summary>
        /// 装修施工证编号
        /// </summary>
        public string certificateNumber { get; set; }
        /// <summary>
        /// 办理时间
        /// </summary>
        public string transactTime { get; set; }
        /// <summary>
        /// 开工时间
        /// </summary>
        public string startTime { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// 装修内容
        /// </summary>
        public DecorationContent[] contents { get; set; }
        /// <summary>
        /// 是否封阳台
        /// </summary>
        public string needSealingBalcony { get; set; }
        /// <summary>
        /// 验收时间
        /// </summary>
        public string checkTime { get; set; }
        /// <summary>
        /// 验收结果
        /// </summary>
        public string checkResult { get; set; }
        /// <summary>
        /// 验收结果说明
        /// </summary>
        public string checkResultExplain { get; set; }

    }

    public class TeamMember
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string phone { get; set; }
        /// <summary>
        /// 身份证号码
        /// </summary>
        public string idCard { get; set; }
    }

    public class DecorationContent
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string number { get; set; }
        /// <summary>
        /// 装修内容
        /// </summary>
        public string content { get; set; }
    }
}