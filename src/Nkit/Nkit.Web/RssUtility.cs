/* License
 * This work is licensed under the Creative Commons Attribution 2.5 License
 * http://creativecommons.org/licenses/by/2.5/

 * You are free:
 *     to copy, distribute, display, and perform the work
 *     to make derivative works
 *     to make commercial use of the work
 * 
 * Under the following conditions:
 *     You must attribute the work in the manner specified by the author or licensor:
 *          - If you find this component useful a email to [sobne.cn@gmail.com] would be appreciated.
 *     For any reuse or distribution, you must make clear to others the license terms of this work.
 *     Any of these conditions can be waived if you get permission from the copyright holder.
 * 
 * Copyright sobne.cn All Rights Reserved.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Nkit.Core.Utils;

namespace Nkit.Web
{
    /// <summary>
    /// RSS 单元
    /// </summary>
    public class RssItem
    {
        [XmlElement("title")]
        public string Title;
        [XmlElement("link")]
        public string Link;
        [XmlElement("description")]
        public string Description;
        [XmlElement("author")]
        public string Author;
        [XmlElement("category")]
        public string Category;
        [XmlElement("comments")]
        public string Comments;
        [XmlElement("enclosure")]
        public string Enclosure;
        [XmlElement("guid")]
        public string Guid;
        [XmlElement("pubDate")]
        public string PubDate;
        [XmlElement("source")]
        public string Source;
    }

    /// <summary>
    /// RSS 频道
    /// </summary>
    [XmlInclude(typeof(RssItem))]
    public class RssChannel
    {
        [XmlElement("title")]
        public string Title;
        [XmlElement("link")]
        public string Link;
        [XmlElement("description")]
        public string Description;

        [XmlElement("language")]
        public string Language;
        [XmlElement("copyright")]
        public string CopyRight;
        [XmlElement("managingEditor")]
        public string ManagingEditor;
        [XmlElement("webMaster")]
        public string WebMaster;
        [XmlElement("pubDate")]
        public string PubDate;
        [XmlElement("lastBuildDate")]
        public string LastBuildDate;
        [XmlElement("category")]
        public string Category;
        [XmlElement("docs")]
        public string Docs;
        [XmlElement("cloud")]
        public string Cloud;
        [XmlElement("ttl")]
        public string TTL;
        [XmlElement("image")]
        public string Image;
        [XmlElement("rating")]
        public string Rating;
        [XmlElement("textInput")]
        public string TextInput;
        [XmlElement("skipHours")]
        public string SkipHours;
        [XmlElement("skipDays")]
        public string SkipDays;


        [XmlElement("item")]
        public List<RssItem> Items = new List<RssItem>();
    }
    /// <summary>
    /// RssUtility
    /// </summary>
    [XmlInclude(typeof(RssChannel))]
    [XmlRoot("rss")]
    public class RssUtility
    {
        [XmlElement("channel")]
        public RssChannel Channel;

        /// <summary>
        /// 从XML字符串中解析
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static RssUtility LoadXmlString(string xml)
        {
            return XMLHelper.LoadXmlString(xml, typeof(RssUtility)) as RssUtility;
        }

        /// <summary>
        /// 保存为XML字符串
        /// </summary>
        /// <returns></returns>
        public string SaveXmlString()
        {
            return XMLHelper.SaveXmlString(this);
        }

        /// <summary>
        /// 读取指定URL的RSS，并解析之
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static RssUtility GetRss(string url)
        {
            string xml =PageFetcher.FetchPage(url);
            return XMLHelper.LoadXmlString(xml, typeof(RssUtility)) as RssUtility;
        }
    }
}
