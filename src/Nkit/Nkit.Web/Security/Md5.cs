using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nkit.Web.Security
{
    public class Md5
    {
        /// <summary>
        /// MD5加密字符串处理
        /// </summary>
        /// <param name="Input">待加密码字符串</param>
        /// <param name="Half">加密是16位还是32位；如果为true为16位</param>
        /// <returns></returns>
        public static string Encrypt(string Input, bool Half)
        {
            string output = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(Input, "MD5").ToLower();
            if (Half)//16位MD5加密（取32位加密的9~25字符）
                output = output.Substring(8, 16);
            return output;
        }
    }
}
