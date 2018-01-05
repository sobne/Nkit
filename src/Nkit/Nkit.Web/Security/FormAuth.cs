using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;

namespace Nkit.Web.Security
{
    public class FormAuth
    {
        /// <summary>
        /// encrypting string
        /// </summary>
        /// <param name="str">before encrypt string</param>
        /// <returns></returns>
        public static string Encrypt(string str)
        {
            FormsAuthenticationTicket ticket = new System.Web.Security.FormsAuthenticationTicket(str, true, 2);
            return FormsAuthentication.Encrypt(ticket).ToString();
        }
        /// <summary>
        /// decrypt string
        /// </summary>
        /// <param name="str">encrypted string</param>
        /// <returns></returns>
        public static string Decrypt(string str)
        {
            return FormsAuthentication.Decrypt(str).Name.ToString();
        }
    }
}
