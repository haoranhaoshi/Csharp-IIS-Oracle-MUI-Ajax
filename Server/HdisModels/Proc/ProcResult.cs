using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HdisModels.Proc
{
    /// <summary>
    /// [功能描述:透析结果实体]
    /// [创建者:yinbsh]
    /// [创建时间:2017-06-08]
    /// </summary>
    public class ProcResult
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
        ///结果序号
        /// </summary>
        public string RESULTSEQ
        {
            get;
            set;
        }


        /// <summary>
        ///床位编码
        /// </summary>
        public string BEDID
        {
            get;
            set;
        }


        /// <summary>
        ///仪器编码
        /// </summary>
        public string MACHINEID
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
        ///肝素
        /// </summary>
        public string HEPARIN
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
        ///动脉压
        /// </summary>
        public string ARTERIALPRESSURE
        {
            get;
            set;
        }


        /// <summary>
        ///静脉压
        /// </summary>
        public string VENOUSPRESSURE
        {
            get;
            set;
        }


        /// <summary>
        ///跨膜压
        /// </summary>
        public string TRANSMEMBRANEPRESSURE
        {
            get;
            set;
        }


        /// <summary>
        ///超滤量
        /// </summary>
        public string ULTRAFILTRATIONVOLUME
        {
            get;
            set;
        }


        /// <summary>
        ///结果产生时间
        /// </summary>
        public DateTime RESULTTIME
        {
            get;
            set;
        }


        /// <summary>
        ///护士编码
        /// </summary>
        public string NURSEID
        {
            get;
            set;
        }


        /// <summary>
        ///护士名称
        /// </summary>
        public string NURSENAME
        {
            get;
            set;
        }

        /// <summary>
        ///血氧饱和度
        /// </summary>
        public string SPO2
        {
            get;
            set;
        }

    }
}
