using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace HdisCommon
{
    /// <summary>
    /// [功能描述: 	
    /// 添加配置文件管理功能
    /// ]<br></br>
    /// [创 建 者: Zhouw]<br></br>
    /// [创建时间: 2017-9-20]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间=''
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public class Configer
    {
        #region 字段
        // xml文件基路径
        public string strFileName = System.AppDomain.CurrentDomain.BaseDirectory + "Configure";

        // 错误
        public string Err { set; get; }
        #endregion

        public Configer()
        {
            strFileName = strFileName + "\\" + "*.xml";
        }

        public Configer(string name)
        {
            strFileName = strFileName + "\\" + name + ".xml";
        }

        #region 方法
        /// <summary>
        /// 添加XML注释
        /// </summary>
        /// <param name="tn">添加注释的节点</param>
        /// <param name="strComment">添加的注释</param>
        /// <param name="xml">想修改的xml文件</param>
        private  void AddXmlComment(XmlNode tn, string strComment,XmlDocument xml)
        {
            XmlComment comment = xml.CreateComment(strComment);
            tn.ParentNode.InsertBefore(comment, tn);
            xml.Save(strFileName);
        }

        /// <summary>
        /// 递归子节点返回指定子节点的值
        /// </summary>
        /// <param name="node">根节点</param>
        /// <param name="a">字符串分离的数组</param>
        /// <param name="value">返回的子节点的值</param>
        private void GetGuidNode(XmlNode node, string[] a, ref string value)
        {
            foreach (XmlNode nodechild in node.ChildNodes)
            {
                if (nodechild.ChildNodes.Count > 0)
                {
                    GetGuidNode(nodechild, a, ref value);
                }
                else
                {
                    // 最后的子节点
                    int longa = a.Length - 1;
                    // 倒数第二个子节点
                    int longb = longa - 1;
                    if ((a[longa] == nodechild.ParentNode.Name) && (nodechild.ParentNode.ParentNode.Name == a[longb]))
                    {
                        value = nodechild.InnerText;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 递归子节点修改指定子节点的值
        /// </summary>
        /// <param name="node">根节点</param>
        /// <param name="a">字符串分离的数组</param>
        /// <param name="value">修改的值</param>
        /// <param name="xml">修改的xml文件</param>
        /// <param name="strComment">添加的注释</param>
        private void SetGuidNode(XmlNode node, string[] a, string value, XmlDocument xml, string strComment)
        {
            foreach (XmlNode nodechild in node.ChildNodes)
            {
                if (nodechild.ChildNodes.Count > 0)
                {
                    SetGuidNode(nodechild, a, value, xml, strComment);
                }
                else
                {
                    // 最后的子节点
                    int longa = a.Length - 1;
                    // 倒数第二个子节点
                    int longb = longa - 1;
                    if ((a[longa] == nodechild.ParentNode.Name) && (nodechild.ParentNode.ParentNode.Name == a[longb]))
                    {
                        nodechild.InnerText = value;
                        AddXmlComment(nodechild.ParentNode, strComment, xml);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 修改XML
        /// </summary>
        /// <param name="key">想要修改的子节点</param>
        /// <param name="value">修改的值</param>
        /// <param name="strComment">添加的注释</param>
        /// <returns></returns>
        public bool SetConfig(string key, string value,string strComment)
        {
            try
            {
                XmlDocument xml = new XmlDocument();

                //加载Xml
                xml.Load(strFileName);

                // 分离key
                string[] strval = key.Split('/');

                // 根节点
                XmlNode root = xml.SelectSingleNode(strval[1]);
                SetGuidNode(root, strval, value, xml, strComment);

                //保存上面的修改
                xml.Save(strFileName);
            }
            catch (Exception ex)
            {
                Err = ex.Message;
                return false;
            }
            return true;
        }

        /// <summary>
        /// 读取XML
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool GetConfig(string key, ref string value)
        {
            try
            {
                XmlDocument doc = new XmlDocument();

                // 加载Xml
                if (File.Exists(strFileName))
                {
                    doc.Load(strFileName);

                    // 分离key
                    string[] strval = key.Split('/');

                    // 根节点
                    XmlNode root = doc.SelectSingleNode(strval[1]);
                    GetGuidNode(root, strval, ref value);
                }
                else
                {
                    if (string.IsNullOrEmpty(value))
                    {
                        return false;
                    }
                }
            }
            catch(Exception ex) 
            {
                Err = ex.Message;
                return false;
            }
            return true;
        }
        #endregion
    }
}
