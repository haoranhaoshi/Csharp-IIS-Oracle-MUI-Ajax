using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HdisModels.SystemConfig
{
   public class BedAndMachineModel:Object
    {
        private string bedId = string.Empty;
        private string bedName = string.Empty;
        private string machineId = string.Empty;
        private string machineName = string.Empty;
        /// <summary>
        /// 床位编码
        /// </summary>
        public string BedID
        {
            set
            {
                bedId = value;
            }
            get
            {
                return bedId;
            }
        }

        /// <summary>
        /// 床位名称
        /// </summary>
        public string BedName
        {
            set
            {
                bedName = value;
            }
            get
            {
                return bedName;
            }
        }

        /// <summary>
        /// 设备编码
        /// </summary>
        public string MachineID
        {
            set
            {
                machineId = value;
            }
            get
            {
                return machineId;
            }
        }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string MachineName
        {
            set
            {
                machineName = value;
            }
            get
            {
                return machineName;
            }
        }

        /// <summary>
        /// 区域编码
        /// </summary>
        public string AREAID { set; get; }
        /// <summary>
        /// 区域名称
        /// </summary>
        public string AREANAME { set; get; }
        /// <summary>
        /// 科室编码
        /// </summary>
        public string DEPTID { set; get; }
        /// <summary>
        /// 科室名称
        /// </summary>
        public string DEPTNAME { set; get; }
        /// <summary>
        /// 分院编码
        /// </summary>
        public string HOSPITALID { set; get; }
        /// <summary>
        /// 分院名称
        /// </summary>
        public string HOSPITALNAME { set; get; }
        /// <summary>
        /// 是否显示
        /// </summary>
        public bool ISUSED { set; get; }
        /// <summary>
        /// 床位类型
        /// </summary>
        public string BEDTYPE { set; get; }

        /// <summary>
        /// 设备厂家
        /// </summary>
        public string FACTORY { set; get; }
        /// <summary>
        /// 设备型号
        /// </summary>
        public string VERSION { set; get; }

        /// <summary>
        /// 透析器
        /// </summary>
        public string DIALYZER { set; get; }

        /// <summary>
        /// 透析模式
        /// </summary>
        public string DIALYSISMODE { set; get; }

        /// <summary>
        /// 床位优先级
        /// </summary>
        public string BEDPRIORITY { set; get; }
    }
}
