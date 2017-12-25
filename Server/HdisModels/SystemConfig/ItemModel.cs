using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HdisModels.SystemConfig
{
  public  class ItemModel:Object
    {

        /// <summary>
        ///项目编码
        /// </summary>
        public string ITEMID
        {
            get;
            set;
        }


        /// <summary>
        ///项目中文名
        /// </summary>
        public string ITEMNAME
        {
            get;
            set;
        }


        /// <summary>
        ///通道号
        /// </summary>
        public string CHANNEL
        {
            get;
            set;
        }


        /// <summary>
        ///项目英文名
        /// </summary>
        public string ENGNAME
        {
            get;
            set;
        }


        /// <summary>
        ///单位
        /// </summary>
        public string UNIT
        {
            get;
            set;
        }


        /// <summary>
        ///小数位数(原始值,0,1,2,3,4,5,6)
        /// </summary>
        public string DOTNUM
        {
            get;
            set;
        }


        /// <summary>
        ///显示顺序
        /// </summary>
        public int SHOWSEQ
        {
            get;
            set;
        }


        /// <summary>
        ///操作人员
        /// </summary>
        public string OPERID
        {
            get;
            set;
        }


        /// <summary>
        ///操作时间
        /// </summary>
        public DateTime OPERTIME
        {
            get;
            set;
        }


        /// <summary>
        ///是否启用（0,不启用;1,启用）
        /// </summary>
        public bool ISUSE
        {
            get;
            set;
        }
    }
}
