using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Nkit.Security
{
    /// <summary>
    /// SHA256加密
    /// </summary>
    public class Sha
    {
        /// <summary>
        /// SHA256加密函数
        /// </summary>
        /// <param name="str">加密字符串</param>
        /// <returns>加密后字符串</returns>
        public string Encrypt(string str)
        {
            byte[] SHA256Data = Encoding.UTF8.GetBytes(str);
            SHA256Managed Sha256 = new SHA256Managed();
            byte[] Result = Sha256.ComputeHash(SHA256Data);
            return Convert.ToBase64String(Result);
        }
    }
}
