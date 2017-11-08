using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShootMatchServer.Models;
using ShootMatchServer.Common;
using System.Data.SqlClient;

namespace ShootMatchServer.Business
{
    public class PhotoDal
    {
        public static StatusReport InsertPhotoInfo (Photo photo)
        {
            string sqlString = "Insert into ** (昵称,联系方式,图片名称,上传日期,图片路径) " +
                               "select (@昵称,@联系方式,@图片名称,@上传日期,@图片路径)";
            StatusReport sr = SqlHelper.Insert(sqlString,
                new SqlParameter("@昵称",photo.Name),
                new SqlParameter("@联系方式",photo.PhoneNumber),
                new SqlParameter("@图片名称", photo.PhotoName),
                new SqlParameter("@上传日期", photo.UploadDate),
                new SqlParameter("@图片路径", photo.PhotoPath)
                );
            return sr;
        }
    }
}