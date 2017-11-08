using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using WanBoWebapp.model;


namespace WanBoWebapp.util
{
    public class SqlHelper
    {
        private static string connectionString = "Data Source=192.168.0.233\\cy2012;Initial Catalog=wb02;Persist Security Info=False;User ID=sa;Password=CY2012";//连接字符串
        /**
         * 该静态方法，用于根据传入的sql语句和相关参数，在数据库中查询
         * 数据，并以表的数据表（DataTable）的形式返回查询到的数据。
         * */
        public static DataTable ExecuteQuery (string sqlString, params SqlParameter[] parameters) 
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = sqlString;
                    cmd.Parameters.AddRange(parameters);
                    DataTable dt = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }

        /**
         * 该静态方法，用于根据传入的sql语句和相关参数，在数据库中查询
         * 数据，并以表的数据表（DataTable）的形式返回查询到的数据。
         * */
        public static int ExecuteScalar(string sqlString, params SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = sqlString;
                    cmd.Parameters.AddRange(parameters);
                    int id = Convert.ToInt32(cmd.ExecuteScalar());
                    return id;
                }
            }
        }

        /**
         * 该静态方法，用于根据传入的sql语句和相关参数，在数据库中插入数据，
         * 并将插入的结果返回
         * */
        public static StatusReport Insert (string sqlString, params SqlParameter[] parameters)
        {
            StatusReport sr = new StatusReport();
            sr.status = "Success";
            sr.result = "操作成功";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = sqlString;
                        cmd.Parameters.AddRange(parameters);
                        int lines = cmd.ExecuteNonQuery();//执行插入操作，并返回受影响行数
                        if (lines < 1)
                        {
                            sr.status = "Fail";
                            sr.result = "数据写入失败";
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                sr.status = "Fail";
                sr.result = exp.Message;
                throw exp;
            }
            finally
            {

            }
            return sr;
        }



        public static StatusReport Update(string sqlString, params SqlParameter[] parameters)
        {
            StatusReport sr = new StatusReport();
            sr.status = "Success";
            sr.result = "操作成功";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = sqlString;
                        cmd.Parameters.AddRange(parameters);
                        int lines = cmd.ExecuteNonQuery();//执行插入操作，并返回受影响行数
                        if (lines < 1)
                        {
                            sr.status = "Fail";
                            sr.result = "数据写入失败";
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                sr.status = "Fail";
                sr.result = exp.Message;
            }
            finally
            {

            }
            return sr;
        }
    }
}