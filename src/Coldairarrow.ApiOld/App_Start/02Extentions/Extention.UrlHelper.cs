using Microsoft.AspNetCore.Http;
using System.IO;

namespace Microsoft.AspNetCore.Mvc
{
    public static partial class Extention
    {
        /// <summary>
        /// 获取最新的s文件或css文件
        /// 注：解决缓存问题，只有文件修改后才会获取最新版
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="scriptVirtualPath"></param>
        /// <returns></returns>
        public static string Scrpit(this IUrlHelper helper, string scriptVirtualPath)
        {
            string filePath = helper.ActionContext.HttpContext.MapPath(scriptVirtualPath);
            FileInfo fileInfo = new FileInfo(filePath);
            var lastTime = fileInfo.LastWriteTime.GetHashCode();
            return helper.Content($"{scriptVirtualPath}?_v={lastTime}");
        }
    }
}