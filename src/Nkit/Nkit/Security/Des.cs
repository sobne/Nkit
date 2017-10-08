using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using Nkit.Core.Expression;

namespace Nkit.Security
{
    /// <summary>
    /// Des 加/解密
    /// </summary>
    public class Des
    {
        private string IV = "0FF3E210-F76E-46f2-B56F-6997DDDD58F1";
        private string KEY = "C50E23B7-4CA8-433e-8C13-25B53B71B5F0";
        
        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="str">要解密的字符串</param>
        /// <returns>解密后的字符串</returns>
        public string Decrypt(string str)
        {
            string s = KEY.Substring(0, 8);
            string str2 = IV.Substring(0, 8);
            SymmetricAlgorithm algorithm = new DESCryptoServiceProvider();
            algorithm.Key = Encoding.ASCII.GetBytes(s);
            algorithm.IV = Encoding.ASCII.GetBytes(str2);
            ICryptoTransform transform = algorithm.CreateDecryptor();
            byte[] buffer = Convert.FromBase64String(str);
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Write);
            stream2.Write(buffer, 0, buffer.Length);
            stream2.FlushFinalBlock();
            stream2.Close();
            return Encoding.UTF8.GetString(stream.ToArray());
        }
        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="str">需要加密的字符串</param>
        /// <returns>加密后的字符串</returns>
        public string Encrypt(string str)
        {
            string s = KEY.Substring(0, 8);
            string str2 = IV.Substring(0, 8);
            SymmetricAlgorithm algorithm = new DESCryptoServiceProvider();
            algorithm.Key = Encoding.ASCII.GetBytes(s);
            algorithm.IV = Encoding.ASCII.GetBytes(str2);
            ICryptoTransform transform = algorithm.CreateEncryptor();
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Write);
            stream2.Write(bytes, 0, bytes.Length);
            stream2.FlushFinalBlock();
            stream2.Close();
            return Convert.ToBase64String(stream.ToArray());
        }
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="str">要解密的字符串</param>
        /// <returns></returns>
        public string Decode(string str)
        {
            return Decode(str, CopyRightExp.PersonalWebSite);
        }
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="str">要加密的字符串</param>
        /// <returns></returns>
        public string Encode(string str)
        {
            return Encode(str, CopyRightExp.PersonalWebSite);
        }

        /// <summary>
        /// 加密 GB2312 
        /// </summary>
        /// <param name="str">加密串</param>
        /// <param name="key">Key</param>
        /// <returns></returns>
        public string Encode(string str, string key)
        {
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            provider.Key = Encoding.ASCII.GetBytes(key.Substring(0, 8));
            provider.IV = Encoding.ASCII.GetBytes(key.Substring(0, 8));
            byte[] bytes = Encoding.GetEncoding("GB2312").GetBytes(str);
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, provider.CreateEncryptor(), CryptoStreamMode.Write);
            stream2.Write(bytes, 0, bytes.Length);
            stream2.FlushFinalBlock();
            StringBuilder builder = new StringBuilder();
            foreach (byte num in stream.ToArray())
            {
                builder.AppendFormat("{0:X2}", num);
            }
            stream.Close();
            return builder.ToString();
        }

        /// <summary>
        /// 解密 GB2312 
        /// </summary>
        /// <param name="str">Desc string</param>
        /// <param name="key">Key ,必须为8位 </param>
        /// <returns></returns>
        public string Decode(string str, string key)
        {
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            provider.Key = Encoding.ASCII.GetBytes(key.Substring(0, 8));
            provider.IV = Encoding.ASCII.GetBytes(key.Substring(0, 8));
            byte[] buffer = new byte[str.Length / 2];
            for (int i = 0; i < (str.Length / 2); i++)
            {
                int num2 = Convert.ToInt32(str.Substring(i * 2, 2), 0x10);
                buffer[i] = (byte)num2;
            }
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, provider.CreateDecryptor(), CryptoStreamMode.Write);
            stream2.Write(buffer, 0, buffer.Length);
            stream2.FlushFinalBlock();
            stream.Close();
            return Encoding.GetEncoding("GB2312").GetString(stream.ToArray());
        }
    }
}
