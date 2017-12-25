using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HdisModels.Doctor
{
    /// <summary>
    /// [功能描述:患者实体]
    /// [创建时间:2016-01-21]
    /// </summary>
    public class PatientModel : Object
    {
        public PatientModel()
        {
            this.CARDID = "";
            this.FIRSTDATE = new DateTime();
            this.DIALYSISTIMES = "";
            this.PATIENTHEIGHT = 0;
            this.PATIENTWEIGHT = 0;
            this.PATIENTID = "";
            this.PATIENTTYPE = "";
            this.PATIENTNAME = "";
            this.PATIENTSEX = "";
            this.PATIENTAGE = "";
            this.BIRTHDAY = new DateTime();
            this.TELPHONE = "";
            this.CELLPHONE = "";
            this.ABOBLOODTYPE = "";
            this.RHBLOODTYPE = "";
            this.MEDICARETYPE = "";
            this.IDCARD = "";
            this.NATIONAL = "";
            this.NATIVEPLACE = "";
            this.COMPANY = "";
            this.ADDRESS = "";
            this.CCANDHPI = "";
            this.PASTHISTORY = "";
            this.ALLERGIES = "";
            this.FAMILYHISTORY = "";
            this.OTHPTHISTORYANDVA = "";
            this.REMARK = "";
            this.DOCID = "";
            this.DOCNAME = "";
            this.FILEDATE = new DateTime();
            this.HOSPITALAREAID = "";
            this.HOSPITALAREANAME = "";
            this.DEPARTMENTID = "";
            this.DEPARTMENTNAME = "";
            this.ISPATIENT = "";
            this.SPECIALPATIENTTYPE = "";
        }

        /// <summary>
        /// 是否有效
        /// </summary>
        public string ISPATIENT { get; set; }

        /// <summary>
        /// 特殊患者类型
        /// </summary>
        public string SPECIALPATIENTTYPE { get; set; }

        /// <summary>
        ///患者卡号
        /// </summary>
        public string CARDID
        {
            get;
            set;
        }


        /// <summary>
        ///首次透析时间
        /// </summary>
        public DateTime FIRSTDATE
        {
            get;
            set;
        }


        /// <summary>
        ///透析次数
        /// </summary>
        public string DIALYSISTIMES
        {
            get;
            set;
        }


        /// <summary>
        ///患者身高
        /// </summary>
        public decimal PATIENTHEIGHT
        {
            get;
            set;
        }


        /// <summary>
        ///患者体重
        /// </summary>
        public decimal PATIENTWEIGHT
        {
            get;
            set;
        }


        /// <summary>
        ///患者编号
        /// </summary>
        public string PATIENTID
        {
            get;
            set;
        }


        /// <summary>
        ///患者类型
        /// </summary>
        public string PATIENTTYPE
        {
            get;
            set;
        }


        /// <summary>
        ///患者姓名
        /// </summary>
        public string PATIENTNAME
        {
            get;
            set;
        }


        /// <summary>
        ///患者性别
        /// </summary>
        public string PATIENTSEX
        {
            get;
            set;
        }


        /// <summary>
        ///患者年龄
        /// </summary>
        public string PATIENTAGE
        {
            get;
            set;
        }


        /// <summary>
        ///出生日期
        /// </summary>
        public DateTime BIRTHDAY
        {
            get;
            set;
        }


        /// <summary>
        ///住宅电话
        /// </summary>
        public string TELPHONE
        {
            get;
            set;
        }


        /// <summary>
        ///手机号码
        /// </summary>
        public string CELLPHONE
        {
            get;
            set;
        }


        /// <summary>
        ///ABO血型
        /// </summary>
        public string ABOBLOODTYPE
        {
            get;
            set;
        }


        /// <summary>
        ///RH血型
        /// </summary>
        public string RHBLOODTYPE
        {
            get;
            set;
        }


        /// <summary>
        ///医保类型
        /// </summary>
        public string MEDICARETYPE
        {
            get;
            set;
        }


        /// <summary>
        ///身份证号
        /// </summary>
        public string IDCARD
        {
            get;
            set;
        }


        /// <summary>
        ///民族
        /// </summary>
        public string NATIONAL
        {
            get;
            set;
        }


        /// <summary>
        ///籍贯
        /// </summary>
        public string NATIVEPLACE
        {
            get;
            set;
        }


        /// <summary>
        ///工作单位
        /// </summary>
        public string COMPANY
        {
            get;
            set;
        }


        /// <summary>
        ///家庭住址
        /// </summary>
        public string ADDRESS
        {
            get;
            set;
        }


        /// <summary>
        ///主诉及现病史
        /// </summary>
        public string CCANDHPI
        {
            get;
            set;
        }


        /// <summary>
        ///既往史
        /// </summary>
        public string PASTHISTORY
        {
            get;
            set;
        }


        /// <summary>
        ///过敏史
        /// </summary>
        public string ALLERGIES
        {
            get;
            set;
        }


        /// <summary>
        ///家族史
        /// </summary>
        public string FAMILYHISTORY
        {
            get;
            set;
        }


        /// <summary>
        ///外院透析病史及血管通路情况
        /// </summary>
        public string OTHPTHISTORYANDVA
        {
            get;
            set;
        }


        /// <summary>
        ///备注
        /// </summary>
        public string REMARK
        {
            get;
            set;
        }


        /// <summary>
        ///建档医生工号
        /// </summary>
        public string DOCID
        {
            get;
            set;
        }


        /// <summary>
        ///建档医生姓名
        /// </summary>
        public string DOCNAME
        {
            get;
            set;
        }


        /// <summary>
        ///建档日期
        /// </summary>
        public DateTime FILEDATE
        {
            get;
            set;
        }


        /// <summary>
        ///院区编码
        /// </summary>
        public string HOSPITALAREAID
        {
            get;
            set;
        }


        /// <summary>
        ///院区名称
        /// </summary>
        public string HOSPITALAREANAME
        {
            get;
            set;
        }


        /// <summary>
        ///科室编码
        /// </summary>
        public string DEPARTMENTID
        {
            get;
            set;
        }


        /// <summary>
        ///科室名称
        /// </summary>
        public string DEPARTMENTNAME
        {
            get;
            set;
        }




        /// <summary>
        /// 所在区编码
        /// </summary>
        public string AREAID
        {
            get;
            set;
        }

        /// <summary>
        ///所在区名称 
        /// </summary>
        public string AREANAME
        {
            get;
            set;
        }
    }
}
