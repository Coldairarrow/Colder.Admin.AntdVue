using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Coldairarrow.Util
{
    public static class GlobalData
    {
        static GlobalData()
        {
            string rootPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            AllFxAssemblies = Directory.GetFiles(rootPath, "*.dll")
                .Where(x => new FileInfo(x).Name.Contains(FXASSEMBLY_PATTERN))
                .Select(x => Assembly.LoadFrom(x))
                .Where(x => !x.IsDynamic)
                .ToList();

            AllFxAssemblies.ForEach(aAssembly =>
            {
                try
                {
                    AllFxTypes.AddRange(aAssembly.GetTypes());
                }
                catch
                {

                }
            });
        }

        /// <summary>
        /// 解决方案程序集匹配名
        /// </summary>
        public const string FXASSEMBLY_PATTERN = "Coldairarrow";

        /// <summary>
        /// 解决方案所有程序集
        /// </summary>
        public static readonly List<Assembly> AllFxAssemblies;

        /// <summary>
        /// 解决方案所有自定义类
        /// </summary>
        public static readonly List<Type> AllFxTypes = new List<Type>();

        /// <summary>
        /// 超级管理员UserIId
        /// </summary>
        public const string ADMINID = "Admin";
    }
}
