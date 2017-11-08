using System;
using System.Collections.Generic;
using System.Web;

namespace WanBoWebapp.model
{
    public class StatusReport
    {
        public string status { get; set; }//状态
        public string result { get; set; }//详情
        public string data { get; set; }//返回数据
        public string parameters { get; set; }//参数信息
    }
}