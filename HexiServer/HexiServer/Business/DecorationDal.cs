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
    public class DecorationDal
    {
        public static StatusReport GetDecoration(string classify, string isDone)
        {
            StatusReport sr = new StatusReport();
            string done = "";
            if (isDone == "0")
            {
                done = " where (分类 = @分类) AND 验收时间 is null ORDER BY ID DESC";
            }
            else 
            {
                done = " where  (分类 = @分类)  AND  验收时间 is not null ORDER BY ID DESC ";
            }
            string sqlstring = " SELECT ID, 分类, 房产编号, 办理时间, 开工时间, 装修内容, 验收时间, 施工队负责人, 施工队成员, " +
                " 业主姓名, 业主电话, 客服专员, 装修类型, 施工队, 施工队负责人电话, 装修施工证编号, 状态, 是否封阳台, 验收结果, " +
                " 验收结果说明 " +
                " FROM dbo.基础资料_装修管理 ";
            sqlstring += done;

            DataTable dt = SQLHelper.ExecuteQuery("wyt", sqlstring,
                new SqlParameter("@分类", classify));
            if (dt.Rows.Count == 0)
            {
                sr.status = "Fail";
                sr.result = "未查询到任何数据";
                sr.parameters = sqlstring;
                return sr;
            }
            List<Decoration> decorationList = new List<Decoration>();
            foreach (DataRow dr in dt.Rows)
            {
                Decoration decoration = new Decoration();
                decoration.id = DataTypeHelper.GetIntValue(dr["ID"]);
                decoration.classify = DataTypeHelper.GetStringValue(dr["分类"]);
                decoration.roomNumber = DataTypeHelper.GetStringValue(dr["房产编号"]);
                decoration.name = DataTypeHelper.GetStringValue(dr["业主姓名"]);
                decoration.phone = DataTypeHelper.GetStringValue(dr["业主电话"]);
                decoration.attache = DataTypeHelper.GetStringValue(dr["客服专员"]);
                decoration.type = DataTypeHelper.GetStringValue(dr["装修类型"]);
                decoration.constructionTeam = DataTypeHelper.GetStringValue(dr["施工队"]);
                decoration.teamLeader = DataTypeHelper.GetStringValue(dr["施工队负责人"]);
                decoration.teamLeaderPhone = DataTypeHelper.GetStringValue(dr["施工队负责人电话"]);
                decoration.teamMembers = GetTeamMember(decoration.id);
                decoration.certificateNumber = DataTypeHelper.GetStringValue(dr["装修施工证编号"]);
                decoration.transactTime = DataTypeHelper.GetDateStringValue(dr["办理时间"]);
                decoration.startTime = DataTypeHelper.GetDateStringValue(dr["开工时间"]);
                decoration.status = DataTypeHelper.GetStringValue(dr["状态"]);
                decoration.contents = GetDecorationContent(decoration.id);
                decoration.needSealingBalcony = DataTypeHelper.GetStringValue(dr["是否封阳台"]);
                decoration.checkTime = DataTypeHelper.GetDateStringValue(dr["验收时间"]);
                decoration.checkResult = DataTypeHelper.GetStringValue(dr["验收结果"]);
                decoration.checkResultExplain = DataTypeHelper.GetStringValue(dr["验收结果说明"]);
                decorationList.Add(decoration);
            }
            sr.status = "Success";
            sr.result = "成功";
            sr.data = decorationList.ToArray();
            sr.parameters = sqlstring;
            return sr;
        }

        private static TeamMember[] GetTeamMember(int? pid)
        {
            if (pid.HasValue)
            {
                string sqlstring = " SELECT 姓名, 联系电话, 身份证号码 FROM dbo.基础资料_装修管理_施工队成员 " +
                    " where PID = @PID ";

                DataTable dt = SQLHelper.ExecuteQuery("wyt", sqlstring,
                    new SqlParameter("@PID", pid));
                if (dt.Rows.Count == 0)
                {
                    return null;
                }
                List<TeamMember> tmList = new List<TeamMember>();
                foreach (DataRow dr in dt.Rows)
                {
                    TeamMember tm = new TeamMember();
                    tm.name = DataTypeHelper.GetStringValue(dr["姓名"]);
                    tm.phone = DataTypeHelper.GetStringValue(dr["联系电话"]);
                    tm.idCard = DataTypeHelper.GetStringValue(dr["身份证号码"]);
                    tmList.Add(tm);
                }
                return tmList.ToArray();
            }
            else
            {
                return null;
            }
        }
        private static DecorationContent[] GetDecorationContent(int? pid)
        {
            if (pid.HasValue)
            {
                string sqlstring = " SELECT 序号, 装修内容 FROM dbo.基础资料_装修管理_装修内容 " +
                    " where PID = @PID ";

                DataTable dt = SQLHelper.ExecuteQuery("wyt", sqlstring,
                    new SqlParameter("@PID", pid));
                if (dt.Rows.Count == 0)
                {
                    return null;
                }
                List<DecorationContent> dcList = new List<DecorationContent>();
                foreach (DataRow dr in dt.Rows)
                {
                    DecorationContent dc = new DecorationContent();
                    dc.number = DataTypeHelper.GetStringValue(dr["序号"]);
                    dc.content = DataTypeHelper.GetStringValue(dr["装修内容"]);
                    dcList.Add(dc);
                }
                return dcList.ToArray();
            }
            else
            {
                return null;
            }
        }

    }
}