using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text.RegularExpressions;

namespace Coldairarrow.Util
{
    /// <summary>
    /// 图片操作帮助类
    /// </summary>
    public class ImgHelper
    {
        /// <summary>
        /// 从文件获取图片
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        public static Image GetImgFromFile(string fileName)
        {
            return Image.FromFile(fileName);
        }

        /// <summary>
        /// 从base64字符串读入图片
        /// </summary>
        /// <param name="base64">base64字符串</param>
        /// <returns></returns>
        public static Image GetImgFromBase64(string base64)
        {
            byte[] bytes = Convert.FromBase64String(base64);
            MemoryStream memStream = new MemoryStream(bytes);
            Image img = Image.FromStream(memStream);

            return img;
        }

        /// <summary>
        /// 从URL格式的Base64图片获取真正的图片
        /// 即去掉data:image/jpg;base64,这样的格式
        /// </summary>
        /// <param name="base64Url">图片Base64的URL形式</param>
        /// <returns></returns>
        public static Image GetImgFromBase64Url(string base64Url)
        {
            string base64 = GetBase64String(base64Url);

            return GetImgFromBase64(base64);
        }

        /// <summary>
        /// 压缩图片
        /// 注:等比压缩
        /// </summary>
        /// <param name="img">原图片</param>
        /// <param name="width">压缩后宽度</param>
        /// <returns></returns>
        public static Image CompressImg(Image img, int width)
        {
            return CompressImg(img, width, (int)(((double)width) / img.Width * img.Height));
        }

        /// <summary>
        /// 压缩图片
        /// </summary>
        /// <param name="img">原图片</param>
        /// <param name="width">压缩后宽度</param>
        /// <param name="height">压缩后高度</param>
        /// <returns></returns>
        public static Image CompressImg(Image img, int width, int height)
        {
            Bitmap bitmap = new Bitmap(img, width, height);

            return bitmap;
        }

        /// <summary>
        /// 将图片转为base64字符串
        /// 默认使用jpg格式
        /// </summary>
        /// <param name="img">图片对象</param>
        /// <returns></returns>
        public static string ToBase64String(Image img)
        {
            return ToBase64String(img, ImageFormat.Jpeg);
        }

        /// <summary>
        /// 将图片转为base64字符串
        /// 使用指定格式
        /// </summary>
        /// <param name="img">图片对象</param>
        /// <param name="imageFormat">指定格式</param>
        /// <returns></returns>
        public static string ToBase64String(Image img, ImageFormat imageFormat)
        {
            MemoryStream memStream = new MemoryStream();
            img.Save(memStream, imageFormat);
            byte[] bytes = memStream.ToArray();
            string base64 = Convert.ToBase64String(bytes);

            return base64;
        }

        /// <summary>
        /// 将图片转为base64字符串
        /// 默认使用jpg格式,并添加data:image/jpg;base64,前缀
        /// </summary>
        /// <param name="img">图片对象</param>
        /// <returns></returns>
        public static string ToBase64StringUrl(Image img)
        {
            return "data:image/jpg;base64," + ToBase64String(img, ImageFormat.Jpeg);
        }

        /// <summary>
        /// 将图片转为base64字符串
        /// 使用指定格式,并添加data:image/jpg;base64,前缀
        /// </summary>
        /// <param name="img">图片对象</param>
        /// <param name="imageFormat">指定格式</param>
        /// <returns></returns>
        public static string ToBase64StringUrl(Image img, ImageFormat imageFormat)
        {
            string base64 = ToBase64String(img, imageFormat);

            return $"data:image/{imageFormat.ToString().ToLower()};base64,{base64}";
        }

        /// <summary>
        /// 获取真正的图片base64数据
        /// 即去掉data:image/jpg;base64,这样的格式
        /// </summary>
        /// <param name="base64UrlStr">带前缀的base64图片字符串</param>
        /// <returns></returns>
        public static string GetBase64String(string base64UrlStr)
        {
            string parttern = "^(data:image/.*?;base64,).*?$";

            var match = Regex.Match(base64UrlStr, parttern);
            if (match.Groups.Count > 1)
                base64UrlStr = base64UrlStr.Replace(match.Groups[1].ToString(), "");

            return base64UrlStr;
        }

        /// <summary>
        /// 将图片的URL或者Base64字符串转为图片并上传到服务器，返回上传后的完整图片URL
        /// </summary>
        /// <param name="imgBase64OrUrl">URL地址或者Base64字符串</param>
        /// <returns></returns>
        public static string GetImgUrl(string imgBase64OrUrl)
        {
            if (imgBase64OrUrl.Contains("data:image"))
            {
                Image img = ImgHelper.GetImgFromBase64Url(imgBase64OrUrl);
                string fileName = $"{GuidHelper.GenerateKey()}.jpg";

                string dir = Path.Combine(GlobalSwitch.WebRootPath, "Upload", "Img");
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                img.Save(Path.Combine(dir, fileName));

                return $"{GlobalSwitch.WebRootUrl}/Upload/Img/{fileName}";
            }
            else
                return imgBase64OrUrl;
        }
    }
}
