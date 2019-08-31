using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace Coldairarrow.Web
{
    public class UEditorController : BaseController
    {
        public ActionResult UploadFile()
        {
            var files = Request.Form.Files;
            string callback = Request.Query["callback"];
            string editorId = Request.Query["editorid"];
            if (files != null && files.Count > 0)
            {
                var file = files[0];
                string contentPath = GlobalSwitch.WebRootPath;
                string fileDir = Path.Combine(contentPath, "Upload","Img");
                if (!Directory.Exists(fileDir))
                {
                    Directory.CreateDirectory(fileDir);
                }
                string fileExt = Path.GetExtension(file.FileName);
                string fileName = Guid.NewGuid().ToString() + fileExt;
                string filePath = Path.Combine(fileDir, fileName);
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fs);
                }
                var fileInfo = GetResponseUploadInfo($"{GlobalSwitch.WebRootUrl}/Upload/Img/{fileName}", file.FileName,
                    Path.GetFileName(filePath), file.Length, fileExt);

                return Content(fileInfo.ToJson(), "text/html; charset=utf-8");
            }
            return NoContent();
        }

        private object GetResponseUploadInfo(string url, string originalName, string name, long size, string type, string state = "SUCCESS")
        {
            var res = new
            {
                state,
                url,
                originalName,
                name = Path.GetFileName(url),
                size,
                type = Path.GetExtension(originalName)
            };

            return res;
        }
    }
}