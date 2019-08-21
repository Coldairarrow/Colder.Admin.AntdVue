using Coldairarrow.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Coldairarrow.Business.Base_SysManage
{
    /// <summary>
    /// 系统菜单管理
    /// </summary>
    public class SystemMenuManage : ISystemMenuManage, IDependency
    {
        #region 构造函数

        public SystemMenuManage(IOperator @operator, IPermissionManage permissionManage)
        {
            _operator = @operator;
            _permissionManage = permissionManage;
        }
        IOperator _operator { get; }
        IPermissionManage _permissionManage { get; }

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static SystemMenuManage()
        {
            InitAllMenu();
        }

        #endregion

        #region 私有成员

        private static List<Menu> _allMenu { get; set; }
        private static void InitAllMenu()
        {
            Action<Menu, XElement> SetMenuProperty = (menu, element) =>
            {
                List<string> exceptProperties = new List<string> { "id", "IsShow", "targetType", "isHeader", "children", "_url" };
                menu.GetType().GetProperties().Where(x => !exceptProperties.Contains(x.Name)).ForEach(aProperty =>
                {
                    aProperty.SetValue(menu, element.Attribute(aProperty.Name)?.Value);
                });
            };

            string filePath = _configFile;
            XElement xe = XElement.Load(filePath);
            List<Menu> menus = new List<Menu>();
            xe.Elements("FirstMenu")?.ForEach(aElement1 =>
            {
                Menu newMenu1 = new Menu();
                menus.Add(newMenu1);
                SetMenuProperty(newMenu1, aElement1);
                newMenu1.children = new List<Menu>();
                aElement1.Elements("SecondMenu")?.ForEach(aElement2 =>
                {
                    Menu newMenu2 = new Menu();
                    newMenu1.children.Add(newMenu2);
                    SetMenuProperty(newMenu2, aElement2);
                    newMenu2.children = new List<Menu>();

                    aElement2.Elements("ThirdMenu")?.ForEach(aElement3 =>
                    {
                        Menu newMenu3 = new Menu();
                        newMenu2.children.Add(newMenu3);
                        SetMenuProperty(newMenu3, aElement3);
                        if (!newMenu3.url.IsNullOrEmpty())
                        {
                            newMenu3.url = GetUrl(newMenu3.url);
                        }
                    });
                });
            });

            if (GlobalSwitch.RunModel == RunModel.LocalTest)
            {
                Menu developMenu = new Menu
                {
                    text = "开发",
                    icon = "glyphicon glyphicon-console",
                    children = new List<Menu>()
                };
                menus.Add(developMenu);
                developMenu.children.Add(new Menu
                {
                    text = "代码生成",
                    icon = "fa fa-circle-o",
                    url = GetUrl("~/Base_SysManage/RapidDevelopment/Index")
                });
                developMenu.children.Add(new Menu
                {
                    text = "数据库连接管理",
                    icon = "fa fa-circle-o",
                    url = GetUrl("~/Base_SysManage/Base_DatabaseLink/Index")
                });
                developMenu.children.Add(new Menu
                {
                    text = "UMEditor Demo",
                    icon = "fa fa-circle-o",
                    url = GetUrl("~/Demo/UMEditor")
                });
                developMenu.children.Add(new Menu
                {
                    text = "下拉搜索",
                    icon = "fa fa-circle-o",
                    url = GetUrl("~/Demo/SelectSearch")
                });
                developMenu.children.Add(new Menu
                {
                    text = "上传文件",
                    icon = "fa fa-circle-o",
                    url = GetUrl("~/Demo/UploadFile")
                });
                developMenu.children.Add(new Menu
                {
                    text = "下载文件",
                    icon = "fa fa-circle-o",
                    url = GetUrl("~/Demo/DownloadFile")
                });
                developMenu.children.Add(new Menu
                {
                    text = "表格树及下拉树",
                    icon = "fa fa-circle-o",
                    url = GetUrl("~/Base_SysManage/Base_Department/Index")
                });
                developMenu.children.Add(new Menu
                {
                    text = "API签名Demo",
                    icon = "fa fa-circle-o",
                    url = GetUrl("~/Demo/ApiSignDemo")
                });
                developMenu.children.Add(new Menu
                {
                    text = "Tab页",
                    icon = "fa fa-circle-o",
                    url = GetUrl("~/Demo/Tab")
                });
                developMenu.children.Add(new Menu
                {
                    text = "详情页",
                    icon = "fa fa-circle-o",
                    url = GetUrl("~/Demo/Details")
                });
            }

            _allMenu = menus;
        }
        private static string _configFile { get => PathHelper.GetAbsolutePath("~/Config/SystemMenu.config"); }
        public static string GetUrl(string virtualUrl) => PathHelper.GetUrl(virtualUrl);

        #endregion

        #region 外部接口

        /// <summary>
        /// 获取系统所有菜单
        /// </summary>
        /// <returns></returns>
        public List<Menu> GetAllSysMenu()
        {
            return _allMenu.DeepClone();
        }

        /// <summary>
        /// 获取用户菜单
        /// </summary>
        /// <returns></returns>
        public List<Menu> GetOperatorMenu()
        {
            List<Menu> resList = GetAllSysMenu();

            if (_operator.IsAdmin())
                return resList;

            var userPermissions = _permissionManage.GetUserPermissionValues(_operator.UserId);
            RemoveNoPermission(resList, userPermissions);

            return resList;

            void RemoveNoPermission(List<Menu> menus, List<string> userPermissionValues)
            {
                for (int i = menus.Count - 1; i >= 0; i--)
                {
                    var theMenu = menus[i];
                    if (!theMenu.Permission.IsNullOrEmpty() && !userPermissions.Contains(theMenu.Permission))
                        menus.RemoveAt(i);
                    else if (theMenu.children?.Count > 0)
                    {
                        RemoveNoPermission(theMenu.children, userPermissions);
                        if (theMenu.children.Count == 0 && theMenu.url.IsNullOrEmpty())
                            menus.RemoveAt(i);
                    }
                }
            }
        }

        #endregion
    }
}