using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Data;
using System.Text;

namespace Nkit.Web
{
    public class JsonUtils
    {
        public JsonUtils()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// JavaScriptSerializer().Serialize(object)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Serialize<T>(T data)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(data);
        }
        /// <summary>
        ///  JavaScriptSerializer().Deserialize<T>(json)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string json)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Deserialize<T>(json);
        }
        //public static string ToJson(this Object obj)
        //{
        //    JavaScriptSerializer jss = new JavaScriptSerializer();
        //    return jss.Serialize(obj);
        //}

        public static string toJson(DataTable dt)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            foreach (DataRow dr in dt.Rows)
            {
                sb.Append(toJson(dt, dr));
                sb.Append(",");
            }
            if (sb.Length > 1)
                sb.Remove(sb.Length - 1, 1);
            sb.Append("]");
            return sb.ToString();
        }
        public static string toJson(DataTable dt, DataRow dr)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            foreach (DataColumn dc in dt.Columns)
            {
                sb.Append("\"");
                sb.Append(dc.ColumnName);
                sb.Append("\":\"");
                sb.Append(JsonCharFilter(dr[dc.ColumnName].ToString()));
                sb.Append("\",");
            }
            if (sb.Length > 1)
                sb.Remove(sb.Length - 1, 1);
            sb.Append("}");
            return sb.ToString();
        }
        /// <summary>
        /// Json特符字符过滤
        /// </summary>
        /// <param name="sourceStr">要过滤的源字符串</param>
        /// <returns>返回过滤的字符串</returns>
        public static string JsonCharFilter(string s)
        {
            s = s.Replace("\\", "\\\\");
            s = s.Replace("\b", "\\\b");
            s = s.Replace("\t", "\\\t");
            s = s.Replace("\n", "\\\n");
            s = s.Replace("\n", "\\\n");
            s = s.Replace("\f", "\\\f");
            s = s.Replace("\r", "\\\r");
            s = s.Replace("\"", "\\\"");
            s = s.Replace("[", "");
            s = s.Replace("]", "");
            return s;
        }
    }
}
