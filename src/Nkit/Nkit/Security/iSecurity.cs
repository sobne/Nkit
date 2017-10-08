using System;
using System.Text;
using System.Security.Cryptography;//加密
using System.IO;
using Nkit.Core.Expression;
namespace Nkit.Security
{   
    public interface iSecurity
    {
        string Encrypt(string str);
        string Decrypt(string str);

        string Encrypt(string str, string key);
        string Decrypt(string str, string key);

        string Encode(string str);
        string Decode(string str);
        
    }
}
