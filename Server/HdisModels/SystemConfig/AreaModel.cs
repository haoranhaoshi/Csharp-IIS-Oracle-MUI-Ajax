using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HdisModels.SystemConfig
{
  public  class AreaModel:Object
    {
        private string areaId = string.Empty;
        private string areaName = string.Empty;
        /// <summary>
        /// 设备编码
        /// </summary>
        public string AreaID
        {
            set
            {
                areaId = value;
                this.Id = areaId;
            }
            get
            {
                return areaId;
            }
        }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string AreaName
        {
            set
            {
                areaName = value;
                this.Name = areaName;
            }
            get
            {
                return areaName;
            }
        }
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
        /// 是否有效
        /// </summary>
        public bool ISAVAILABLE { set; get; }
        /// <summary>
        /// 横向数量
        /// </summary>
        public int LANDSCAPECOUNT { set; get; }
        /// <summary>
        /// 区域类别（普通、急诊、特殊（乙肝，丙肝，HIV....））
        /// </summary>
        public string AREATYPE { set; get; }

        /// <summary>
        /// 区域优先级
        /// </summary>
        public string AREAPRIORITY { set; get; }
    }
}
