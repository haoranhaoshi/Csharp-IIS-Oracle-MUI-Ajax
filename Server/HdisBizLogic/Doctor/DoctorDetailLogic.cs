using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HdisModels.Doctor;

namespace HdisBizLogic.Doctor
{
    public class DoctorDetailLogic : ManagementBizlogic
    {

        public DoctorDetail QueryDoctorDetail(string testDate, string cardId)
        {
            
            DoctorDetail model = null;
            string sql = @"SELECT t.*,h.testdate as LASTTESTDATE FROM HDIS_DOC_DETAIL t left join (select * from (select * from hdis_doc_detail t where cardid='{1}' and testdate<'{0}' order by testdate desc) where rownum=1) h on t.CARDID = h.CARDID WHERE t.cardid='{1}' and t.TESTDATE ='{0}'";


            sql = string.Format(sql, testDate, cardId);
            try
            {
                if (this.ExecSQLQuery(sql) < 0)
                {
                    return model;
                }
                if (this.Reader.Read())
                {
                    model = ReaderToModel();
                }
                return model;
            }
            catch (Exception ex)
            {
                return model;
            }
            finally
            {
                this.oracleDb.CloseReader();
                this.oracleDb.CloseDB();
            }
        }

        /// <summary>
        /// 护士置确认
        /// </summary>
        /// <param name="cardId"></param>
        /// <param name="testDate"></param>
        /// <returns></returns>
        public int EditDocConfirmState(string cardId, string testDate)
        {
            string sql = @" UPDATE HDIS_DOC_DETAIL SET STATE = '1'  WHERE CARDID='{0}' AND TESTDATE = '{1}'";

            sql = string.Format(sql, cardId, testDate.Replace("-", ""));
            if (this.ExecSQL(sql) > -1)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 实体与数据库对应
        /// </summary>
        /// <returns></returns>
        private DoctorDetail ReaderToModel()
        {
            DoctorDetail model = new DoctorDetail();

            model.CARDID = Reader["CARDID"].ToString();
            model.TESTDATE = Reader["TESTDATE"].ToString();
            model.SEQID = Reader["SEQID"].ToString();
            model.DIALYSISTIMES = Reader["DIALYSISTIMES"].ToString();
            model.PATIENTID = Reader["PATIENTID"].ToString();
            model.PATIENTTYPE = Reader["PATIENTTYPE"].ToString();
            model.PATIENTNAME = Reader["PATIENTNAME"].ToString();
            model.PATIENTSEX = Reader["PATIENTSEX"].ToString();
            model.PATIENTAGE = Reader["PATIENTAGE"].ToString();
            model.SPRESSURE = Reader["SPRESSURE"].ToString();
            model.DPRESSURE = Reader["DPRESSURE"].ToString();
            model.PULSE = Reader["PULSE"].ToString();
            model.BREATHING = Reader["BREATHING"].ToString();
            model.TEMPERATURE = Reader["TEMPERATURE"].ToString();
            model.DRYWEIGHT = Reader["DRYWEIGHT"].ToString();
            model.FRONTWEIGHT = Reader["FRONTWEIGHT"].ToString();
            model.INCREASEWEIGHT = Reader["INCREASEWEIGHT"].ToString();
            model.AFTERWEIGHT = Reader["AFTERWEIGHT"].ToString();
            model.FACTSFILTRATIONQUANTITY = Reader["FACTSFILTRATIONQUANTITY"].ToString();
            model.MEASUREMENTID = Reader["MEASUREMENTID"].ToString();
            model.MEASUREMENTNAME = Reader["MEASUREMENTNAME"].ToString();
            model.MEASUREMENTTIME = DateTime.Parse(Reader["MEASUREMENTTIME"].ToString());
            model.RECORDID = Reader["RECORDID"].ToString();
            model.RECORDNAME = Reader["RECORDNAME"].ToString();
            model.RECORDTIME = DateTime.Parse(Reader["RECORDTIME"].ToString());
            model.DIALYSISMODE = Reader["DIALYSISMODE"].ToString();
            model.DIALYZER = Reader["DIALYZER"].ToString();
            model.TREATMENTDURATION = Reader["TREATMENTDURATION"].ToString();
            model.BLOODFOLW = Reader["BLOODFOLW"].ToString();
            model.TARGETWEIGHT = Reader["TARGETWEIGHT"].ToString();
            model.FILTRATIONQUANTITY = Reader["FILTRATIONQUANTITY"].ToString();
            model.CHANGEQUANTITY = Reader["CHANGEQUANTITY"].ToString();
            model.MACHINETYPE = Reader["MACHINETYPE"].ToString();
            model.VASCULARACCESS = Reader["VASCULARACCESS"].ToString();
            model.DIALYSATETYPE = Reader["DIALYSATETYPE"].ToString();
            model.FOLW = Reader["FOLW"].ToString();
            model.NA = Reader["NA"].ToString();
            model.CA = Reader["CA"].ToString();
            model.HCO2 = Reader["HCO2"].ToString();
            model.ANTICOAGULANT = Reader["ANTICOAGULANT"].ToString();
            model.FRIST = Reader["FRIST"].ToString();
            model.FRISTUNIT = Reader["FRISTUNIT"].ToString();
            model.SECOND = Reader["SECOND"].ToString();
            model.SECONDUNIT = Reader["SECONDUNIT"].ToString();
            model.MEMO = Reader["MEMO"].ToString();
            model.Summary = Reader["Summary"].ToString();
            model.STATE = Reader["STATE"].ToString();
            model.LASTTESTDATE = Reader["LASTTESTDATE"].ToString();
            model.DANCHAOHOUR = Reader["DANCHAOHOUR"].ToString();
            model.DANCHAOML = Reader["DANCHAOML"].ToString();
            model.TOTAL = Reader["TOTAL"].ToString();
            model.TOTALUNIT = Reader["TOTALUNIT"].ToString();

            return model;
        }

    }
}
