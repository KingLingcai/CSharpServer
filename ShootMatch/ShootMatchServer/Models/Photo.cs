using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace ShootMatchServer.Models
{
    public class Photo
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string PhotoName { get; set; }
        public string PhotoPath { get; set; }
        public DateTime UploadDate { get; set; }

        public Photo (NameValueCollection nvc)
        {
            Name = nvc.Get("name");
            PhoneNumber = nvc.Get("phoneNumber");
            PhotoName = nvc.Get("photoName");
            UploadDate = DateTime.Now;
        }
    }
}