using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HdisModels.State
{
    /// <summary>
    /// [功能描述:登录状态实体]
    /// [创建者:zhangh]
    /// [创建时间:2017-06-20]
    /// </summary>
    public class DalysisState : Object
    {
        /// <summary>
        ///状态ID
        /// </summary>
        public string DICID
        {
            get;
            set;
        }

        /// <summary>
        ///状态名称
        /// </summary>
        public string DICNAME
        {
            get;
            set;
        }

        /// <summary>
        ///状态颜色
        /// </summary>
        public string MEMO3
        {
            get;
            set;
        }
    }
}
