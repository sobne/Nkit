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
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.Caching;
using System.Net;
using System.IO;
using Nkit.Core;

namespace Nkit.Web.Object
{
    /// <summary>
    /// Request对象访问控制工具类
    /// </summary>
    public class RequestAccessor
    {
        static HttpContext context = HttpContext.Current;
        static HttpRequest request = HttpContext.Current.Request;
        #region Request/RequestInt/RequestFloat
        /// <summary>
        /// 接收传值
        /// </summary>
        /// <param name="Name">参数名称</param>
        /// <returns>参数对应的值</returns>
        public static String Request(String Name)
        {
            return HttpContext.Current.Request[Name] == null ? "" : HttpContext.Current.Request[Name].ToString();
        }
        /// <summary>
        /// RequestInt
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="defaultValue">可选参数,默认为0</param>
        /// <returns></returns>
        public static int RequestInt(string Name, int defaultValue=0)
        {
            return Request(Name).ToInt(defaultValue);
        }
        /// <summary>
        /// RequestFloat
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="defaultValue">可选参数,默认为0.0</param>
        /// <returns></returns>
        public static float RequestFloat(string Name, float defaultValue=0.0f)
        {
            return Request(Name).ToFloat(defaultValue);
        }
        #endregion

        #region Params/ParamsInt/ParamsFloat
        /// <summary>
        /// 接收传值
        /// </summary>
        /// <param name="Name">参数名称</param>
        /// <returns>参数对应的值</returns>
        public static String Params(String Name)
        {
            return HttpContext.Current.Request.Params[Name] == null ? "" : HttpContext.Current.Request.Params[Name].ToString();
        }
        /// <summary>
        /// ParamsInt
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="defaultValue">可选参数,默认为0</param>
        /// <returns></returns>
        public static int ParamsInt(string Name, int defaultValue = 0)
        {
            return Params(Name).ToInt(defaultValue);
        }
        /// <summary>
        /// RequestFloat
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="defaultValue">可选参数,默认为0.0</param>
        /// <returns></returns>
        public static float ParamsFloat(string Name, float defaultValue = 0.0f)
        {
            return Params(Name).ToFloat(defaultValue);
        }
        #endregion

        #region QueryString/QueryStringInt/QueryStringFloat
        /// <summary>
        /// 取URL上的参数
        /// </summary>
        /// <param name="Name">参数名</param>
        /// <returns>返回参数值</returns>
        public static String QueryString(String Name)
        {
            return HttpContext.Current.Request.QueryString[Name] == null ? "" : HttpContext.Current.Request.QueryString[Name].ToString();
        }
        /// <summary>
        /// QueryStringInt
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="defaultValue">可选参数,默认为0</param>
        /// <returns></returns>
        public static int QueryStringInt(string Name, int defaultValue=0)
        {
            return QueryString(Name).ToInt(defaultValue);
        }
        /// <summary>
        /// QueryStringFloat
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="defaultValue">可选参数,默认为0.0</param>
        /// <returns></returns>
        public static float QueryStringFloat(string Name, float defaultValue=0.0f)
        {
            return QueryString(Name).ToFloat(defaultValue);
        }
        #endregion

        #region RequestForm/RequestFormInt/RequestFormFloat
        /// <summary>
        /// 取POST提交的数据
        /// </summary>
        /// <param name="Name">名称</param>
        /// <returns>返回值</returns>
        public static String RequestForm(String Name)
        {
            return HttpContext.Current.Request.Form[Name] == null ? "" : HttpContext.Current.Request.Form[Name].ToString();
        }
        /// <summary>
        /// RequestFormInt
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="defaultValue">可选参数,默认为0</param>
        /// <returns></returns>
        public static int RequestFormInt(string Name, int defaultValue=0)
        {
            return RequestForm(Name).ToInt(defaultValue);
        }
        /// <summary>
        /// RequestFormFloat
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="defaultValue">可选参数,默认为0.0</param>
        /// <returns></returns>
        public static float RequestFormFloat(string Name, float defaultValue=0.0f)
        {
            return RequestForm(Name).ToFloat(defaultValue);
        }
        #endregion

        #region IsPost/IsGet
        /// <summary>
        /// 判断当前页面是否接收到了Post请求
        /// </summary>
        /// <returns>是否接收到了Post请求</returns>
        public static bool IsPost()
        {
            return HttpContext.Current.Request.HttpMethod.Equals("POST");
        }
        /// <summary>
        /// 判断当前页面是否接收到了Get请求
        /// </summary>
        /// <returns>是否接收到了Get请求</returns>
        public static bool IsGet()
        {
            return HttpContext.Current.Request.HttpMethod.Equals("GET");
        }
        #endregion

        #region 服务器变量名
        /// <summary>
        /// 返回指定的服务器变量信息
        /// </summary>
        /// <param name="Name">服务器变量名</param>
        /// <returns>服务器变量信息</returns>
        public static string ServerVariables(string Name)
        {
            return HttpContext.Current.Request.ServerVariables[Name] == null ? "" : HttpContext.Current.Request.ServerVariables[Name].ToString();
        }
        #endregion

        #region GetRawUrl/IsBrowserGet/IsSearchEnginesGet/GetPageName/GetQParamCount/GetFParamCount/GetParamCount/
        /// <summary>
        /// 获取当前请求的原始 URL(URL 中域信息之后的部分,包括查询字符串(如果存在))
        /// </summary>
        /// <returns>原始 URL</returns>
        public static string GetRawUrl()
        {
            return HttpContext.Current.Request.RawUrl;
        }
        /// <summary>
        /// 判断当前访问是否来自浏览器软件
        /// </summary>
        /// <returns>当前访问是否来自浏览器软件</returns>
        public static bool IsBrowserGet()
        {
            string[] BrowserName = { "ie", "opera", "netscape", "mozilla", "konqueror", "firefox" };
            string curBrowser = HttpContext.Current.Request.Browser.Type.ToLower();
            for (int i = 0; i < BrowserName.Length; i++)
            {
                if (curBrowser.IndexOf(BrowserName[i]) >= 0) return true;
            }
            return false;
        }
        /// <summary>
        /// 判断是否来自搜索引擎链接
        /// </summary>
        /// <returns>是否来自搜索引擎链接</returns>
        public static bool IsSearchEnginesGet()
        {
            if (HttpContext.Current.Request.UrlReferrer != null)
            {
                string[] strArray = new string[] { "google", "yahoo", "msn", "baidu", "sogou", "sohu", "sina", "163", "lycos", "tom", "yisou", "iask", "soso", "gougou", "zhongsou" };
                string str = HttpContext.Current.Request.UrlReferrer.ToString().ToLower();
                for (int i = 0; i < strArray.Length; i++)
                {
                    if (str.IndexOf(strArray[i]) >= 0) return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 获得当前页面的名称
        /// </summary>
        /// <returns>当前页面的名称</returns>
        public static string GetPageName()
        {
            string[] urlArr = HttpContext.Current.Request.Url.AbsolutePath.Split('/');
            return urlArr[urlArr.Length - 1].ToLower();
        }
        /// <summary>
        /// 返回表单或Url参数的总个数
        /// </summary>
        /// <returns></returns>
        public static int GetParamCount()
        {
            return HttpContext.Current.Request.Form.Count + HttpContext.Current.Request.QueryString.Count;
        }
        /// <summary>
        /// GET ParamCount
        /// </summary>
        /// <returns></returns>
        public static int GetQParamCount() { return (HttpContext.Current.Request.QueryString.Count); }
        /// <summary>
        /// POST ParamCount
        /// </summary>
        /// <returns></returns>
        public static int GetFParamCount() { return (HttpContext.Current.Request.Form.Count); }
        #endregion

        #region GetCurrentFullHost/GetHost/GetIP/GetUrl/GetReferrer/SaveRequestFile/GetOS/GetBrowser
        /// <summary>
        /// 取全IP包括端口
        /// </summary>
        /// <returns>IP和端口</returns>
        public static string GetCurrentFullHost()
        {
            HttpRequest request = HttpContext.Current.Request;
            if (!request.Url.IsDefaultPort)
                return string.Format("{0}:{1}", request.Url.Host, request.Url.Port.ToString());
            return request.Url.Host;
        }
        /// <summary>
        /// 取主机名
        /// </summary>
        /// <returns></returns>
        public static string GetHost() { return HttpContext.Current.Request.Url.Host; }
        /// <summary>
        /// 前台URL
        /// </summary>
        /// <returns></returns>
        public static string GetUrl() { return HttpContext.Current.Request.Url.ToString(); }
        /// <summary>
        /// 来源URL
        /// </summary>
        /// <returns></returns>
        public static string GetReferrer()
        {
            string str = null;
            try
            {
                str = ServerVariables("HTTP_REFERER").Trim();
                if (str.Length == 0) str = HttpContext.Current.Request.UrlReferrer.ToString();
            }
            catch { }

            if (str == null) return "";
            return str;
        }
        /// <summary>
        /// 保存Request文件
        /// </summary>
        /// <param name="path">保存到文件名</param>
        public static void SaveRequestFile(string path)
        {
            if (HttpContext.Current.Request.Files.Count > 0) HttpContext.Current.Request.Files[0].SaveAs(path);
        }
        /// <summary>
        /// 取操作系统
        /// </summary>
        /// <returns>返回操作系统</returns>
        public static string GetOS()
        {
            HttpBrowserCapabilities bc = new HttpBrowserCapabilities();
            bc = System.Web.HttpContext.Current.Request.Browser;
            return bc.Platform;
        }
        /// <summary>
        /// 取游览器
        /// </summary>
        /// <returns>返回游览器</returns>
        public static string GetBrowser()
        {
            HttpBrowserCapabilities bc = new HttpBrowserCapabilities();
            bc = System.Web.HttpContext.Current.Request.Browser;
            return bc.Type;
        }
        #endregion
    }
}
