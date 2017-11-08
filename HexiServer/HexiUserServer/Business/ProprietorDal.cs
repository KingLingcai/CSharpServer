using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using HexiUtils;
using HexiUserServer.Models;

namespace HexiUserServer.Business
{
    public class ProprietorDal
    {
        /// <summary>
        /// 判断该OpenId是否对应系统中的某占用者
        /// 如果对应，则返回该占用者的信息
        /// 如果不对应，则返回错误信息
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        public static StatusReport CheckOpenIdExist(string openId)
        {
            StatusReport sr = new StatusReport();

            string sqlString =
                "select " +
                "占用者ID " +
                "from 基础资料_微信占用者绑定表 " +
                "where OpenID = @OpenId";

            int proprietorId = SQLHelper.ExecuteScalar(sqlString,
                new SqlParameter("@OpenId", openId));

            if (proprietorId > 0)
            {
                string sqlStr =
                    " SELECT ID, 房产单元编号, 占用者名称, 联系电话, 帐套名称, 帐套代码 " +
                    " FROM dbo.小程序_现场查询 " +
                    " where ID = @ID";
                DataTable dt = SQLHelper.ExecuteQuery(sqlStr, new SqlParameter("@ID", proprietorId));
                DataRow dr = dt.Rows[0];

                Proprietor proprietor = new Proprietor()
                {
                    Id = DataTypeHelper.GetIntValue(dr["ID"]),
                    RoomNumber = DataTypeHelper.GetStringValue(dr["房产单元编号"]),
                    Name = DataTypeHelper.GetStringValue(dr["占用者名称"]),
                    Phone = DataTypeHelper.GetStringValue(dr["联系电话"]),
                    ZTName = DataTypeHelper.GetStringValue(dr["帐套名称"]),
                    ZTCode = DataTypeHelper.GetStringValue(dr["帐套代码"])
                };
                sr.status = "Success";
                sr.result = "成功";
                sr.data = JsonConvert.SerializeObject(proprietor);
                return sr;
            }
            else
            {
                sr.status = "Fail";
                sr.result = "无此用户";
                return sr;
            }
        }



        /// <summary>
        /// 判断该占用者是否存在
        /// </summary>
        /// <param name="name"></param>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public static int CheckProprietorExist(string userName, string phoneNumber)
        {
            //string sqlString =
            //    "select " +
            //    "ID " +
            //    "from 用户 " +
            //    "where UserCode = @UserCode and Password = @Password";

            //int id = SQLHelper.ExecuteScalar(sqlString,
            //    new SqlParameter("@UserCode",name),
            //    new SqlParameter("@Password",password));
            string sqlString =
               "select " +
               "ID " +
               "from 资源占用者 " +
               "where 占用者名称 = @占用者名称 and 联系电话 like '%" + phoneNumber + "%'";

            int id = SQLHelper.ExecuteScalar(sqlString,
                new SqlParameter("@占用者名称", userName));

            if (id > 0)
            {
                return id;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 将微信OpenId和系统中的占用者ID绑定，保存到微信占用者绑定表
        /// </summary>
        /// <param name="id"></param>
        /// <param name="openId"></param>
        /// <returns></returns>
        public static StatusReport BindProprietor(int id, string openId)
        {
            string sqlString =
                "insert into 基础资料_微信占用者绑定表 " +
                "(占用者ID , OpenID) " +
                "select " +
                "@占用者ID, @OpenID";

            StatusReport sr = SQLHelper.Insert(sqlString,
                new SqlParameter("@占用者ID", id),
                new SqlParameter("@OpenID", openId));

            return sr;
        }


        //private static Employee GetZTInfo(Employee employee, string[] ztcodes)
        //{
        //    List<ZT> zts = new List<ZT>();

        //    for (int i = 0; i < ztcodes.Length; i++)
        //    {
        //        string ztcode = ztcodes[i];
        //        string sqlString = "select 帐套代码,帐套名称 from 资源帐套表 where 帐套代码 = @帐套代码";
        //        DataTable dt = SQLHelper.ExecuteQuery(sqlString, new SqlParameter("@帐套代码", ztcode));
        //        DataRow dr = dt.Rows[0];
        //        ZT zt = new ZT((string)dr["帐套代码"], (string)dr["帐套名称"]);
        //        zts.Add(zt);
        //    }
        //    employee.ZTInfo = zts.ToArray();
        //    return employee;
        //}

        //private static Employee GetJurisdictionInfo(Employee employee)
        //{

        //    return null;
        //}
    }
}