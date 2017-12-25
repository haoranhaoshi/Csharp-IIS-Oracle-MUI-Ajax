using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HdisBizLogic.Doctor
{
    public class OrderLogic : ManagementBizlogic
    {
        /// <summary>
        /// 查询所有医嘱
        /// </summary>
        /// <param name="strCardId"></param>
        /// <returns></returns>
        public List<HdisModels.Doctor.OrderModel> QueryOrder(string cardId)
        {
            List<HdisModels.Doctor.OrderModel> list = new List<HdisModels.Doctor.OrderModel>();
            HdisModels.Doctor.OrderModel model = null;
            string sql = @"select * from HDIS_DOC_RECIPEDETAIL t where cardid = '{0}'  and t.oper_date>sysdate-45 order by testDate desc ,t.recipe_no,t.recipe_seq";
            sql = string.Format(sql, cardId);
            try
            {
                if (this.ExecSQLQuery(sql) < 0)
                {
                    return list;
                }
                while (this.Reader.Read())
                {
                    model = new HdisModels.Doctor.OrderModel();
                    model.CARDID = Reader["CARDID"].ToString();
                    model.TESTDATE = Reader["TESTDATE"].ToString();
                    model.SEQID = Reader["SEQID"].ToString();
                    model.ITEM_CODE = Reader["ITEM_CODE"].ToString();
                    model.ITEM_NAME = Reader["ITEM_NAME"].ToString();
                    model.SPECS = Reader["SPECS"].ToString();
                    model.DRUG_FLAG = Reader["DRUG_FLAG"].ToString();
                    model.CLASS_CODE = Reader["CLASS_CODE"].ToString();
                    model.FEE_CODE = Reader["FEE_CODE"].ToString();
                    model.UNIT_PRICE = int.Parse(Reader["UNIT_PRICE"].ToString());
                    model.QTY = int.Parse(Reader["QTY"].ToString());
                    model.PACK_QTY = int.Parse(Reader["PACK_QTY"].ToString());
                    model.ITEM_UNIT = Reader["ITEM_UNIT"].ToString();
                    model.TOT_COST = int.Parse(Reader["TOT_COST"].ToString());
                    model.OWN_COST = int.Parse(Reader["OWN_COST"].ToString());
                    model.PAY_COST = int.Parse(Reader["PAY_COST"].ToString());
                    model.PUB_COST = int.Parse(Reader["PUB_COST"].ToString());
                    model.BASE_DOSE = int.Parse(Reader["BASE_DOSE"].ToString());
                    model.ONCE_DOSE = int.Parse(Reader["ONCE_DOSE"].ToString());
                    model.ONCE_UNIT = Reader["ONCE_UNIT"].ToString();
                    model.DOSE_MODEL_CODE = Reader["DOSE_MODEL_CODE"].ToString();
                    model.FREQUENCY_CODE = Reader["FREQUENCY_CODE"].ToString();
                    model.FREQUENCY_NAME = Reader["FREQUENCY_NAME"].ToString();
                    model.USAGE_CODE = Reader["USAGE_CODE"].ToString();
                    model.USAGE_NAME = Reader["USAGE_NAME"].ToString();
                    model.EXEC_DPCD = Reader["EXEC_DPCD"].ToString();
                    model.EXEC_DPNM = Reader["EXEC_DPNM"].ToString();
                    model.MAIN_DRUG = Reader["MAIN_DRUG"].ToString();
                    model.COMB_NO = Reader["COMB_NO"].ToString();
                    model.HYPOTEST = Reader["HYPOTEST"].ToString();
                    model.REMARK = Reader["REMARK"].ToString();
                    model.DOCT_CODE = Reader["DOCT_CODE"].ToString();
                    model.DOCT_NAME = Reader["DOCT_NAME"].ToString();
                    model.DOCT_DPCD = Reader["DOCT_DPCD"].ToString();
                    try
                    {
                        model.OPER_DATE = DateTime.Parse(Reader["OPER_DATE"].ToString());
                    }
                    catch
                    {
                    }
                    model.STATUS = Reader["STATUS"].ToString();
                    model.CANCEL_USERID = Reader["CANCEL_USERID"].ToString();
                    try
                    {
                        model.CANCEL_DATE = DateTime.Parse(Reader["CANCEL_DATE"].ToString());
                    }
                    catch
                    {
                    }
                    model.EMC_FLAG = Reader["EMC_FLAG"].ToString();
                    model.LAB_TYPE = Reader["LAB_TYPE"].ToString();
                    model.CHECK_BODY = Reader["CHECK_BODY"].ToString();
                    model.APPLY_NO = Reader["APPLY_NO"].ToString();
                    model.SUBTBL_FLAG = Reader["SUBTBL_FLAG"].ToString();
                    model.NEED_CONFIRM = Reader["NEED_CONFIRM"].ToString();
                    model.CONFIRM_CODE = Reader["CONFIRM_CODE"].ToString();
                    model.CONFIRM_DEPT = Reader["CONFIRM_DEPT"].ToString();
                    try
                    {
                        model.CONFIRM_DATE = DateTime.Parse(Reader["CONFIRM_DATE"].ToString());
                    }
                    catch
                    {
                    }
                    model.CHARGE_FLAG = Reader["CHARGE_FLAG"].ToString();
                    model.CHARGE_CODE = Reader["CHARGE_CODE"].ToString();
                    try
                    {
                        model.CHARGE_DATE = DateTime.Parse(Reader["CHARGE_DATE"].ToString());
                    }
                    catch
                    {
                    }
                    model.RECIPE_NO = Reader["RECIPE_NO"].ToString();
                    model.RECIPE_SEQ = int.Parse(Reader["RECIPE_SEQ"].ToString());
                    model.PHAMARCY_CODE = Reader["PHAMARCY_CODE"].ToString();
                    model.SORT_ID = int.Parse(Reader["SORT_ID"].ToString());
                    model.CLINICDIAG_CODE = Reader["CLINICDIAG_CODE"].ToString();
                    model.CLINICDIAG_NAME = Reader["CLINICDIAG_NAME"].ToString();
                    model.CLINICOTHERDIAG1_CODE = Reader["CLINICOTHERDIAG1_CODE"].ToString();
                    model.CLINICOTHERDIAG1_NAME = Reader["CLINICOTHERDIAG1_NAME"].ToString();
                    model.CLINICOTHERDIAG2_CODE = Reader["CLINICOTHERDIAG2_CODE"].ToString();
                    model.CLINICOTHERDIAG2_NAME = Reader["CLINICOTHERDIAG2_NAME"].ToString();
                    model.MO_ORADER = Reader["MO_ORADER"].ToString();
                    list.Add(model);
                }
                return list;
            }
            catch
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
        /// 查询未执行与待执行医嘱
        /// </summary>
        /// <param name="strCardId"></param>
        /// <returns></returns>
        public List<HdisModels.Doctor.OrderModel> QueryExecuteOrder(string strCardId,string testDate)
        {
            List<HdisModels.Doctor.OrderModel> list = new List<HdisModels.Doctor.OrderModel>();
            HdisModels.Doctor.OrderModel model = null;
            string sql = @"select * from HDIS_DOC_RECIPEDETAIL t left join (select personid, personname from hdis_rol_person ) h on h.personid=t.confirm_code where t.cardid = '{0}' and t.testdate='{1}'  and t.oper_date>sysdate-45 and (t.status='1' or t.status='2') order by t.status,t.CONFIRM_DATE,t.comb_no";
            sql = string.Format(sql, strCardId, testDate);
            try
            {
                if (this.ExecSQLQuery(sql) < 0)
                {
                    return list;
                }
                while (this.Reader.Read())
                {
                    model = new HdisModels.Doctor.OrderModel();
                    model.CARDID = Reader["CARDID"].ToString();
                    model.TESTDATE = Reader["TESTDATE"].ToString();
                    model.SEQID = Reader["SEQID"].ToString();
                    model.ITEM_CODE = Reader["ITEM_CODE"].ToString();
                    model.ITEM_NAME = Reader["ITEM_NAME"].ToString();
                    model.SPECS = Reader["SPECS"].ToString();
                    model.DRUG_FLAG = Reader["DRUG_FLAG"].ToString();
                    model.CLASS_CODE = Reader["CLASS_CODE"].ToString();
                    model.FEE_CODE = Reader["FEE_CODE"].ToString();
                    model.UNIT_PRICE = int.Parse(Reader["UNIT_PRICE"].ToString());
                    model.QTY = int.Parse(Reader["QTY"].ToString());
                    model.PACK_QTY = int.Parse(Reader["PACK_QTY"].ToString());
                    model.ITEM_UNIT = Reader["ITEM_UNIT"].ToString();
                    model.TOT_COST = int.Parse(Reader["TOT_COST"].ToString());
                    model.OWN_COST = int.Parse(Reader["OWN_COST"].ToString());
                    model.PAY_COST = int.Parse(Reader["PAY_COST"].ToString());
                    model.PUB_COST = int.Parse(Reader["PUB_COST"].ToString());
                    model.BASE_DOSE = int.Parse(Reader["BASE_DOSE"].ToString());
                    model.ONCE_DOSE = Decimal.Parse(Reader["ONCE_DOSE"].ToString());
                    model.ONCE_UNIT = Reader["ONCE_UNIT"].ToString();
                    model.DOSE_MODEL_CODE = Reader["DOSE_MODEL_CODE"].ToString();
                    model.FREQUENCY_CODE = Reader["FREQUENCY_CODE"].ToString();
                    model.FREQUENCY_NAME = Reader["FREQUENCY_NAME"].ToString();
                    model.USAGE_CODE = Reader["USAGE_CODE"].ToString();
                    model.USAGE_NAME = Reader["USAGE_NAME"].ToString();
                    model.EXEC_DPCD = Reader["EXEC_DPCD"].ToString();
                    model.EXEC_DPNM = Reader["EXEC_DPNM"].ToString();
                    model.MAIN_DRUG = Reader["MAIN_DRUG"].ToString();
                    model.COMB_NO = Reader["COMB_NO"].ToString();
                    model.HYPOTEST = Reader["HYPOTEST"].ToString();
                    model.REMARK = Reader["REMARK"].ToString();
                    model.DOCT_CODE = Reader["DOCT_CODE"].ToString();
                    model.DOCT_NAME = Reader["DOCT_NAME"].ToString();
                    model.DOCT_DPCD = Reader["DOCT_DPCD"].ToString();
                    try
                    {
                        model.OPER_DATE = DateTime.Parse(Reader["OPER_DATE"].ToString());
                    }
                    catch
                    {
                    }
                    model.STATUS = Reader["STATUS"].ToString();
                    model.CANCEL_USERID = Reader["CANCEL_USERID"].ToString();
                    try
                    {
                        model.CANCEL_DATE = DateTime.Parse(Reader["CANCEL_DATE"].ToString());
                    }
                    catch
                    {
                    }
                    model.EMC_FLAG = Reader["EMC_FLAG"].ToString();
                    model.LAB_TYPE = Reader["LAB_TYPE"].ToString();
                    model.CHECK_BODY = Reader["CHECK_BODY"].ToString();
                    model.APPLY_NO = Reader["APPLY_NO"].ToString();
                    model.SUBTBL_FLAG = Reader["SUBTBL_FLAG"].ToString();
                    model.NEED_CONFIRM = Reader["NEED_CONFIRM"].ToString();
                    model.CONFIRM_CODE = Reader["CONFIRM_CODE"].ToString();
                    model.CONFIRM_NAME = Reader["PERSONNAME"].ToString();
                    model.CONFIRM_DEPT = Reader["CONFIRM_DEPT"].ToString();
                    try
                    {
                        model.CONFIRM_DATE = DateTime.Parse(Reader["CONFIRM_DATE"].ToString());
                    }
                    catch
                    {
                    }
                    model.CHARGE_FLAG = Reader["CHARGE_FLAG"].ToString();
                    model.CHARGE_CODE = Reader["CHARGE_CODE"].ToString();
                    try
                    {
                        model.CHARGE_DATE = DateTime.Parse(Reader["CHARGE_DATE"].ToString());
                    }
                    catch
                    {
                    }
                    model.RECIPE_NO = Reader["RECIPE_NO"].ToString();
                    model.RECIPE_SEQ = int.Parse(Reader["RECIPE_SEQ"].ToString());
                    model.PHAMARCY_CODE = Reader["PHAMARCY_CODE"].ToString();
                    model.SORT_ID = int.Parse(Reader["SORT_ID"].ToString());
                    model.CLINICDIAG_CODE = Reader["CLINICDIAG_CODE"].ToString();
                    model.CLINICDIAG_NAME = Reader["CLINICDIAG_NAME"].ToString();
                    model.CLINICOTHERDIAG1_CODE = Reader["CLINICOTHERDIAG1_CODE"].ToString();
                    model.CLINICOTHERDIAG1_NAME = Reader["CLINICOTHERDIAG1_NAME"].ToString();
                    model.CLINICOTHERDIAG2_CODE = Reader["CLINICOTHERDIAG2_CODE"].ToString();
                    model.CLINICOTHERDIAG2_NAME = Reader["CLINICOTHERDIAG2_NAME"].ToString();
                    model.MO_ORADER = Reader["MO_ORADER"].ToString();
                    model.MEMO = Reader["MEMO"].ToString();
                    list.Add(model);
                }
                return list;
            }
            catch
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
        /// 审核该患者所有的医嘱
        /// </summary>
        /// <param name="strCardId">透析号</param>
        /// <param name="strRecipeNo">处方号</param>
        /// <param name="strRecipeSeq">处方内流水号</param>
        /// <param name="strConfimCode">确认人</param>
        /// <param name="strConfimDept">确认科室</param>
        /// <returns></returns>
        public int ExecuteOrder(string strCardId, string strRecipeNo, string strRecipeSeq, string strConfimCode, string strConfimDept)
        {
            string sql = @"update hdis_doc_recipedetail t
   set t.status       = '2',
       t.confirm_code = '{3}',
       t.confirm_dept = '{4}',
       t.confirm_date = sysdate
 where t.cardid = '{0}'
   and t.recipe_no = '{1}'
   and t.recipe_seq = '{2}'  
   and t.status = '1'";
            try
            {
                sql = string.Format(sql, strCardId, strRecipeNo, strRecipeSeq, strConfimCode, strConfimDept);
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

    }
}
