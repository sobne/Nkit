using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Data;

namespace LotteryChooser.Utils
{
    /// <summary>
    /// 不要
    /// </summary>
    public class XmlHelper
    {
        protected XmlDocument XmlDoc = new XmlDocument();

        public string ApplicationPath { get; set; }

        public XmlHelper(string xmlfile)
        {
            try
            {
                //ApplicationPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
                //XmlDoc.Load(ApplicationPath + xmlfile);
                XmlDoc.Load(xmlfile);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        ~XmlHelper()
        {
            XmlDoc = null;  //释放XmlDocument对象
        }


        public DataSet GetDataSet(string XmlPathNode)
        {
            DataSet ds = new DataSet();

            try
            {
                System.IO.StringReader read = new System.IO.StringReader(XmlDoc.SelectSingleNode(XmlPathNode).OuterXml);
                ds.ReadXml(read);   //利用DataSet的ReadXml方法读取StringReader文件流 87.                
                read.Close();
            }
            catch { }
            return ds;

        }

        public string SelectAttrib(string XmlPathNode, string Attrib)
        {
            string _strNode = "";
            try
            {
                _strNode = XmlDoc.SelectSingleNode(XmlPathNode).Attributes[Attrib].Value;
            }
            catch { }
            return _strNode;
        }
        /// <summary> 
        /// 节点查询，返回节点值 
        /// </summary> 
        /// <param name="XmlPathNode">节点的路径</param>
        /// <returns></returns>
        public string SelectNodeText(string XmlPathNode)
        {
            string _nodeTxt = XmlDoc.SelectSingleNode(XmlPathNode).InnerText;
            if (_nodeTxt == null || _nodeTxt == "")
                return "";
            else
                return _nodeTxt;
        }

        public XmlNodeList SelectNodes(string XmlPathNode)
        {
           XmlNodeList element= XmlDoc.SelectNodes(XmlPathNode);
           return element;
        }

        public bool SetNode(string XmlPathNode, int index, string NodeText)
        {
            try
            {
                XmlNodeList _NodeList = XmlDoc.SelectNodes(XmlPathNode);
                //循环遍历节点，查询是否存在该节点                
                for (int i = 0; i < _NodeList.Count; i++)
                {
                    if (_NodeList[i].ChildNodes[index].InnerText == NodeText)
                    {
                        return true;
                    }
                }
            }
            catch
            {
            }
            return false;
        }

        public int NodeCount(string XmlPathNode)
        {
            int i = 0;
            try
            {
                i = XmlDoc.SelectSingleNode(XmlPathNode).ChildNodes.Count;
            }
            catch
            {
                i = 0;
            }
            return i;
        }

        public bool UpdateNode(string XmlPathNode, string NodeContent)
        {
            try
            {
                XmlDoc.SelectSingleNode(XmlPathNode).InnerText = NodeContent;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateNode(string XmlParentNode, string[] XmlNode, string[] NodeContent)
        {
            try
            {
                //根据节点数组循环修改节点值 204.                
                for (int i = 0; i < XmlNode.Length; i++)
                {
                    XmlDoc.SelectSingleNode(XmlParentNode + "/" + XmlNode[i]).InnerText = NodeContent[i];
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateAttrib(string XmlPathNode, string Attrib, string AttribContent)
        {
            try
            {
                //修改属性值 228.                
                XmlDoc.SelectSingleNode(XmlPathNode).Attributes[Attrib].Value = AttribContent;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool InsertAttrib(string MainNode, string Attrib, string AttribContent)
        {
            try
            {
                XmlElement objNode = (XmlElement)XmlDoc.SelectSingleNode(MainNode); //强制转化成XmlElement对象 
                objNode.SetAttribute(Attrib, AttribContent);    //XmlElement对象添加属性方法   
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool InsertNode(string MainNode, string ChildNode, string[] Element, string[] Content)
        {
            try
            {
                XmlNode objRootNode = XmlDoc.SelectSingleNode(MainNode);    //声明XmlNode对象 
                XmlElement objChildNode = XmlDoc.CreateElement(ChildNode);  //创建XmlElement对象
                objRootNode.AppendChild(objChildNode);
                for (int i = 0; i < Element.Length; i++)    //循环插入节点元素 276.               
                {
                    XmlElement objElement = XmlDoc.CreateElement(Element[i]);
                    objElement.InnerText = Content[i];
                    objChildNode.AppendChild(objElement);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteNode(string Node)
        {
            try
            {
                //XmlNode的RemoveChild方法来删除节点及其所有子节点 299.                
                XmlDoc.SelectSingleNode(Node).ParentNode.RemoveChild(XmlDoc.SelectSingleNode(Node));
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
