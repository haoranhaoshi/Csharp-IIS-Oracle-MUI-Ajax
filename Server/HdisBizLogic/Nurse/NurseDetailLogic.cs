using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HdisBizLogic.Nurse
{
    /// <summary>
    /// [功能描述:护士透析主表数据操作]
    /// [创建者:yinbsh]
    /// [创建时间:2017-06-06]
    /// </summary>
    public class NurseDetailLogic : ManagementBizlogic
    {


        /// <summary>
        /// 透析流水号=透析号+补零后的透析次数
        /// </summary>
        /// <param name="strDialysisTimes">透析次数</param>
        /// <returns></returns>
        private string DialysisSeqFillZero(string strDialysisTimes)
        {
            try
            {
                int tmpInt = int.Parse(strDialysisTimes);
                strDialysisTimes = (tmpInt.ToString("0000"));
            }
            catch (Exception ex)
            {
                strDialysisTimes = "0000";
            }

            /*
            for (int i = 0; i < 4 - strDialysisTimes.Length; i++)
            {
                strDialysisTimes = "0" + strDialysisTimes;
            }
             */
            return strDialysisTimes;
        }

        /// <summary>
        /// 查询当日透析空床位
        /// </summary>
        /// <param name="strHospitalId">院区</param>
        /// <param name="strDeptId">科室</param>
        /// <param name="strAreaIid">工作区</param>
        /// <param name="strTestDate">透析日期、当天</param>
        /// <param name="strClasses">班次</param>
        /// <returns></returns>
        public List<HdisModels.SystemConfig.BedAndMachineModel> QueryEmptyBedList(string strHospitalId, string strDeptId, string strAreaIid, string strTestDate, string strClasses, string patientType)
        {
            List<HdisModels.SystemConfig.BedAndMachineModel> list = new List<HdisModels.SystemConfig.BedAndMachineModel>();
            string sql = string.Empty;
            try
            {
                sql = @"select * from hdis_com_bedandmachine  t 
left join hdis_com_area c on t.hospitalid=c.hospitalid and t.deptid=c.deptid and t.areaid=c.areaid 
where t.bedid not in (
select a.bedid from hdis_pat_appoint_plan a where a.appointdate='{3}' and a.classes='{4}')  and t.bedtype='{5}' 
and t.hospitalid='{0}' and t.deptid='{1}' and (t.areaid='{2}' or '{2}' ='ALL') 
and c.isavailable='1' and t.isused='1' 
order by t.hospitalid,t.deptid,c.areapriority,t.bedpriority";               
                sql = string.Format(sql, strHospitalId, strDeptId, strAreaIid, strTestDate, strClasses, patientType);
                if (this.ExecSQLQuery(sql) == -1)
                {
                    return list;
                }
                HdisModels.SystemConfig.BedAndMachineModel model;
                while (this.Reader.Read())
                {
                    model = new HdisModels.SystemConfig.BedAndMachineModel();
                    model.DEPTID = Reader["DEPTID"].ToString();
                    model.DEPTNAME = Reader["DEPTNAME"].ToString();
                    model.HOSPITALID = Reader["HOSPITALID"].ToString();
                    model.HOSPITALNAME = Reader["HOSPITALNAME"].ToString();
                    model.AREAID = Reader["AREAID"].ToString();
                    model.AREANAME = Reader["AREANAME"].ToString();
                    model.BedID = Reader["BedID"].ToString();
                    model.BedName = Reader["BedName"].ToString();
                    model.BEDTYPE = Reader["BEDTYPE"].ToString();
                    model.MachineID = Reader["MachineID"].ToString();
                    model.MachineName = Reader["MachineName"].ToString();
                    model.VERSION = Reader["VERSION"].ToString();
                    model.FACTORY = Reader["FACTORY"].ToString();
                    model.DIALYSISMODE = Reader["DIALYSISMODE"].ToString();
                    model.DIALYZER = Reader["DIALYZER"].ToString();
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
                this.Reader.Close();
                this.oracleDb.CloseDB();
            }
        }

        /// <summary>
        /// 通过透析日期查询当日的透析患者列表
        /// </summary>
        /// <param name="strTestDate"></param>
        /// <returns></returns>
        public List<HdisModels.Nurse.NurseDetail> QueryPatientListByTestDate(string strTestDate, string strSigninState)
        {
            List<HdisModels.Nurse.NurseDetail> list = new List<HdisModels.Nurse.NurseDetail>();
            HdisModels.Nurse.NurseDetail model = null;
            string sql = string.Empty;
            sql = @"select * from hdis_pat_appoint_plan a
                          left join hdis_pat_patientinfo b on a.cardid = b.cardid
                         where a.appointdate = '{0}' and (a.signinstate = '{1}' or '{1}'='ALL') order by a.maketime";
            sql = string.Format(sql, strTestDate, strSigninState);
            try
            {
                if (this.ExecSQLQuery(sql) == -1)
                {
                    return list;
                }
                while (this.Reader.Read())
                {
                    model = new HdisModels.Nurse.NurseDetail();
                    model.CARDID = this.Reader["CARDID"].ToString();
                    model.SEQID = this.Reader["CARDID"].ToString() + this.DialysisSeqFillZero(this.Reader["DIALYSISTIMES"].ToString());
                    try
                    {
                        model.TESTDATE = Convert.ToDateTime(this.Reader["APPOINTDATE"]).ToString("yyyy-MM-dd");
                    }
                    catch
                    { }
                    model.DIALYSISSTATE = this.Reader["DIALYSISSTATE"].ToString();
                    model.Patient.PATIENTID = this.Reader["PATIENTID"].ToString();
                    model.Patient.PATIENTNAME = this.Reader["PATIENTNAME"].ToString();
                    model.Patient.PATIENTSEX = this.Reader["PATIENTSEX"].ToString();
                    model.Patient.PATIENTAGE = this.Reader["PATIENTAGE"].ToString();
                    model.Patient.PATIENTTYPE = this.Reader["PATIENTTYPE"].ToString();
                    try
                    {
                        model.Patient.FIRSTDATE = DateTime.Parse(this.Reader["FIRSTDATE"].ToString());
                    }
                    catch
                    { }
                    model.Patient.TELPHONE = this.Reader["TELPHONE"].ToString();
                    model.Patient.CELLPHONE = this.Reader["CELLPHONE"].ToString();
                    model.Patient.ADDRESS = this.Reader["ADDRESS"].ToString();
                    model.Patient.ABOBLOODTYPE = this.Reader["ABOBLOODTYPE"].ToString();
                    model.Patient.RHBLOODTYPE = this.Reader["RHBLOODTYPE"].ToString();
                    model.Patient.MEDICARETYPE = this.Reader["MEDICARETYPE"].ToString();
                    model.Patient.IDCARD = this.Reader["IDCARD"].ToString();
                    model.Patient.NATIONAL = this.Reader["NATIONAL"].ToString();
                    model.Patient.NATIVEPLACE = this.Reader["NATIVEPLACE"].ToString();
                    model.Patient.COMPANY = this.Reader["COMPANY"].ToString();
                    model.Patient.CCANDHPI = this.Reader["CCANDHPI"].ToString();
                    model.Patient.PASTHISTORY = this.Reader["PASTHISTORY"].ToString();
                    model.Patient.ALLERGIES = this.Reader["ALLERGIES"].ToString();
                    model.Patient.FAMILYHISTORY = this.Reader["FAMILYHISTORY"].ToString();
                    model.Patient.HOSPITALAREAID = this.Reader["HOSPITALAREAID"].ToString();
                    model.Patient.HOSPITALAREANAME = this.Reader["HOSPITALAREANAME"].ToString();
                    model.Patient.DEPARTMENTID = this.Reader["DEPARTMENTID"].ToString();
                    model.Patient.DEPARTMENTNAME = this.Reader["DEPARTMENTNAME"].ToString();
                    model.Patient.SPECIALPATIENTTYPE = this.Reader["SPECIALPATIENTTYPE"].ToString();
                    model.SIGNINSTATE = this.Reader["SIGNINSTATE"].ToString();
                    model.AREAID = this.Reader["AREAID"].ToString();
                    model.AREANAME = this.Reader["AREANAME"].ToString();
                    model.BEDID = this.Reader["BEDID"].ToString();
                    model.BEDNAME = this.Reader["BEDNAME"].ToString();
                    model.CLASSES = this.Reader["CLASSES"].ToString();
                    model.DIALYSISMODE = this.Reader["DIALYSISMODE"].ToString();
                    model.DIALYZER = this.Reader["DIALYZER"].ToString();
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
        ///  通过透析号查询当前患者信息
        /// </summary>
        /// <param name="strTestDate">预约时间/透析时间</param>
        /// <param name="strCardId">透析号</param>
        /// <returns></returns>
        public HdisModels.Nurse.NurseDetail QueryCurrentPatientInfo(string strTestDate, string strCardId)
        {
            HdisModels.Nurse.NurseDetail model = null;
            string sql = string.Empty;
            sql = @"select b.dialysistimes as dialysistimes_real,a.*,b.*,C.*,d.machineid as Machineid1,d.machinename as machinename1 from hdis_pat_appoint_plan a
                          left join hdis_pat_patientinfo b on a.cardid = b.cardid
                          left join hdis_nur_detail c on a.cardid=c.cardid and a.appointdate=c.testdate
                         ,hdis_com_bedandmachine d
                          where  a.bedid=d.bedid and a.appointdate = '{0}' and a.cardid='{1}'";
            sql = string.Format(sql, strTestDate, strCardId);
            try
            {
                if (this.ExecSQLQuery(sql) < 0)
                {
                    return null;
                }
                if (this.Reader.Read())
                {
                    model = new HdisModels.Nurse.NurseDetail();
                    model.CARDID = this.Reader["CARDID"].ToString();
                    model.SEQID = this.Reader["CARDID"].ToString() + this.DialysisSeqFillZero(this.Reader["DIALYSISTIMES_REAL"].ToString());
                    try
                    {
                        model.TESTDATE = Convert.ToDateTime(this.Reader["APPOINTDATE"]).ToString("yyyy-MM-dd");
                    }
                    catch
                    { }
                    model.DIALYSISSTATE = this.Reader["DIALYSISSTATE"].ToString();
                    model.Patient.PATIENTID = this.Reader["PATIENTID"].ToString();
                    model.Patient.PATIENTNAME = this.Reader["PATIENTNAME"].ToString();
                    model.Patient.PATIENTSEX = this.Reader["PATIENTSEX"].ToString();
                    model.Patient.PATIENTAGE = this.Reader["PATIENTAGE"].ToString();
                    model.Patient.PATIENTTYPE = this.Reader["PATIENTTYPE"].ToString();
                    try
                    {
                        model.Patient.FIRSTDATE = DateTime.Parse(this.Reader["FIRSTDATE"].ToString());
                    }
                    catch
                    { }
                    model.Patient.TELPHONE = this.Reader["TELPHONE"].ToString();
                    model.Patient.CELLPHONE = this.Reader["CELLPHONE"].ToString();
                    model.Patient.ADDRESS = this.Reader["ADDRESS"].ToString();
                    model.Patient.ABOBLOODTYPE = this.Reader["ABOBLOODTYPE"].ToString();
                    model.Patient.RHBLOODTYPE = this.Reader["RHBLOODTYPE"].ToString();
                    model.Patient.MEDICARETYPE = this.Reader["MEDICARETYPE"].ToString();
                    model.Patient.IDCARD = this.Reader["IDCARD"].ToString();
                    model.Patient.NATIONAL = this.Reader["NATIONAL"].ToString();
                    model.Patient.NATIVEPLACE = this.Reader["NATIVEPLACE"].ToString();
                    model.Patient.COMPANY = this.Reader["COMPANY"].ToString();
                    model.Patient.CCANDHPI = this.Reader["CCANDHPI"].ToString();
                    model.Patient.PASTHISTORY = this.Reader["PASTHISTORY"].ToString();
                    model.Patient.ALLERGIES = this.Reader["ALLERGIES"].ToString();
                    model.Patient.FAMILYHISTORY = this.Reader["FAMILYHISTORY"].ToString();
                    model.Patient.HOSPITALAREAID = this.Reader["HOSPITALAREAID"].ToString();
                    model.Patient.HOSPITALAREANAME = this.Reader["HOSPITALAREANAME"].ToString();
                    model.Patient.DEPARTMENTID = this.Reader["DEPARTMENTID"].ToString();
                    model.Patient.DEPARTMENTNAME = this.Reader["DEPARTMENTNAME"].ToString();
                    model.Patient.SPECIALPATIENTTYPE = this.Reader["SPECIALPATIENTTYPE"].ToString();
                    model.AREAID = this.Reader["AREAID"].ToString();
                    model.AREANAME = this.Reader["AREANAME"].ToString();
                    model.BEDID = this.Reader["BEDID"].ToString();
                    model.BEDNAME = this.Reader["BEDNAME"].ToString();
                    model.CLASSES = this.Reader["CLASSES"].ToString();
                    model.DIALYSISMODE = this.Reader["DIALYSISMODE"].ToString();
                    model.DIALYZER = this.Reader["DIALYZER"].ToString();
                    model.MACHINEID = this.Reader["machineid1"].ToString();
                    model.MACHINENAME = this.Reader["machinename1"].ToString();
                    model.SIGNINSTATE = this.Reader["signinstate"].ToString();
                }
                return model;
            }
            catch
            {
                return null;
            }
            finally
            {
                this.oracleDb.CloseReader();
                this.oracleDb.CloseDB();
            }
        }


        /// <summary>
        /// 更新新透析计划时间表
        /// hdis_pat_appoint_plan
        /// </summary>
        /// <param name="model">护士实体</param>
        /// <returns></returns>
        public int UpdateDialysisAppointplan(HdisModels.Nurse.NurseDetail model)
        {
            string sql = string.Empty;
            sql = @"update hdis_pat_appoint_plan a set a.dialysisstate='{2}' where a.appointdate='{0}' and a.cardid='{1}'";
            try
            {
                sql = string.Format(sql, model.TESTDATE, model.CARDID, model.DIALYSISSTATE);
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
        /// 透析患者上机操作
        /// </summary>
        /// <param name="model"></param>
        /// <param name="strUpMachineNurseId"></param>
        /// <param name="strUpMachineNurseName"></param>
        /// <returns></returns>
        public int UpMachine(HdisModels.Nurse.NurseDetail model,string strUpMachineNurseId,string strUpMachineNurseName)
        {
            string sql = string.Empty;
            sql = @"insert into hdis_nur_detail (cardid,seqid,testdate,dialysisstate,departmentid,departmentname,areaid,areaname,
                            patientid,bedid,bedname,machineid,machinename,upmachinetime,upmachinenurseid,upmachinenursename,upmachinestate,classes)
                        values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}',sysdate,'{13}','{14}','{15}','{16}')";
           
            try
            {
                sql = string.Format(sql, model.CARDID, model.SEQID, model.TESTDATE, model.DIALYSISSTATE, model.Patient.DEPARTMENTID, model.Patient.DEPARTMENTNAME,
                    model.AREAID, model.AREANAME, model.Patient.PATIENTID, model.BEDID, model.BEDNAME, model.MACHINEID,
                    model.MACHINENAME, strUpMachineNurseId, strUpMachineNurseName, model.UPMACHINESTATE, model.CLASSES);
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
        /// 透析患者取消上机
        /// </summary>
        /// <param name="model">护士实体</param>
        /// <returns></returns>
        public int CancelUpMachine(HdisModels.Nurse.NurseDetail model)
        {
            string sql = string.Empty;
            sql = @"delete hdis_nur_detail where testdate='{0}' and cardid='{1}'";
            try
            {
                sql = string.Format(sql, model.TESTDATE, model.CARDID);
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
        /// 透析患者下机
        /// 透析患者取消下机
        /// </summary>
        /// <param name="model">护士主表实体</param>
        /// <param name="strNurseId">操作护士编码</param>
        /// <param name="strNurseName">操作护士名称</param>
        /// <returns></returns>
        public int DownMachine(HdisModels.Nurse.NurseDetail model,string strNurseId,string strNurseName)
        {
            string sql = string.Empty;
            sql = @"update hdis_nur_detail a set a.downmachinestate= '{2}',a.downmachinenurseid= '{3}',a.downmachinenursename= '{4}',
                        a.dialysisstate= '{5}',a.downmachinetime= sysdate where testdate='{0}' and cardid='{1}'";
            try
            {
                sql = string.Format(sql, model.TESTDATE, model.CARDID, model.DOWNMACHINESTATE, strNurseId, strNurseName, model.DIALYSISSTATE);
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
        /// 保存透析小结
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int SaveDialysisSummary(HdisModels.Nurse.NurseDetail model)
        {
            string sql = string.Empty;
            sql = @"update hdis_nur_detail a set 
                    a.dialysissummary='{2}',
                    factsfiltrationquantity ='{3}',
                    treatmentduration ='{4}',
                    afterweight ='{5}',
                    lessenweight ='{6}',
                    temperature ='{7}',
                    spressure ='{8}',
                    dpressure ='{9}',
                    pulse ='{10}',
                    breathing ='{11}',
                    heartrate ='{12}'
                    where testdate='{0}' and cardid='{1}'";          
            try
            {
                sql = string.Format(sql, model.TESTDATE, model.CARDID,
                    model.DIALYSISSUMMARY,
                    model.FACTSFILTRATIONQUANTITY,
                    model.TREATMENTDURATION,
                    model.AFTERWEIGHT,
                    model.LESSENWEIGHT,
                    model.DoctorDetail.TEMPERATURE,
                    model.DoctorDetail.SPRESSURE,
                    model.DoctorDetail.DPRESSURE,
                    model.DoctorDetail.PULSE,
                    model.DoctorDetail.BREATHING,
                    model.HEARTRATE
                    );
                if (this.ExecSQL(sql) < 0)
                {                    
                    return -1;
                }
                return 1;
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
        /// 护士确认透析结束（护士表）
        /// </summary>
        /// <param name="cardId">透析号</param>
        /// <param name="testDate">透析日期</param>
        /// <param name="seqId">透析流水号</param>
        /// <returns></returns>
        public int EditNurConfirmState(string cardId, string testDate)
        {
            string sql = @" UPDATE HDIS_NUR_DETAIL SET DIALYSISSTATE = '4'  WHERE CARDID='{0}' AND TESTDATE = '{1}'";
            
            try{
                sql = string.Format(sql, cardId, testDate);
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
        /// 护士确认透析结束（预约表）
        /// </summary>
        /// <param name="cardId">透析号</param>
        /// <param name="testDate">透析日期</param>
        /// <param name="seqId">透析流水号</param>
        /// <returns></returns>
        public int EditAppointConfirmState(string cardId, string testDate)
        {
            
            string sql = @" UPDATE HDIS_PAT_APPOINT_PLAN SET DIALYSISSTATE = '4'  WHERE CARDID='{0}' AND APPOINTDATE = '{1}'";
            try
            {
                
                sql = string.Format(sql, cardId, testDate);
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
        /// 透析患者换床
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int ChangePatientBed(string testDate, string cardId, string classes, string areaId, string areaName, string bedId, string bedName)
        {
            string sql = string.Empty;
            sql = @"update hdis_pat_appoint_plan a set a.classes='{2}',a.areaid='{3}',a.areaname='{4}',a.bedid='{5}',a.bedname='{6}' where a.appointdate='{0}' and a.cardid='{1}'";
            try
            {
                sql = string.Format(sql, testDate, cardId, classes, areaId, areaName, bedId, bedName);
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
        /// 查询患者上机时间
        /// </summary>
        public DateTime QueryUpMachineTime(string cardId,string testDate)
        {
            DateTime dt = new DateTime();
            string sql = @" SELECT UPMACHINETIME FROM HDIS_NUR_DETAIL WHERE CARDID= '{0}' and testdate='{1}'";
            sql = string.Format(sql, cardId, testDate);
            if (this.ExecSQLQuery(sql) < 0)
            {
                return dt;
            }
            if (this.Reader.Read())
            {
                dt = Convert.ToDateTime(this.Reader[0].ToString());
            }
            return dt;
        }

        /// <summary>
        /// 查询患者上机护士
        /// </summary>
        public string QueryUpMachineNurseName(string cardId, string testDate)
        {
            string upMachineNurseName = string.Empty;
            try
            {
                string sql = @" SELECT UPMACHINENURSENAME FROM HDIS_NUR_DETAIL WHERE CARDID= '{0}' and testdate='{1}'";
                sql = string.Format(sql, cardId, testDate);
                if (this.ExecSQLQuery(sql) < 0)
                {
                    return upMachineNurseName;
                }
                if (this.Reader.Read())
                {
                    upMachineNurseName = this.Reader[0].ToString();
                }
                return upMachineNurseName;
            }
            catch (Exception ex)
            {
                return upMachineNurseName;
            }
            finally
            {
                this.Reader.Close();
                this.oracleDb.CloseDB();
            }
        }

        /// <summary>
        /// 查询透析小结
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public HdisModels.Nurse.NurseDetail GetDialysisSummary(string TESTDATE, string CARDID)
        {
            HdisModels.Nurse.NurseDetail nurseDetail = new HdisModels.Nurse.NurseDetail();
            string sql = string.Empty;
            sql = @"select * from hdis_nur_detail t  where t.testdate='{0}' and t.cardid='{1}'";            
            sql = string.Format(sql, TESTDATE, CARDID);
            try
            {
                if (this.ExecSQLQuery(sql) < 0)
                {
                    return null;
                }
                if (this.Reader.Read())
                {
                    nurseDetail.DIALYSISSUMMARY = this.Reader["DIALYSISSUMMARY"].ToString();
                    nurseDetail.TREATMENTDURATION = this.Reader["TREATMENTDURATION"].ToString();
                    nurseDetail.DoctorDetail.FACTSFILTRATIONQUANTITY = this.Reader["FACTSFILTRATIONQUANTITY"].ToString();
                    nurseDetail.DoctorDetail.AFTERWEIGHT = this.Reader["AFTERWEIGHT"].ToString();
                    nurseDetail.LESSENWEIGHT = this.Reader["LESSENWEIGHT"].ToString();
                    nurseDetail.DoctorDetail.TEMPERATURE = this.Reader["TEMPERATURE"].ToString();
                    nurseDetail.DoctorDetail.PULSE = this.Reader["PULSE"].ToString();
                    nurseDetail.DoctorDetail.SPRESSURE = this.Reader["SPRESSURE"].ToString();
                    nurseDetail.DoctorDetail.DPRESSURE = this.Reader["DPRESSURE"].ToString();
                    nurseDetail.DoctorDetail.BREATHING = this.Reader["BREATHING"].ToString();
                    nurseDetail.HEARTRATE = this.Reader["HEARTRATE"].ToString();
                    return nurseDetail;
                }
                return null;
            }
            catch (Exception ex)
            {                
                return null;
            }
            finally
            {
                this.Reader.Close();
                this.oracleDb.CloseDB();
            }
        }
    }
}
