using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;

namespace Coldairarrow.Api.Controllers.Base_Manage
{
    [Route("/Base_Manage/[controller]/[action]")]
    public class UploadController : BaseController
    {
        [HttpPost]
        public IActionResult UploadFileByForm()
        {
            var file = Request.Form.Files.FirstOrDefault();
            if (file == null)
                return JsonContent(new { status = "error" }.ToJson());

            string path = $"/Upload/{Guid.NewGuid().ToString("N")}/{file.FileName}";
            string physicPath = PathHelper.GetAbsolutePath($"~{path}");
            string dir = Path.GetDirectoryName(physicPath);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            using (FileStream fs = new FileStream(physicPath, FileMode.Create))
            {
                file.CopyTo(fs);
            }

            string url = $"{GlobalSwitch.WebRootUrl}{path}";
            var res = new
            {
                name = file.FileName,
                status = "done",
                thumbUrl = url,
                url = url
            };

            return JsonContent(res.ToJson());
        }
    }
}
