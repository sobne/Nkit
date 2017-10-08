using System;
using System.Text;
using System.Text.RegularExpressions;
using Nkit.Core.Expression;

namespace Nkit.Core.Utils
{
    /// <summary>
    /// 各种验证类
    /// </summary>
    public class VerifyHelper
    {
        #region 正则表达式
        /// <summary>
        /// 检测有效IP
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Boolean IsIP(string s)
        {
            if (Regex.IsMatch(s, @"\d{1,3}.\d{1,3}.\d{1,3}.\d{1,3}"))
            {
                String[] ipParts = s.Split('.');
                if (ipParts.Length != 4)
                {
                    return false;
                }
                for (Int32 i = 0; i < ipParts.Length; i++)
                {
                    if (ipParts[i].Length > 3 || ipParts[i].Length < 1)
                    {
                        return false;
                    }
                    if (!IsNumber(ipParts[i]))
                    {
                        return false;
                    }
                    Int32 num = Convert.ToInt32(ipParts[i]);
                    if (num < 0 || num > 255)
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }
        /// <summary>
        /// 是否正确的E-Mail地址
        /// </summary>
        /// <param name="sEmail">需要检测的E-Mail地址串</param>
        /// <returns>boolen值</returns>
        public static bool IsEmail(string sEmail)
        {
            Regex regex = new Regex(RegularExp.EMAIL);
            return regex.Match(sEmail).Success;
        }
        /// <summary>
        /// 判断是否是中文
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsChineseWord(string s)
        {
            string[] stringMatchs = new string[]
            {
                @"[\u3040-\u318f]+",
                @"[\u3300-\u337f]+",
                @"[\u3400-\u3d2d]+",
                @"[\u4e00-\u9fff]+",
                @"[\u4e00-\u9fa5]+",
                @"[\uf900-\ufaff]+"
            };
            foreach (string stringMatch in stringMatchs)
                if (Regex.IsMatch(s, stringMatch))
                    return true;
            return false;
        }
        #endregion

        #region 一般方法

        /// <summary>
        /// 检测字符串是否为数值。
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Boolean IsNumber(String s)
        {
            if (String.IsNullOrEmpty(s))
                return false;
            for (Int32 i = 0; i < s.Length; i++)
            {
                char chr = s[i];
                if (!char.IsNumber(chr))
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 判断是否数字
        /// </summary>
        /// <param name="oText">接收判断的字符串</param>
        /// <returns>判断结果\1真</returns>
        public static bool IsNumberic(string oText)
        {
            try
            {
                int var1 = Convert.ToInt32(oText);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 检测是否为指定长度的数值。
        /// </summary>
        /// <param name="s"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static Boolean IsNumber(String s, Int32 length)
        {
            if (s.Length > length)
                return false;
            return IsNumber(s);
        }

        /// <summary>
        /// 判断是否为十进制。
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Boolean IsDecimal(String s)
        {
            try
            {
                Decimal.Parse(s);
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 判断是否为整数。
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Boolean IsInt32(String s)
        {
            try
            {
                Int32.Parse(s);
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 判断是否为浮点型。
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Boolean IsSingle(String s)
        {
            try
            {
                Single.Parse(s);
            }
            catch
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 判断是否为日期
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Boolean IsDateTime(String s)
        {
            try
            {
                DateTime.Parse(s);
            }
            catch
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 判断两日期是否为区间
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static Boolean IsValidDateTimeSpan(DateTime start, DateTime end)
        {
            return DateTime.Compare(start, end) <= 0;
        }
        
        #endregion

        #region 验证码 || 随机数
        /// <summary>
        /// 生成随机数
        /// </summary>
        /// <param name="iNum">随机数的位数</param>
        /// <returns></returns>
        public static string GenerateCheckCode(int iNum)
        {
            int number;
            char code;
            string checkCode = String.Empty;

            System.Random random = new Random();

            for (int i = 0; i < iNum; i++)
            {
                number = random.Next();

                if (number % 2 == 0)
                    code = (char)('0' + (char)(number % 10));
                else
                    code = (char)('A' + (char)(number % 26));

                checkCode += code.ToString();
            }

            return checkCode;
        }

        /// <summary>
        /// 生成随机数字
        /// </summary>
        /// <param name="Length">生成长度</param>
        /// <returns></returns>
        public static string Number(int Length)
        {
            return Number(Length, false);
        }

        /// <summary>
        /// 生成随机数字
        /// </summary>
        /// <param name="Length">生成长度</param>
        /// <param name="Sleep">是否要在生成前将当前线程阻止以避免重复</param>
        /// <returns></returns>
        public static string Number(int Length, bool Sleep)
        {
            if (Sleep)
                System.Threading.Thread.Sleep(3);
            string result = "";
            System.Random random = new Random();
            for (int i = 0; i < Length; i++)
            {
                result += random.Next(10).ToString();
            }
            return result;
        }

        /// <summary>
        /// 生成随机字母与数字
        /// </summary>
        /// <param name="Length">生成长度</param>
        /// <returns></returns>
        public static string Str(int Length)
        {
            return Str(Length, false);
        }
        /// <summary>
        /// 生成随机字母与数字
        /// </summary>
        /// <param name="Length">生成长度</param>
        /// <param name="Sleep">是否要在生成前将当前线程阻止以避免重复</param>
        /// <returns></returns>
        public static string Str(int Length, bool Sleep)
        {
            if (Sleep)
                System.Threading.Thread.Sleep(3);
            char[] Pattern = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            string result = "";
            int n = Pattern.Length;
            System.Random random = new Random(~unchecked((int)DateTime.Now.Ticks));
            for (int i = 0; i < Length; i++)
            {
                int rnd = random.Next(0, n);
                result += Pattern[rnd];
            }
            return result;
        }


        /// <summary>
        /// 生成随机纯字母随机数
        /// </summary>
        /// <param name="Length">生成长度</param>
        /// <returns></returns>
        public static string Str_char(int Length)
        {
            return Str_char(Length, false);
        }

        /// <summary>
        /// 生成随机纯字母随机数
        /// </summary>
        /// <param name="Length">生成长度</param>
        /// <param name="Sleep">是否要在生成前将当前线程阻止以避免重复</param>
        /// <returns></returns>
        public static string Str_char(int Length, bool Sleep)
        {
            if (Sleep)
                System.Threading.Thread.Sleep(3);
            char[] Pattern = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            string result = "";
            int n = Pattern.Length;
            System.Random random = new Random(~unchecked((int)DateTime.Now.Ticks));
            for (int i = 0; i < Length; i++)
            {
                int rnd = random.Next(0, n);
                result += Pattern[rnd];
            }
            return result;
        }
        #endregion

        #region object
        /// <summary>
        /// 判断是否Int型
        /// </summary>
        /// <param name="obj">需要判断的对象</param>
        /// <returns></returns>
        static public bool IsInt(object obj)
        {
            bool bReturn = false;
            try
            {
                int iValue = Convert.ToInt32(obj.ToString());
                bReturn = true;
            }
            catch
            {
            }
            return bReturn;
        }
        /// <summary>
        /// 判断是否Double型
        /// </summary>
        /// <param name="obj">需要判断的对象</param>
        /// <returns></returns>
        static public bool IsDouble(object obj)
        {
            bool bReturn = false;
            try
            {
                double dValue = Convert.ToDouble(obj.ToString());
                bReturn = true;
            }
            catch
            {
            }
            return bReturn;
        }
        /// <summary>
        /// 判断是否Bool型
        /// </summary>
        /// <param name="obj">需要判断的对象</param>
        /// <returns></returns>
        static public bool IsBool(object obj)
        {
            bool bReturn = false;
            try
            {
                bool bValue = Convert.ToBoolean(obj.ToString());
                bReturn = true;
            }
            catch
            {
            }
            return bReturn;
        }
        /// <summary>
        /// 判断是否DateTime型
        /// </summary>
        /// <param name="obj">需要判断的对象</param>
        /// <returns></returns>
        static public bool IsDate(object obj)
        {
            bool bReturn = false;
            try
            {
                DateTime dtValue = Convert.ToDateTime(obj.ToString());
                bReturn = true;
            }
            catch
            {
            }
            return bReturn;
        }


        #endregion
    }
}
