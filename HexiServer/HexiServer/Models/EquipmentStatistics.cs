using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HexiServer.Models
{
    public class EquipmentStatistics
    {
        public string ztName { get; set; }
        public string countFinished { get; set; }
        public string countUnfinished { get; set; }
        public string countShouldFinished { get; set; }
        public string rateFinished { get; set; }
        public string rateUnfinished { get; set; }
    }

    public class EquipmentStatisticsCompany
    {
        public string countFinished { get; set; }
        public string countUnfinished { get; set; }
        public string countShouldFinished { get; set; }
        public string rateFinished { get; set; }
        public string rateUnfinished { get; set; }

        public EquipmentStatistics[] equipmentStatisticsProjects { get; set; }
    }
}