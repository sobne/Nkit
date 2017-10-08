using System;
using System.Xml;
using System.Collections;
using System.Globalization;
using System.Web;

namespace Nkit.Web
{
    /// <summary>
    /// RSS频道结构
    /// </summary>
    public struct Rss_Channel
    {
        public string title;        //标题
        public string link;         //连接
        public string language;     //语言            
        public string description;  //描述
        public string webMaster;    //发布者
    }

    /// <summary>
    /// RSS图片信息
    /// </summary>
    public struct Rss_Image
    {
        public string url;      //地址
        public string title;    //标题
        public int height;      //高度
        public int width;       //长度
    }

    /// <summary>
    /// RSS项结构
    /// </summary>
    public struct Rss_Item
    {
        public string title;        //标题
        public string catalog;      //类别
        public string link;         //连接
        public DateTime pubDate;    //发布日期
        public string description;  //描述

    }
    /// <summary>
    /// Rss帮助类
    /// </summary>
    public class RssHelper
    {
        const string dublinCoreNamespaceUri = @"http://purl.org/dc/elements/1.1/";
        const string slashNamespaceUri = @"http://purl.org/rss/1.0/modules/slash/";
        const string syndicationNamespaceUri = @"http://purl.org/rss/1.0/modules/syndication/";
        /// <summary>
        /// 构造函数
        /// </summary>
        public RssHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        /// <summary>
        ///添加rss版本信息
        /// </summary>
        /// <param name="xmlDocument"></param>
        /// <returns></returns>
        public static XmlDocument AddRssPreamble(XmlDocument xmlDocument)
        {
            //声明创建1.0版本的xml
            XmlDeclaration xmlDeclaration = xmlDocument.CreateXmlDeclaration("1.0", "utf-8", null);
            xmlDocument.InsertBefore(xmlDeclaration, xmlDocument.DocumentElement);

            XmlElement rssElement = xmlDocument.CreateElement("rss");

            XmlAttribute rssVersionAttribute = xmlDocument.CreateAttribute("version");
            rssVersionAttribute.InnerText = "2.0";
            rssElement.Attributes.Append(rssVersionAttribute);
            xmlDocument.AppendChild(rssElement);


            XmlAttribute dublicCoreNamespaceUriAttribute = xmlDocument.CreateAttribute("xmlns:dc");
            dublicCoreNamespaceUriAttribute.InnerText = dublinCoreNamespaceUri;
            rssElement.Attributes.Append(dublicCoreNamespaceUriAttribute);

            XmlAttribute slashNamespaceUriAttribute = xmlDocument.CreateAttribute("xmlns:slash");
            slashNamespaceUriAttribute.InnerText = slashNamespaceUri;
            rssElement.Attributes.Append(slashNamespaceUriAttribute);

            XmlAttribute syndicationNamespaceUriAttribute = xmlDocument.CreateAttribute("xmlns:sy");
            syndicationNamespaceUriAttribute.InnerText = syndicationNamespaceUri;
            rssElement.Attributes.Append(syndicationNamespaceUriAttribute);


            return xmlDocument;
        }

        /// <summary>
        /// 添加频道
        /// </summary>
        /// <param name="xmlDocument"></param>
        /// <param name="channel"></param>
        /// <returns></returns>
        public static XmlDocument AddRssChannel(XmlDocument xmlDocument, Rss_Channel channel)
        {
            XmlElement channelElement = xmlDocument.CreateElement("channel");
            XmlNode rssElement = xmlDocument.SelectSingleNode("rss");

            rssElement.AppendChild(channelElement);

            //添加标题
            XmlElement channelTitleElement = xmlDocument.CreateElement("title");
            channelTitleElement.InnerText = channel.title;
            channelElement.AppendChild(channelTitleElement);

            //添加连接
            XmlElement channelLinkElement = xmlDocument.CreateElement("link");
            channelLinkElement.InnerText = channel.link;
            channelElement.AppendChild(channelLinkElement);

            //添加描述
            XmlElement channelDescriptionElement = xmlDocument.CreateElement("description");
            XmlCDataSection cDataDescriptionSection = xmlDocument.CreateCDataSection(channel.description);
            channelDescriptionElement.AppendChild(cDataDescriptionSection);
            channelElement.AppendChild(channelDescriptionElement);

            //添加语言
            XmlElement languageElement = xmlDocument.CreateElement("language");
            languageElement.InnerText = channel.language;
            channelElement.AppendChild(languageElement);

            //添加发布者
            XmlElement webMasterElement = xmlDocument.CreateElement("webMaster");
            webMasterElement.InnerText = channel.webMaster;
            channelElement.AppendChild(webMasterElement);

            return xmlDocument;
        }


        /// <summary>
        /// 添加RssImage
        /// </summary>
        /// <param name="xmlDocument"></param>
        /// <param name="img"></param>
        /// <returns></returns>
        public static XmlDocument AddRssImage(XmlDocument xmlDocument, Rss_Image img)
        {
            XmlElement imgElement = xmlDocument.CreateElement("image");
            XmlNode channelElement = xmlDocument.SelectSingleNode("rss/channel");

            //创建标题
            XmlElement imageTitleElement = xmlDocument.CreateElement("title");
            imageTitleElement.InnerText = img.title;
            imgElement.AppendChild(imageTitleElement);

            //创建地址
            XmlElement imageUrlElement = xmlDocument.CreateElement("url");
            imageUrlElement.InnerText = img.url;
            imgElement.AppendChild(imageUrlElement);

            //创建高度
            XmlElement imageHeightElement = xmlDocument.CreateElement("height");
            imageHeightElement.InnerText = img.height.ToString();
            imgElement.AppendChild(imageHeightElement);

            //创建长度
            XmlElement imageWidthElement = xmlDocument.CreateElement("width");
            imageWidthElement.InnerText = img.width.ToString();
            imgElement.AppendChild(imageWidthElement);

            //将图像节点添加到频道节点里面
            channelElement.AppendChild(imgElement);
            return xmlDocument;

        }


        /// <summary>
        /// 添加项信息
        /// </summary>
        /// <param name="xmlDocument"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static XmlDocument AddRssItem(XmlDocument xmlDocument, Rss_Item item)
        {
            XmlElement itemElement = xmlDocument.CreateElement("item");
            XmlNode channelElement = xmlDocument.SelectSingleNode("rss/channel");

            //创建标题
            XmlElement itemTitleElement = xmlDocument.CreateElement("title");
            XmlCDataSection cDataTitleSection = xmlDocument.CreateCDataSection(item.title);
            itemTitleElement.AppendChild(cDataTitleSection);
            itemElement.AppendChild(itemTitleElement);
            //创建日期
            XmlElement pubDateElement = xmlDocument.CreateElement("pubDate");
            pubDateElement.InnerText = XmlConvert.ToString(item.pubDate.ToUniversalTime(), "yyyy-MM-ddTHH:mm:ss");
            itemElement.AppendChild(pubDateElement);

            //添加连接
            XmlElement itemLinkElement = xmlDocument.CreateElement("link");
            itemLinkElement.InnerText = item.link;
            itemElement.AppendChild(itemLinkElement);
            //创建描述
            XmlElement itemDescriptionElement = xmlDocument.CreateElement("description");
            XmlCDataSection cDataDescriptionSection = xmlDocument.CreateCDataSection(item.description);
            itemDescriptionElement.AppendChild(cDataDescriptionSection);
            itemElement.AppendChild(itemDescriptionElement);


            //创建类型
            XmlElement itemcatalogElement = xmlDocument.CreateElement("catalog");
            itemcatalogElement.InnerText = item.catalog;
            itemElement.AppendChild(itemcatalogElement);

            //将RssItem添加到频道节点里面
            channelElement.AppendChild(itemElement);
            return xmlDocument;
        }
    }
}
