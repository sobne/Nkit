using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Text;

namespace Nkit.Net.Http
{
    public enum HttpMethod
    {
        GET,
        POST,
        PUT,
        DELETE,
        HEAD,
        CONNECT,
        OPTIONS,
        TRACE
    }
    public enum HttpContentType
    {
        [Description("text")]
        form_data,
        [Description("application/x-www-form-urlencoded")]
        x_www_form_urlencoded,
        [Description("text")]
        text,
        [Description("text/plain")]
        text_pain,
        [Description("application/json")]
        json,
        [Description("application/javascript")]
        javascript,
        [Description("application/xml")]
        xml,
        [Description("text/xml")]
        text_xml,
        [Description("text/html")]
        text_html
    }
    /// <summary>
    /// 请求参数类
    /// </summary>
    public class WebRequestParameter
    {
        public WebRequestParameter()
        {
            Encoding = Encoding.UTF8;
        }
        /// <summary>
        /// 请求编码
        /// </summary>
        public Encoding Encoding { get; set; }

        public HttpMethod Method { get; set; }

        public HttpContentType ContentType { get; set; }
        public string Url { get; set; }
        public WebCookieType Cookie { get; set; }
        public IDictionary<string,string> Parameters { get; set; }
        public string Json { get; set; }
        /// <summary>
        /// 引用页
        /// </summary>
        public string RefererUrl { get; set; }

        public IPEndPoint BindIpEndPoint { get; set; }
    }
}
