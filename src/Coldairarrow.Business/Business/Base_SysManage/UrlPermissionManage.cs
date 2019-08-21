using Coldairarrow.Util;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Coldairarrow.Business.Base_SysManage
{
    public class UrlPermissionManage : IUrlPermissionManage, IDependency
    {
        #region 构造函数

        static UrlPermissionManage()
        {
            InitAllUrlPermissions();
        }

        #endregion

        #region 私有成员

        private static string _configFile { get; } = "~/Config/UrlPermission.config";
        private static List<ActionPermission> _allUrlPermissions { get; set; }
        private static void InitAllUrlPermissions()
        {
            List<ActionPermission> resList = new List<ActionPermission>();
            string filePath = PathHelper.GetAbsolutePath(_configFile);
            XElement xe = XElement.Load(filePath);
            xe.Elements("action")?.ForEach(aUrl =>
            {
                ActionPermission newUrl = new ActionPermission
                {
                    Url = aUrl.Attribute("url")?.Value,
                    PermissionValue = aUrl.Attribute("needPermission")?.Value
                };
                if (!newUrl.Url.IsNullOrEmpty() && !newUrl.PermissionValue.IsNullOrEmpty())
                    resList.Add(newUrl);
            });

            _allUrlPermissions = resList;
        }

        #endregion

        #region 外部接口

        /// <summary>
        /// 获取所有URL需要的权限
        /// </summary>
        /// <returns></returns>
        public List<ActionPermission> GetAllUrlPermissions()
        {
            return _allUrlPermissions.DeepClone();
        }

        #endregion
    }
}