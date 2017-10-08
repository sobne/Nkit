using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nkit.Core
{
    public class Base
    {
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
