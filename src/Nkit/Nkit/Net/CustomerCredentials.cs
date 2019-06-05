using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Nkit.Net
{
    [Serializable]
    public class CustomerCredentials
    {
        private string _userName;
        private string _passWord;
        private string _domainName;
        public CustomerCredentials(string UserName, string PassWord, string DomainName)
        {
            _userName = UserName;
            _passWord = PassWord;
            _domainName = DomainName;
        }
        public NetworkCredential NetworkCredentials
        {
            get
            {
                if (string.IsNullOrEmpty(_userName) || string.IsNullOrEmpty(_passWord))
                {
                    return null;
                }
                else if (string.IsNullOrEmpty(_domainName))
                {
                    return new NetworkCredential(_userName, _passWord);
                }
                else
                {
                    return new NetworkCredential(_userName, _passWord, _domainName);
                }
            }
        }
        public WindowsIdentity ImpersonationUser
        {
            get
            {
                return null;
            }
        }
        public bool GetFormsCredentials(out Cookie authCookie, out string user, out string password, out string authority)
        {
            authCookie = null;
            user = password = authority = null;
            return false;
        }
    }
}
