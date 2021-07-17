using Coldairarrow.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NSwag.Annotations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Coldairarrow.Api.Controllers.Base_Manage
{
    [Route("/Base_Manage/[controller]/[action]")]
    [OpenApiTag("上传")]
    public class UploadController : BaseApiController
    {
        readonly IConfiguration _configuration;
        public UploadController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// 单文件上传
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        [HttpPost]
        //[AllowAnonymous]
        public IActionResult UploadFileByForm(IFormFile formFile)
        {
            var file = formFile;
            if (file == null)
            {
                return JsonContent(new { status = "error" }.ToJson());
            }

            string path = $"/Upload/{Guid.NewGuid().ToString("N")}/{file.FileName}";
            string physicPath = GetAbsolutePath($"~{path}");
            string dir = Path.GetDirectoryName(physicPath);

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            using (FileStream fs = new FileStream(physicPath, FileMode.Create))
            {
                file.CopyTo(fs);
            }

            string url = $"{_configuration["WebRootUrl"]}{path}";
            var res = new
            {
                name = file.FileName,
                status = "done",
                thumbUrl = url,
                url = url
            };

            return JsonContent(res.ToJson());
        }

        /// <summary>
        /// 单文件上传
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult UploadFileByFormFiles()
        {
            var file = Request.Form.Files.FirstOrDefault();
            if (file == null)
            {
                return JsonContent(new { status = "error" }.ToJson());
            }

            string path = $"/Upload/{Guid.NewGuid().ToString("N")}/{file.FileName}";
            string physicPath = GetAbsolutePath($"~{path}");
            string dir = Path.GetDirectoryName(physicPath);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            using (FileStream fs = new FileStream(physicPath, FileMode.Create))
            {
                file.CopyTo(fs);
            }

            string url = $"{_configuration["WebRootUrl"]}{path}";

            var res = new
            {
                name = file.FileName,
                status = "done",
                thumbUrl = url,
                url = url
            };

            return JsonContent(res.ToJson());
        }

        /// <summary>
        /// 单文件上传
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult UploadFileByFormFilesForRich()
        {
            var file = Request.Form.Files.FirstOrDefault();
            if (file == null)
            {
                return JsonContent(new { status = "error" }.ToJson());
            }

            string path = $"/Upload/{Guid.NewGuid().ToString("N")}/{file.FileName}";
            string physicPath = GetAbsolutePath($"~{path}");
            string dir = Path.GetDirectoryName(physicPath);

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            using (FileStream fs = new FileStream(physicPath, FileMode.Create))
            {
                file.CopyTo(fs);
            }

            string url = $"{_configuration["WebRootUrl"]}{path}";

            var res = new
            {
                name = file.FileName,
                status = "done",
                thumbUrl = url,
                url = url
            };

            return JsonContent(res.ToJson());
        }

        /// <summary>
        /// 多文件上传
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult UploadMultiFileByFormFilesForRich()
        {
            var files = Request.Form.Files;

            if (files == null || files.Count == 0)
            {
                return JsonContent(new { status = "error" }.ToJson());
            }

            var resultList = new List<object>();

            foreach (var file in files)
            {
                if (file == null)
                {
                    return JsonContent(new { status = "error" }.ToJson());
                }

                string path = $"/Upload/{Guid.NewGuid().ToString("N")}/{file.FileName}";
                string physicPath = GetAbsolutePath($"~{path}");
                string dir = Path.GetDirectoryName(physicPath);
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                using (FileStream fs = new FileStream(physicPath, FileMode.Create))
                {
                    file.CopyTo(fs);
                }

                string url = $"{_configuration["WebRootUrl"]}{path}";

                var res = new
                {
                    name = file.FileName,
                    status = "done",
                    thumbUrl = url,
                    url = url
                };

                resultList.Add(res);
            }

            return JsonContent(resultList.ToJson());
        }
    }

}
