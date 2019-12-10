using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;

namespace Coldairarrow.Util
{
    /// <summary>
    /// 文件压缩帮助类
    /// </summary>
    public class FileZipHelper
    {
        /// <summary>
        /// 压缩一个文件
        /// </summary>
        /// <param name="file">文件信息</param>
        /// <returns></returns>
        public static byte[] ZipFile(FileEntry file)
        {
            return ZipFile(new List<FileEntry> { file });
        }

        /// <summary>
        /// 压缩多个文件
        /// </summary>
        /// <param name="files">文件信息列表</param>
        /// <returns></returns>
        public static byte[] ZipFile(List<FileEntry> files)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (ZipOutputStream zipStream = new ZipOutputStream(ms))
                {
                    files.ForEach(aFile =>
                    {
                        byte[] fileBytes = aFile.FileBytes;
                        ZipEntry entry = new ZipEntry(aFile.FileName)
                        {
                            DateTime = DateTime.Now,
                            IsUnicodeText = true
                        };
                        zipStream.PutNextEntry(entry);
                        zipStream.Write(fileBytes, 0, fileBytes.Length);
                        zipStream.CloseEntry();
                    });

                    zipStream.IsStreamOwner = false;
                    zipStream.Finish();
                    zipStream.Close();
                    ms.Position = 0;

                    return ms.ToArray();
                }
            }
        }
    }
}
