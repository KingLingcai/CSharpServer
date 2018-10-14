using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using HexiServer.Common;
using HexiServer.Models;
using HexiUtils;

namespace HexiServer.Business
{
    public class ChargeDal
    {
        public static StatusReport GetChargedList(string homeNumber, string buildingNumber, string name, string ztcode, string startMonth, string endMonth)
        {
            StatusReport sr = new StatusReport();
            string sqlString = "SELECT 房产单元编号, 占用者名称, SUM(应收金额) AS 已收总额, 帐套代码 " +
                                "FROM dbo.小程序_已收查询 " +
                                "WHERE 帐套代码 = " + ztcode +
                                (string.IsNullOrEmpty(homeNumber) ? "" : "and (房产单元编号 like '%" + homeNumber + "%') ") +
                                (string.IsNullOrEmpty(buildingNumber) ? "" : "and (所属楼宇 like '%" + buildingNumber + "%') ") +
                                (string.IsNullOrEmpty(name) ? "" : "and (占用者名称 like '%" + name + "%') ") +
                                " and 计费开始年月 >= " + startMonth +
                                " and 计费开始年月 <= " + endMonth +
                                " GROUP BY 房产单元编号, 占用者名称, 帐套代码 " +
                                " ORDER BY 占用者名称 ";

            DataTable dt = SQLHelper.ExecuteQuery("wyt", sqlString);
            if (dt.Rows.Count == 0)
            {
                sr.status = "Fail";
                sr.result = "未查询到任何记录";
                return sr;
            }
            List<Charged> chargedList = new List<Charged>();

            foreach (DataRow row in dt.Rows)
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
            sr.status = "Success";
            sr.result = "成功";
            sr.data = chargedList.ToArray();
            return sr;
        }

        public static StatusReport GetChargeList(string homeNumber, string name, string ztcode, string buildingNumber)
        {
            StatusReport sr = new StatusReport();
            string sqlString = "SELECT 资源编号, 占用者名称, SUM(应收金额) AS 已收总额, 帐套代码 " +
                                "FROM 小程序_未收查询 " +
                                "WHERE 帐套代码 = " + ztcode +
                                " and 收费状态 is null " +
                                (string.IsNullOrEmpty(homeNumber) ? "" : "and (资源编号 like '%" + homeNumber + "%') ") +
                                (string.IsNullOrEmpty(buildingNumber) ? "" : "and (所属楼宇 like '%" + buildingNumber + "%') ") +
                                (string.IsNullOrEmpty(name) ? "" : "and (占用者名称 like '%" + name + "%') ") +
                                "GROUP BY 资源编号, 占用者名称, 帐套代码 " +
                                "ORDER BY 占用者名称";
            DataTable dt = SQLHelper.ExecuteQuery("wyt", sqlString);
            if (dt.Rows.Count == 0)
            {
                sr.status = "Fail";
                sr.result = "未查询到任何记录";
                sr.parameters = sqlString;
                return sr;
            }
            List<Charged> chargedList = new List<Charged>();

            foreach (DataRow row in dt.Rows)
            {
                Charged c = new Charged()
                {
                    RoomNumber = DataTypeHelper.GetStringValue(row["资源编号"]),
                    Name = DataTypeHelper.GetStringValue(row["占用者名称"]),
                    Total = DataTypeHelper.GetDoubleValue(row["已收总额"]),
                    ZTCode = DataTypeHelper.GetStringValue(row["帐套代码"]),
                };
                chargedList.Add(c);
            }
            sr.status = "Success";
            sr.result = "成功";
            sr.data = chargedList.ToArray();
            return sr;
        }

        public static StatusReport GetChargedDetail(string homeNumber, string name, string ztcode, string startMonth, string endMonth)
        {
            StatusReport sr = new StatusReport();
            string sqlString = "SELECT 应收金额, 计费年月, 付款方式, 费用名称, 计费开始年月, 计费截至年月, 收款人, 收费日期 " +
                "FROM dbo.小程序_已收查询 " +
                "WHERE 房产单元编号 = @房产单元编号 and 占用者名称 = @占用者名称 and 帐套代码 = @帐套代码 " +
                "and 计费开始年月 >= @计费开始年月 and 计费开始年月 <= @计费截止年月 " +
                "ORDER BY 计费年月,费用名称 ";
            DataTable dt = SQLHelper.ExecuteQuery("wyt", sqlString,
                new SqlParameter("@房产单元编号", homeNumber),
                new SqlParameter("@占用者名称", name),
                new SqlParameter("@帐套代码", ztcode),
                new SqlParameter("@计费开始年月", startMonth),
                new SqlParameter("@计费截止年月", endMonth));

            if (dt.Rows.Count == 0)
            {
                sr.status = "Fail";
                sr.result = "未查询到任何记录";
                return sr;
            }
            List<ChargedDetail> cdList = new List<ChargedDetail>();
            foreach (DataRow row in dt.Rows)
            {
                ChargedDetail cd = new ChargedDetail()
                {
                    AmountReceivable = DataTypeHelper.GetDoubleValue(row["应收金额"]),
                    AmountMonth = string.IsNullOrEmpty(DataTypeHelper.GetStringValue(row["计费年月"])) ? "其他费用" : DataTypeHelper.GetStringValue(row["计费年月"]),
                    ChargeName = DataTypeHelper.GetStringValue(row["费用名称"]),
                    ChargeDate = FormatDate(DataTypeHelper.GetDateStringValue(row["收费日期"])),
                    StartMonth = DataTypeHelper.GetStringValue(row["计费开始年月"]),
                    EndMonth = DataTypeHelper.GetStringValue(row["计费截至年月"]),
                    Cashier = DataTypeHelper.GetStringValue(row["收款人"]),
                    ChargeWay = DataTypeHelper.GetStringValue(row["付款方式"])
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
            sr.status = "Success";
            sr.result = "成功";
            sr.data = resList.ToArray();
            return sr;
        }

        public static StatusReport GetCharges(string ztCode, string roomNumber, string userName)
        {

            //string sqlString = 
            //    " SELECT dbo.应收款.ID, dbo.应收款.计费年月, dbo.费用项目.费用名称, dbo.应收款.费用说明," +
            //    " dbo.应收款.应收金额,dbo.应收款.收费状态, dbo.应收款.收款ID, dbo.应收款.收据ID, dbo.资源帐套表.帐套名称 " +
            //    " FROM dbo.应收款 " +
            //    " LEFT OUTER JOIN dbo.资源帐套表 ON dbo.应收款.帐套代码 = dbo.资源帐套表.帐套代码 " +
            //    " LEFT OUTER JOIN dbo.费用项目 ON dbo.应收款.费用项目ID = dbo.费用项目.ID " +
            //    " WHERE (dbo.应收款.占用者ID = @占用者ID) " +
            //    " AND (dbo.应收款.房产单元ID = @房产单元ID) " +
            //    " AND (dbo.应收款.帐套代码 = @帐套代码) " +
            //    " AND (dbo.应收款.收费状态 IS NULL)";

            StatusReport sr = new StatusReport();

            string sqlString = " select 应收款ID as ID,计费年月,费用名称,费用说明,应收金额,收费状态,帐套名称 " +
                                " from 应收款APP" +
                                " where 帐套代码 = @帐套代码 " +
                                " and 房号 = @房号 " +
                                " and 占用者名称 = @占用者名称 " +
                                " and 收费状态 IS NULL " +
                                " order by 费用名称";

            DataTable dt = SQLHelper.ExecuteQuery("wx", sqlString,
                new SqlParameter("@占用者名称", userName),
                new SqlParameter("@房号", roomNumber),
                new SqlParameter("@帐套代码", ztCode));
            if (dt.Rows.Count == 0)
            {
                sr.status = "Fail";
                sr.result = "未查询到任何记录";
                return sr;
            }

            List<ChargeDetail> cdList = new List<ChargeDetail>();

            foreach (DataRow row in dt.Rows)
            {
                ChargeDetail cd = new ChargeDetail()
                {
                    Id = DataTypeHelper.GetIntValue(row["ID"]),
                    ChargeTime = DataTypeHelper.GetStringValue(row["计费年月"]),
                    ChargeName = DataTypeHelper.GetStringValue(row["费用名称"]),
                    ChargeInfo = DataTypeHelper.GetStringValue(row["费用说明"]),
                    Charge = DataTypeHelper.GetDoubleValue(row["应收金额"]),
                    ChargeStatus = DataTypeHelper.GetStringValue(row["收费状态"])
                };
                cdList.Add(cd);
            }

            ChargeDetail[] cdArray = cdList.ToArray();

            if (cdArray.Length == 0)
            {
                return null;
            }
            /////////////////////////////////////////////////
            string chargeName = cdArray[0].ChargeName;

            List<Charge> chargeList = new List<Charge>();

            int i = 0;
            while (i < cdArray.Length)
            {
                //string chargeTime = cdArray[0].ChargeTime;
                //int k = 0;
                //while (k < cdArray.Length)
                //{
                //    ChargeResult chargeResult = new ChargeResult();

                //}

                Charge charge = new Charge();
                charge.ChargeName = chargeName;
                List<ChargeDetail> list = new List<ChargeDetail>();

                for (int j = i; j < cdArray.Length; j++)
                {
                    if (cdArray[j].ChargeName == chargeName)
                    {
                        list.Add(cdArray[j]);
                        if (j == cdArray.Length - 1)
                        {
                            charge.ChargeDetails = list.ToArray();
                            i = cdArray.Length;
                            break;
                        }
                    }
                    else
                    {
                        charge.ChargeDetails = list.ToArray();
                        chargeName = cdArray[j].ChargeName;
                        i = j;
                        break;
                    }
                }
                chargeList.Add(charge);
            }
            //ChargeResult chargeResult = new ChargeResult();
            //chargeResult.Charges = chargeList.ToArray();

            sr.status = "Success";
            sr.result = "成功";
            sr.data = chargeList.ToArray();
            return sr;
            ///////////////////////////////////////////////////
        }

        public static StatusReport SetCharges(string datetime, string name, string[] chargeIds, string paymentMethod)
        {
            StatusReport sr = new StatusReport();
            string sqlString = "update weixin.dbo.应收款APP " +
                                "set 收费状态 = '已收费', " +
                                " 收费日期 = @收费日期, " +
                                " 付款方式 = @付款方式, " +
                                " 收款人 = @收款人 " +

                                " where 应收款ID in (";
            foreach (string ID in chargeIds)
            {
                sqlString += ID + ",";
            }
            sqlString = sqlString.Substring(0, sqlString.Length - 1) + ")";

            sr = SQLHelper.Update("wyt", sqlString,
                new SqlParameter("@收费日期", datetime),
                new SqlParameter("@收款人", name),
                new SqlParameter("@付款方式", paymentMethod));
            sr.parameters = sqlString;
            return sr;
        }

        //public static StatusReport GetChargeStatistics(string ztCode, string level, string userCode, string month)
        //{
        //    StatusReport sr = new StatusReport();
        //    DataTable dt = new DataTable();
        //    ChargeStatisticsBase[] csbs = null;
        //    sr.status = "Fail";
        //    sr.result = "未查询到任何数据";
        //    string sqlString = " SELECT ID,费用种类,帐套代码,帐套名称,所属组团,所属楼宇,所属单元,结转本年当期,结转本年后期, " +
        //                " 结转以后年度,上年结转实收合计,追缴前期 ,实收本年当期 ,实收本年后期,实收以后年度 ,当期实收合计,实收当期合计, " +
        //                " 当期应收,累计应收,累计实收 ,本年后期应收,实收后期应收,统计月份,报送日期 " +
        //                " FROM dbo.报送_物业管理费绩效考核统计表 ";
        //    switch (level)
        //    {
        //        case "一线":
        //            sqlString +=
        //                " WHERE (帐套代码 = @帐套代码) AND (所属组团 = @所属组团) AND (统计月份 = @统计月份) " +
        //                " ORDER BY 帐套代码,所属组团,所属楼宇,所属单元 ";
        //            dt = SQLHelper.ExecuteQuery("weixin", sqlString,
        //                new SqlParameter("@帐套代码", ztCode),
        //                new SqlParameter("@所属组团", userCode),
        //                new SqlParameter("@统计月份", month));
        //            csbs = GetCsbs(dt);
        //            if (csbs.Length == 0)
        //            {
        //                return sr;
        //            }

        //            break;
        //        case "助理":
        //        case "项目经理":
        //            sqlString +=
        //                " WHERE (帐套代码 = @帐套代码) AND (统计月份 = @统计月份) " +
        //                " ORDER BY 帐套代码,所属组团,所属楼宇,所属单元 ";
        //            dt = SQLHelper.ExecuteQuery("weixin", sqlString,
        //                new SqlParameter("@帐套代码", ztCode),
        //                new SqlParameter("@所属组团", userCode),
        //                new SqlParameter("@统计月份", month));
        //            csbs = GetCsbs(dt);
        //            if (csbs.Length == 0)
        //            {
        //                return sr;
        //            }
        //            break;
        //        case "公司":
        //            sqlString +=
        //                " WHERE (统计月份 = @统计月份) " +
        //                " ORDER BY 帐套代码,所属组团,所属楼宇,所属单元 ";
        //            dt = SQLHelper.ExecuteQuery("weixin", sqlString,
        //                new SqlParameter("@帐套代码", ztCode),
        //                new SqlParameter("@所属组团", userCode),
        //                new SqlParameter("@统计月份", month));
        //            csbs = GetCsbs(dt);
        //            if (csbs.Length == 0)
        //            {
        //                return sr;
        //            }
        //            break;
        //    }
        //    return sr;
        //}

        public static StatusReport GetGroupChargeStatistics(string ztCode, string userCode, string month)
        {
            StatusReport sr = new StatusReport();
            DataTable dt = new DataTable();
            ChargeStatisticsBase[] csbs = null;
            sr.status = "Fail";
            sr.result = "未查询到任何数据";
            string sqlString = " SELECT 实收当期合计,当期应收,累计应收,累计实收,实收后期应收 " +
                        " FROM 视图_物业管理费绩效考核统计表_区中所有楼宇 " +
                        " WHERE(帐套代码 = @帐套代码) AND(所属组团 = @所属组团) AND(统计月份 = @统计月份) " +
                        " ORDER BY 帐套代码,所属组团,所属楼宇,所属单元 "; 
            dt = SQLHelper.ExecuteQuery("weixin", sqlString,
                        new SqlParameter("@帐套代码", ztCode),
                        new SqlParameter("@所属组团", userCode),
                        new SqlParameter("@统计月份", month));
            csbs = GetCsbs(dt);
            if (csbs.Length == 0)
            {
                return sr;
            }
            
            return sr;
        }

        private static ChargeStatisticsBase[] GetCsbs(DataTable dt)
        {
            if (dt.Rows.Count == 0)
            {
                return null;
            }
            List<ChargeStatisticsBase> csbList = new List<ChargeStatisticsBase>();
            foreach (DataRow dr in dt.Rows)
            {
                ChargeStatisticsBase csb = new ChargeStatisticsBase()
                {
                    chargeType = DataTypeHelper.GetStringValue(dr["费用种类"]),
                    ztCode = DataTypeHelper.GetStringValue(dr["帐套代码"]),
                    ztName = DataTypeHelper.GetStringValue(dr["帐套名称"]),
                    group = DataTypeHelper.GetStringValue(dr["所属组团"]),
                    building = DataTypeHelper.GetStringValue(dr["所属楼宇"]),
                    unit = DataTypeHelper.GetStringValue(dr["所属单元"]),
                    carryOverThisYearNow = DataTypeHelper.GetDecimalValue(dr["结转本年当期"]),
                    carryOverThisYearAfter = DataTypeHelper.GetDecimalValue(dr["结转本年后期"]),
                    carryOverAfterYear = DataTypeHelper.GetDecimalValue(dr["结转以后年度"]),
                    beforeYearCarryOverReceived = DataTypeHelper.GetDecimalValue(dr["上年结转实收合计"]),
                    recoveredAfter = DataTypeHelper.GetDecimalValue(dr["追缴前期"]),
                    receivedThisYearNow = DataTypeHelper.GetDecimalValue(dr["实收本年当期"]),
                    receivedThisYearAfter = DataTypeHelper.GetDecimalValue(dr["实收本年后期"]),
                    receivedAfterYear = DataTypeHelper.GetDecimalValue(dr["实收以后年度"]),
                    nowReceived = DataTypeHelper.GetDecimalValue(dr["当期实收合计"]),
                    receivedNow = DataTypeHelper.GetDecimalValue(dr["实收当期合计"]),
                    nowShouldReceived = DataTypeHelper.GetDecimalValue(dr["当期应收"]),
                    addupShouldReceived = DataTypeHelper.GetDecimalValue(dr["累计应收"]),
                    addupReceived = DataTypeHelper.GetDecimalValue(dr["累计实收"]),
                    thisYearAfterShouldReceive = DataTypeHelper.GetDecimalValue(dr["本年后期应收"]),
                    receivedAfterShouldReceived = DataTypeHelper.GetDecimalValue(dr["实收后期应收"]),
                    month = DataTypeHelper.GetStringValue(dr["统计月份"]),
                    date = DataTypeHelper.GetDateStringValue(dr["报送日期"])
                };
                csbList.Add(csb);
            }
            return csbList.ToArray();
        }

        private static string FormatDate(string date)
        {
            return date.Split(' ')[0];
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


/**
 * 实收当期合计 （已有字段）
 * 当期应收 （已有字段）
 * 累计欠费 （累计应收 - 累计实收）
 * 本期收缴率 （实收当期合计 / 当期应收）
 * 累计收费率 （累计实收 / 累计应收）
 * 预收费率 （实收后期应收 / 当期应收）
 * 
 * 需查询的字段： 实收当期合计、当期应收、 累计应收、累计实收、实收后期应收
 **/
