using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace HdisCommon
{
    public class Fuction
    {
        /// <summary>
        /// 登录人员
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string PersonInfoForXml(HdisModels.Person.PersonModel model)
        {
            string strReturn = string.Empty;
            using (System.IO.StringWriter stringWriter = new StringWriter(new StringBuilder()))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(HdisModels.Person.PersonModel));
                xmlSerializer.Serialize(stringWriter, model);
                strReturn = stringWriter.ToString();
            }
            return strReturn;
        }


        /// <summary>
        /// XML患者列表
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string PatientListForXml(List<HdisModels.Nurse.NurseDetail> list)
        {
            string strReturn = string.Empty;
            using (System.IO.StringWriter stringWriter = new StringWriter(new StringBuilder()))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<HdisModels.Nurse.NurseDetail>));
                xmlSerializer.Serialize(stringWriter, list);
                strReturn = stringWriter.ToString();
            }
            return strReturn;
        }


        /// <summary>
        /// XML工作区列表
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string AreaListForXml(List<HdisModels.SystemConfig.AreaModel> list)
        {
            string strReturn = string.Empty;
            using (System.IO.StringWriter stringWriter = new StringWriter(new StringBuilder()))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<HdisModels.SystemConfig.AreaModel>));
                xmlSerializer.Serialize(stringWriter, list);
                strReturn = stringWriter.ToString();
            }
            return strReturn;
        }


        /// <summary>
        /// XML班次列表
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string ClassesListForXml(List<HdisModels.Object> list)
        {
            string strReturn = string.Empty;
            using (System.IO.StringWriter stringWriter = new StringWriter(new StringBuilder()))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<HdisModels.Object>));
                xmlSerializer.Serialize(stringWriter, list);
                strReturn = stringWriter.ToString();
            }
            return strReturn;
        }

        /// <summary>
        /// XML透析项目列表
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string DialysisItemListForXml(List<HdisModels.SystemConfig.ItemModel> list)
        {
            string strReturn = string.Empty;
            using (System.IO.StringWriter stringWriter = new StringWriter(new StringBuilder()))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<HdisModels.SystemConfig.ItemModel>));
                xmlSerializer.Serialize(stringWriter, list);
                strReturn = stringWriter.ToString();
            }
            return strReturn;
        }

        /// <summary>
        /// XML透前透后信息
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string DialysisInfoForXml(HdisModels.Doctor.DoctorDetail doctorDetail)
        {
            string strReturn = string.Empty;
            using (System.IO.StringWriter stringWriter = new StringWriter(new StringBuilder()))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(HdisModels.Doctor.DoctorDetail));
                xmlSerializer.Serialize(stringWriter, doctorDetail);
                strReturn = stringWriter.ToString();
            }
            return strReturn;
        }

        /// <summary>
        /// XML透析过程记录
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string DialysisRecordForXml(List<HdisModels.Proc.ProcResult> list)
        {
            string strReturn = string.Empty;
            using (System.IO.StringWriter stringWriter = new StringWriter(new StringBuilder()))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<HdisModels.Proc.ProcResult>));
                xmlSerializer.Serialize(stringWriter, list);
                strReturn = stringWriter.ToString();
            }
            return strReturn;
        }

        /// <summary>
        /// XML医嘱执行查询
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string OrderExecuteForXml(List<HdisModels.Doctor.OrderModel> list)
        {
            string strReturn = string.Empty;
            using (System.IO.StringWriter stringWriter = new StringWriter(new StringBuilder()))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<HdisModels.Doctor.OrderModel>));
                xmlSerializer.Serialize(stringWriter, list);
                strReturn = stringWriter.ToString();
            }
            return strReturn;
        }

        /// <summary>
        /// XML未执行医嘱查询
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string UnExecuteOrderForXml(List<HdisModels.Doctor.OrderModel> list)
        {
            string strReturn = string.Empty;
            using (System.IO.StringWriter stringWriter = new StringWriter(new StringBuilder()))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<HdisModels.Doctor.OrderModel>));
                xmlSerializer.Serialize(stringWriter, list);
                strReturn = stringWriter.ToString();
            }
            return strReturn;
        }

        /// <summary>
        /// XML患者基本信息
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string BaseInfoForXml(HdisModels.Nurse.NurseDetail nurDetail)
        {
            string strReturn = string.Empty;
            using (System.IO.StringWriter stringWriter = new StringWriter(new StringBuilder()))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(HdisModels.Nurse.NurseDetail));
                xmlSerializer.Serialize(stringWriter, nurDetail);
                strReturn = stringWriter.ToString();
            }
            return strReturn;
        }

        /// <summary>
        /// XML患者基本信息
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string DalysisStateForXml(List<HdisModels.State.DalysisState> list)
        {
            string strReturn = string.Empty;
            using (System.IO.StringWriter stringWriter = new StringWriter(new StringBuilder()))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<HdisModels.State.DalysisState>));
                xmlSerializer.Serialize(stringWriter, list);
                strReturn = stringWriter.ToString();
            }
            return strReturn;
        }

        /// <summary>
        /// XML空床信息
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string EmptyBedForXml(List<HdisModels.SystemConfig.BedAndMachineModel> list)
        {
            string strReturn = string.Empty;
            using (System.IO.StringWriter stringWriter = new StringWriter(new StringBuilder()))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<HdisModels.SystemConfig.BedAndMachineModel>));
                xmlSerializer.Serialize(stringWriter, list);
                strReturn = stringWriter.ToString();
            }
            return strReturn;
        }

        /// <summary>
        /// XML护士信息
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string NurseForXml(List<HdisModels.Person.PersonModel> list)
        {
            string strReturn = string.Empty;
            using (System.IO.StringWriter stringWriter = new StringWriter(new StringBuilder()))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<HdisModels.Person.PersonModel>));
                xmlSerializer.Serialize(stringWriter, list);
                strReturn = stringWriter.ToString();
            }
            return strReturn;
        }

        /// <summary>
        /// XML透后护士记录
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string DialysisSummaryForXml(HdisModels.Nurse.NurseDetail nurDetail)
        {
            string strReturn = string.Empty;
            using (System.IO.StringWriter stringWriter = new StringWriter(new StringBuilder()))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(HdisModels.Nurse.NurseDetail));
                xmlSerializer.Serialize(stringWriter, nurDetail);
                strReturn = stringWriter.ToString();
            }
            return strReturn;
        }

    }
}
