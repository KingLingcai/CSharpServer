using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using HexiUtils;
using HexiServer.Models;

namespace HexiServer.Business
{
    public class EquipmentDal
    {
        public static StatusReport GetEquipment(string classify, string isDone)
        {
            StatusReport sr = new StatusReport();
            string done = "";
            if (isDone == "0")
            {
                done = "(是否完成 is NULL)";
            }
            else
            {
                done = "(是否完成 = 1)";
            }
            string sqlstring = " SELECT ID, 分类, 设备运行编号, 设备编号, 设备型号, 设备名称, 系统名称, 出厂日期, " +
                               " 使用日期, 设备价格, 出厂序号, 设计寿命, 卡号, 安装地点, 产地, 设备保养管理代号, 设备保养管理内容, " +
                               " 设备保养管理日期, 工作名称, 工作日期, 是否完成, 录入日期, 录入人, 完成说明, 序次, 保养前照片, 保养后照片 " +
                               " FROM dbo.小程序_设备管理 " +
                               " WHERE (分类 = @分类)  AND " +
                               done +
                               //isDone == "0" ? "(是否完成 is NULL)" : "(是否完成 = 1)" +
                               " ORDER BY ID DESC ";
            DataTable dt = SQLHelper.ExecuteQuery("wyt", sqlstring,
                new SqlParameter("@分类", classify));
            if (dt.Rows.Count == 0)
            {
                sr.status = "Fail";
                sr.result = "未查询到任何数据";
                sr.parameters = sqlstring;
                return sr;
            }
            List<Equipment> equipmentList = new List<Equipment>();
            foreach (DataRow dr in dt.Rows)
            {
                Equipment equipment = new Equipment();
                equipment.ID = DataTypeHelper.GetIntValue(dr["ID"]);
                equipment.Classify = DataTypeHelper.GetStringValue(dr["分类"]);
                equipment.OperationNumber = DataTypeHelper.GetStringValue(dr["设备运行编号"]);
                equipment.Number = DataTypeHelper.GetStringValue(dr["设备编号"]);
                equipment.Name = DataTypeHelper.GetStringValue(dr["设备名称"]);
                equipment.SystemName = DataTypeHelper.GetStringValue(dr["系统名称"]);
                equipment.ProductionDate = DataTypeHelper.GetDateStringValue(dr["出厂日期"]);
                equipment.UseDate = DataTypeHelper.GetDateStringValue(dr["使用日期"]);
                equipment.price = DataTypeHelper.GetDoubleValue(dr["设备价格"]);
                equipment.ProductionNumber = DataTypeHelper.GetStringValue(dr["出厂序号"]);
                equipment.DesignedLife = DataTypeHelper.GetStringValue(dr["设计寿命"]);
                equipment.CardNumber = DataTypeHelper.GetStringValue(dr["卡号"]);
                equipment.UseAddress = DataTypeHelper.GetStringValue(dr["安装地点"]);
                equipment.ProductionAddress = DataTypeHelper.GetStringValue(dr["产地"]);
                equipment.MaintainNumber = DataTypeHelper.GetStringValue(dr["设备保养管理代号"]);
                equipment.MaintainContent = DataTypeHelper.GetStringValue(dr["设备保养管理内容"]);
                equipment.MaintainDate = DataTypeHelper.GetDateStringValue(dr["设备保养管理日期"]);
                equipment.WorkDate = DataTypeHelper.GetDateStringValue(dr["工作日期"]);
                equipment.IsDone = DataTypeHelper.GetBooleanValue(dr["是否完成"]) == true ? 1 : 0;
                equipment.InputDate = DataTypeHelper.GetDateStringValue(dr["录入日期"]);
                equipment.InputMan = DataTypeHelper.GetStringValue(dr["录入人"]);
                equipment.DoneInfo = DataTypeHelper.GetStringValue(dr["完成说明"]);
                equipment.BeforeImage = DataTypeHelper.GetStringValue(dr["保养前照片"]);
                equipment.AfterImage = DataTypeHelper.GetStringValue(dr["保养后照片"]);
                equipment.Order = DataTypeHelper.GetBooleanValue(dr["序次"]) == true ? "1" : "0";
                equipmentList.Add(equipment);
            }
            sr.status = "Success";
            sr.result = "成功";
            sr.data = equipmentList.ToArray();
            sr.parameters = sqlstring;
            return sr;
        }

        public static StatusReport SetEquipment(string id, string isDone, string inputMan, string doneInfo, string inputDate)
        {
            StatusReport sr = new StatusReport();
            string sqlstring = "update dbo.小程序_设备管理 set " +
                                "是否完成 = @是否完成, " +
                                "完成说明 = @完成说明, " +
                                "录入人 = @录入人, " +
                                "录入日期 = @录入日期 " +
                                "where ID = @ID";
            sr = SQLHelper.Update("wyt", sqlstring,
                new SqlParameter("@是否完成", isDone == "0" ? false : true),
                new SqlParameter("@完成说明", doneInfo),
                new SqlParameter("@录入人", inputMan),
                new SqlParameter("@录入日期", inputDate),
                new SqlParameter("@ID", id));
            return sr;
        }

        public static StatusReport SetEquipmentImage(string id, string func, string imagePath)
        {
            StatusReport sr = new StatusReport();
            string sqlstring = "";
            if (func == "before")
            {
                sqlstring = "update dbo.小程序_设备管理 set 保养前照片 = @保养前照片 where ID = @ID";
            }
            else
            {
                sqlstring = "update dbo.小程序_设备管理 set 保养后照片 = @保养后照片 where ID = @ID";
            }

            sr = SQLHelper.Update("wyt", sqlstring,
                new SqlParameter("@保养前照片", imagePath),
                new SqlParameter("@保养后照片", imagePath),
                new SqlParameter("@ID", id));
            return sr;

        }
    }
}