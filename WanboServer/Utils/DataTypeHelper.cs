﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HexiUtils
{
    public class DataTypeHelper
    {
        /// <summary>
        /// 将数据库中取出的数据转换为string类型
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public static string GetStringValue(object row)
        {
            if (row == DBNull.Value)
            {
                return "";
            }
            else
            {
                return (string)row;
            }
        }

        /// <summary>
        /// 将数据库中取出的数据转换为DateTime类型
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public static DateTime? GetDateValue(object row)
        {
            if (row == DBNull.Value)
            {
                return null;
            }
            else
            {
                return (DateTime)row;
            }
        }

        /// <summary>
        /// 将数据库中取出的DateTime类型数据转换为string类型
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public static string GetDateStringValue(object row)
        {
            if (row == DBNull.Value)
            {
                return "";
            }
            else
            {
                DateTime dt = (DateTime)row;
                string dstring = dt.ToString("yyyy-MM-dd HH:mm:ss");
                return dstring;
            }
        }

        /// <summary>
        /// 将数据库中取出的数据转换为double?类型
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public static double? GetDoubleValue(object row)
        {
            if (row == DBNull.Value)
            {
                return null;
            }
            else
            {
                return Convert.ToDouble(row);
            }
        }

        /// <summary>
        /// 将数据库中取出的数据转换为int?类型
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public static int? GetIntValue(object row)
        {
            if (row == DBNull.Value)
            {
                return null;
            }
            else
            {
                return Convert.ToInt32(row);
            }
        }

        /// <summary>
        /// 将数据库中取出的数据转换为int?类型
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public static bool? GetBooleanValue(object row)
        {
            if (row == DBNull.Value)
            {
                return null;
            }
            else
            {
                return Convert.ToBoolean(row);
            }
        }
    }
}