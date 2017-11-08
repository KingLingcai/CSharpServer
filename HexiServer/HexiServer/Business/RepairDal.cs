using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using HexiServer.Models;
using HexiServer.Common;

namespace HexiServer.Business
{
    public class RepairDal
    {
        /// <summary>
        /// 获取满足条件的工单的列表
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="ztcode"></param>
        /// <returns></returns>
        public static Repair[] GetRepairOrder(string userCode, string ztcode)
        {
            //string receivePerson = UserHelper.GetUser(openId);
            string sqlString =
                "select" +
                " ID,序号,部门,地址,报修人,联系电话,服务项目,服务类别," +
                "紧急程度,报修说明,报修时间,预约服务时间,谈好上门时间,发单人,接单人,派工时间," +
                "到场时间,操作人,完成时间,材料费,人工费,是否阅读,完成情况及所耗物料 " +
                "from 基础资料_服务任务管理 " +
                "where 接单人 = @接单人 and left(分类,2) = @分类 " +
                "order by ID desc ";
            DataTable dt = SQLHelper.ExecuteQuery(sqlString,
                new SqlParameter("@接单人", userCode),
                new SqlParameter("@分类", ztcode));

            List<Repair> repairList = new List<Repair>();
            foreach (DataRow row in dt.Rows)
            {
                Repair r = new Repair();
                r.Id = DataTypeHelper.GetIntValue(row["ID"]);
                r.SerialNumber = DataTypeHelper.GetStringValue(row["序号"]);
                r.Department = DataTypeHelper.GetStringValue(row["部门"]);
                r.Address = DataTypeHelper.GetStringValue(row["地址"]);
                r.RepairPerson = DataTypeHelper.GetStringValue(row["报修人"]);
                r.PhoneNumber = DataTypeHelper.GetStringValue(row["联系电话"]);
                r.ServiceProject = DataTypeHelper.GetStringValue(row["服务项目"]);
                r.ServiceCategory = DataTypeHelper.GetStringValue(row["服务类别"]);
                r.Level = DataTypeHelper.GetStringValue(row["紧急程度"]);
                r.RepairExplain = DataTypeHelper.GetStringValue(row["报修说明"]);
                r.RepairTime = DataTypeHelper.GetDateStringValue(row["报修时间"]);
                r.OrderTime = DataTypeHelper.GetDateStringValue(row["预约服务时间"]);
                r.VisitTime = DataTypeHelper.GetDateStringValue(row["谈好上门时间"]);
                r.SendPerson = DataTypeHelper.GetStringValue(row["发单人"]);
                r.ReceivePerson = DataTypeHelper.GetStringValue(row["接单人"]);
                r.DispatchTime = DataTypeHelper.GetDateStringValue(row["派工时间"]);
                r.ArriveTime = DataTypeHelper.GetDateStringValue(row["到场时间"]);
                r.OperatePerson = DataTypeHelper.GetStringValue(row["操作人"]);
                r.CompleteTime = DataTypeHelper.GetDateStringValue(row["完成时间"]);
                r.MaterialExpense = DataTypeHelper.GetDoubleValue(row["材料费"]);
                r.LaborExpense = DataTypeHelper.GetDoubleValue(row["人工费"]);
                r.IsRead = DataTypeHelper.GetIntValue(row["是否阅读"]);
                r.CompleteStatus = DataTypeHelper.GetStringValue(row["完成情况及所耗物料"]);
                repairList.Add(r);
            }
            return repairList.ToArray();
        }

        /// <summary>
        /// 更新库中的工单记录
        /// </summary>
        /// <param name="id"></param>
        /// <param name="arriveTime"></param>
        /// <param name="completeTime"></param>
        /// <param name="completeStatus"></param>
        /// <param name="laborExpense"></param>
        /// <param name="materialExpense"></param>
        /// <returns></returns>
        public static StatusReport SetRepairOrder(string id, string arriveTime, string completeTime, string completeStatus, string laborExpense, string materialExpense)
        {
            StatusReport sr = new StatusReport();
            string sqlString = 
                "update 基础资料_服务任务管理 " +
                "set " +
                "到场时间 = @到场时间, " +
                "完成时间 = @完成时间, " +
                "完成情况及所耗物料 = @完成情况及所耗物料, " +
                "人工费 = @人工费, " +
                "材料费 = @材料费 " +
                "where ID = @ID";
            sr = SQLHelper.Update(sqlString,
                new SqlParameter("@到场时间", arriveTime),
                new SqlParameter("@完成时间", completeTime),
                new SqlParameter("@完成情况及所耗物料", completeStatus),
                new SqlParameter("@人工费", Convert.ToDouble(laborExpense)),
                new SqlParameter("@材料费", Convert.ToDouble(materialExpense)),
                new SqlParameter("@ID", Convert.ToInt32(id)));

            return sr;
        }






        //private static string GetUser(string openid)
        //{
        //    string sqlString =
        //        "select UserCode " +
        //        "from 用户 " +
        //        "where ID in (select 用户表Id from 基础资料_微信员工绑定表 where OpenId = @OpenId)";
        //    DataTable dt = SQLHelper.ExecuteQuery(sqlString,
        //        new SqlParameter("@OpenId", openid));
        //    DataRow dr = dt.Rows[0];

        //    return (string)dr["UserCode"];
        //}
    }
}