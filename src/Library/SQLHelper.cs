using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Library
{
    public class SQLHelper
    {
        public static int ExecuteNonQuery(string sql)
        {
            string constr = "server=(local);database=demo;uid=sa;pwd=sa;";
            SqlConnection con = null;
            SqlCommand cmd = null;
            try
            {
                con = new SqlConnection(constr);
                con.Open();

                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                int iResult = cmd.ExecuteNonQuery();
                return iResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                con.Dispose();
            }
        }
    }
}
