using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using HexiServer.Common;
using HexiServer.Models;

namespace HexiServer.Business
{
    public class ChargeDal
    {
        public static Charged[] GetChargedList(string homeNumber, string name, string ztcode, string startMonth, string endMonth)
        {
            string sqlString = "SELECT 房产单元编号, 占用者名称, SUM(应收金额) AS 已收总额, 帐套代码 " +
                                "FROM dbo.小程序_已收查询 " +
                                "WHERE 帐套代码 = " + ztcode +
                                (string.IsNullOrEmpty(homeNumber) ? "" : "and (房产单元编号 like '%" + homeNumber + "%') ") +
                                (string.IsNullOrEmpty(name) ? "" : "and (占用者名称 like '%" + name + "%') ") +
                                "GROUP BY 房产单元编号, 占用者名称, 帐套代码 " +
                                "ORDER BY 占用者名称";

            DataTable dt = SQLHelper.ExecuteQuery(sqlString);

            List<Charged> chargedList = new List<Charged>();

            foreach(DataRow row in dt.Rows)
            {
                Charged c = new Charged()
                {
                    RoomNumber = DataTypeHelper.GetStringValue(row["房产单元编号"]),
                    Name = DataTypeHelper.GetStringValue(row["占用者名称"]),
                    Total = DataTypeHelper.GetDoubleValue(row["已收总额"]),
                    ZTCode = DataTypeHelper.GetStringValue(row["帐套代码"]),
                };
                chargedList.Add(c);
            }
            return chargedList.ToArray();
        }

        public static ChargedResult[] GetChargedDetail(string homeNumber, string name, string ztcode, string startMonth, string endMonth)
        {
            string sqlString = "SELECT 应收金额, 计费年月, 付款方式, 费用名称, 计费开始年月, 计费截至年月 " +
                "FROM dbo.小程序_已收查询 " +
                "WHERE 房产单元编号 = @房产单元编号 and 占用者名称 = @占用者名称 and 帐套代码 = @帐套代码 " +
                "ORDER BY 计费年月 ";
            DataTable dt = SQLHelper.ExecuteQuery(sqlString,
                new SqlParameter("@房产单元编号", homeNumber),
                new SqlParameter("@占用者名称", name),
                new SqlParameter("@帐套代码", ztcode));
            List<ChargedDetail> cdList = new List<ChargedDetail>();
            foreach (DataRow row in dt.Rows)
            {
                ChargedDetail cd = new ChargedDetail()
                {
                    AmountReceivable = DataTypeHelper.GetDoubleValue(row["应收金额"]),
                    AmountMonth = string.IsNullOrEmpty(DataTypeHelper.GetStringValue(row["计费年月"])) ? "其他费用" : DataTypeHelper.GetStringValue(row["计费年月"]),
                    ChargeName = DataTypeHelper.GetStringValue(row["费用名称"]),
                    StartMonth = DataTypeHelper.GetStringValue(row["计费开始年月"]),
                    EndMonth = DataTypeHelper.GetStringValue(row["计费截至年月"])
                };
                cdList.Add(cd);
            }
            ChargedDetail[] cdArray = cdList.ToArray();
            if (cdArray.Length == 0)
            {
                return null;
            }
            string month = cdArray[0].AmountMonth;

            List<ChargedResult> resList = new List<ChargedResult>();

            int i = 0;
            while (i < cdArray.Length)
            {
                ChargedResult cr = new ChargedResult(); 
                cr.AmountMonth = month;
                List<ChargedDetail> list = new List<ChargedDetail>();

                for (int j = i; j < cdArray.Length; j++)
                {
                    if (cdArray[j].AmountMonth == month)
                    {
                        list.Add(cdArray[j]);
                        if (j == cdArray.Length - 1)
                        {
                            cr.Detail = list.ToArray();
                            i = cdArray.Length;
                            break;
                        }
                    }
                    else
                    {
                        cr.Detail = list.ToArray();
                        month = cdArray[j].AmountMonth;
                        i = j;
                        break;
                    }
                }
                resList.Add(cr);
            }
            
            return resList.ToArray();
        }

   

        //public static Charged[] GetChargedList(string homeNumber, string name, string ztcode, string startMonth, string endMonth)
        //{
        //    string sqlString = "SELECT   房产单元编号, 占用者名称, 应收金额, 计费年月, 费用名称, 计费开始年月, 计费截至年月, 帐套代码, 付款方式 " +
        //                        "FROM dbo.小程序_已收查询 " +
        //                        "WHERE 帐套代码 = " + ztcode +
        //                        (string.IsNullOrEmpty(homeNumber) ? "" : "and (房产单元编号 like '%" + homeNumber + "%') ") +
        //                        (string.IsNullOrEmpty(name) ? "" : "and (占用者名称 like '%" + name + "%') ");

                        //    DataTable dt = SQLHelper.ExecuteQuery(sqlString);

                        //    List<Charged> chargeList = new List<Charged>();
                        //    DataRow dr = null;
                        //    foreach (DataRow row in dt.Rows)
                        //    {
                        //        if (row != dr)
                        //        {
                        //            dr = row;
                        //        }

                        //    }
                        //}
                    }
                }