using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace HdisBizLogic
{
  public  class ManagementBizlogic
    {
        public System.Data.IDataReader Reader;

        public HdisDataBase.OracleDB oracleDb = new HdisDataBase.OracleDB();


        /// <summary>
        /// 全局变量Sql哈希表
        /// </summary>
        public static System.Collections.Hashtable htSql = new System.Collections.Hashtable();

        /// <summary>
        /// 执行插入、修改、删除
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public int ExecSQL(string strSql)
        {
            int iReturn = 0;
            try
            {
                strSql = strSql.Replace("--", "~~");
                iReturn = this.oracleDb.ExecuteSQL(strSql);
                if (iReturn > -1)
                {
                    HdisCommon.Log.Logging("执行Sql成功：{0}", strSql);
                    return iReturn;
                }
                else
                {
                    HdisCommon.Log.Logging(HdisCommon.Log.Loglevel.Error, "执行Sql失败：{0}", strSql);
                    return -1;
                }
            }
            catch (Exception e)
            {
                HdisCommon.Log.Logging(HdisCommon.Log.Loglevel.Error, "错误信息：{0}",e.Message);
                HdisCommon.Log.Logging(HdisCommon.Log.Loglevel.Error, "执行Sql失败：{0}", strSql);
                return -1;
            }
        }

        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public int ExecSQLQuery(string strSql)
        {
            try
            {
                this.Reader = this.oracleDb.ExecuteSQLQuery(strSql);
            }
            catch (Exception e)
            {
                this.Reader.Close();
                HdisCommon.Log.Logging(HdisCommon.Log.Loglevel.Error, "错误信息：{0}",e.Message);
                HdisCommon.Log.Logging(HdisCommon.Log.Loglevel.Error, "执行Sql失败：{0}", strSql);
                return -1;
            }
            HdisCommon.Log.Logging("执行Sql成功：{0}", strSql);
            return 0;
        }

        /// <summary>
        /// 执行查询返回DataSet
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public DataSet ExecuteSqlToDataSet(string strSql)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = this.oracleDb.ExecuteSqlToDataSet(strSql);
            }
            catch (Exception e)
            {
                HdisCommon.Log.Logging("错误信息：{0}", e.Message);
                HdisCommon.Log.Logging(HdisCommon.Log.Loglevel.Error, "执行Sql失败:{0}", strSql);
                return ds;
            }
            HdisCommon.Log.Logging("执行Sql成功:{0}", strSql);
            return ds;
        }


        /// <summary>
        /// 获取序列号
        /// </summary>
        /// <param name="SeqName"></param>
        /// <returns></returns>
        public string GetSequenceNo(string SeqName)
        {
            string strSeqNo = string.Empty;
            string strSQL = @"select {0}.nextval from dual";
            strSQL = string.Format(strSQL, SeqName);
            if (this.ExecSQLQuery(strSQL) == -1)
            {
                return "-1";
            }
            while (this.Reader.Read())
            {
                strSeqNo = this.Reader[0].ToString();
            }
            return strSeqNo;
        }

        /// <summary>
        /// 加载所有的查询Sql
        /// </summary>
        /// <returns></returns>
        public int LoadSql()
        {
            string strSQL = "select sqlindex, sqlcontent, memo from hdis_com_sql";
            try
            {
                this.ExecSQLQuery(strSQL);
                while (this.Reader.Read())
                {
                    HdisModels.Object objValue = new HdisModels.Object();
                    objValue.Id = this.Reader[0].ToString();
                    objValue.Name = this.Reader[1].ToString();
                    objValue.Name = objValue.Name.Replace("\r", " ");
                    objValue.Name = objValue.Name.Replace("\t", " ");
                    htSql.Add(objValue.Id, objValue.Name);
                }
            }
            catch
            {
                this.Reader.Close();
                return -1;
            }
            this.Reader.Close();
            return 0;
        }

        /// <summary>
        /// 通过ID查找Sql
        /// </summary>
        /// <param name="SqlID"></param>
        /// <param name="Sql"></param>
        /// <returns></returns>
        public int LoadSql(string SqlID, ref string Sql)
        {
            string strSQL = "select sqlindex, sqlcontent, memo from hdis_com_sql where sqlindex = '{0}'";
            strSQL = string.Format(strSQL, SqlID);
            this.ExecSQLQuery(strSQL);
            if (this.Reader.Read())
            {
                HdisModels.Object objValue = new HdisModels.Object();
                objValue.Id = this.Reader[0].ToString();
                objValue.Name = this.Reader[1].ToString();
                objValue.Name = objValue.Name.Replace("\r", " ");
                objValue.Name = objValue.Name.Replace("\t", " ");
                Sql = objValue.Name;
                htSql.Add(objValue.Id, objValue.Name);
                this.Reader.Close();
                return 0;
            }
            else
            {
                this.Reader.Close();
                return -1;
            }
        }

        /// <summary>
        /// 获得sql语句
        /// </summary>
        /// <param name="index"></param>
        /// <param name="Sql"></param>
        /// <returns></returns>
        public int GetSql(string index, ref string Sql)
        {
            try
            {
                Sql = htSql[index].ToString();
            }
            catch
            {
                Sql = "";
            }
            if (Sql == "")
            {
                return this.LoadSql(index, ref Sql);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 获取服务器时间
        /// </summary>
        /// <returns></returns>
        public DateTime GetSystemDateTime()
        {
            string strSql = "SELECT sysdate FROM dual";
            DateTime state = new DateTime();
            try
            {
                this.ExecSQLQuery(strSql);
                if (this.Reader.Read())
                    state = DateTime.Parse(this.Reader[0].ToString());
            }
            catch
            {
                throw;
            }
            finally
            {
                this.Reader.Close();
            }
            return state;
        }






    }
}
