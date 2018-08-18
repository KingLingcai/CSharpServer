using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HexiServer.Models
{
    public class ComplainStatistics
    {
        public string name { get; set; }
        public string countReceive { get; set; }
        public string countValid { get; set; }
        public string countInvalid { get; set; }
        public string countFinished { get; set; }
        public string countUnfinished { get; set; }
        public string countClosed { get; set; }
        public string rateValid { get; set; }
        public string rateInvalid { get; set; }
        public string rateFinished { get; set; }
        public string rateUnfinished { get; set; }
        public string rateClosed { get; set; }
    }

    public class ComplainStatisticsProject
    {
        public string ztName { get; set; }
        public string countReceive { get; set; }
        public string countValid { get; set; }
        public string countInvalid { get; set; }
        public string countFinished { get; set; }
        public string countUnfinished { get; set; }
        public string countClosed { get; set; }
        public string rateValid { get; set; }
        public string rateInvalid { get; set; }
        public string rateFinished { get; set; }
        public string rateUnfinished { get; set; }
        public string rateClosed { get; set; }
        public ComplainStatistics[] complainStatisticsPersonal { get; set; }
    }

    public class ComplainStatisticsCompany
    {
        
        public string countReceive { get; set; }
        public string countValid { get; set; }
        public string countInvalid { get; set; }
        public string countFinished { get; set; }
        public string countUnfinished { get; set; }
        public string countClosed { get; set; }
        public string rateValid { get; set; }
        public string rateInvalid { get; set; }
        public string rateFinished { get; set; }
        public string rateUnfinished { get; set; }
        public string rateClosed { get; set; }
        public ComplainStatisticsProject[] complainStatisticsProject { get; set; }
    }
}