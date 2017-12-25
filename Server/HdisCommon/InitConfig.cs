using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HdisCommon
{
    /// <summary>
    /// [功能描述: 可配信息]
    /// [创建者:zhangh]
    /// [创建时间:2017-08-17]
    /// </summary>
    public class InitConfig
    {
        //是否读过配置文件
        public static bool isReadConfig = false;

        //服务器连接串
        public static string ConnectionString = string.Empty;

        //APP版本号
        public static string VersionNumber = string.Empty;

        //护士小结默认内容
        public static string NurseSummary = string.Empty;

        /// <summary>
        /// 读取INI配置
        /// </summary>
        public void GetIniConfig()
        {
            string value = "";
            String key = "/Config/ConnectionString";
            HdisCommon.Configer configer = new Configer("InitConfig");
            configer.GetConfig(key,ref value);
            InitConfig.ConnectionString = value;

            string key1 = "/Config/VersionNumber";
            configer.GetConfig(key1,ref value);
            InitConfig.VersionNumber = value;
            isReadConfig = true;

            //String key2 = "/Config/NurseSummary";
            //configer.GetConfig(key2, ref value);
            //InitConfig.NurseSummary = value;
        } 
    }
}
