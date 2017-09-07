using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nkit.Utils
{
    public static class ObjectExt
    {
        public static int ToInt32(this object obj, int defValue)
        {
            int result;
            if (obj == DBNull.Value || obj == null)
            {
                result = defValue;
            }
            else
            {
                string s = obj.ToString(defValue.ToString());
                int num = defValue;
                double num2 = (double)defValue;
                if (int.TryParse(s, out num))
                {
                    result = num;
                }
                else
                {
                    if (double.TryParse(s, out num2))
                    {
                        num = (int)num2;
                        result = num;
                    }
                    else
                    {
                        result = defValue;
                    }
                }
            }
            return result;
        }
        public static float ToFloat(this string str)
        {
            string text = "";
            for (int i = 0; i < str.Length; i++)
            {
                if (char.IsDigit(str[i]) || str[i] == '.')
                {
                    text += str[i].ToString().Trim();
                }
            }
            float result = 0f;
            float.TryParse(text, out result);
            return result;
        }
        public static string ToSqlDateString(this DateTime date)
        {
            string result;
            if (date == default(DateTime))
            {
                result = "null";
            }
            else
            {
                result = string.Format("'{0}'", date.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            return result;
        }
        public static DateTime ToBeginDate(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 0);
        }
        public static DateTime ToEndDate(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, 999);
        }
        public static float ToSingle(this object obj, float defValue)
        {
            float result;
            if (obj == DBNull.Value || obj == null)
            {
                result = defValue;
            }
            else
            {
                string s = obj.ToString(defValue.ToString());
                float num = defValue;
                if (float.TryParse(s, out num))
                {
                    result = num;
                }
                else
                {
                    result = defValue;
                }
            }
            return result;
        }
        public static double ToDouble(this object obj, double defValue)
        {
            double result;
            if (obj == DBNull.Value || obj == null)
            {
                result = defValue;
            }
            else
            {
                string s = obj.ToString(defValue.ToString());
                double num = defValue;
                if (double.TryParse(s, out num))
                {
                    result = num;
                }
                else
                {
                    result = defValue;
                }
            }
            return result;
        }
        public static DateTime ToDateTime(this object obj, DateTime defValue)
        {
            string text = obj.ToString("");
            DateTime result;
            if (text.Trim() == string.Empty)
            {
                result = defValue;
            }
            else
            {
                DateTime dateTime = default(DateTime);
                if (DateTime.TryParse(text, out dateTime))
                {
                    result = dateTime;
                }
                else
                {
                    result = defValue;
                }
            }
            return result;
        }
        public static bool ToBoolean(this object obj, bool defValue)
        {
            bool result;
            if (obj == DBNull.Value || obj == null)
            {
                result = defValue;
            }
            else
            {
                string value = obj.ToString(defValue.ToString());
                bool flag = defValue;
                if (bool.TryParse(value, out flag))
                {
                    result = flag;
                }
                else
                {
                    result = defValue;
                }
            }
            return result;
        }
        public static string ToString(this object obj, string defVal)
        {
            string result;
            if (obj == DBNull.Value || obj == null)
            {
                result = defVal;
            }
            else
            {
                result = obj.ToString();
            }
            return result;
        }
    }
}
