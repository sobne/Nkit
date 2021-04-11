namespace Nkit.Security
{
    /// <summary>
    /// 移位加/解密
    /// </summary>
    public class Shift
    {
        /// <summary>
        /// 字符串加密  进行位移操作
        /// </summary>
        /// <param name="str">待加密数据</param>
        /// <returns>加密后的数据</returns>C:\p\code\Nkit\src\Nkit\Nkit\Security\Shift.cs
        public string Encrypt(string str)
        {
            string _temp = "";
            int _inttemp;
            char[] _chartemp = str.ToCharArray();
            for (int i = 0; i < _chartemp.Length; i++)
            {
                _inttemp = _chartemp[i] + 1;
                _chartemp[i] = (char)_inttemp;
                _temp += _chartemp[i];
            }
            return _temp;
        }
        /// <summary>
        /// 字符串解密 位移操作后的
        /// </summary>
        /// <param name="str">待解密数据</param>
        /// <returns>解密成功后的数据</returns>
        public string Decrypt(string str)
        {
            string _temp = "";
            int _inttemp;
            char[] _chartemp = str.ToCharArray();
            for (int i = 0; i < _chartemp.Length; i++)
            {
                _inttemp = _chartemp[i] - 1;
                _chartemp[i] = (char)_inttemp;
                _temp += _chartemp[i];
            }
            return _temp;
        }
    }
}
