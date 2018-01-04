using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Net;
using System.Diagnostics;
using System.Collections.Specialized;

namespace HexiPublicCloudServer.Controllers
{
    public class SendDataController : Controller
    {
        [HttpPost]
        public string OnSendData()
        {
            WebRequest request = null;
            Stream requestStream = null;
            WebResponse response = null;
            Stream responseStream = null;
            StreamReader streamReader = null;
            NameValueCollection queryString = Request.QueryString;
            string qString = queryString.ToString();
            string url = Request["serverURL"];
            if (string.IsNullOrEmpty(url))
            {
                return "{\"status\": \"Fail\", \"result\": \"云服务器发生错误：" + "目标url未指定" + "\"}";
            }
            try
            {
                byte[] byteParam = System.Text.Encoding.UTF8.GetBytes(qString);
                request = HttpWebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded;charset=uft-8";
                request.ContentLength = byteParam.Length;

                //requestStream = request.GetRequestStream();
                //requestStream.Write(byteParam, 0, byteParam.Length);

                response = request.GetResponse();

                responseStream = response.GetResponseStream();

                streamReader = new StreamReader(responseStream);
                string responseString = streamReader.ReadToEnd();
                //Response.Write(responseString);
                return responseString;
            }
            catch (Exception exp)
            {
                Debug.Write(exp.Message);
                return "{\"status\": \"Fail\", \"result\": \"云服务器发生错误：" + exp.Message + "\"}";
            }
            finally
            {
                if (!(streamReader == null))
                {
                    streamReader.Close();
                }
                if (!(requestStream == null))
                {
                    requestStream.Close();
                }
                if (!(responseStream == null))
                {
                    responseStream.Close();
                }
            }
        }
    }
}