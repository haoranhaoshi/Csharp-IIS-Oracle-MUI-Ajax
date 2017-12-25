using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HdisBizLogic.Proc
{
    public class ProcResultLogic : ManagementBizlogic
    {


        /// <summary>
        /// 保存透析结果行
        /// </summary>
        /// <param name="model">透析结果实体</param>
        /// <returns></returns>
        public int SaveDialysisResult(HdisModels.Proc.ProcResult model)
        {
            string sql = string.Empty;
            sql = @"insert into hdis_proc_result(cardid,testdate,seqid,resultseq,bedid,machineid,nurseid,nursename,resulttime) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}',to_date('{8}','yyyy-MM-dd hh24:mi:ss'))";
            try
            {
                sql = string.Format(sql, model.CARDID, model.TESTDATE, model.SEQID,
                    model.RESULTSEQ, model.BEDID, model.MACHINEID, model.NURSEID, model.NURSENAME, model.RESULTTIME);
                if (this.ExecSQL(sql) < 0)
                {
                    return -1;
                }
                return 1;
            }
            catch
            {
                return -1;
            }
            finally
            {
                this.oracleDb.CloseDB();
            }
        }

        /// <summary>
        ///  透析结果自动保存
        /// </summary>
        /// <param name="strItemId">项目编码</param>
        /// <param name="strResult">结果</param>
        /// <param name="strResultSeq">结果序列号</param>
        /// <returns></returns>
        public int UpdateDialysisResultByResultSeq(string strItemId, string strResult, string strResultSeq)
        {
            string sql = string.Empty;
            if (strItemId != "RESULTTIME")
            {
                sql = @"update hdis_proc_result  set {1}='{2}' where resultseq='{0}'";
            }
            else
            {
                sql = @"update hdis_proc_result  set {1}=to_date('{2}','yyyy-MM-dd hh24:mi:ss') where resultseq='{0}'";
            }           
            try
            {
                sql = string.Format(sql, strResultSeq, strItemId, strResult);
                return this.ExecSQL(sql);
            }
            catch (Exception ex)
            {
                return -1;
            }
            finally
            {
                this.oracleDb.CloseDB();
            }
        }

        /// <summary>
        /// 删除透析结果
        /// </summary>
        /// <param name="strResultSeq">结果序列号</param>
        /// <returns></returns>
        public int DeleteDialysisResult(string strResultSeq)
        {
            string sql = string.Empty;
            sql = @"delete from hdis_proc_result where resultseq = '{0}'";          
            try
            {
                sql = string.Format(sql, strResultSeq);
                return this.ExecSQL(sql);
            }
            catch (Exception ex)
            {
                return -1;
            }
            finally
            {
                this.oracleDb.CloseDB();
            }
        }

        /// <summary>
        /// 查询透析结果
        /// </summary>
        /// <param name="model">患者基本信息实体</param>
        /// <returns></returns>
        public List<HdisModels.Proc.ProcResult> QueryDialysisResultList(string cardid, string testdate)
        {
            List<HdisModels.Proc.ProcResult> list = new List<HdisModels.Proc.ProcResult>();
            HdisModels.Proc.ProcResult model = null;
            string sql = string.Empty;
            sql = @"select * from hdis_proc_result a where a.cardid='{0}' and a.testdate='{1}' order by a.resultseq";
            try
            {
                sql = string.Format(sql, cardid, testdate);
                if (this.ExecSQLQuery(sql) < 0)
                {
                    return null;
                }
                while (this.Reader.Read())
                {
                    model = new HdisModels.Proc.ProcResult();
                    model.CARDID = this.Reader["CARDID"].ToString();
                    model.TESTDATE = this.Reader["TESTDATE"].ToString();
                    model.SEQID = this.Reader["SEQID"].ToString();
                    model.RESULTSEQ = this.Reader["RESULTSEQ"].ToString();
                    model.BEDID = this.Reader["BEDID"].ToString();
                    model.MACHINEID = this.Reader["MACHINEID"].ToString();
                    model.SPO2 = this.Reader["SPO2"].ToString();
                    model.SPRESSURE = this.Reader["SPRESSURE"].ToString();
                    model.DPRESSURE = this.Reader["DPRESSURE"].ToString();
                    model.PULSE = this.Reader["PULSE"].ToString();
                    model.HEPARIN = this.Reader["HEPARIN"].ToString();
                    model.BLOODFOLW = this.Reader["BLOODFOLW"].ToString();
                    model.ARTERIALPRESSURE = this.Reader["ARTERIALPRESSURE"].ToString();
                    model.VENOUSPRESSURE = this.Reader["VENOUSPRESSURE"].ToString();
                    model.TRANSMEMBRANEPRESSURE = this.Reader["TRANSMEMBRANEPRESSURE"].ToString();
                    model.ULTRAFILTRATIONVOLUME = this.Reader["ULTRAFILTRATIONVOLUME"].ToString();
                    model.RESULTTIME = Convert.ToDateTime(this.Reader["RESULTTIME"]);
                    model.NURSEID = this.Reader["NURSEID"].ToString();
                    model.NURSENAME = this.Reader["NURSENAME"].ToString();
                    list.Add(model);
                }
                return list;
            }
            catch (Exception ex)
            {
                return list;
            }
            finally
            {
                this.oracleDb.CloseReader();
                this.oracleDb.CloseDB();
            }
        }

        /// <summary>
        /// 通过SEQ_HDIS_RESULTSEQ序列获取结果序列号
        /// </summary>
        /// <returns></returns>
        public string GetResultSeq()
        {
            string strResultSeq = string.Empty;
            string sql = string.Empty;
            sql = @"select seq_hdis_resultseq.nextval from dual";
            try
            {
                if (this.ExecSQLQuery(sql) < 0)
                {                  
                    return string.Empty;
                }
                if (this.Reader.Read())
                {
                    strResultSeq = this.Reader[0].ToString();
                }
            }
            catch (Exception ex)
            {                
                return string.Empty;
            }
            finally
            {
                this.oracleDb.CloseReader();
                this.oracleDb.CloseDB();
            }
            return strResultSeq;
        }
    }
}
