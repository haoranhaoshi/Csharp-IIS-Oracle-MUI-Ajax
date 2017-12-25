using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HdisModels
{
    /// <summary>
    /// [功能描述:护士透析主实体]
    /// [创建者:yinbsh]
    /// [创建时间:2017-04-14]
    /// </summary>
    public class NurseDetail : Object
    {
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
        ///透析次数
        /// </summary>
        public string DIALYSISTIMES
        {
            get;
            set;
        }


        /// <summary>
        ///透析频率
        /// </summary>
        public string DIALYSISFREQUENCY
        {
            get;
            set;
        }


        /// <summary>
        ///抗凝方法
        /// </summary>
        public string ANTICOAGULANTMETHOD
        {
            get;
            set;
        }


        /// <summary>
        ///干体重（基本体重）
        /// </summary>
        public string BASICWEIGHT
        {
            get;
            set;
        }


        /// <summary>
        ///上次透后体重
        /// </summary>
        public string LASTWEIGHT
        {
            get;
            set;
        }


        /// <summary>
        ///本次透前体重
        /// </summary>
        public string THISWEIGHT
        {
            get;
            set;
        }


        /// <summary>
        ///透气器
        /// </summary>
        public string DIALYZER
        {
            get;
            set;
        }


        /// <summary>
        ///透析方式
        /// </summary>
        public string DIALYSISMODEL
        {
            get;
            set;
        }


        /// <summary>
        ///透析状态（0：未透析，1：透析中，2：透析后，3：透析结束）
        /// </summary>
        public string DIALYSISSTATE
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
        ///乙肝
        /// </summary>
        public string HBV
        {
            get;
            set;
        }


        /// <summary>
        ///丙肝
        /// </summary>
        public string HCV
        {
            get;
            set;
        }


        /// <summary>
        ///HIV
        /// </summary>
        public string HIV
        {
            get;
            set;
        }


        /// <summary>
        ///梅毒
        /// </summary>
        public string TP
        {
            get;
            set;
        }


        /// <summary>
        ///ABO血型
        /// </summary>
        public string ABO
        {
            get;
            set;
        }


        /// <summary>
        ///RH血型
        /// </summary>
        public string RH
        {
            get;
            set;
        }


        /// <summary>
        ///是否特殊患者（乙肝病毒携带者，丙肝病毒携带者..）
        /// </summary>
        public string ISSPECIALPATIENT
        {
            get;
            set;
        }


        /// <summary>
        ///透析科室编码
        /// </summary>
        public string DEPARTMENTID
        {
            get;
            set;
        }


        /// <summary>
        ///透析科室名称
        /// </summary>
        public string DEPARTMENTNAME
        {
            get;
            set;
        }

        /// <summary>
        /// 分院编码
        /// </summary>
        public string HOSPITALID { set; get; }
        /// <summary>
        /// 分院名称
        /// </summary>
        public string HOSPITALNAME { set; get; }

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
        ///患者编码
        /// </summary>
        public string PATIENTID
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
        ///患者类别
        /// </summary>
        public string PATIENTTYPE
        {
            get;
            set;
        }


        /// <summary>
        ///联系电话
        /// </summary>
        public string PATIENTPHONE
        {
            get;
            set;
        }


        /// <summary>
        ///联系地址
        /// </summary>
        public string PATIENTADDRESS
        {
            get;
            set;
        }


        /// <summary>
        ///医保类型
        /// </summary>
        public string MEDICALINSURANCE
        {
            get;
            set;
        }


        /// <summary>
        ///诊断
        /// </summary>
        public string DIAGNOSIS
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
        ///首次透析日期
        /// </summary>
        public DateTime MEASURINGTIME
        {
            get;
            set;
        }


        /// <summary>
        ///审核医嘱时间
        /// </summary>
        public DateTime ORDERCHECKTIME
        {
            get;
            set;
        }


        /// <summary>
        ///是否审核医嘱
        /// </summary>
        public string ORDERCHECKSTATE
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
        /// 审核医嘱护士编码
        /// </summary>
        public string ORDERCHECKNURSEID
        {
            get;
            set;
        }

        /// <summary>
        /// 审核医嘱护士名称
        /// </summary>
        public string ORDERCHECKNURSENAME
        {
            get;
            set;
        }

        /// <summary>
        /// 分解医嘱时间
        /// </summary>
        public DateTime ORDERANALYZETIME
        {
            get;
            set;
        }

        /// <summary>
        /// 是否分解医嘱
        /// </summary>
        public string ORDERANALYZESTATE
        {
            get;
            set;
        }

        /// <summary>
        /// 分解医嘱护士编码
        /// </summary>
        public string ORDERANALYZENURSEID
        {
            get;
            set;
        }

        /// <summary>
        /// 分解医嘱护士名称
        /// </summary>
        public string ORDERANALYZENURSENAME
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
        /// 首次透析时间
        /// </summary>
        public DateTime FIRSTDIALYSISDATE
        {
            get;
            set;
        }
    }
}
