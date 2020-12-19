using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Xml.Serialization;

namespace Nkit.Core.Utils
{
    /// <summary>
    /// xml帮助类
    /// </summary>
    public class XMLHelper
    {
        /// <summary>
        /// 创建一个dom对象
        /// </summary>
        /// <returns>返回一个dom对象</returns>
        public static XmlDocument CreateNewDoc()
        {
            XmlDocument doc = new XmlDocument();
            return doc;
        }
        /// <summary>
        /// 创建一个dom对象
        /// </summary>
        /// <param name="strXML">xml文件路径</param>
        /// <returns>返回一个dom对象</returns>
        public static XmlDocument CreateNewDoc(string strXML)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(strXML);
                return doc;
            }
            catch (Exception e)
            {
                return null;
                throw e;
            }
        }
        /// <summary>
        /// 功能：将一段XML信息添加到一个Dom对象中去
        /// </summary>
        /// <param name="doc">Dom对象</param>
        /// <param name="parentNode">待加入的父节点</param>
        /// <param name="dataInfo">待加入的XML信息</param>
        /// <returns></returns>
        public static Boolean setNodeDom(XmlDocument doc, string parentNode, string dataInfo)
        {
            try
            {
                string strFirstNodeName;
                string strTmpData;
                XmlNode node;
                try
                {
                    XmlDocument docTmp = CreateNewDoc();
                    docTmp.LoadXml(dataInfo);
                    strFirstNodeName = docTmp.DocumentElement.Name;
                    strTmpData = docTmp.DocumentElement.InnerXml;
                    docTmp = null;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                node = doc.SelectSingleNode(parentNode);

                XmlElement elem = doc.CreateElement(strFirstNodeName);
                elem.InnerXml = strTmpData;

                node.AppendChild(elem);

                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 取节点
        /// </summary>
        /// <param name="doc">xml文档</param>
        /// <param name="xPath">xml路径</param>
        /// <returns></returns>
        public static XmlNode getNode(XmlDocument doc, string xPath)
        {
            try
            {
                return doc.SelectSingleNode(xPath);
            }
            catch (Exception e)
            {
                return null;
            }
        }
        /// <summary>
        /// 取节点
        /// </summary>
        /// <param name="el">xml元素</param>
        /// <param name="xPath">xml路径</param>
        /// <returns></returns>
        public static XmlNode getNode(XmlElement el, string xPath)
        {
            try
            {
                return el.SelectSingleNode(xPath);
            }
            catch (Exception e)
            {
                return null;
                throw e;
            }
        }
        /// <summary>
        /// 取子节点
        /// </summary>
        /// <param name="doc">XMl文档</param>
        /// <param name="xPath">xml路径</param>
        /// <param name="i">索引</param>
        /// <returns></returns>
        public static XmlNode getNode(XmlDocument doc, string xPath, int i)
        {
            try
            {
                return doc.SelectNodes(xPath).Item(i);
            }
            catch (Exception e)
            {
                return null;
                throw e;
            }
        }
        /// <summary>
        /// 取XML文档元素
        /// </summary>
        /// <param name="doc">XMl文档</param>
        /// <param name="xPath">xml路径</param>
        /// <returns></returns>
        public static XmlElement getElement(XmlDocument doc, string xPath)
        {
            return (XmlElement)getNode(doc, xPath);
        }
        /// <summary>
        /// 生成XML节点
        /// </summary>
        /// <param name="xDocument">XmlDocument</param>
        /// <param name="elementName">元素名称</param>
        /// <param name="textName">文本值</param>
        /// <returns>XmlElement</returns>
        public static XmlElement createXmlNode(XmlDocument xDocument, string elementName, string textName)
        {
            try
            {
                XmlElement xElement = xDocument.CreateElement(elementName);
                XmlText xText = xDocument.CreateTextNode(textName);
                xElement.AppendChild(xText);
                return xElement;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 生成带CDATA的节点
        /// </summary>
        /// <param name="xDocument">XmlDocument</param>
        /// <param name="elementName">元素名称</param>
        /// <param name="cdataValue">CDATA值</param>
        /// <returns>XmlElement</returns>
        public static XmlElement createXmlNodeCDATA(XmlDocument xDocument, string elementName, string cdataValue)
        {
            try
            {
                XmlElement xElement = xDocument.CreateElement(elementName);
                XmlCDataSection cdata = xDocument.CreateCDataSection(cdataValue);
                xElement.AppendChild(cdata);
                return xElement;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 取指定节点的属性的值
        /// </summary>
        /// <param name="filePath">xml文件路径</param>
        /// <param name="xPath">节点路径</param>
        /// <param name="attribute">属性</param>
        /// <returns></returns>
        public static string getValueByXPath(string filePath, string xPath, string attribute)
        {
            XmlDocument document = new XmlDocument();
            document.Load(filePath);
            return document.SelectSingleNode(xPath).Attributes[attribute].Value;
        }



        // object -> xml string
        public static string SaveXmlString(object obj) { return SaveXmlString(obj, null); }
        public static string SaveXmlString(object obj, XmlAttributeOverrides overrides) { return SaveXmlString(obj, obj.GetType(), overrides); }
        public static string SaveXmlString(object obj, Type type, XmlAttributeOverrides overrides)
        {
            MemoryStream stream = new MemoryStream();
            using (StreamWriter writer = new StreamWriter(stream))
            {
                System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(type, overrides);
                xs.Serialize(writer, obj);
                writer.Close();
            }
            string str = UnicodeEncoding.UTF8.GetString(stream.GetBuffer());
            return str;
        }

        // xml string -> obj
        public static object LoadXmlString(string xmlString, Type type) { return LoadXmlString(xmlString, type, null); }
        public static object LoadXmlString(string xmlString, Type type, XmlAttributeOverrides overrides)
        {
            byte[] bytes = UnicodeEncoding.UTF8.GetBytes(xmlString);
            MemoryStream stream = new MemoryStream(bytes);
            using (StreamReader reader = new StreamReader(stream))
            {
                System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(type, overrides);
                object obj = xs.Deserialize(reader);
                reader.Close();
                return obj;
            }
        }
        public static string Format(string outxml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(outxml);
            return Format(doc);
        }
        public static string Format(XmlDocument xml)
        {
            XmlDocument xd = xml as XmlDocument;
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            XmlTextWriter xtw = null;
            try
            {
                xtw = new XmlTextWriter(sw);
                xtw.Formatting = Formatting.Indented;
                xtw.Indentation = 1;
                xtw.IndentChar = '\t';
                xd.WriteTo(xtw);
            }
            finally
            {
                if (xtw == null)
                    xtw.Close();
            }
            return sb.ToString();
        }
    }
}
