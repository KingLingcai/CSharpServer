using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HexiServer.Models
{
    public class RepairReportProject
    {
        public string type { get; set; }
        public Repair[] repairs { get; set; }
    }

    public class RepairReportCompany
    {
        public string type { get; set; }
        public Repair[] repairs { get; set; }
    }




}