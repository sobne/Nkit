using System;
using System.Net;

namespace Nkit.Net.Http
{
    /// <summary>
    /// 响应参数类
    /// </summary>
    public class WebResponseParameter
    {
        public WebResponseParameter()
        {
            Cookie = new WebCookieType();
        }
        /// <summary>
        /// 响应资源标识符
        /// </summary>
        public Uri Uri { get; set; }
        /// <summary>
        /// 响应状态码
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }
        /// <summary>
        /// 响应Cookie对象
        /// </summary>
        public WebCookieType Cookie { get; set; }
        /// <summary>
        /// 响应体
        /// </summary>
        public string Body { get; set; }
    }
}
