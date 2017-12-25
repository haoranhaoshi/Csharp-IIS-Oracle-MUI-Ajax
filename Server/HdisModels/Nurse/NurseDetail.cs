using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HdisModels.Nurse
{
    /// <summary>
    /// [功能描述:护士透析主实体]
    /// [创建者:yinbsh]
    /// [创建时间:2017-04-14]
    /// </summary>
    public class NurseDetail : Object
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public NurseDetail()
        {
            this.AREAID = "";
            this.AREANAME = "";
            this.BEDID = "";
            this.BEDNAME = "";
            this.CLASSES = "";
            this.Patient = new Doctor.PatientModel();
            this.DoctorDetail = new Doctor.DoctorDetail();

        }

        /// <summary>
        /// 患者实体
        /// </summary>
        public Doctor.PatientModel Patient { get; set; }

        ///// <summary>
        ///// 患者透析方案时间
        ///// </summary>
        //public Appointment.AppointmentInfo Appointment { get; set; }

        /// <summary>
        /// 医生实体
        /// </summary>
        public Doctor.DoctorDetail DoctorDetail { get; set; }

        /// <summary>
        ///透析号
        /// </summary>
        public string CARDID
        {
            get;
            set;
        }

        /// <summary>
        ///透析流水号
        /// </summary>
        public string SEQID
        {
            get;
            set;
        }

        /// <summary>
        ///透析日期
        /// </summary>
        public string TESTDATE
        {
            get;
            set;
        }

        /// <summary>
        ///透析状态（0：未检测，1：已检测，2：已上机，3：已下机，4：未完成5：已完成）
        /// </summary>
        public string DIALYSISSTATE
        {
            get;
            set;
        }

        /// <summary>
        ///所在区编码
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

        /// <summary>
        /// 区域类别
        /// </summary>
        public string AREATYPE
        {
            get;
            set;
        }

        /// <summary>
        ///备注
        /// </summary>
        public string MEMO
        {
            get;
            set;
        }

        /// <summary>
        ///床编码
        /// </summary>
        public string BEDID
        {
            get;
            set;
        }

        /// <summary>
        ///床名称
        /// </summary>
        public string BEDNAME
        {
            get;
            set;
        }

        /// <summary>
        /// 床位类型
        /// </summary>
        public string BEDTYPE { set; get; }

        /// <summary>
        ///仪器编码
        /// </summary>
        public string MACHINEID
        {
            get;
            set;
        }

        /// <summary>
        ///仪器名称
        /// </summary>
        public string MACHINENAME
        {
            get;
            set;
        }

        /// <summary>
        ///上机时间
        /// </summary>
        public DateTime UPMACHINETIME
        {
            get;
            set;
        }

        /// <summary>
        ///上机状态
        /// </summary>
        public string UPMACHINESTATE
        {
            get;
            set;
        }

        /// <summary>
        ///下机时间
        /// </summary>
        public DateTime DOWNMACHINETIME
        {
            get;
            set;
        }

        /// <summary>
        ///下机状态
        /// </summary>
        public string DOWNMACHINESTATE
        {
            get;
            set;
        }

        /// <summary>
        /// 上机护士编码
        /// </summary>
        public string UPMACHINENURSEID
        {
            get;
            set;
        }

        /// <summary>
        /// 上机护士名称
        /// </summary>
        public string UPMACHINENURSENAME
        {
            get;
            set;
        }

        /// <summary>
        /// 下机护士编码
        /// </summary>
        public string DOWNMACHINENURSEID
        {
            get;
            set;
        }

        /// <summary>
        /// 下机护士名称
        /// </summary>
        public string DOWNMACHINENURSENAME
        {
            get;
            set;
        }

        /// <summary>
        /// 班次
        /// </summary>
        public string CLASSES
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
        /// 透析治疗模式
        /// </summary>
        public string DIALYSISMODE
        {
            get;
            set;
        }

        /// <summary>
        /// 透析器
        /// </summary>
        public string DIALYZER
        {
            get;
            set;
        }

        /// <summary>
        /// 签到状态
        /// </summary>
        public string SIGNINSTATE
        {
            get;
            set;
        }

        /// <summary>
        /// 透析小结
        /// </summary>
        public string DIALYSISSUMMARY
        {
            get;
            set;
        }

        /// <summary>
        /// 实际治疗时间
        /// </summary>
        public string TREATMENTDURATION
        {
            get;
            set;
        }

        /// <summary>
        /// 实际超滤量
        /// </summary>
        public string FACTSFILTRATIONQUANTITY
        {
            get;
            set;
        }

        /// <summary>
        /// 透后体重
        /// </summary>
        public string AFTERWEIGHT
        {
            get;
            set;
        }

        /// <summary>
        /// 体重减少
        /// </summary>
        public string LESSENWEIGHT
        {
            get;
            set;
        }

        /// <summary>
        /// 心率
        /// </summary>
        public string HEARTRATE
        {
            get;
            set;
        }

    }
}
