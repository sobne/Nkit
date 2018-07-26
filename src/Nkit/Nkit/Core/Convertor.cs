using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

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
    }
}
