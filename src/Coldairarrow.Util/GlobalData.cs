using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Coldairarrow.Util
{
    public static class GlobalData
    {
        public const string FXASSEMBLY = "Coldairarrow";
        public const string ADMINID = "Admin";

        private static List<Type> _allFxTypes;
        private static object _lock = new object();

        /// <summary>
        /// 框架所有自定义类
        /// </summary>
        public static List<Type> AllFxTypes
        {
            get
            {
                if (_allFxTypes == null)
                {
                    lock (_lock)
                    {
                        if (_allFxTypes == null)
                        {
                            _allFxTypes = new List<Type>();

                            Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
                                .Where(x => x.Contains(FXASSEMBLY))
                                .Select(x => Assembly.LoadFrom(x))
                                .Where(x => !x.IsDynamic)
                                .ForEach(aAssembly =>
                                {
                                    try
                                    {
                                        _allFxTypes.AddRange(aAssembly.GetTypes());
                                    }
                                    catch
                                    {

                                    }
                                });
                        }
                    }
                }

                return _allFxTypes;
            }
        }
    }
}
