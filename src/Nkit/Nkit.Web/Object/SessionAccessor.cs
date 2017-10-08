using System;
using System.Web;

namespace Nkit.Web.Object
{
    /// <summary>
    /// Session对象访问控制工具类
    /// </summary>
    /// <typeparam name="T">存取对象的类型</typeparam>
    public class SessionAccessor<T>
    {
        /// <summary>
        /// 是否存在Session
        /// </summary>
        /// <param name="key">Session名称</param>
        /// <returns>True|False</returns>
        public static bool Exists(string key)
        {
            return HttpContext.Current.Session[key] != null;
        }
        /// <summary>
        /// 取session值
        /// </summary>
        /// <param name="key">Session名称</param>
        /// <returns></returns>
        public static T GetValue(string key)
        {
            try
            {
                return (T)HttpContext.Current.Session[key];
            }
            catch
            {
                return default(T);
            }
        }
        /// <summary>
        /// 设置Session值
        /// </summary>
        /// <param name="key">Session名称</param>
        /// <param name="value">Session值</param>
        public static void SetValue(string key, T value)
        {
            //if (Exists(key))
            HttpContext.Current.Session[key] = value;
        }
        /// <summary>
        /// 移除所有对象(键和值)
        /// </summary>
        public static void Clear()
        {
            HttpContext.Current.Session.Clear();
        }
        /// <summary>
        /// 通过名称删除对象
        /// 如果名称为空,则清除所有对象
        /// </summary>
        /// <param name="name">名称</param>
        public static void Remove(string name=null)
        {
            if (string.IsNullOrEmpty(name))
                RemoveAll();
            else
                HttpContext.Current.Session.Remove(name);
        }
        /// <summary>
        /// 移除所有对象(键和值)
        /// </summary>
        public static void RemoveAll()
        {
            HttpContext.Current.Session.RemoveAll();
        }
        /// <summary>
        /// 按索引移除对象
        /// </summary>
        /// <param name="index">索引</param>
        public static void RemoveAt(int index)
        {
            HttpContext.Current.Session.RemoveAt(index);
        }

        #region 静态属性

        /// <summary>
        /// 总数
        /// </summary>
        public static int Count
        {
            get { return HttpContext.Current.Session.Count; }
        }
        /// <summary>
        /// 系统分配ID
        /// </summary>
        public static string SessionID
        {
            get { return HttpContext.Current.Session.SessionID; }
        }
        /// <summary>
        /// 超时时间
        /// </summary>
        public static int Timeout
        {
            get { return HttpContext.Current.Session.Timeout; }
            //set { HttpContext.Current.Session.Timeout = value; }
        }
        #endregion
    }
}
