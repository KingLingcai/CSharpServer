using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WanBoWebapp.model;
using WanBoWebapp.operation;
using Newtonsoft.Json;

namespace WanBoWebapp.action
{
    public partial class GetWatchInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int officeId = -1;
            officeId = Convert.ToInt32(Request["officeId"]);
            Watch w = WatchDal.getWatchInfo(officeId);
            string jsonString = JsonConvert.SerializeObject(w);
            Response.Write(jsonString);
        }
    }
}