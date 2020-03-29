using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml.Serialization;

namespace Nkit.Core
{
    public static class Convertor
    {
        public static byte[] Hex2Bytes(string[] hexs)
        {
            byte[] b = new byte[hexs.Length];
            for (int i = 0; i < hexs.Length; i++)
            {
                b[i] = Convert.ToByte(hexs[i], 16);
            }

            return b;
        }
        public static byte[] Hex2Bytes(string hex)
        {
            string[] hexs = hex.Trim().Split(' ');
            return Hex2Bytes(hexs);
        }
        public static int Hex2Dec(string hex)
        {
            return Convert.ToInt32(hex, 16);
        }
        public static string Hex2String(string[] hex)
        {
            return Bytes2String(Hex2Bytes(hex));
        }
        public static byte[] String2Bytes(string str)
        {
            return Encoding.Default.GetBytes(str);
        }

        public static string Bytes2Hex(byte[] bytes)
        {
            return BitConverter.ToString(bytes).Replace("-", " ");
        }
        public static string[] Bytes2Hexs(byte[] bytes)
        {
            string hex = Bytes2Hex(bytes);
            return hex.Split(' ');
            //List<string> list = new List<string>();
            //foreach (byte b in bytes)
            //{
            //    list.Add(b.ToString("X2"));
            //}
            //return list.ToArray();
        }
        public static string Bytes2AsciiStr(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                sb.Append(b).Append(" ");
            }
            return sb.ToString().Trim();
        }
        public static string Bytes2String(byte[] bytes)
        {
            //StringBuilder sb = new StringBuilder();
            //for (int i = 0; i < bytes.Length; i++)
            //{
            //    ASCIIEncoding coding = new ASCIIEncoding();
            //    char[] ascii = coding.GetChars(bytes);
            //    sb.Append(ascii[i]);
            //}
            //return sb.ToString();
            return Encoding.UTF8.GetString(bytes);
        }
        public static string Dec2Hex(int dec)
        {
            return Convert.ToString(dec, 16);
        }
        public static string Dec2Binary(int dec)
        {
            return Convert.ToString(dec, 2);
        }

        public static byte[] Object2Bytes(object obj)
        {
            var buff = new byte[Marshal.SizeOf(obj)];
            var ptr = Marshal.UnsafeAddrOfPinnedArrayElement(buff, 0);
            Marshal.StructureToPtr(obj, ptr, true);
            return buff;
        }
        public static object Bytes2Object(byte[] buff, Type type)
        {
            var ptr = Marshal.UnsafeAddrOfPinnedArrayElement(buff, 0);
            return Marshal.PtrToStructure(ptr, type);
        }
        public static byte[] ObjectToBytes(object obj)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                return ms.GetBuffer();
            }
        }
        public static object BytesToObject(byte[] Bytes)
        {
            using (MemoryStream ms = new MemoryStream(Bytes))
            {
                IFormatter formatter = new BinaryFormatter();
                return formatter.Deserialize(ms);
            }
        }

        public static long Ip2Long(string ip)
        {
            long result = 0L;
            var arr = ip.Split('.');
            for (int i = 0; i < arr.Length; i++)
            {
                /**
                 * 将每个元素左移（8*i）位，并将结果与前面已经算出的结果做或运算，ip的每
                 * 三位数字都可以用八位二进制表示，将每三位左移（8*i）位后，会将数字向高
                 * 位移动（8*i）位，低位会全部用0补足，与result做或运算后，低位将由result代替，
                 * 最终将四个三位数字组成一个Long类型的整数
                 */
                result |= (long.Parse(arr[i]) << (8 * i));
            }
            return result;
        }

        public static string Long2Ip(long ip)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < 4; i++)
            {
                /**
                 * 将Long整数右移（8*i）位，每次都会将待转化的数字移动到整个Long整数的低八位，
                 * 然后将这个整数与（0xFF）做与运算，（0xFF）的低八位是1，剩余位全都为0，任何数与其做
                 * 与运算都是低八位不变，高位变成0，这样就将每个三位数从Long整数中取出
                 */
                if (i < 3) sb.Append(((ip >> (8 * i)) & 0xff) + ".");
                else sb.Append((ip >> (8 * i)) & 0xff);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 深度拷贝,新对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T DeeperClone<T>(T obj)
        {
            T Ret = default(T);
            if(obj!=null)
            {
                object retval;
                using (MemoryStream ms = new MemoryStream())
                {
                    XmlSerializer xml = new XmlSerializer(typeof(T));
                    xml.Serialize(ms, obj);
                    ms.Seek(0, SeekOrigin.Begin);
                    retval = xml.Deserialize(ms);
                    ms.Close();
                }
                Ret= (T)retval;

                //var ser = new XmlSerializer(typeof(T));
                //var stream = new MemoryStream();
                //ser.Serialize(stream, obj);
                //stream.Seek(0, SeekOrigin.Begin);
                //Ret = (T)ser.Deserialize(stream);
            }
            return Ret;
        }
    }
}
