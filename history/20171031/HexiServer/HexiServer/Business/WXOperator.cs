using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HexiServer.Models;
using HexiServer.Common;
using System.Collections.Specialized;
using System.Web.Helpers;
using System.Diagnostics;
using Newtonsoft.Json;

namespace HexiServer.Business
{
    public class WXOperator
    {
        /// <summary>
        /// 该方法使用code去微信服务器换取open-id等数据。
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static WXUser GetUser(string code)
        {
            string url =string.Format("https://api.weixin.qq.com/sns/jscode2session?appid={0}&secret={1}&js_code={2}&grant_type=authorization_code",CommonData.AppId,CommonData.AppSecret,code);
            StatusReport sr = RequestData.getRequest(url);
            if (sr.status == "success")
            {
                string result = sr.data;
                Debug.Write(result);
                JsonConverter jc = new JsonConverter();
                Debug.Write(JsonConvert.DeserializeObject(result));
                NameValueCollection nvc = JsonConvert.DeserializeObject(result);
                //object nvc = Json.Decode(result);
                //WXUser user = new WXUser()
                //{
                //    OpenId = nvc.Get("openid"),
                //    SessionKey = nvc.Get("session_key")
                //};
                return null;
            }
            else
            {
                return null;
            }
        }
    }
}