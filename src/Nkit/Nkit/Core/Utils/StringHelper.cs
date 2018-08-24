using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Nkit.Core.Utils
{
    /// <summary>
    /// 字符串帮助类
    /// </summary>
    public class StringHelper
    {
        /// <summary>
        /// 取得字符串长度（中文长度为2计算）
        /// </summary>
        /// <param name="s">要计算长度的字符串</param>
        /// <returns></returns>
        public static Int32 GetByteCount(String s)
        {
            return Encoding.Default.GetByteCount(s.ToCharArray());
        }
        /// <summary>
        /// 获得字符串的真实长度,1个汉字长度为2
        /// </summary>
        /// <param name="str">要计算长度的字符串</param>
        /// <returns></returns>
        public static int GetStringLen(string str)
        {
            return Encoding.Default.GetBytes(str).Length;
        }
        /// <summary>
        /// 截断字符串,长出部分用...代替
        /// </summary>
        /// <param name="in_str">要截取的字符串</param>
        /// <param name="short_len">截取长度</param>
        /// <returns>返回截取后的字符串</returns>
        public string GetShort(string in_str, int short_len)
        {
            string _str = in_str;
            int idx = 0;
            int i = 0;
            bool b_long = false;
            for (i = 0; i < _str.Length; i++)
            {
                if (idx > short_len)
                {
                    b_long = true;
                    break;
                }
                if (((int)_str[i]) > 128)
                {
                    idx = idx + 2;
                }
                else
                {
                    idx++;
                }
            }
            try
            {
                _str = _str.Substring(0, i);
                if (b_long)
                {
                    _str = _str + "...";
                }
            }
            catch
            {
                _str = in_str;
            }
            return _str;
        }
        /// <summary>
        /// 截取字符串(中文长度为2计算)
        /// </summary>
        /// <param name="sourceString"></param>
        /// <param name="byteLength"></param>
        /// <returns></returns>
        public static string CutString(string sourceString, int byteLength)
        {
            StringBuilder sb = new StringBuilder();
            Int32 byteCount = 0;
            for (Int32 i = 0; i < sourceString.Length; i++)
            {
                byteCount += Encoding.Default.GetByteCount(sourceString[i].ToString());

                if (byteCount > byteLength)
                {
                    break;
                }
                sb.Append(sourceString[i]);
            }
            return sb.ToString();

        }
        

        /// <summary>
        /// 去除字符串内的空白字符
        /// </summary>
        /// <param name="input">要进行处理的字符串</param>
        /// <returns>处理后的字符串</returns>
        public static string TrimIntraWords(string input)
        {
            Regex regEx = new Regex(@"[\s]+");
            return regEx.Replace(input, " ");
        }
        /// <summary>
        /// 去除新行
        /// </summary>
        /// <param name="input">要去除新行的字符串</param>
        /// <returns>已经去除新行的字符串</returns>
        public static string RemoveNewLines(string input)
        {
            return StringHelper.RemoveNewLines(input, false);
        }
        /// <summary>
        /// 去除新行
        /// </summary>
        /// <param name="input">要去除新行的字符串</param>
        /// <param name="addSpace">是否添加空格</param>
        /// <returns>已经去除新行的字符串</returns>
        public static string RemoveNewLines(string input, bool addSpace)
        {
            string replace = string.Empty;
            if (addSpace)
                replace = " ";

            string pattern = @"[\r|\n]";
            Regex regEx = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Multiline);

            return regEx.Replace(input, replace);
        }

        /// <summary>
        /// 替换字符串(忽略大小写)
        /// </summary>
        /// <param name="input">要进行替换的内容</param>
        /// <param name="oldValue">旧字符串</param>
        /// <param name="newValue">新字符串</param>
        /// <returns>替换后的字符串</returns>
        public static string CaseInsensitiveReplace(string input, string oldValue, string newValue)
        {
            Regex regEx = new Regex(oldValue, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            return regEx.Replace(input, newValue);
        }

        /// <summary>
        /// 替换首次出现的字符串
        /// </summary>
        /// <param name="input">要进行替换的内容</param>
        /// <param name="oldValue">旧字符串</param>
        /// <param name="newValue">新字符串</param>
        /// <returns>替换后的字符串</returns>
        public static string ReplaceFirst(string input, string oldValue, string newValue)
        {
            Regex regEx = new Regex(oldValue, RegexOptions.Multiline);
            return regEx.Replace(input, newValue, 1);
        }

        /// <summary>
        /// 替换最后一次出现的字符串
        /// </summary>
        /// <param name="input">要进行替换的内容</param>
        /// <param name="oldValue">旧字符串</param>
        /// <param name="newValue">新字符串</param>
        /// <returns>替换后的字符串</returns>
        public static string ReplaceLast(string input, string oldValue, string newValue)
        {
            int index = input.LastIndexOf(oldValue);
            if (index < 0)
            {
                return input;
            }
            else
            {
                StringBuilder sb = new StringBuilder(input.Length - oldValue.Length + newValue.Length);
                sb.Append(input.Substring(0, index));
                sb.Append(newValue);
                sb.Append(input.Substring(index + oldValue.Length, input.Length - index - oldValue.Length));
                return sb.ToString();
            }
        }
        /// <summary>
        /// 转成首字母大字形式
        /// </summary>
        /// <param name="input">要进行转换的字符串</param>
        /// <returns>转换后的字符串</returns>
        public static string SentenceCase(string input)
        {
            if (input.Length < 1)
                return input;

            string sentence = input.ToLower();
            return sentence[0].ToString().ToUpper() + sentence.Substring(1);
        }
        /// <summary>
        /// 字符串反转
        /// </summary>
        /// <param name="input">要进行反转的字符串</param>
        /// <returns>反转后的字符串</returns>
        public static string Reverse(string input)
        {
            int i;
            StringBuilder sb = new StringBuilder();

            for (i = input.Length - 1; i >= 0; i--)
            {
                sb.Append(input[i]);
            }

            return sb.ToString();
            //char[] reverse = new char[input.Length];
            //for (int i = 0, k = input.Length - 1; i < input.Length; i++, k--)
            //{
            //    if (char.IsSurrogate(input[k]))
            //    {
            //        reverse[i + 1] = input[k--];
            //        reverse[i++] = input[k];
            //    }
            //    else
            //    {
            //        reverse[i] = input[k];
            //    }
            //}
            //return new System.String(reverse);
        }

        /// <summary> 
        /// 汉字转拼音缩写 
        /// </summary> 
        /// <param name="Input">要转换的汉字字符串</param> 
        /// <returns>拼音缩写</returns> 
        public static string GetPYString(string Input)
        {
            string ret = "";
            foreach (char c in Input)
            {
                if ((int)c >= 33 && (int)c <= 126)
                {//字母和符号原样保留 
                    ret += c.ToString();
                }
                else
                {//累加拼音声母 
                    ret += GetPYChar(c.ToString());
                }
            }
            return ret;
        }

        /// <summary> 
        /// 取单个字符的拼音声母
        /// </summary> 
        /// <param name="c">要转换的单个汉字</param> 
        /// <returns>拼音声母</returns> 
        private static string GetPYChar(string c)
        {
            byte[] array = new byte[2];
            array = System.Text.Encoding.Default.GetBytes(c);
            int i = (short)(array[0] - '\0') * 256 + ((short)(array[1] - '\0'));
            if (i < 0xB0A1) return "*";
            if (i < 0xB0C5) return "A";
            if (i < 0xB2C1) return "B";
            if (i < 0xB4EE) return "C";
            if (i < 0xB6EA) return "D";
            if (i < 0xB7A2) return "E";
            if (i < 0xB8C1) return "F";
            if (i < 0xB9FE) return "G";
            if (i < 0xBBF7) return "H";
            if (i < 0xBFA6) return "G";
            if (i < 0xC0AC) return "K";
            if (i < 0xC2E8) return "L";
            if (i < 0xC4C3) return "M";
            if (i < 0xC5B6) return "N";
            if (i < 0xC5BE) return "O";
            if (i < 0xC6DA) return "P";
            if (i < 0xC8BB) return "Q";
            if (i < 0xC8F6) return "R";
            if (i < 0xCBFA) return "S";
            if (i < 0xCDDA) return "T";
            if (i < 0xCEF4) return "W";
            if (i < 0xD1B9) return "X";
            if (i < 0xD4D1) return "Y";
            if (i < 0xD7FA) return "Z";
            return "*";
        }
        /// <summary>
        /// 获取汉字第一个拼音
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetSpells(string input)
        {
            int len = input.Length;
            string reVal = "";
            for (int i = 0; i < len; i++)
            {
                reVal += GetSpell(input.Substring(i, 1));
            }
            return reVal;
        }
        /// <summary>
        /// 取单个字符的拼音
        /// </summary>
        /// <param name="cn"></param>
        /// <returns></returns>
        public static string GetSpell(string cn)
        {
            byte[] arrCN = Encoding.Default.GetBytes(cn);
            if (arrCN.Length > 1)
            {
                int area = (short)arrCN[0];
                int pos = (short)arrCN[1];
                int code = (area << 8) + pos;
                int[] areacode = { 45217, 45253, 45761, 46318, 46826, 47010, 47297, 47614, 48119, 48119, 49062, 49324, 49896, 50371, 50614, 50622, 50906, 51387, 51446, 52218, 52698, 52698, 52698, 52980, 53689, 54481 };
                for (int i = 0; i < 26; i++)
                {
                    int max = 55290;
                    if (i != 25) max = areacode[i + 1];
                    if (areacode[i] <= code && code < max)
                    {
                        return Encoding.Default.GetString(new byte[] { (byte)(65 + i) });
                    }
                }
                return "?";
            }
            else return cn;
        }
        /// <summary>
        /// 半角转全角
        /// </summary>
        /// <param name="BJstr"></param>
        /// <returns></returns>
        static public string GetQuanJiao(string BJstr)
        {
            char[] c = BJstr.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                byte[] b = System.Text.Encoding.Unicode.GetBytes(c, i, 1);
                if (b.Length == 2)
                {
                    if (b[1] == 0)
                    {
                        b[0] = (byte)(b[0] - 32);
                        b[1] = 255;
                        c[i] = System.Text.Encoding.Unicode.GetChars(b)[0];
                    }
                }
            }

            return new string(c);
        }
        /// <summary>
        /// 全角转半角
        /// </summary>
        /// <param name="QJstr"></param>
        /// <returns></returns>
        static public string GetBanJiao(string QJstr)
        {
            char[] c = QJstr.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                byte[] b = System.Text.Encoding.Unicode.GetBytes(c, i, 1);
                if (b.Length == 2)
                {
                    if (b[1] == 255)
                    {
                        b[0] = (byte)(b[0] + 32);
                        b[1] = 0;
                        c[i] = System.Text.Encoding.Unicode.GetChars(b)[0];
                    }
                }
            }
            return new string(c);
        }
    }
}
