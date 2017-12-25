using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HdisBizLogic.Print
{
    public class PrintPatAfterInfo : ManagementBizlogic
    {
        /// <summary>
        /// 保存透析小结
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int SavePatResult(HdisModels.Nurse.NurseDetail model)
        {
            string sql = string.Empty;

            sql = @"insert into HDIS_PRINT_AFTERINFO(
AFTERCARDID,	
AFTERSEQID,	
AFTERTESTDATE,	
AFTERDIALYSISSTATE,	
AFTERAREANAME,	
AFTERPATIENTID,	
AFTERMEMO,	
AFTERBEDID,	
AFTERBEDNAME,	
AFTERMACHINEID,	
AFTERMACHINENAME,	
AFTERUPMACHINETIME,	
AFTERUPMACHINESTATE,	
AFTERDOWNMACHINETIME,	
AFTERDOWNMACHINESTATE,	
AFTERUPMACHINENURSEID,	
AFTERUPMACHINENURSENAME,	
AFTERDOWNMACHINENURSEID,	
AFTERDOWNMACHINENURSENAME,	
AFTERCLASSES,	
AFTERDIALYSISSUMMARY,	
AFTERAFTERWEIGHT,	
AFTERFACTSFILTRATIONQUANTITY,	
AFTERTREATMENTDURATION,	
AFTERLESSENWEIGHT,	
AFTERTEMPERATURE,	
AFTERSPRESSURE,	
AFTERDPRESSURE,	
AFTERPULSE,	
AFTERBREATHING,
AFTERHEARTRATE	
) 
values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}',to_date('{11}','yyyy-MM-dd hh24:mi:ss'),'{12}',to_date('{13}','yyyy-MM-dd hh24:mi:ss'),'{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}')";

            try
            {
                sql = string.Format(sql,
model.CARDID,
model.SEQID,
model.TESTDATE,
model.DIALYSISSTATE,
model.AREANAME,
model.Patient.PATIENTID,
model.MEMO,
model.BEDID,
model.BEDNAME,
model.MACHINEID,
model.MACHINENAME,
model.UPMACHINETIME,
model.UPMACHINESTATE,
model.DOWNMACHINETIME,
model.DOWNMACHINESTATE,
model.UPMACHINENURSEID,
model.UPMACHINENURSENAME,
model.DOWNMACHINENURSEID,
model.DOWNMACHINENURSENAME,
model.CLASSES,
model.DIALYSISSUMMARY,
model.AFTERWEIGHT,
model.FACTSFILTRATIONQUANTITY,
model.TREATMENTDURATION,
model.LESSENWEIGHT,
model.DoctorDetail.TEMPERATURE,
model.DoctorDetail.SPRESSURE,
model.DoctorDetail.DPRESSURE,
model.DoctorDetail.PULSE,
model.DoctorDetail.BREATHING,
model.HEARTRATE
 );
                if (this.ExecSQL(sql) == -1)
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
    }
}
