using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HexiServer.Models
{
    public class Equipment
    {
        /// <summary>
        /// ID
        /// </summary>
        public int? ID { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public string Classify { get; set; }
        /// <summary>
        /// 设备运行编号
        /// </summary>
        public string OperationNumber { get; set; }
        /// <summary>
        /// 设备编号
        /// </summary>
        public string Number { get; set; }
        /// <summary>
        /// 设备名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 系统名称
        /// </summary>
        public string SystemName { get; set; }
        /// <summary>
        /// 出厂日期
        /// </summary>
        public string ProductionDate { get; set; }
        /// <summary>
        /// 使用日期
        /// </summary>
        public string UseDate { get; set; }
        /// <summary>
        /// 设备价格
        /// </summary>
        public double? price { get; set; }
        /// <summary>
        /// 出厂序号
        /// </summary>
        public string ProductionNumber { get; set; }
        /// <summary>
        /// 设计寿命
        /// </summary>
        public string DesignedLife { get; set; }
        /// <summary>
        /// 卡号
        /// </summary>
        public string CardNumber { get; set; }
        /// <summary>
        /// 安装地点
        /// </summary>
        public string UseAddress { get; set; }
        /// <summary>
        /// 产地
        /// </summary>
        public string ProductionAddress { get; set; }
        /// <summary>
        /// 设备保养管理代号
        /// </summary>
        public string MaintainNumber { get; set; }
        /// <summary>
        /// 设备保养管理内容
        /// </summary>
        public string MaintainContent { get; set; }
        /// <summary>
        /// 设备保养管理日期
        /// </summary>
        public string MaintainDate { get; set; }
        /// <summary>
        /// 工作日期
        /// </summary>
        public string WorkDate { get; set; }
        /// <summary>
        /// 是否完成
        /// </summary>
        public int? IsDone { get; set; }
        /// <summary>
        /// 录入日期
        /// </summary>
        public string InputDate { get; set; }
        /// <summary>
        /// 录入人
        /// </summary>
        public string InputMan { get; set; }
        /// <summary>
        /// 完成说明
        /// </summary>
        public string DoneInfo { get; set; }
        /// <summary>
        /// 保养前照片
        /// </summary>
        public string BeforeImage { get; set; }
        /// <summary>
        /// 保养后照片
        /// </summary>
        public string AfterImage { get; set; }
        /// <summary>
        /// 序次
        /// </summary>
        public string Order { get; set; }

    }
}