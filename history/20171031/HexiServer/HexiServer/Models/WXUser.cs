using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HexiServer.Models
{
    public class WXUser
    {
        //public int Id { get; set; }//id
        public string openid { get; set; }//微信小程序用户的open-id
        public string session_key { get; set; }
        public string unionid { get; set; }
    }
}