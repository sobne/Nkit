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
using System.Web;
using System.Text;
using System.Text.RegularExpressions;

namespace Nkit.Web
{
    /// <summary>
    /// Html帮助类
    /// </summary>
    public class HtmlHelper
    {
        /// <summary>
        /// URL地址编码
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        public static string URLEncode(string Input)
        {
            return HttpContext.Current.Server.UrlEncode(Input);
        }
        /// <summary>
        /// URL地址解码
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        public static string URLDecode(string Input)
        {
            return HttpContext.Current.Server.UrlDecode(Input);
        }
        /// <summary>
        ///  将字符转化为HTML编码
        /// </summary>
        /// <param name="Input">待处理的字符串</param>
        /// <returns></returns>
        public static string HtmlEncode(string Input)
        {
            return HttpContext.Current.Server.HtmlEncode(Input);
        }
        /// <summary>
        /// HTML解码
        /// </summary>
        /// <param name="Input">HTML格式的字符串</param>
        /// <returns></returns>
        public static string HtmlDecode(string Input)
        {
            return HttpContext.Current.Server.HtmlDecode(Input);
        }

        /// <summary>
        /// Html编码
        /// </summary>
        /// <param name="input">要进行编码的字符串</param>
        /// <returns>Html编码后的字符串</returns>
        public static string HtmlSpecialEntitiesEncode(string input)
        {
            return HttpUtility.HtmlEncode(input);
        }
        /// <summary>
        /// Html解码
        /// </summary>
        /// <param name="input">要进行解码的字符串</param>
        /// <returns>解码后的字符串</returns>
        public static string HtmlSpecialEntitiesDecode(string input)
        {
            return HttpUtility.HtmlDecode(input);
        }
        /// <summary>
        /// 换行符转换成Html标签的换行符<br />
        /// </summary>
        /// <param name="input">要进行处理的字符串</param>
        /// <returns>处理后的字符串</returns>
        public static string NewLineToBreak(string input)
        {
            Regex regEx = new Regex(@"[\n|\r]+");
            return regEx.Replace(input, "<br />");
        }
        /// <summary>
        /// 弃掉HTML标记
        /// </summary>
        /// <param name="html">html格式语句</param>
        /// <returns>去掉html格式字符串</returns>
        private static string ParseHtml(string html)
        {
            StringBuilder sb = new StringBuilder(Regex.Replace(html, "<[^>]*>", " "));
            sb.Replace("&nbsp;", " ");
            sb.Replace("  ", " ");
            return sb.ToString();
        }
        /// <summary>
        /// 添加URL参数
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="paramName">参数名称</param>
        /// <param name="value">参数值</param>
        /// <returns></returns>
        public static string AddParam(string url, string paramName, string value)
        {
            Uri uri = new Uri(url);
            if (string.IsNullOrEmpty(uri.Query))
            {
                string eval = HttpContext.Current.Server.UrlEncode(value);
                return String.Concat(url, "?" + paramName + "=" + eval);
            }
            else
            {
                string eval = HttpContext.Current.Server.UrlEncode(value);
                return String.Concat(url, "&" + paramName + "=" + eval);
            }
        }

        /// <summary>
        /// 更新URL参数
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="paramName">参数名称</param>
        /// <param name="value">参数值</param>
        /// <returns></returns>
        public static string UpdateParam(string url, string paramName, string value)
        {
            string keyWord = paramName + "=";
            int index = url.IndexOf(keyWord) + keyWord.Length;
            int index1 = url.IndexOf("&", index);
            if (index1 == -1)
            {
                url = url.Remove(index, url.Length - index);
                url = string.Concat(url, value);
                return url;
            }
            url = url.Remove(index, index1 - index);
            url = url.Insert(index, value);
            return url;
        }
    }
}
