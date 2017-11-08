using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HexiServer.Models
{
    public class Repair
    {
        public int? Id { get; set; }//Id
        public string SerialNumber { get; set; }//序号
        public string Department { get; set; }//部门
        public string Address { get; set; }//地址
        public string RepairPerson { get; set; }//报修人
        public string PhoneNumber { get; set; }//联系电话
        public string ServiceProject { get; set; }//服务项目
        public string ServiceCategory { get; set; }//服务类别
        public string Level { get; set; }//紧急程度
        public string RepairExplain { get; set; }//报修说明
        public string RepairTime { get; set; }//报修时间
        public string OrderTime { get; set; }//预约服务时间
        public string VisitTime { get; set; }//谈好上门时间
        public string SendPerson { get; set; }//发单人
        public string ReceivePerson { get; set; }//接单人
        public string DispatchTime { get; set; }//派工时间
        public string ArriveTime { get; set; }//到场时间
        public string OperatePerson { get; set; }//操作人
        public string CompleteTime { get; set; }//完成时间
        public double? MaterialExpense { get; set; }//材料费
        public double? LaborExpense { get; set; }//人工费
        public int? IsRead { get; set; }//是否阅读
        public string CompleteStatus { get; set; }//完成情况及所耗物料C
    }
}