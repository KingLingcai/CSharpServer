using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SongyuanCloudServer.Models
{
    //public class ShareInfoIntro
    //{
    //    /// <summary>
    //    /// 分享单编号
    //    /// </summary>
    //    public string shareNumber { get; set; }
    //    /// <summary>
    //    /// 分享次数
    //    /// </summary>
    //    public int? shareTimes { get; set; }
    //    /// <summary>
    //    /// 层数
    //    /// </summary>
    //    public int? floors { get; set; }
    //}


    public class ShareInfoIntro
    {
        public int? id { get; set; }
        /// <summary>
        /// 分享单编号
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 分享次数
        /// </summary>
        public int? shareTimes { get; set; }
        /// <summary>
        /// 层数
        /// </summary>
        public int? signUpTimes { get; set; }

        public int? payTimes { get; set; }
    }


    public class shareDetailTemp
    {
        public string shareID { get; set; }
        public string receiverID { get; set; }
        public string receiverName { get; set; }
        public string receiverPhone { get; set; }
        public string receiverNickName { get; set; }
        public string receiverGender { get; set; }
        public int? signupTimes { get; set; }
    }

    public class signupDetail
    {
        public string name { get; set; }
        public string phone { get; set; }
        public string relateName { get; set; }
        public string relation { get; set; }
        public string signuptime { get; set; }
        public string isCharge { get; set; }
    }

    //public class shareDetailTemp
    //{
    //    public string shareID { get; set; }
    //    public string shareName { get; set; }
    //    public string sharePhone { get; set; }
    //    public string shareNickName { get; set; }
    //    public string shareGender { get; set; }
    //    public string receiverID { get; set; }
    //    public string receiverName { get; set; }
    //    public string receiverPhone { get; set; }
    //    public string receiverNickName { get; set; }
    //    public string receiverGender { get; set; }
    //    public string floor { get; set; }
    //}

    public class shareDetail
    {
        //public shareDetail[] childen { get; set; }
        public string shareID { get; set; }
        public string shareName { get; set; }
        public string sharePhone { get; set; }
        public string shareNickName { get; set; }
        public string shareGender { get; set; }
        public string receiverID { get; set; }
        public string receiverName { get; set; }
        public string receiverPhone { get; set; }
        public string receiverNickName { get; set; }
        public string receiverGender { get; set; }
    }

    public class sharePerson
    {
        public string floor { get; set; }
        public string ID { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string nickName { get; set; }
        public string gender { get; set; }
        public string receiverID { get; set; }
        public List<sharePerson> children { get; set; }//下一层的人
        public sharePerson(string floor, string ID, string name, string phone, string nickName, string gender)
        {
            this.floor = floor;
            this.ID = ID;
            this.name = name;
            this.phone = phone;
            this.nickName = nickName;
            this.gender = gender;
            //this.receiverID = receiverID;
            this.children = new List<sharePerson>();
        }
        public sharePerson()
        {

        }
        //public void Find (shareDetailTemp temp)
        //{
        //    if (temp.floor == (Convert.ToInt32(this.floor) + 1).ToString() && temp.shareID == this.ID)
        //    {
        //        this.children.Add(new sharePerson(temp.floor, temp.receiverID, temp.receiverName, temp.receiverPhone, temp.receiverNickName, temp.receiverGender));
        //    }
        //    else
        //    {
        //        if (!(this.children == null))
        //        {
        //            foreach (sharePerson my in this.children)
        //            {
        //                my.Find(temp);
        //            }
        //        }
        //    }
        //}
    }

    //    public class ShareReturndata
    //    {
    //        public string floor { get; set; }
    //        public sharePerson detail { get; set; }
    //        public 
    //    }
}