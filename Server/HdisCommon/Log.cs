using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Configuration;
using System.Xml;

namespace HdisCommon
{
    /// <summary>
    /// [功能描述: 	
    /// 添加打印日志功能
    /// ]<br></br>
    /// [创 建 者: Zhouw]<br></br>
    /// [创建时间: 2017-9-7]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间=''
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public class Log
    {
        #region 枚举
        /// <summary>
        /// 日志级别
        /// </summary>
        public enum Loglevel
        {
            Error = 0,
            Info,
            Debug
        }

        #endregion

        #region 字段

        // 默认Log级别
        private static Loglevel loglevelbase = HdisCommon.Log.Loglevel.Info; 

        // 基目录路径
        private static string logDir = AppDomain.CurrentDomain.BaseDirectory + "\\Log";

        // 配置文件路径
        private static string Loadpath = AppDomain.CurrentDomain.BaseDirectory + "Configure\\LogConfig.xml";
         
        // 默认保留天数
        private static int Retentiontime = 30;

        // 私有静态字段，用作初始化全局变量
        private static Log mLog = new Log();

        #endregion

        private Log()
        {
            GetXml();
             
            // 判断文件路径是否存在，不存在则创建文件夹 
            if (!System.IO.Directory.Exists(logDir))
            {
                // 不存在就创建目录
                System.IO.Directory.CreateDirectory(@logDir);
            }

            DeleteLog(logDir);
        }

        #region 方法

        /// <summary>
        /// 读取配置文件
        /// </summary>
        private void GetXml()
        {
            System.Xml.XmlDocument xml = new System.Xml.XmlDocument();

            if (File.Exists(Loadpath))
            {
                xml.Load(@Loadpath);
                XmlNode root = xml.SelectSingleNode("LogConfigSettings");
                XmlNodeList childrenlist = root.ChildNodes;

                if (null != childrenlist)
                {
                    foreach (XmlNode ai in childrenlist)
                    {
                        switch (ai.Attributes[0].Value)
                        {
                            case "LogLevel": SetLoglevel(ai.Attributes[1].Value.ToString());
                                break;
                            case "LogPath": logDir = ai.Attributes["value"].Value.ToString();
                                break;
                            case "RetentionTime": Retentiontime = Convert.ToInt32(ai.Attributes["value"].Value);
                                break;
                            default:
                                break;
                        }
                    } // end foreach
                } // end if (null != childrenlist)
            }
            else
            {
                loglevelbase = HdisCommon.Log.Loglevel.Info;
                logDir = AppDomain.CurrentDomain.BaseDirectory + "\\Log";
                Retentiontime = 30;
            }
            return;
        }

        /// <summary>
        /// 选择日志等级
        /// </summary>
        /// <param name="str">传入的日志等级</param>
        private void SetLoglevel(string str)
        {
            switch (str)
            {
                case "Debug": loglevelbase = HdisCommon.Log.Loglevel.Debug;
                    break;
                case "Error": loglevelbase = HdisCommon.Log.Loglevel.Error;
                    break;
                case "Info": loglevelbase = HdisCommon.Log.Loglevel.Info;
                    break;
                default: Console.WriteLine("日志等级错误！");
                    break;
            }
        }
        /// <summary>
        /// 定期删除日志
        /// </summary>
        /// <param name="path">文件夹路径</param>
        private void DeleteLog(string path)
        {
            if (Directory.Exists(@path))
            {
                DirectoryInfo dinfor = new DirectoryInfo(path);
                FileInfo[] files = dinfor.GetFiles();
                foreach (FileInfo file in files)
                {
                    try
                    {
                        // 删除日志名称日期是3个月前的日志
                        if (DateTime.Compare(Convert.ToDateTime(file.Name.Substring(0, 10)),
                            DateTime.Now.AddDays(-Retentiontime)) < 0)
                        {
                            file.Delete();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                } //end foreach
            } //end if (Directory.Exists(@path))
        }

        /// <summary>
        /// 写入系统日志
        /// 重载Logging函数，第一个参数默认Log级别为Information<br></br>
        /// </summary>
        /// <param name="format">输出的字符串</param>
        /// <param name="args">传入的任意参数</param>
        public static void Logging(String format, params Object[] args)
        {
            HdisCommon.Log.Logging(HdisCommon.Log.Loglevel.Info, format, args);
        }

        /// <summary>
        /// 写入系统日志
        /// </summary>
        /// <param name="loglevel">日志级别</param>
        /// <param name="format">输出的字符串</param>
        /// <param name="args">传入的任意参数</param>
        public static void Logging(HdisCommon.Log.Loglevel loglevel, String format, params Object[] args)
        {
            if (loglevelbase >= loglevel)
            {
                try
                {
                    string nowDateString = DateTime.Now.ToString("yyyy-MM-dd");
                    string str = " ";
                    str = string.Format(format, args);
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(logDir + "\\" + nowDateString + ".log", true);
                    sw.WriteLine(DateTime.Now.ToString() + " [" + loglevel + "]:" + str);
                    sw.Flush();
                    sw.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception("Logging参数个数不匹配！！！", ex);
                }
            }
            else
            {
                return;
            }
        }
    }
        #endregion
}
