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
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Nkit.Core.Expression;
using System.Data;
using System.Reflection;

namespace Nkit.Core
{
    /// <summary>
    /// Extension方法
    /// </summary>
    public static class StaticExtension
    {

        #region Enum
        /// <summary>
        /// 获取Enum的描述信息
        /// </summary>
        /// <param name="enum"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum @enum)
        {
            return Base.GetEnumDescription(@enum);
        }
        public static string GetDescription4Enum(this Enum @enum, Boolean nameInstead = true)
        {
            return Base.GetDescription4Enum(@enum);
        }
        #endregion

        #region object
        public static string ToString(this object obj, string defaultValue)
        {
            return (obj == DBNull.Value || obj == null) ? defaultValue : obj.ToString();
        }

        public static int ToInt32(this object obj, int defaultValue)
        {
            int result;
            if (obj == DBNull.Value || obj == null)
            {
                result = defaultValue;
            }
            else
            {
                string s = obj.ToString(defaultValue.ToString());
                int num = defaultValue;
                double num2 = (double)defaultValue;
                if (int.TryParse(s, out num))
                {
                    result = num;
                }
                else
                {
                    if (double.TryParse(s, out num2))
                    {
                        num = (int)num2;
                        result = num;
                    }
                    else
                    {
                        result = defaultValue;
                    }
                }
            }
            return result;
        }
        public static float ToSingle(this object obj, float defaultValue)
        {
            float result;
            if (obj == DBNull.Value || obj == null)
            {
                result = defaultValue;
            }
            else
            {
                string s = obj.ToString(defaultValue.ToString());
                float num = defaultValue;
                if (float.TryParse(s, out num))
                {
                    result = num;
                }
                else
                {
                    result = defaultValue;
                }
            }
            return result;
        }
        public static double ToDouble(this object obj, double defaultValue)
        {
            double result;
            if (obj == DBNull.Value || obj == null)
            {
                result = defaultValue;
            }
            else
            {
                string s = obj.ToString(defaultValue.ToString());
                double num = defaultValue;
                if (double.TryParse(s, out num))
                {
                    result = num;
                }
                else
                {
                    result = defaultValue;
                }
            }
            return result;
        }
        public static DateTime ToDateTime(this object obj, DateTime defaultValue)
        {
            string text = obj.ToString("");
            DateTime result;
            if (text.Trim() == string.Empty)
            {
                result = defaultValue;
            }
            else
            {
                DateTime dateTime = default(DateTime);
                if (DateTime.TryParse(text, out dateTime))
                {
                    result = dateTime;
                }
                else
                {
                    result = defaultValue;
                }
            }
            return result;
        }
        public static bool ToBoolean(this object obj, bool defaultValue)
        {
            bool result;
            if (obj == DBNull.Value || obj == null)
            {
                result = defaultValue;
            }
            else
            {
                string value = obj.ToString(defaultValue.ToString());
                bool flag = defaultValue;
                if (bool.TryParse(value, out flag))
                {
                    result = flag;
                }
                else
                {
                    result = defaultValue;
                }
            }
            return result;
        }
        
        #endregion


        /// <summary>
        /// 转化一个DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(this IEnumerable<T> list)
        {
            //创建属性的集合
            List<PropertyInfo> pList = new List<PropertyInfo>();
            //获得反射的入口
            Type type = typeof(T);
            DataTable dt = new DataTable();
            //把所有的public属性加入到集合 并添加DataTable的列
            Array.ForEach<PropertyInfo>(type.GetProperties(), p => { pList.Add(p); dt.Columns.Add(p.Name, p.PropertyType); });
            foreach (var item in list)
            {
                //创建一个DataRow实例
                DataRow row = dt.NewRow();
                //给row 赋值
                pList.ForEach(p => row[p.Name] = p.GetValue(item, null));
                //加入到DataTable
                dt.Rows.Add(row);
            }
            return dt;
        }
        /// <summary>
        /// DataTable 转换为List 集合
        /// </summary>
        /// <typeparam name="TResult">类型</typeparam>
        /// <param name="dt">DataTable</param>
        /// <returns></returns>
        public static List<T> ToList<T>(this DataTable dt) where T : class, new()
        {
            //创建一个属性的列表
            List<PropertyInfo> prlist = new List<PropertyInfo>();
            //获取TResult的类型实例  反射的入口
            Type t = typeof(T);
            //获得TResult 的所有的Public 属性 并找出TResult属性和DataTable的列名称相同的属性(PropertyInfo) 并加入到属性列表 
            Array.ForEach<PropertyInfo>(t.GetProperties(), p => { if (dt.Columns.IndexOf(p.Name) != -1) prlist.Add(p); });
            //创建返回的集合
            List<T> oblist = new List<T>();

            foreach (DataRow row in dt.Rows)
            {
                //创建TResult的实例
                T ob = new T();
                //找到对应的数据  并赋值
                prlist.ForEach(p => { if (row[p.Name] != DBNull.Value) p.SetValue(ob, row[p.Name], null); });
                //放入到返回的集合中.
                oblist.Add(ob);
            }
            return oblist;
        }
        private static List<T> TableToList<T>(this DataTable dt) where T : class, new()
        {
            Type t = typeof(T);
            //获取当前Type的公共属性
            PropertyInfo[] propertys = t.GetProperties();
            List<T> list = new List<T>();
            string typeName = string.Empty;
            foreach (DataRow dr in dt.Rows)
            {
                T entity = new T();
                foreach (PropertyInfo pi in propertys)
                {
                    typeName = pi.Name;
                    if (dt.Columns.Contains(typeName))
                    {
                        //获取一个值,该值指定此属性是否可写 set 
                        if (!pi.CanWrite) continue;
                        object value = dr[typeName];
                        if (value == DBNull.Value) continue;
                        //获取此属性的类型是否是string类型
                        if (pi.PropertyType == typeof(string))
                        {
                            //PropertyInfo.SetValue()三个参数
                            //第一个 将设置其属性值的对象。
                            //第二个 新的属性值。
                            //第三个 索引化属性的可选索引值。 对于非索引化属性，该值应为 null。
                            pi.SetValue(entity, value.ToString(), null);
                        }
                        else if (pi.PropertyType == typeof(int))
                        {
                            pi.SetValue(entity, int.Parse(value.ToString()), null);
                        }
                        else if (pi.PropertyType == typeof(DateTime))
                        {
                            pi.SetValue(entity, DateTime.Parse(value.ToString()), null);
                        }
                        else
                        {
                            pi.SetValue(entity, value, null);
                        }
                    }
                }
                list.Add(entity);
            }
            return list;
        }

        /// <summary>
        /// 将字符串转换为list
        /// </summary>
        /// <typeparam name="T">List类型</typeparam>
        /// <param name="str">字符串</param>
        /// <param name="split">分隔符</param>
        /// <param name="convertHandler">类型转换</param>
        /// <returns>List</returns>
        public static List<T> ToList<T>(this string str, string split, Converter<string, T> convertHandler)
        {
            if (string.IsNullOrEmpty(str))
                return new List<T>();
            else
            {
                string[] arr = str.Split(new char[] { Convert.ToChar(split) });
                T[] Tarr = Array.ConvertAll(arr, convertHandler);
                return new List<T>(Tarr);
            }
        }
        #region int
        /// <summary>
        /// 检测输入字符串是否是有效的32位整数
        /// </summary>
        /// <param name="s">输入字符串</param>
        /// <returns>Boolen值</returns>
        public static bool IsInt(this string s)
        {
            int i;
            return int.TryParse(s, out i);
        }
        /// <summary>
        /// 将数字的字符串表示形式转换为它的等效 32 位有符号整数
        /// </summary>
        /// <param name="s">包含要转换的数字的字符串</param>
        /// <returns>与 s 中包含的数字等效的 32 位有符号整数</returns>
        public static int ToInt(this string s)
        {
            return int.Parse(s);
        }
        /// <summary>
        /// 将数字的字符串表示形式转换为它的等效 32 位有符号整数
        /// </summary>
        /// <param name="s">包含要转换的数字的字符串</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static int ToInt(this string s, int defaultValue)
        {
            if (s.IsInt())
                return int.Parse(s);
            else
                return defaultValue;
        }
        #endregion
        #region float
        /// <summary>
        /// 检测输入字符串是否是有效的32位浮点数
        /// </summary>
        /// <param name="s">输入字符串</param>
        /// <returns>Boolen值</returns>
        public static bool IsFloat(this string s)
        {
            float i;
            return float.TryParse(s, out i);
        }
        /// <summary>
        /// 将数字的字符串表示形式转换为它的等效单精度浮点数字
        /// </summary>
        /// <param name="s">包含要转换的数字的字符串</param>
        /// <returns>与 s 中指定的数值或符号等效的单精度浮点数字</returns>
        public static float ToFloat(this string s)
        {
            return float.Parse(s);
        }
        /// <summary>
        /// 将数字的字符串表示形式转换为它的等效单精度浮点数字
        /// </summary>
        /// <param name="s">包含要转换的数字的字符串</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static float ToFloat(this string s, float defaultValue)
        {
            if (s.IsFloat())
                return s.ToFloat();
            else
                return defaultValue;
        }
        #endregion
        #region string
        /// <summary>
        /// 字符串是否为空
        /// </summary>
        /// <param name="s">需要检测的字符串</param>
        /// <returns>boolen值</returns>
        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }
        #endregion


        public static IList<T> GetPager<T>(this List<T> list, int page, int pagesize)
        {
            IList<T> targetList = new List<T>();
            for (var i = 0; i < list.Count; i++)
            {
                if (i >= (page - 1) * pagesize && i < page * pagesize)
                {
                    targetList.Add(list[i]);
                }
            }
            return targetList;
        }
        public static DataTable GetPager(this DataTable datatable, int page, int pagesize)
        {
            var dt = datatable.Clone();
            for (var i = 0; i < datatable.Rows.Count; i++)
            {
                if (i >= (page - 1) * pagesize && i < page * pagesize)
                {
                    dt.Rows.Add(datatable.Rows[i].ItemArray);
                }
            }
            return dt;
        }

        #region Regex
        /// <summary>
        /// 指示所指定的正则表达式在指定的输入字符串中是否找到了匹配项。
        /// </summary>
        /// <param name="s">要搜索匹配项的字符串</param>
        /// <param name="pattern">要匹配的正则表达式模式</param>
        /// <returns>如果正则表达式找到匹配项，则为 true；否则，为 false</returns>
        public static bool IsMatch(this string s, string pattern)
        {
            return (s == null) ? false : Regex.IsMatch(s, pattern);
        }
        /// <summary>
        /// 在指定的输入字符串中搜索指定的正则表达式的第一个匹配项
        /// </summary>
        /// <param name="s">要搜索匹配项的字符串</param>
        /// <param name="pattern">要匹配的正则表达式模式</param>
        /// <returns>一个对象，包含有关匹配项的信息</returns>
        public static string Match(this string s, string pattern)
        {
            return (s == null) ? "" : Regex.Match(s, pattern).Value;
        }
        #endregion
    }
}
