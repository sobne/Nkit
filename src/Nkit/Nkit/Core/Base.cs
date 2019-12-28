using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Nkit.Core
{
    public class Base
    {
        /// <summary>
        /// 获取Enum描述信息
        /// </summary>
        /// <param name="enum"></param>
        /// <returns></returns>
        public static string GetEnumDescription(Enum @enum)
        {
            var type = @enum.GetType();
            var memberInfos = type.GetMember(@enum.ToString());
            if (memberInfos != null && memberInfos.Length > 0)
            {
                if (memberInfos[0].GetCustomAttributes(typeof(DescriptionAttribute), false) is DescriptionAttribute[] attrs && attrs.Length > 0)
                {
                    return attrs[0].Description;
                }
            }
            return @enum.ToString();
        }
        /// <summary>
        /// 扩展方法，获得枚举的Description
        /// </summary>
        /// <param name="value">枚举值</param>
        /// <param name="nameInstead">当枚举值没有定义DescriptionAttribute，是否使用枚举名代替，默认是使用</param>
        /// <returns>枚举的Description</returns>
        public static string GetDescription4Enum(Enum value, Boolean nameInstead = true)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name == null)
            {
                return null;
            }

            FieldInfo field = type.GetField(name);
            DescriptionAttribute attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;

            if (attribute == null && nameInstead == true)
            {
                return name;
            }
            return attribute == null ? null : attribute.Description;
        }
        /// <summary>
        /// 比较大小,使用范型,返回较大的那个数
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="op1">数据1</param>
        /// <param name="op2"><数据2/param>
        /// <returns>较大的那个数</returns>
        public static T Max<T>(T op1, T op2) where T : IComparable
        {
            if (op1.CompareTo(op2) > 0)
                return op1;
            return op2;
        }
        /// <summary>
        /// 返回两个数中较大的那个
        /// </summary>
        public static Func<int, int, int> Max1 = (a, b) => a > b ? a : b;
        /// <summary>
        /// 返回两个数中较大的那个
        /// </summary>
        public static Func<int, int, int> Max11 = delegate(int a, int b)
        {
            if (a > b)
                return a;
            else
                return b;
        };
    }
}
