using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Coldairarrow.Util
{
    public static partial class Extention
    {
        /// <summary>
        /// 将流Stream转为byte数组
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static byte[] ReadToBytes(this Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            stream.Seek(0, SeekOrigin.Begin);

            return bytes;
        }

        /// <summary>
        /// 将流读为字符串
        /// 注：默认使用UTF-8编码
        /// </summary>
        /// <param name="stream">流</param>
        /// <param name="encoding">指定编码</param>
        /// <returns></returns>
        public static string ReadToString(this Stream stream, Encoding encoding = null)
        {
            if (encoding == null)
                encoding = Encoding.UTF8;

            if (stream.CanSeek)
            {
                stream.Seek(0, SeekOrigin.Begin);
            }

            string resStr = string.Empty;
            resStr = new StreamReader(stream, encoding).ReadToEnd();

            if (stream.CanSeek)
            {
                stream.Seek(0, SeekOrigin.Begin);
            }

            return resStr;
        }

        /// <summary>
        /// 将流读为字符串
        /// 注：默认使用UTF-8编码
        /// </summary>
        /// <param name="stream">流</param>
        /// <param name="encoding">指定编码</param>
        /// <returns></returns>
        public static async Task<string> ReadToStringAsync(this Stream stream, Encoding encoding = null)
        {
            if (encoding == null)
                encoding = Encoding.UTF8;

            if (stream.CanSeek)
            {
                stream.Seek(0, SeekOrigin.Begin);
            }

            string resStr = string.Empty;
            resStr = await new StreamReader(stream, encoding).ReadToEndAsync();

            if (stream.CanSeek)
            {
                stream.Seek(0, SeekOrigin.Begin);
            }

            return resStr;
        }
    }
}
