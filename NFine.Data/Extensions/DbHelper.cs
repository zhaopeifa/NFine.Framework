/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace NFine.Data.Extensions
{
    public class DbHelper
    {
        private static string connstring = ConfigurationManager.ConnectionStrings["NFineDbContext"].ConnectionString;
        public static int ExecuteSqlCommand(string cmdText)
        {
            using (DbConnection conn = new SqlConnection(connstring))
            {
                DbCommand cmd = new SqlCommand();
                PrepareCommand(cmd, conn, null, CommandType.Text, cmdText, null);
                return cmd.ExecuteNonQuery();
            }
        }
        private static void PrepareCommand(DbCommand cmd, DbConnection conn, DbTransaction isOpenTrans, CommandType cmdType, string cmdText, DbParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (isOpenTrans != null)
                cmd.Transaction = isOpenTrans;
            cmd.CommandType = cmdType;
            if (cmdParms != null)
            {
                cmd.Parameters.AddRange(cmdParms);
            }
        }

        /// <summary>
        /// 返回DataTable
        /// </summary>
        /// <param name="sql">所用的sql语句</param>
        /// <param name="param">可变，可以传参也可以不传参数</param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(string sql, params SqlParameter[] param)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(connstring))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, con))
                {
                    if (param != null)
                    {
                        //添加参数
                        adapter.SelectCommand.Parameters.AddRange(param);
                    }
                    adapter.Fill(dt);
                }
            }
            return dt;
        }


        /// <summary>
        /// 执行查询，返回SqlDataReader对象
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(string sql, params SqlParameter[] param)
        {
            SqlDataReader reader = null;
            using (SqlConnection con = new SqlConnection(connstring))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    if (param != null)
                    {
                        cmd.Parameters.AddRange(param);
                    }


                    con.Open();

                    reader = cmd.ExecuteReader();
                }
            }
            return reader;
        }
    }
}
