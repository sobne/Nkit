using System;
using System.Web;
using System.Web.Caching;

namespace Nkit.Web.Object
{
    /// <summary>
    /// Cache对象访问控制工具类
    /// </summary>
    /// <typeparam name="T">存取对象的类型</typeparam>
    public class CacheAccessor<T>
    {
        private static Cache Buffer = HttpRuntime.Cache;

        /// <summary>
        /// 写入缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        public static void CacheEntity(string key, object obj)
        {
            if (object.Equals(obj, null))
                return;
            Buffer[key] = obj;
        }
        /// <summary>
        /// 获取缓存值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object GetCachedEntity(string key)
        {
            return Buffer[key];
        }
        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key"></param>
        public static void RemoveCachedEntity(string key)
        {
            Buffer.Remove(key);
        }

        /// <summary>
        /// 判断是否存在Cache
        /// </summary>
        /// <param name="key">标识</param>
        /// <returns>True|False</returns>
        public static bool Exists(string key)
        {
            return HttpContext.Current.Cache[key] != null;
        }
        /// <summary>
        /// 读取缓存值
        /// </summary>
        /// <param name="key">标识</param>
        /// <returns></returns>
        public static T Get(string key)
        {
            return (T)HttpContext.Current.Cache[key];
        }
        /// <summary>
        /// 设置缓存值
        /// </summary>
        /// <param name="key">标识</param>
        /// <param name="value">对象值</param>
        public static void Set(string key, T value)
        {
            if (Exists(key))
                HttpContext.Current.Cache[key] = value;
        }
        /// <summary>
        /// 设置缓存值
        /// </summary>
        /// <param name="key">标识</param>
        /// <param name="value">对象值</param>
        /// <param name="dependency"></param>
        public static void Insert(string key, T value, CacheDependency dependency)
        {
            HttpContext.Current.Cache.Insert(key, value, dependency);
        }
        /// <summary>
        /// 移除指定Cache
        /// </summary>
        /// <param name="key">标识</param>
        public static void Remove(string key)
        {
            if (Exists(key))
                HttpContext.Current.Cache.Remove(key);
        }
        #region 静态属性
        public static int Count
        {
            get { return HttpContext.Current.Cache.Count; }
        }
        #endregion
    }
}
