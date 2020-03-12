using System.Net;

namespace Nkit.Net.Http
{
    /// <summary>
    /// Cookie对应类
    /// </summary>
    public class WebCookieType
    {
        /// <summary>
        /// cookie集合
        /// </summary>
        public CookieCollection CookieCollection { get; set; }
        /// <summary>
        /// Cookie字符串
        /// </summary>
        public string CookieString { get; set; }
    }
}
