using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Net;
using System.Diagnostics;
using System.Collections.Specialized;
using System.Net.Http;

namespace HexiInnerCloudServer.Controllers
{
    public class SendDataController : Controller
    {
        [HttpPost]
        public string OnSendData()
        {
            NameValueCollection parameters = Request.Params;
            Debug.WriteLine(parameters.ToString());
            string url = Request["serverURL"];
            if (string.IsNullOrEmpty(url))
            {
                return "{\"status\": \"Fail\", \"result\": \"云服务器发生错误：" + "目标url未指定" + "\"}";
            }

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Clear();
            client.BaseAddress = new Uri(url);
            HttpRequestMessage requestMessage = new HttpRequestMessage();
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            requestMessage.Headers.Clear();
            requestMessage.Headers.Add("ContentType", Request.ContentType);
            requestMessage.Method = HttpMethod.Post;
            requestMessage.Content = new FormUrlEncodedContent(ToDictionary(parameters));
            
            responseMessage = client.SendAsync(requestMessage).Result;
            return responseMessage.Content.ReadAsStringAsync().Result;
            
        }

        private static Dictionary<string, string> ToDictionary(NameValueCollection nvc)
        {
            string[] keys = nvc.AllKeys;
            Dictionary<string, string> resDic = new Dictionary<string, string>();
            foreach (string key in keys)
            {
                resDic.Add(key, nvc[key]);
            }
            return resDic;
        }
    }
}


