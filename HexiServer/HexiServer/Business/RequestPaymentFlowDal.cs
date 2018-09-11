using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using HexiUtils;
using HexiServer.Models;
using static HexiUtils.DataTypeHelper;

namespace HexiServer.Business
{
    public class RequestPaymentFlowDal
    {
        public static StatusReport GetRequestPaymentSheet(string userCode)
        {
            StatusReport sr = new StatusReport();
            string sqlString = " SELECT ID,登记ID,业务ID,环节ID,人员ID,上一实例ID,创建日期, " +
                " 结束日期,留言,阅读,文档编号,表单变化标识,任务,任务说明,登记说明, " +
                " 登记日期,登记人,登记人部门,状态标记,请款单位,请款人 ,请款日期,收款单位 ,合同编号 ,合同金额, " +
                " 用途 ,支付方式,合同ID,支出性质 ,费用种类,预算年度 ,预算审核员,预算审核员审批时间,预算审核员审批意见, " +
                " 分公司分管财务,分公司分管财务审批时间,分公司分管财务审批意见,分公司运行部,分公司运行部审批时间, " +
                " 分公司运行部审批意见,分公司行政部,分公司行政部审批时间,分公司行政部审批意见, " +
                " 分公司审批,分公司审批时间,分公司审批意见,分公司领导, " +
                " 分公司领导审批时间,分公司领导审批意见, " +
                " 是否采购计划内 ,采购编号, " +
                " 预算处,请款编号,请款月份,预算类别 " +
                " FROM wytnetsz.dbo.小程序_请款流程 ";
            DataTable dt = SQLHelper.ExecuteQuery("wyt", sqlString);
            if (dt.Rows.Count == 0)
            {
                sr.status = "Fail";
                sr.result = "未查询到任何记录";
                return sr;
            }

            List<RequestPaymentSheet> rpsList = new List<RequestPaymentSheet>();
            foreach(DataRow dr in dt.Rows)
            {
                RequestPaymentSheet rps = new RequestPaymentSheet
                {
                    id = GetIntValue(dr["ID"]),
                    registerId = GetIntValue(dr["登记ID"]),
                    businessId = GetIntValue(dr["业务ID"]),
                    linkId = GetIntValue(dr["环节ID"]),
                    userId = GetIntValue(dr["人员ID"]),
                    lastId = GetIntValue(dr["上一实例ID"]),
                    createTime = GetDateStringValue(dr["创建日期"]),
                    endTime = GetDateStringValue(dr["结束日期"]),
                    leaveWords = GetStringValue(dr["留言"]),
                    isRead = GetIntValue(dr["阅读"]),
                    documentNumber = GetStringValue(dr["文档编号"]),
                    task = GetStringValue(dr["任务"]),
                    taskExplain = GetStringValue(dr["任务"]),
                    registerExplain = GetStringValue(dr["登记说明"]),
                    registerTime = GetDateStringValue(dr["登记日期"]),
                    registerMan = GetStringValue(dr["登记人"]),
                    registerDepartment = GetStringValue(dr["登记人部门"]),
                    requestMan = GetStringValue(dr["请款人"]),
                    requestDepartment = GetStringValue(dr["请款单位"]),
                    requestTime = GetDateStringValue(dr["请款日期"]),
                    receiptDepartment = GetStringValue(dr["收款单位"]),
                    compactNumber = GetStringValue(dr["合同编号"]),
                    compactSum = GetDecimalValue(dr["合同金额"]),
                    purpose = GetStringValue(dr["用途"]),
                    payMethod = GetStringValue(dr["支付方式"]),
                    expenseProperty = GetStringValue(dr["支出性质"]),
                    chargeType = GetStringValue(dr["费用种类"]),
                    budgetYear = GetStringValue(dr["预算年度"]),
                    budgeAssessor = GetStringValue(dr["预算审核员"]),
                    budgeAssessorApprovalTime = GetStringValue(dr["预算审核员审批时间"]),
                    budgeAssessorApprovalOpinion = GetStringValue(dr["预算审核员审批意见"]),
                    controlledCorporationAccountant = GetStringValue(dr["分公司分管财务"]),
                    controlledCorporationAccountantApprovalTime = GetDateStringValue(dr["分公司分管财务审批时间"]),
                    controlledCorporationAccountantApprovalOpinion = GetStringValue(dr["分公司分管财务审批意见"]),
                    controlledCorporationOperationDepartment = GetStringValue(dr["分公司运行部"]),
                    controlledCorporationOperationDepartmentApprovalTime = GetDateStringValue(dr["分公司运行部审批时间"]),
                    controlledCorporationOperationDepartmentApprovalOpinion = GetStringValue(dr["分公司运行部审批意见"]),
                    controlledCorporationAdministrativeDepartment = GetStringValue(dr["分公司行政部"]),
                    controlledCorporationAdministrativeDepartmentApprovalTime = GetDateStringValue(dr["分公司行政部审批时间"]),
                    controlledCorporationAdministrativeDepartmentApprovalOpinion = GetStringValue(dr["分公司行政部审批意见"]),
                    controlledCorporationApproval = GetStringValue(dr["分公司审批"]),
                    controlledCorporationApprovalTime = GetDateStringValue(dr["分公司审批时间"]),
                    controlledCorporationApprovalOpinion = GetStringValue(dr["分公司审批意见"]),
                    controlledCorporationLeader = GetStringValue(dr["分公司领导"]),
                    controlledCorporationLeaderApprovalTime = GetDateStringValue(dr["分公司领导审批时间"]),
                    controlledCorporationLeaderApprovalOpinion = GetStringValue(dr["分公司领导审批意见"]),
                    isInPurchasePlan = GetStringValue(dr["是否采购计划内"]),
                    purchaseNumber = GetStringValue(dr["采购编号"]),
                    budgeBureau = GetStringValue(dr["预算处"]),
                    requestNumber = GetStringValue(dr["请款编号"]),
                    requestMonth = GetStringValue(dr["请款月份"]),
                    budgeType = GetStringValue(dr["预算类别"])
                };
                rpsList.Add(rps);
            }
            sr.status = "Success";
            sr.result = "成功";
            sr.data = rpsList.ToArray();
            return sr;
    }
        }
}