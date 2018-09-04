using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;

namespace AsyMonitor
{
    class SqlHelper
    {
        private static string _connStr = "data source=.;initial catalog=snwzhykt;user id=sa;pwd=123456;Connection Timeout=15;MultipleActiveResultSets=true";//
        //private static string _connStr = "data source=192.168.1.183;initial catalog=SDNX;user id=sa;pwd=123456;Connection Timeout=15;MultipleActiveResultSets=true";
        private static SqlConnection _conn;
        private static bool _connected = false;

        public static bool Connected { get => _connected; set => _connected = value; }

        public static bool Init()
        {
            string iniFileName = System.AppDomain.CurrentDomain.BaseDirectory + "SysData.ini";
            string catalog = IniFile.INIGetStringValue(iniFileName, "SYSCONFIG", "SQLDataBase", "");
            string source = IniFile.INIGetStringValue(iniFileName, "SYSCONFIG", "SQLServer", "");
            string user = IniFile.INIGetStringValue(iniFileName, "SYSCONFIG", "SQLSysUser", "");
            string pwd = IniFile.INIGetStringValue(iniFileName, "SYSCONFIG", "SQLSysPassword", "");
            return Connect(catalog, source, user, pwd);
        }

        public static bool Connect(string catalog, string source, string user, string pwd)
        {
            _connStr = String.Format("data source={0};initial catalog={1};user id={2};pwd={3};Connection Timeout=5;MultipleActiveResultSets=true", source, catalog, user, pwd);
            if (Connected)
            {
                Close();
            }
            _conn = new SqlConnection(_connStr);
            try
            {
                _conn.Open();
                Connected = true;
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message);
                Connected = false;
            }
            catch (Exception e)
            {
                //记录日志，退出
                MessageBox.Show(e.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                Connected = false;
            }
            return Connected;
        }

        public static bool Close()
        {
            _conn.Close();
            _conn.Dispose();
            Connected = false;

            return true;
        }

        //只用来执行查询结果比较小的sql
        public static DataSet ExecuteDataSet(string sql)
        {
            using (SqlCommand cmd = _conn.CreateCommand())
            {
                cmd.CommandText = sql;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet dataset = new DataSet();
                adapter.Fill(dataset);
                return dataset;
            }
        }

        public static DataTable ExecuteDataTable(string sql, SqlParameter[] parameters)
        {
            using (SqlCommand cmd = _conn.CreateCommand())
            {
                cmd.CommandText = sql;
                if (null != parameters)
                {
                    cmd.Parameters.AddRange(parameters);
                }

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet dataset = new DataSet();
                adapter.Fill(dataset);
                return dataset.Tables[0];
            }
        }

        public static bool ExecuteSql(string sql, SqlParameter[] parameters)
        {
            using (SqlCommand cmd = _conn.CreateCommand())
            {
                cmd.CommandText = sql;
                if (null != parameters)
                {
                    cmd.Parameters.AddRange(parameters);
                }

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet dataset = new DataSet();
                adapter.Fill(dataset);
                if(dataset.Tables.Count > 0)
                {
                    if(dataset.Tables[0].Rows.Count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
