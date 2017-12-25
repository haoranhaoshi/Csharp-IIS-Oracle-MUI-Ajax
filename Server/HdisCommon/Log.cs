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
    /// [��������: 	
    /// ��Ӵ�ӡ��־����
    /// ]<br></br>
    /// [�� �� ��: Zhouw]<br></br>
    /// [����ʱ��: 2017-9-7]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public class Log
    {
        #region ö��
        /// <summary>
        /// ��־����
        /// </summary>
        public enum Loglevel
        {
            Error = 0,
            Info,
            Debug
        }

        #endregion

        #region �ֶ�

        // Ĭ��Log����
        private static Loglevel loglevelbase = HdisCommon.Log.Loglevel.Info; 

        // ��Ŀ¼·��
        private static string logDir = AppDomain.CurrentDomain.BaseDirectory + "\\Log";

        // �����ļ�·��
        private static string Loadpath = AppDomain.CurrentDomain.BaseDirectory + "Configure\\LogConfig.xml";
         
        // Ĭ�ϱ�������
        private static int Retentiontime = 30;

        // ˽�о�̬�ֶΣ�������ʼ��ȫ�ֱ���
        private static Log mLog = new Log();

        #endregion

        private Log()
        {
            GetXml();
             
            // �ж��ļ�·���Ƿ���ڣ��������򴴽��ļ��� 
            if (!System.IO.Directory.Exists(logDir))
            {
                // �����ھʹ���Ŀ¼
                System.IO.Directory.CreateDirectory(@logDir);
            }

            DeleteLog(logDir);
        }

        #region ����

        /// <summary>
        /// ��ȡ�����ļ�
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
        /// ѡ����־�ȼ�
        /// </summary>
        /// <param name="str">�������־�ȼ�</param>
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
                default: Console.WriteLine("��־�ȼ�����");
                    break;
            }
        }
        /// <summary>
        /// ����ɾ����־
        /// </summary>
        /// <param name="path">�ļ���·��</param>
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
                        // ɾ����־����������3����ǰ����־
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
        /// д��ϵͳ��־
        /// ����Logging��������һ������Ĭ��Log����ΪInformation<br></br>
        /// </summary>
        /// <param name="format">������ַ���</param>
        /// <param name="args">������������</param>
        public static void Logging(String format, params Object[] args)
        {
            HdisCommon.Log.Logging(HdisCommon.Log.Loglevel.Info, format, args);
        }

        /// <summary>
        /// д��ϵͳ��־
        /// </summary>
        /// <param name="loglevel">��־����</param>
        /// <param name="format">������ַ���</param>
        /// <param name="args">������������</param>
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
                    throw new Exception("Logging����������ƥ�䣡����", ex);
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
