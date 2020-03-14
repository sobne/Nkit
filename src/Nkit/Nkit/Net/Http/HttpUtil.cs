using Nkit.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;

namespace Nkit.Net.Http
{
    public class HttpUtil
    {
        /// <summary>
        /// 执行方法
        /// </summary>
        /// <param name="requestParameter">请求报文</param>
        /// <returns>响应报文</returns>
        public static WebResponseParameter Excute(WebRequestParameter requestParameter)
        {
            // 1.实例化
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(new Uri(requestParameter.Url, UriKind.RelativeOrAbsolute));
            // 2.设置请求头
            SetHeader(webRequest, requestParameter);
            // 3.设置请求Cookie
            SetCookie(webRequest, requestParameter);
            // 4.ssl/https请求设置
            if (Regex.IsMatch(requestParameter.Url, "^https://"))
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
                ServicePointManager.ServerCertificateValidationCallback = CheckValidationResult;
            }
            // 5.设置请求参数[Post方式下]
            SetParameter(webRequest, requestParameter);
            // 6.返回响应报文
            return SetResponse(webRequest, requestParameter);
        }

        /// <summary>
        /// 设置请求头
        /// </summary>
        /// <param name="webRequest">HttpWebRequest对象</param>
        /// <param name="requestParameter">请求参数对象</param>
        static void SetHeader(HttpWebRequest webRequest, WebRequestParameter requestParameter)
        {
            webRequest.Method = requestParameter.Method.ToString();
            webRequest.ContentType = requestParameter.ContentType.GetDescription();
            webRequest.Accept = "text/html, application/xhtml+xml, */*";
            webRequest.Timeout = 20000;
            //webRequest.KeepAlive = true;
            //webRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko/20100101 Firefox/22.0";
            //webRequest.AllowAutoRedirect = true;
            //webRequest.ProtocolVersion = HttpVersion.Version11;

            if(requestParameter.BindIpEndPoint!=null)
            {
                webRequest.ServicePoint.BindIPEndPointDelegate = (servicePoint, remoteEp, retryCount) =>
                {
                    return requestParameter.BindIpEndPoint;
                };
            }
        }

        /// <summary>
        /// 设置请求Cookie
        /// </summary>
        /// <param name="webRequest">HttpWebRequest对象</param>
        /// <param name="requestParameter">请求参数对象</param>
        private static void SetCookie(HttpWebRequest webRequest, WebRequestParameter requestParameter)
        {
            // 必须实例化，否则响应中获取不到Cookie
            webRequest.CookieContainer = new CookieContainer();
            if (requestParameter.Cookie != null && !string.IsNullOrEmpty(requestParameter.Cookie.CookieString))
            {
                webRequest.Headers[HttpRequestHeader.Cookie] = requestParameter.Cookie.CookieString;
            }
            if (requestParameter.Cookie != null && requestParameter.Cookie.CookieCollection != null && requestParameter.Cookie.CookieCollection.Count > 0)
            {
                webRequest.CookieContainer.Add(requestParameter.Cookie.CookieCollection);
            }
        }

        /// <summary>
        /// ssl/https请求
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="certificate"></param>
        /// <param name="chain"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }

        /// <summary>
        /// 设置请求参数（只有Post请求方式才设置）
        /// </summary>
        /// <param name="webRequest">HttpWebRequest对象</param>
        /// <param name="requestParameter">请求参数对象</param>
        static void SetParameter(HttpWebRequest webRequest, WebRequestParameter requestParameter)
        {

            
            if (requestParameter.Method== HttpMethod.POST)
            {
                var para = string.Empty;
                if(requestParameter.ContentType== HttpContentType.x_www_form_urlencoded)
                {
                    if (requestParameter.Parameters == null || requestParameter.Parameters.Count <= 0) return;
                    StringBuilder data = new StringBuilder(string.Empty);
                    foreach (KeyValuePair<string, string> keyValuePair in requestParameter.Parameters)
                    {
                        data.AppendFormat("{0}={1}&", keyValuePair.Key, keyValuePair.Value);
                    }
                    para = data.Remove(data.Length - 1, 1).ToString();
                }
                else if(requestParameter.ContentType == HttpContentType.json)
                {
                    if (!string.IsNullOrEmpty(requestParameter.Json))
                        para = requestParameter.Json;
                }

                byte[] bytePosts = requestParameter.Encoding.GetBytes(para);
                webRequest.ContentLength = bytePosts.Length;
                using (Stream requestStream = webRequest.GetRequestStream())
                {
                    requestStream.Write(bytePosts, 0, bytePosts.Length);
                    requestStream.Close();
                }
            }
        }

        /// <summary>
        /// 返回响应报文
        /// </summary>
        /// <param name="webRequest">HttpWebRequest对象</param>
        /// <param name="requestParameter">请求参数对象</param>
        /// <returns>响应对象</returns>
        static WebResponseParameter SetResponse(HttpWebRequest webRequest, WebRequestParameter requestParameter)
        {
            WebResponseParameter responseParameter = new WebResponseParameter();
            using (HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse())
            {
                responseParameter.Uri = webResponse.ResponseUri;
                responseParameter.StatusCode = webResponse.StatusCode;
                responseParameter.Cookie = new WebCookieType
                {
                    CookieCollection = webResponse.Cookies,
                    CookieString = webResponse.Headers["Set-Cookie"]
                };
                using (StreamReader reader = new StreamReader(webResponse.GetResponseStream(), requestParameter.Encoding))
                {
                    responseParameter.Body = reader.ReadToEnd();
                }
            }
            return responseParameter;
        }
    }
}
