using System;
using System.Collections.Generic;
 
using System.Text;
using System.Collections;

namespace HdisDataBase
{
    public interface IDataBase
    {
        void ConnectDB();
        void OpenDB();
        void CloseDB();
        void CloseReader();
        int ExecuteSQL(string strSQL);
        int ExecuteSQL(string strSQL, byte[] bt);
        int ExecuteSQL(string strSQL, byte[] bt, string paramName);
        System.Data.IDataReader ExecuteSQLQuery(string strSQL);
        int GetFieldCount();
    }

    public static class DBFactory
    {
        public static IDataBase CreateDB(string DBName)
        {
            switch (DBName.ToUpper())
            {
                //case "ACCESS":
                    //return new AccessDB();
                case "ORACLE":
                    return new OracleDB();
                ////case "SQLSERVER":
                ////    return new SqlServerDB();
                ////case "MYSQL":
                ////    return new MySqlDB();
                ////case "DB2":
                ////    return new DB2DB();
                default:
                    return null;
            }
        }
    }

    
}
