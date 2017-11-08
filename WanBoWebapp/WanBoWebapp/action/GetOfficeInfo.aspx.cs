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
    public partial class GetOfficeInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Office[] offices = OfficeDal.GetAllOffice();
            string jsonString = JsonConvert.SerializeObject(offices);
            Response.Write(jsonString);
        }
    }
}