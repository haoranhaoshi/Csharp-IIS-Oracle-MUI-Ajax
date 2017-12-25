using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
//配置数据库连接静态类
namespace HdisDataBase
{
    public class ConnectConfig
    {
        //Oracle数据库连接串
        public static string strOracleConnectString = string.Empty;
        //数据库类型
        public static string strDbType = string.Empty;
        //数据库连接状态
        public static Boolean dbConnectState = false;

        public string GetOracleConnectString()
        {
            try
            {
                //用XmlTextReader类的对象来读取该XML文档
               // XmlTextReader xmlReader = new System.Xml.XmlTextReader(Application.StartupPath + "\\ConnectSetting.xml");
                XmlTextReader xmlReader = new System.Xml.XmlTextReader(AppDomain.CurrentDomain.BaseDirectory + "\\bin\\ConnectSetting.xml");
                //用XmlDocument类的对象来加载XML文档
                XmlDocument xmlDoc = new System.Xml.XmlDocument();
                //加载xmlReader
                xmlDoc.Load(xmlReader);
                //读取节点
                XmlNodeList xmlNodeList = xmlDoc.SelectNodes("数据库连接/Oracle数据库");
                //读取数据库配置串
                strOracleConnectString = xmlNodeList.Item(0).Attributes[0].Value;

                //strOracleConnectString = "data source=orcl;password=lis;persist security info=True;user id=yinbsh";
                //读取数据库类型
                strDbType = xmlNodeList.Item(0).Attributes[1].Value;
                //strDbType = "ORACLE";
                return strOracleConnectString;
            }
            catch
            {
                HdisCommon.Log.Logging("{0}", "判断连接串是否正确！");
                
                return "";
            }
        }
    }
}
