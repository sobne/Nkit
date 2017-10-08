using System;

namespace Nkit.Core.Expression
{
    /// <summary>
    /// 常用正则表达式
    /// </summary>
    public class RegularExp
    {
        /// <summary>
        /// QQ号
        /// </summary>
        public static readonly string QQ_NO = "[1-9][0-9]{4,}";
        /// <summary>
        /// MSN 或 EMail
        /// </summary>
        public static readonly string EMAIL = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
        /// <summary>
        /// 固定电话 (匹配形式如 010-1234567 或 010-88888888)
        /// </summary>
        public static readonly string TELEPHONE = @"\d{2,5}-\d{7,8}(-\d{1,})?";
        /// <summary>
        /// 手机号码
        /// </summary>
        public static readonly string MOBILE = @"^1[3,5]\d{9}$";
        /// <summary>
        /// 身份证号    (中国的身份证为15位或18位)
        /// </summary>
        public static readonly string IDENTITYCARD = @"(^\d{15}$)|(^\d{17}([0-9]|X)$)";
        /// <summary>
        /// 邮政编码     (中国邮政编码为6位数字)
        /// </summary>
        public static readonly string POSTCODE = @"[1-9]\d{5}(?!\d)";
        /// <summary>
        /// 中文字符
        /// </summary>
        public static readonly string CHINESE = "[\u4e00-\u9fa5]";
        /// <summary>
        /// 双字节字符  (包括汉字在内)(一个双字节字符长度计2，ASCII字符计1)
        /// </summary>
        public static readonly string DOUBLE_BYTE = "[^\x00-\xff]";
        /// <summary>
        /// HTML标记
        /// </summary>
        public static readonly string HTML = @"<(\S*?)[^>]*>.*?</\1>|<.*? />";
        /// <summary>
        /// 网址URL
        /// </summary>
        public static readonly string URL = @"[a-zA-z]+://[^\s]*";
        /// <summary>
        /// ip地址
        /// </summary>
        public static readonly string IP = @"\d+\.\d+\.\d+\.\d+";
        /// <summary>
        /// 26个英文字母组成的字符串
        /// </summary>
        public static readonly string ENGLISH_CHAR = @"^[A-Za-z]+$";
        /// <summary>
        /// 26个英文字母的大写组成的字符串
        /// </summary>
        public static readonly string UPCHAR = @"^[A-Z]+$";
        /// <summary>
        /// 26个英文字母的小写组成的字符串
        /// </summary>
        public static readonly string LOWERCHAR = @"^[a-z]+$";
        /// <summary>
        /// 数字和26个英文字母组成的字符串
        /// </summary>
        public static readonly string CHAR_ANDN_NUMBER = @"^[A-Za-z0-9]+$";
        /// <summary>
        /// 数字、26个英文字母或者下划线组成的字符串
        /// </summary>
        public static readonly string SPECIAL_STRING = @"^\w+$";
        /// <summary>
        /// 匹配正整数
        /// </summary>
        public static readonly string POSITIVE_INTEGERS = @"^[1-9]\d*$";
        /// <summary>
        /// 匹配负整数
        /// </summary>
        public static readonly string NEGATIVE_INTEGERS = @"^-[1-9]\d*$";
        /// <summary>
        /// 匹配整数
        /// </summary>
        public static readonly string INTEGERS = @"^-?[1-9]\d*$";
        /// <summary>
        /// 匹配非负整数（正整数 + 0）
        /// </summary>
        public static readonly string NO_NEGATIVE_INTEGERS = @"^[1-9]\d*|0$";
        /// <summary>
        /// 匹配非正整数（负整数 + 0）
        /// </summary>
        public static readonly string NO_POSITIVE_INTEGERS = @"^-[1-9]\d*|0$";
        /// <summary>
        /// 匹配正浮点数
        /// </summary>
        public static readonly string POSITIVE_FLOATS = @"^[1-9]\d*\.\d*|0\.\d*[1-9]\d*$";
        /// <summary>
        /// 匹配负浮点数
        /// </summary>
        public static readonly string NEGATIVE_FLOATS = @"^-([1-9]\d*\.\d*|0\.\d*[1-9]\d*)$";
        /// <summary>
        /// 匹配浮点数
        /// </summary>
        public static readonly string FLOATS = @"^-?([1-9]\d*\.\d*|0\.\d*[1-9]\d*|0?\.0+|0)$";
        /// <summary>
        /// 匹配非负浮点数（正浮点数 + 0）
        /// </summary>
        public static readonly string NO_NEGATIVE_FLOATS = @"^[1-9]\d*\.\d*|0\.\d*[1-9]\d*|0?\.0+|0$";
        /// <summary>
        /// 匹配非正浮点数（负浮点数 + 0）
        /// </summary>
        public static readonly string NO_POSITIVE_FLOATS = @"^(-([1-9]\d*\.\d*|0\.\d*[1-9]\d*))|0?\.0+|0$";
    }
}
