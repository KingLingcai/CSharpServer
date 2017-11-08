using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using System.IO;
using System.Data.SqlClient;
using WanBoWebapp.util;
using WanBoWebapp.model;
using Newtonsoft.Json;

namespace WanBoWebapp.action
{
    public partial class SetWatchImages : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string id = Request["id"];
            string time = Request["time"];
            string mainPath = "D:\\WYYTNET\\wb iis\\wb iis\\wximages\\";
            string sqlMainPath = "\\wximages\\";
            //string mainPath = "\\wximages\\";
            //string mainPath = "D:\\WanboServer\\";
            string imagePath1 = "";
            string sqlImagePath1 = "";
            string imagePath2 = "";
            string sqlImagePath2 = "";
            StatusReport sr = new StatusReport();
            //string imagePath2 = "";

            HttpPostedFile uploadFile1 = Request.Files["image1"];
            HttpPostedFile uploadFile2 = Request.Files["image2"];

            int imageLength1 = 0;
            int imageLength2 = 0;
            if (!(uploadFile1 == null))
            {
                imageLength1 = uploadFile1.ContentLength;
            }
            if (!(uploadFile2 == null))
            {
                imageLength2 = uploadFile2.ContentLength;
            }
            if (imageLength1 > 0)
            {

                string contentType = uploadFile1.ContentType;
                if (contentType == "image/png")
                {
                    imagePath1 = mainPath + "image1" + id + DateTime.Now.Ticks.ToString() + ".png";
                    sqlImagePath1 = sqlMainPath + "image1" + id + DateTime.Now.Ticks.ToString() + ".png";
                }
                else if (contentType == "image/jpeg" || contentType == "image/jpg")
                {
                    imagePath1 = mainPath + "image1" + id + DateTime.Now.Ticks.ToString() + ".jpg";
                    sqlImagePath1 = sqlMainPath + "image1" + id + DateTime.Now.Ticks.ToString() + ".jpg";
                }
                uploadFile1.SaveAs(imagePath1);
                string sqlString = "update 基础_微信巡更 set 巡更图片1 = @巡更图片1 where ID = @ID";
                sr = SqlHelper.Update(sqlString,
                    new SqlParameter("@巡更图片1", sqlImagePath1),
                    new SqlParameter("@ID", Convert.ToInt32(id))
                    );
                sr.data = imagePath1 + "," + id;
            }
            if (imageLength2 > 0)
            {

                string contentType = uploadFile2.ContentType;
                if (contentType == "image/png")
                {
                    imagePath2 = mainPath + "image2" + id + DateTime.Now.Ticks.ToString() + ".png";
                    sqlImagePath2 = sqlMainPath + "image2" + id + DateTime.Now.Ticks.ToString() + ".png";
                }
                else if (contentType == "image/jpeg" || contentType == "image/jpg")
                {
                    imagePath2 = mainPath + "image2" + id + DateTime.Now.Ticks.ToString() + ".jpg";
                    sqlImagePath2 = sqlMainPath + "image2" + id + DateTime.Now.Ticks.ToString() + ".jpg";
                }
                uploadFile2.SaveAs(imagePath2);
                string sqlString = "update 基础_微信巡更 set 巡更图片2 = @巡更图片2 where ID = @ID";
                sr = SqlHelper.Update(sqlString,
                    new SqlParameter("@巡更图片2", sqlImagePath2),
                    new SqlParameter("@ID", Convert.ToInt32(id))
                    );
                sr.data = imagePath2 + "," + id;
            }
            //sr.data = contentType;
            string srString = JsonConvert.SerializeObject(sr);
            Response.Write(srString);
            //Response.Write(imagePath1 + "," + imagePath2 + "," + id);

        
        }
    }
}