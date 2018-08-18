using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HexiServer.Models
{
    public class RepairStatisticsPersonal
    {
        public string name { get; set; }
        public string ztName { get; set; }
        /// <summary>
        /// 接单数
        /// </summary>
        public string countReceive { get; set; }
        /// <summary>
        /// 完成数
        /// </summary>
        public string countFinished { get; set; }
        /// <summary>
        /// 未完成数
        /// </summary>
        public string countUnfinished { get; set; }
        /// <summary>
        /// 完成率
        /// </summary>
        public string rateFinish { get; set; }
        /// <summary>
        /// 未完成率
        /// </summary>
        public string rateUnfinish { get; set; }
        /// <summary>
        /// 评价总数
        /// </summary>
        public string countEvaluation { get; set; }
        /// <summary>
        /// 非常满意数
        /// </summary>
        public string countVerySatisfy { get; set; }
        /// <summary>
        /// 满意数
        /// </summary>
        public string countSatisfy { get; set; }
        /// <summary>
        /// 不满意数
        /// </summary>
        public string countUnsatisfy { get; set; }
        /// <summary>
        /// 非常满意率
        /// </summary>
        public string rateVerySatisfy { get; set; }
        /// <summary>
        /// 满意率
        /// </summary>
        public string rateSatisfy { get; set; }
        /// <summary>
        /// 不满意率
        /// </summary>
        public string rateUnsatisfy { get; set; }
    }

    public class RepairStatisticsProject
    {
        public RepairStatisticsProject()
        {
            this.countReceive = "0";
            this.countFinished = "0";
            this.countUnfinished = "0";
            this.countVerySatisfy = "0";
            this.countSatisfy = "0";
            this.countUnsatisfy = "0";
        }
        public string ztName { get; set; }
        /// <summary>
        /// 接单数
        /// </summary>
        public string countReceive { get; set; }
        /// <summary>
        /// 完成数
        /// </summary>
        public string countFinished { get; set; }
        /// <summary>
        /// 未完成数
        /// </summary>
        public string countUnfinished { get; set; }
        /// <summary>
        /// 完成率
        /// </summary>
        public string rateFinish { get; set; }
        /// <summary>
        /// 未完成率
        /// </summary>
        public string rateUnfinish { get; set; }
        /// <summary>
        /// 评价总数
        /// </summary>
        public string countEvaluation { get; set; }
        /// <summary>
        /// 非常满意数
        /// </summary>
        public string countVerySatisfy { get; set; }
        /// <summary>
        /// 满意数
        /// </summary>
        public string countSatisfy { get; set; }
        /// <summary>
        /// 不满意数
        /// </summary>
        public string countUnsatisfy { get; set; }
        /// <summary>
        /// 非常满意率
        /// </summary>
        public string rateVerySatisfy { get; set; }
        /// <summary>
        /// 满意率
        /// </summary>
        public string rateSatisfy { get; set; }
        /// <summary>
        /// 不满意率
        /// </summary>
        public string rateUnsatisfy { get; set; }

        public RepairStatisticsPersonal[] repairStatisticsPersonal { get; set; }
    }


    public class RepairStatisticsCompany
    {
        public string ztName { get; set; }
        /// <summary>
        /// 接单数
        /// </summary>
        public string countReceive { get; set; }
        /// <summary>
        /// 完成数
        /// </summary>
        public string countFinished { get; set; }
        /// <summary>
        /// 未完成数
        /// </summary>
        public string countUnfinished { get; set; }
        /// <summary>
        /// 完成率
        /// </summary>
        public string rateFinish { get; set; }
        /// <summary>
        /// 未完成率
        /// </summary>
        public string rateUnfinish { get; set; }
        /// <summary>
        /// 评价总数
        /// </summary>
        public string countEvaluation { get; set; }
        /// <summary>
        /// 非常满意数
        /// </summary>
        public string countVerySatisfy { get; set; }
        /// <summary>
        /// 满意数
        /// </summary>
        public string countSatisfy { get; set; }
        /// <summary>
        /// 不满意数
        /// </summary>
        public string countUnsatisfy { get; set; }
        /// <summary>
        /// 非常满意率
        /// </summary>
        public string rateVerySatisfy { get; set; }
        /// <summary>
        /// 满意率
        /// </summary>
        public string rateSatisfy { get; set; }
        /// <summary>
        /// 不满意率
        /// </summary>
        public string rateUnsatisfy { get; set; }

        public RepairStatisticsProject[] repairStatisticsProject { get; set; }
    }
}