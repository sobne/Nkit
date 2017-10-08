/* License
 * This work is licensed under the Creative Commons Attribution 2.5 License
 * http://creativecommons.org/licenses/by/2.5/

 * You are free:
 *     to copy, distribute, display, and perform the work
 *     to make derivative works
 *     to make commercial use of the work
 * 
 * Under the following conditions:
 *     You must attribute the work in the manner specified by the author or licensor:
 *          - If you find this component useful a email to [sobne.cn@gmail.com] would be appreciated.
 *     For any reuse or distribution, you must make clear to others the license terms of this work.
 *     Any of these conditions can be waived if you get permission from the copyright holder.
 * 
 * Copyright sobne.cn All Rights Reserved.
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace Nkit
{
    public class SerializationHelper
    {
        #region xml
        // object -> xml string
        public static string SaveXmlString(object obj) { return SaveXmlString(obj, null); }
        public static string SaveXmlString(object obj, XmlAttributeOverrides overrides) { return SaveXmlString(obj, obj.GetType(), overrides); }
        public static string SaveXmlString(object obj, Type type, XmlAttributeOverrides overrides)
        {
            MemoryStream stream = new MemoryStream();
            using (StreamWriter writer = new StreamWriter(stream))
            {
                System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(type, overrides);
                xs.Serialize(writer, obj);
                writer.Close();
            }
            string str = UnicodeEncoding.UTF8.GetString(stream.GetBuffer());
            return str;
        }

        // xml string -> obj
        public static object LoadXmlString(string xmlString, Type type) { return LoadXmlString(xmlString, type, null); }
        public static object LoadXmlString(string xmlString, Type type, XmlAttributeOverrides overrides)
        {
            byte[] bytes = UnicodeEncoding.UTF8.GetBytes(xmlString);
            MemoryStream stream = new MemoryStream(bytes);
            using (StreamReader reader = new StreamReader(stream))
            {
                System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(type, overrides);
                object obj = xs.Deserialize(reader);
                reader.Close();
                return obj;
            }
        }
        #endregion

        #region json
        /// <summary>
        /// 将对象序列化Json格式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SerializeJson<T>(T obj)
        {
            DataContractJsonSerializer json = new DataContractJsonSerializer(obj.GetType());
            using (MemoryStream stream = new MemoryStream())
            {
                json.WriteObject(stream, obj);
                return Encoding.UTF8.GetString(stream.ToArray());
            }
        }
        /// <summary>
        /// 将json格式串反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T DeSerializeJson<T>(string json)
        {
            T obj = Activator.CreateInstance<T>();
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
                return (T)serializer.ReadObject(ms);
            }
        }
        #endregion
    }
}
