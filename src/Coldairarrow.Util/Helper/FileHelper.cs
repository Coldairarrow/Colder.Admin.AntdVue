using System;
using System.IO;
using System.Text;

namespace Coldairarrow.Util
{
    /// <summary>
    /// 文件操作帮助类
    /// </summary>
    public class FileHelper
    {
        #region 读操作

        /// <summary>
        /// 判断文件是否存在
        /// </summary>
        /// <param name="path">文件目录</param>
        /// <returns></returns>
        public static bool Exists(string path)
        {
            return File.Exists(path);
        }

        /// <summary>
        /// 获取当前程序根目录
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentDir()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        #endregion

        #region 写操作

        /// <summary>
        /// 输出字符串到文件
        /// 注：使用系统默认编码;若文件不存在则创建新的,若存在则覆盖
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="path">文件路径</param>
        public static void WriteTxt(string content, string path)
        {
            WriteTxt(content, path, null, null);
        }

        /// <summary>
        /// 输出字符串到文件
        /// 注：使用自定义编码;若文件不存在则创建新的,若存在则覆盖
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="path">文件路径</param>
        /// <param name="encoding">编码</param>
        public static void WriteTxt(string content, string path, Encoding encoding)
        {
            WriteTxt(content, path, encoding, null);
        }

        /// <summary>
        /// 输出字符串到文件
        /// 注：使用自定义模式,使用UTF-8编码
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="path">文件路径</param>
        /// <param name="fileModel">输出方法</param>
        public static void WriteTxt(string content, string path, FileMode fileModel)
        {
            WriteTxt(content, path, Encoding.UTF8, fileModel);
        }

        /// <summary>
        /// 输出字符串到文件
        /// 注：使用自定义编码以及写入模式
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="path">文件路径</param>
        /// <param name="encoding">字符编码</param>
        /// <param name="fileModel">写入模式</param>
        public static void WriteTxt(string content, string path, Encoding encoding, FileMode fileModel)
        {
            WriteTxt(content, path, encoding, (FileMode?)fileModel);
        }

        /// <summary>
        /// 输出字符串到文件
        /// 注：使用自定义编码以及写入模式
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="path">文件路径</param>
        /// <param name="encoding">字符编码</param>
        /// <param name="fileModel">写入模式</param>
        private static void WriteTxt(string content, string path, Encoding encoding, FileMode? fileModel)
        {
            encoding = encoding ?? Encoding.UTF8;
            fileModel = fileModel ?? FileMode.Create;
            string dir = Path.GetDirectoryName(path);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            using (FileStream fileStream = new FileStream(path, fileModel.Value))
            {
                using (StreamWriter streamWriter = new StreamWriter(fileStream, encoding))
                {
                    streamWriter.Write(content);
                    streamWriter.Flush();
                }
            }
        }

        /// <summary>
        /// 输出日志到指定文件
        /// </summary>
        /// <param name="msg">日志消息</param>
        /// <param name="path">日志文件位置（默认为D:\测试\a.log）</param>
        public static void WriteLog(string msg, string path = @"Log.txt")
        {
            string content = $"{DateTime.Now.ToCstTime().ToString("yyyy-MM-dd HH:mm:ss")}:{msg}";

            WriteTxt(content, $"{GetCurrentDir()}{content}");
        }

        #endregion
    }
}
