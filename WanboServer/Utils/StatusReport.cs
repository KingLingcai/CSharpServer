using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexiUtils
{
    public class StatusReport
    {
        public string status { get; set; }//状态
        public string result { get; set; }//详情
        public object data { get; set; }//返回数据
        public object parameters { get; set; }//参数信息
    }
}
