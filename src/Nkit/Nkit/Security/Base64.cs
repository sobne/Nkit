using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nkit.Security
{
    /// <summary>
    /// Base64 反/编码
    /// </summary>
    public class Base64
    {
        /// <summary>
        /// 对字符串进行base64编码
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>base64编码串</returns>
        public string Encode(string str)
        {
            byte[] encbuff = Encoding.UTF8.GetBytes(str);
            return Convert.ToBase64String(encbuff);
        }
        /// <summary>
        /// 对字符串进行反编码
        /// </summary>
        /// <param name="str">base64编码串</param>
        /// <returns>字符串</returns>
        public string Decode(string str)
        {
            byte[] decbuff = Convert.FromBase64String(str);
            return Encoding.UTF8.GetString(decbuff);
        }
    }
}
