using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data.OracleClient;
using System.Xml;
using System.Data;

namespace HdisDataBase
{
    public class OracleDB : IDataBase
    {

        private XmlDocument doc;
        private OracleConnection conn;
        private OracleCommand command;
        private OracleParameter param;
        private System.Data.IDataReader reader;
        private string m_DBName = string.Empty;
        private string m_DBUserName = string.Empty;
        private string m_DBPwd = string.Empty;

        static private int DBOpenTimes = 0;
        static private int DBCloseTimes = 0;
        static private int ReCloseTimes = 0;

        public OracleDB()
        {
            conn = new OracleConnection();
            command = new OracleCommand();
            param = new OracleParameter("img", OracleType.Blob);
            doc = new XmlDocument();
        }

        public void ConnectDB()
        {
            try
            {
                string strDbType = "ORACLE";
                DBFactory.CreateDB(strDbType);
                if (!HdisCommon.InitConfig.isReadConfig)
                {
                    HdisCommon.InitConfig ic = new HdisCommon.InitConfig();
                    ic.GetIniConfig();
                }
                conn.ConnectionString = HdisCommon.InitConfig.ConnectionString;
                conn.Open();
                DBOpenTimes++;
                HdisCommon.Log.Logging("{0}", "ConnectDB()" + " DBOpenTimes:" + DBOpenTimes);
                command.Connection = this.conn;
            }
            catch(Exception ex)
            {

            }
        }

        public void OpenDB()
        {
            if (this.conn.State.ToString() == "Closed")
            {
                this.ConnectDB();
            }
        }

        public void CloseDB()
        {
            command.Dispose();
            conn.Close();
            conn.Dispose();
            DBCloseTimes++;
            HdisCommon.Log.Logging("{0}", "CloseDB()" + " DBCloseTimes:" + DBCloseTimes);
        }

        public void CloseReader()
        {
            reader.Close();
            reader.Dispose();
            ReCloseTimes++;
            HdisCommon.Log.Logging("{0}", "CloseReader()" + " ReCloseTimes:" + ReCloseTimes);
        }

        public void DBCommit()
        {
            this.command.Transaction.Commit();
        }

        public System.Data.IDataReader ExecuteSQLQuery(string strSQL)
        {
            this.OpenDB();
            this.command.CommandText = strSQL;
            this.command.CommandType = System.Data.CommandType.Text;
            try
            {
                this.reader = this.command.ExecuteReader();
            }
            catch (Exception ex)
            {
                HdisCommon.Log.Logging(HdisCommon.Log.Loglevel.Error,"{0}", "ExecuteSQLQuery():" + ex);
            }
            return reader;
        }

        public DataSet ExecuteSqlToDataSet(string strSQL)
        {
            DataSet ds = new DataSet();
            try
            {
                this.OpenDB();
                OracleDataAdapter da = new OracleDataAdapter(strSQL, conn.ConnectionString);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }


        public int ExecuteSQL(string strSQL)
        {
            int i = 0;
            this.OpenDB();
            this.command.CommandText = strSQL;
            this.command.CommandType = System.Data.CommandType.Text;
            try
            {
                this.command.ExecuteNonQuery();
                i = 0;
            }
            catch
            {
                return -1;
            }
            return i;
        }



        public int ExecuteSQL(string strSQL, byte[] bt, string paramName)
        {
            try
            {
                this.OpenDB();
                this.command.CommandText = strSQL;
                this.command.CommandType = System.Data.CommandType.Text;
                this.param = new OracleParameter(paramName, OracleType.Blob);
                this.param.Value = bt;
                this.command.Parameters.Add(this.param);
                int ret = this.command.ExecuteNonQuery();
                this.command.Parameters.Clear();
                return ret;

            }
            catch
            {
                return -1;
            }
        }

        public int ExecuteSQL(string strSQL, byte[] bt)
        {
            try
            {
                this.OpenDB();
                this.command.CommandText = strSQL;
                this.command.CommandType = System.Data.CommandType.Text;
                this.param = new OracleParameter("img", OracleType.Blob);
                this.param.Value = bt;
                this.command.Parameters.Add(this.param);
                int ret = this.command.ExecuteNonQuery();
                this.command.Parameters.Clear();
                return ret;

            }
            catch
            {
                return -1;
            }
        }
        public object GetValue(int i)
        {
            return this.reader.GetValue(i);
        }

        public string GetValue(string str)
        {
            throw new NotImplementedException();
        }

        public int GetFieldCount()
        {
            return this.reader.FieldCount;
        }





    }
}
