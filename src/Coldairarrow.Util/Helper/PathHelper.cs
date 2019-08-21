using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using System.IO;

namespace Coldairarrow.Util
{
    public static class PathHelper
    {
        /// <summary>
        /// 获取Url
        /// </summary>
        /// <param name="virtualUrl">虚拟Url</param>
        /// <returns></returns>
        public static string GetUrl(string virtualUrl)
        {
            if (!virtualUrl.IsNullOrEmpty())
            {
                UrlHelper urlHelper = new UrlHelper(AutofacHelper.GetScopeService<IActionContextAccessor>().ActionContext);

                return urlHelper.Content(virtualUrl);
            }
            else
                return null;
        }

        /// <summary>
        /// 获取绝对路径
        /// </summary>
        /// <param name="virtualPath">虚拟路径</param>
        /// <returns></returns>
        public static string GetAbsolutePath(string virtualPath)
        {
            string path = virtualPath.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
            if (path[0] == '~')
                path = path.Remove(0, 2);
            string rootPath = AutofacHelper.GetScopeService<IHostingEnvironment>().WebRootPath;

            return Path.Combine(rootPath, path);
        }
    }
}
