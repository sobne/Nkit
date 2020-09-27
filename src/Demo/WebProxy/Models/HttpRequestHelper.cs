using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace WebProxy.Models
{
    public class HttpRequestHelper
    {
        /// <summary>
        /// WebApi调用
        /// </summary>
        /// <param name="<PostData>"><已经格式化为JSON的上传参数></param>
        /// 
        public static string HttpRequest(string url, string method, string data)
        {
            Stream MyResponseStream = null;
            StreamReader MyStreamReader = null;
            try
            {
                if (string.IsNullOrEmpty(method))
                    method = "GET";//默认为GET方式
                if (method == "GET")
                    url += "?" + data;
                HttpWebRequest ReQuest = (HttpWebRequest)WebRequest.Create(url);//创建WebApi访问
                ReQuest.Method = method;
                ReQuest.ReadWriteTimeout = 5000;
                ReQuest.ContentType = "application/json";// "application/x-www-form-urlencoded;charset=UTF-8";

                if (method.ToLower() == "post")
                {
                    byte[] DataByte = Encoding.UTF8.GetBytes(data);    //参数转化为ascii码
                    ReQuest.ContentLength = DataByte.Length;
                    using (Stream ReqStream = ReQuest.GetRequestStream())
                    {
                        ReqStream.Write(DataByte, 0, DataByte.Length);
                    }
                }
                using (WebResponse Response = ReQuest.GetResponse())
                {
                    MyResponseStream = Response.GetResponseStream();
                    MyStreamReader = new StreamReader(MyResponseStream, Encoding.GetEncoding("utf-8"));
                    return MyStreamReader.ReadToEnd();
                }
            }
            catch (Exception E)
            {
                return E.ToString();
            }
            finally
            {
                if (MyResponseStream != null)
                {
                    MyResponseStream.Close();
                    MyResponseStream.Dispose();
                }
                if (MyStreamReader != null)
                {
                    MyStreamReader.Close();
                    MyStreamReader.Dispose();
                }
            }
        }
    }
}