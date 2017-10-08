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

namespace Nkit.Web.Object
{
    /// <summary>
    /// Application对象访问控制工具类
    /// </summary>
    public class ApplicationAccessor
    {
        /// <summary>
        /// 判断是否存在此对象
        /// </summary>
        /// <param name="name">对象名称</param>
        /// <returns>True|False</returns>
        public static bool Exists(string name)
        {
            return HttpContext.Current.Application[name] != null;
        }
        /// <summary>
        /// 获取Application值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="name">名称</param>
        /// <returns>值</returns>
        public static T Get<T>(string name)
        {
            if (Exists(name))
                return (T)HttpContext.Current.Application[name];
            else
                return default(T);
        }
        /// <summary>
        /// 获取Application值,带默认值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="name">键</param>
        /// <param name="defValue">默认值</param>
        /// <returns>值</returns>
        public static T TryGet<T>(string name, T defValue)
        {
            if (Exists(name))
                return (T)HttpContext.Current.Application[name];
            else
                return defValue;
        }

        /// <summary>
        /// 新增一个Application对象
        /// </summary>
        /// <param name="name">对象名</param>
        /// <param name="value">对象值</param>
        public static void Add(string name,object value)
        {
            HttpContext.Current.Application.Add(name, value);
        }
        /// <summary>
        /// 移除所有Application对象
        /// </summary>
        public static void Clear()
        {
            HttpContext.Current.Application.Clear();
        }
        /// <summary>
        /// 通过索引获取对象
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns></returns>
        public static object Get(int index)
        {
            return HttpContext.Current.Application.Get(index);
        }
        /// <summary>
        /// 通过名称获取对象
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns></returns>
        public static object Get(string name)
        {
            return HttpContext.Current.Application.Get(name);
        }
        /// <summary>
        /// 通过索引获取变量名称
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns>对象名称</returns>
        public static string GetKey(int index)
        {
            return HttpContext.Current.Application.GetKey(index);
        }
        /// <summary>
        /// 设置Application值
        /// </summary>
        /// <param name="name">键</param>
        /// <param name="value">值</param>
        public static void Set(string name, object value)
        {
            if (Exists(name))
                HttpContext.Current.Application[name] = value;
        }
        /// <summary>
        /// 移除Application对象
        /// 如果参数为空,则移除所有对象
        /// </summary>
        /// <param name="name">对象名</param>
        public static void Remove(string name = null)
        {
            if (string.IsNullOrEmpty(name))
                HttpContext.Current.Application.RemoveAll();
            else
                HttpContext.Current.Application.Remove(name);
        }
        /// <summary>
        /// 移除所有Application对象
        /// </summary>
        public static void RemoveAll()
        {
            HttpContext.Current.Application.RemoveAll();
        }
        /// <summary>
        /// 按索引移除Application对象
        /// </summary>
        /// <param name="index">索引</param>
        public static void RemoveAt(int index)
        {
            HttpContext.Current.Application.RemoveAt(index);
        }
        /// <summary>
        /// 锁定
        /// </summary>
        public static void Lock()
        {
            HttpContext.Current.Application.Lock();
        }
        /// <summary>
        /// 取消锁定
        /// </summary>
        public static void UnLock()
        {
            HttpContext.Current.Application.UnLock();
        }

        #region 静态属性
        /// <summary>
        /// 总数
        /// </summary>
        public static int Count
        {
            get { return HttpContext.Current.Application.Count; }
        }
        /// <summary>
        /// 对象名的字符串数组
        /// </summary>
        public static string[] AllKeys
        {
            get { return HttpContext.Current.Application.AllKeys; }
        }
        #endregion
    }
}
