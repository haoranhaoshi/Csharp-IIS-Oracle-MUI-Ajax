using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HdisModels.Doctor
{
   public class DoctorDetail
    {

        public DoctorDetail()
        {
            this.CARDID = "";
            this.TESTDATE = "";
            this.SEQID = "";
            this.DIALYSISTIMES = "";
            this.PATIENTID = "";
            this.PATIENTTYPE = "";
            this.PATIENTNAME = "";
            this.PATIENTSEX = "";
            this.PATIENTAGE = "";
            this.SPRESSURE = "";
            this.DPRESSURE = "";
            this.PULSE = "";
            this.BREATHING = "";
            this.TEMPERATURE = "";
            this.DRYWEIGHT = "";
            this.FRONTWEIGHT = "";
            this.INCREASEWEIGHT = "";
            this.AFTERWEIGHT = "";
            this.FACTSFILTRATIONQUANTITY = "";
            this.MEASUREMENTID = "";
            this.MEASUREMENTNAME = "";
            this.MEASUREMENTTIME = new DateTime();
            this.RECORDID = "";
            this.RECORDNAME = "";
            this.RECORDTIME = new DateTime();
            this.DIALYSISMODE = "";
            this.DIALYZER = "";
            this.TREATMENTDURATION = "";
            this.BLOODFOLW = "";
            this.TARGETWEIGHT = "";
            this.FILTRATIONQUANTITY = "";
            this.CHANGEQUANTITY = "";
            this.MACHINETYPE = "";
            this.VASCULARACCESS = "";
            this.DIALYSATETYPE = "";
            this.FOLW = "";
            this.NA = "";
            this.CA = "";
            this.HCO2 = "";
            this.ANTICOAGULANT = "";
            this.FRIST = "";
            this.FRISTUNIT = "";
            this.SECOND = "";
            this.SECONDUNIT = "";
            this.MEMO = "";
            this.LASTTESTDATE = "";
            this.DANCHAOHOUR = "";
            this.DANCHAOML = "";
            this.TOTAL = "";
            this.TOTALUNIT = "";
        }

        /// <summary>
        ///透析号
        /// </summary>
        public string CARDID
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
        ///透析流水号
        /// </summary>
        public string SEQID
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
        ///患者号
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
        ///性别
        /// </summary>
        public string PATIENTSEX
        {
            get;
            set;
        }


        /// <summary>
        ///年龄
        /// </summary>
        public string PATIENTAGE
        {
            get;
            set;
        }


        /// <summary>
        ///收缩压
        /// </summary>
        public string SPRESSURE
        {
            get;
            set;
        }


        /// <summary>
        ///舒张压
        /// </summary>
        public string DPRESSURE
        {
            get;
            set;
        }


        /// <summary>
        ///脉搏
        /// </summary>
        public string PULSE
        {
            get;
            set;
        }


        /// <summary>
        ///呼吸次数
        /// </summary>
        public string BREATHING
        {
            get;
            set;
        }


        /// <summary>
        ///体温
        /// </summary>
        public string TEMPERATURE
        {
            get;
            set;
        }


        /// <summary>
        ///上次透后体重
        /// </summary>
        public string DRYWEIGHT
        {
            get;
            set;
        }


        /// <summary>
        ///透前体重
        /// </summary>
        public string FRONTWEIGHT
        {
            get;
            set;
        }


        /// <summary>
        ///增加体重
        /// </summary>
        public string INCREASEWEIGHT
        {
            get;
            set;
        }


        /// <summary>
        ///透后体重
        /// </summary>
        public string AFTERWEIGHT
        {
            get;
            set;
        }


        /// <summary>
        ///实际超滤量
        /// </summary>
        public string FACTSFILTRATIONQUANTITY
        {
            get;
            set;
        }


        /// <summary>
        ///测量医生ID
        /// </summary>
        public string MEASUREMENTID
        {
            get;
            set;
        }


        /// <summary>
        ///测量医生名
        /// </summary>
        public string MEASUREMENTNAME
        {
            get;
            set;
        }


        /// <summary>
        ///测量时间
        /// </summary>
        public DateTime MEASUREMENTTIME
        {
            get;
            set;
        }


        /// <summary>
        ///记录医生ID
        /// </summary>
        public string RECORDID
        {
            get;
            set;
        }


        /// <summary>
        ///记录医生名
        /// </summary>
        public string RECORDNAME
        {
            get;
            set;
        }


        /// <summary>
        ///记录时间
        /// </summary>
        public DateTime RECORDTIME
        {
            get;
            set;
        }


        /// <summary>
        ///治疗模式
        /// </summary>
        public string DIALYSISMODE
        {
            get;
            set;
        }


        /// <summary>
        ///透析器
        /// </summary>
        public string DIALYZER
        {
            get;
            set;
        }


        /// <summary>
        ///治疗时长
        /// </summary>
        public string TREATMENTDURATION
        {
            get;
            set;
        }


        /// <summary>
        ///血流量
        /// </summary>
        public string BLOODFOLW
        {
            get;
            set;
        }


        /// <summary>
        ///目标体重
        /// </summary>
        public string TARGETWEIGHT
        {
            get;
            set;
        }


        /// <summary>
        ///超滤总量
        /// </summary>
        public string FILTRATIONQUANTITY
        {
            get;
            set;
        }


        /// <summary>
        ///置换量
        /// </summary>
        public string CHANGEQUANTITY
        {
            get;
            set;
        }


        /// <summary>
        ///透析机类型
        /// </summary>
        public string MACHINETYPE
        {
            get;
            set;
        }


        /// <summary>
        ///血管通路
        /// </summary>
        public string VASCULARACCESS
        {
            get;
            set;
        }


        /// <summary>
        ///透析液类型
        /// </summary>
        public string DIALYSATETYPE
        {
            get;
            set;
        }


        /// <summary>
        ///流量
        /// </summary>
        public string FOLW
        {
            get;
            set;
        }


        /// <summary>
        ///钠
        /// </summary>
        public string NA
        {
            get;
            set;
        }


        /// <summary>
        ///钙
        /// </summary>
        public string CA
        {
            get;
            set;
        }


        /// <summary>
        ///碳酸氢根
        /// </summary>
        public string HCO2
        {
            get;
            set;
        }


        /// <summary>
        ///抗凝剂
        /// </summary>
        public string ANTICOAGULANT
        {
            get;
            set;
        }


        /// <summary>
        ///首量
        /// </summary>
        public string FRIST
        {
            get;
            set;
        }


        /// <summary>
        ///首量单位
        /// </summary>
        public string FRISTUNIT
        {
            get;
            set;
        }


        /// <summary>
        ///追加
        /// </summary>
        public string SECOND
        {
            get;
            set;
        }


        /// <summary>
        ///追加单位
        /// </summary>
        public string SECONDUNIT
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
        /// 总结
        /// </summary>
        public string Summary
        {
            get;
            set;
        }

        /// <summary>
        /// 透析状态
        /// </summary>
        public string STATE
        {
            get;
            set;
        }

        /// <summary>
        /// 前次透析日期
        /// </summary>
        public string LASTTESTDATE
        {
            get;
            set;
        }

        /// <summary>
        /// 单超时间
        /// </summary>
        public string DANCHAOHOUR
        {
            get;
            set;
        }

        /// <summary>
        /// 单超毫升
        /// </summary>
        public string DANCHAOML
        {
            get;
            set;
        }

        /// <summary>
        /// 总量
        /// </summary>
        public string TOTAL
        {
            get;
            set;
        }

        /// <summary>
        /// 总量单位
        /// </summary>
        public string TOTALUNIT
        {
            get;
            set;
        }

    }
}
