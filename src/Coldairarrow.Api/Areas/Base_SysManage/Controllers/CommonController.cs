using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Coldairarrow.Web.Areas.Base_SysManage.Controllers
{
    [Area("Base_SysManage")]
    public class CommonController : BaseController
    {
        public ActionResult ShowBigImg(string url)
        {
            ViewData["url"] = url;
            return View();
        }

        public ActionResult UploadImg(string fileName, string data)
        {
            var url = ImgHelper.GetImgUrl(data);

            var res = new
            {
                success = true,
                src = url
            };
            return JsonContent(res.ToJson());
        }

        public ActionResult UploadFile(string fileName, string data, string fileType)
        {
            string fileBase64 = GetBase64String(data).Replace(" ", "+");
            byte[] bytes = fileBase64.ToBytes_FromBase64Str();

            string relativeDir = $"/Upload/File/{Guid.NewGuid().ToString()}";
            string absoluteDir = PathHelper.GetAbsolutePath($"~{relativeDir}");
            if (!Directory.Exists(absoluteDir))
                Directory.CreateDirectory(absoluteDir);
            string filePath = Path.Combine(absoluteDir, fileName);
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                using (MemoryStream m = new MemoryStream(bytes))
                {
                    m.WriteTo(fileStream);
                }
            }

            string url = $"{GlobalSwitch.WebRootUrl}{relativeDir}/{fileName}";

            var res = new
            {
                success = true,
                src = url
            };
            return JsonContent(res.ToJson());

            string GetBase64String(string base64Url)
            {
                string parttern = "^.*?base64,(.*?)$";
                var match = Regex.Match(base64Url, parttern);

                return match.Groups[1].ToString();
            }
        }
    }
}