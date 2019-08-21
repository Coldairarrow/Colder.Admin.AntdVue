using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Coldairarrow.Util
{
    /// <summary>
    /// 拓展类
    /// </summary>
    public static partial class Extention
    {
        /// <summary>
        /// byte[]转string
        /// 注：默认使用UTF8编码
        /// </summary>
        /// <param name="bytes">byte[]数组</param>
        /// <returns></returns>
        public static string ToString(this byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }

        /// <summary>
        /// byte[]转string
        /// </summary>
        /// <param name="bytes">byte[]数组</param>
        /// <param name="encoding">指定编码</param>
        /// <returns></returns>
        public static string ToString(this byte[] bytes, Encoding encoding)
        {
            return encoding.GetString(bytes);
        }

        /// <summary>
        /// 将byte[]转为Base64字符串
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <returns></returns>
        public static string ToBase64String(this byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// 转为二进制字符串
        /// </summary>
        /// <param name="aByte">字节</param>
        /// <returns></returns>
        public static string ToBinString(this byte aByte)
        {
            return new byte[] { aByte }.ToBinString();
        }

        /// <summary>
        /// 转为二进制字符串
        /// 注:一个字节转为8位二进制
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <returns></returns>
        public static string ToBinString(this byte[] bytes)
        {
            StringBuilder builder = new StringBuilder();
            foreach (var aByte in bytes)
            {
                builder.Append(Convert.ToString(aByte, 2).PadLeft(8, '0'));
            }

            return builder.ToString();
        }

        /// <summary>
        /// Byte数组转为对应的16进制字符串
        /// </summary>
        /// <param name="bytes">Byte数组</param>
        /// <returns></returns>
        public static string To0XString(this byte[] bytes)
        {
            StringBuilder resStr = new StringBuilder();
            bytes.ToList().ForEach(aByte =>
            {
                resStr.Append(aByte.ToString("x2"));
            });

            return resStr.ToString();
        }

        /// <summary>
        /// Byte数组转为对应的16进制字符串
        /// </summary>
        /// <param name="aByte">一个Byte</param>
        /// <returns></returns>
        public static string To0XString(this byte aByte)
        {
            return new byte[] { aByte }.To0XString();
        }

        /// <summary>
        /// 转为ASCII字符串（一个字节对应一个字符）
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <returns></returns>
        public static string ToASCIIString(this byte[] bytes)
        {
            StringBuilder stringBuilder = new StringBuilder();
            bytes.ToList().ForEach(aByte =>
            {
                stringBuilder.Append((char)aByte);
            });

            return stringBuilder.ToString();
        }

        /// <summary>
        /// 转为ASCII字符串（一个字节对应一个字符）
        /// </summary>
        /// <param name="aByte">字节数组</param>
        /// <returns></returns>
        public static string ToASCIIString(this byte aByte)
        {
            return new byte[] { aByte }.ToASCIIString();
        }

        /// <summary>
        /// 获取异或值
        /// 注：每个字节异或
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <returns></returns>
        public static byte GetXOR(this byte[] bytes)
        {
            int value = bytes[0];
            for (int i = 1; i < bytes.Length; i++)
            {
                value = value ^ bytes[i];
            }

            return (byte)value;
        }

        /// <summary>
        /// 将字节数组转为Int类型
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <returns></returns>
        public static int ToInt(this byte[] bytes)
        {
            int num = 0;
            for (int i = 0; i < bytes.Length; i++)
            {
                num += bytes[i] * ((int)Math.Pow(256, bytes.Length - i - 1));
            }

            return num;
        }

        /// <summary> 
        /// 将一个序列化后的byte[]数组还原
        /// </summary>
        /// <param name="bytes"></param>         
        /// <returns></returns>
        public static object ToObject(this byte[] bytes)
        {
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                IFormatter formatter = new BinaryFormatter();
                return formatter.Deserialize(ms);
            }
        }

        /// <summary>
        /// 将一个序列化后的byte[]数组还原
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="bytes">字节数组</param>
        /// <returns></returns>
        public static T ToObject<T>(this byte[] bytes)
        {
            return (T)ToObject(bytes);
        }

        /// <summary>
        /// 将字节数组保存为文件
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <param name="path">文件完成路径</param>
        public static void ToFile(this byte[] bytes, string path)
        {
            using (var fs = File.OpenWrite(path))
            {
                fs.Write(bytes, 0, bytes.Length);
            }
        }
    }
}
