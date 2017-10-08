using System;
using System.Web;

namespace Nkit.Web.Object
{
    /// <summary>
    /// Cookie对象访问控制工具类
    /// </summary>
    public class CookieAccessor
    {
        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool Exists(string name)
        {
            return HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[name] != null;
        }
        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="value">值</param>
        public static void Write(string name, string value)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[name];
            if (cookie == null)
                cookie = new HttpCookie(name);
            cookie.Value = value;
            HttpContext.Current.Response.AppendCookie(cookie);
        }
        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="value">值</param>
        /// <param name="expires">过期时间(分钟)</param>
        public static void Write(string name, string value, int expires)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[name];
            if (cookie == null)
                cookie = new HttpCookie(name);
            cookie.Value = value;
            cookie.Expires = DateTime.Now.AddMinutes(expires);
            HttpContext.Current.Response.AppendCookie(cookie);
        }
        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="value">值</param>
        /// <param name="expires">过期时间(分钟)</param>
        public static void Add(string name, string value, int expires)
        {
            Write(name, value, expires);
        }
        /// <summary>
        /// 读cookie值
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>cookie值</returns>
        public static string Get(string name)
        {
            if (Exists(name))
                return HttpContext.Current.Request.Cookies[name].Value.ToString();
            return "";
        }
        /// <summary>
        /// 清除所有Cookies
        /// </summary>
        public static void Clear()
        {
            HttpContext.Current.Request.Cookies.Clear();
        }
        /// <summary>
        /// 通过索引值获取名称
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns>名称</returns>
        public static string GetKey(int index)
        {
            return HttpContext.Current.Request.Cookies.GetKey(index);
        }
        /// <summary>
        /// 通过名称删除Cookies
        /// 如果名称为空,则删除所有Cookies
        /// </summary>
        /// <param name="name">名称</param>
        public static void Remove(string name = null)
        {
            if (string.IsNullOrEmpty(name))
                RemoveAll();
            else
                HttpContext.Current.Request.Cookies.Remove(name);
        }
        /// <summary>
        /// 清除所有Cookie
        /// </summary>
        public static void RemoveAll()
        { 
            
        }
        #region 静态属性

        #endregion
    }
}
