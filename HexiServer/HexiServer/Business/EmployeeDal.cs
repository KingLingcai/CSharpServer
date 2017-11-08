using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using HexiServer.Common;
using HexiServer.Models;

namespace HexiServer.Business
{
    public class EmployeeDal
    {
        /// <summary>
        /// 判断该OpenId是否对应系统中的某员工
        /// 如果对应，则返回该员工的信息
        /// 如果不对应，则返回错误信息
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        public static StatusReport CheckOpenIdExist(string openId)
        {
            StatusReport sr = new StatusReport();

            string sqlString =
                "select " +
                "用户表Id " +
                "from 基础资料_微信员工绑定表 " +
                "where OpenId = @OpenId";

            int empId = SQLHelper.ExecuteScalar(sqlString,
                new SqlParameter("@OpenId", openId));
            
            if (empId > 0)
            {
                string sqlStr =
                    "select " +
                    "ID,UserCode,UserName,员工Id,ZTCodes " +
                    "from 用户 " +
                    "where ID = @ID";

                DataTable dt = SQLHelper.ExecuteQuery(sqlStr, new SqlParameter("@ID", empId));
                DataRow dr = dt.Rows[0];

                Employee emp = new Employee()
                {
                    Id = Convert.ToInt32(dr["ID"]),
                    UserCode = (string)dr["UserCode"],
                    UserName = (string)dr["UserName"],
                    //ZTCodes = ((string)dr["ZTCodes"]).Split(',')
                };
                string[] ztcodes = ((string)dr["ZTCodes"]).Split(',');
                emp = GetZTInfo(emp, ztcodes);
                sr.status = "Success";
                sr.result = "成功";
                sr.data = JsonConvert.SerializeObject(emp);
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
        /// 判断该员工是否存在
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static int CheckEmployeeExist(string userName,string password)
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
               "from 用户 " +
               "where UserCode = @UserCode";

            int id = SQLHelper.ExecuteScalar(sqlString,
                new SqlParameter("@UserCode", userName));

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
        /// 将微信OpenId和系统中的用户Id绑定，保存到微信员工绑定表
        /// </summary>
        /// <param name="id"></param>
        /// <param name="openId"></param>
        /// <returns></returns>
        public static StatusReport BindEmployee(int id,string openId)
        {
            string sqlString =
                "insert into 基础资料_微信员工绑定表 " +
                "(用户表Id , OpenId) " +
                "select " +
                "@用户表Id, @OpenId";

            StatusReport sr = SQLHelper.Insert(sqlString,
                new SqlParameter("@用户表Id", id),
                new SqlParameter("@OpenId", openId));

            return sr;
        }


        private static Employee GetZTInfo(Employee employee, string[] ztcodes)
        {
            List<ZT> zts = new List<ZT>();

            for (int i = 0; i < ztcodes.Length; i++)
            {
                string ztcode = ztcodes[i];
                string sqlString = "select 帐套代码,帐套名称 from 资源帐套表 where 帐套代码 = @帐套代码";
                DataTable dt = SQLHelper.ExecuteQuery(sqlString, new SqlParameter("@帐套代码", ztcode));
                DataRow dr = dt.Rows[0];
                ZT zt = new ZT((string)dr["帐套代码"], (string)dr["帐套名称"]);
                zts.Add(zt);
            }
            employee.ZTInfo = zts.ToArray();
            return employee;
        }

        private static Employee GetJurisdictionInfo(Employee employee)
        {

            return null;
        }
    }
}