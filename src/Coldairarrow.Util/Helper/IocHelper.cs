using System;
using System.Collections.Concurrent;

namespace Coldairarrow.Util
{
    /// <summary>
    /// Ioc容器帮助类
    /// </summary>
    public class IocHelper
    {
        #region 私有成员

        private string _lastName { get; } = "Last";
        private ConcurrentDictionary<Type, ConcurrentDictionary<string, Type>> _mapping { get; } = new ConcurrentDictionary<Type, ConcurrentDictionary<string, Type>>();

        #endregion

        #region 注册类型

        /// <summary>
        /// 注册类型
        /// </summary>
        /// <typeparam name="TFrom">定义类型</typeparam>
        /// <typeparam name="TTo">实现类型</typeparam>
        public void RegisterType<TFrom, TTo>() where TTo : TFrom
        {
            RegisterType(typeof(TFrom), typeof(TTo), null);
        }

        /// <summary>
        /// 注册类型
        /// </summary>
        /// <param name="name">注册名</param>
        /// <typeparam name="TFrom">定义类型</typeparam>
        /// <typeparam name="TTo">实现类型</typeparam>
        public void RegisterType<TFrom, TTo>(string name) where TTo : TFrom
        {
            RegisterType(typeof(TFrom), typeof(TTo), name);
        }

        /// <summary>
        /// 注册类型
        /// </summary>
        /// <param name="typeFrom">定义类型</param>
        /// <param name="typeTo">实现类型</param>
        /// <param name="name">注册名</param>
        public void RegisterType(Type typeFrom, Type typeTo, string name)
        {
            ConcurrentDictionary<string, Type> typeMapping = null;
            if (!_mapping.ContainsKey(typeFrom))
            {
                typeMapping = new ConcurrentDictionary<string, Type>();
                _mapping[typeFrom] = typeMapping;
            }
            else
                typeMapping = _mapping[typeFrom];

            if (name.IsNullOrEmpty())
                name = _lastName;

            typeMapping[name] = typeTo;
        }

        #endregion

        #region 获取类型

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <returns></returns>
        public T Resolve<T>()
        {
            return (T)Resolve(typeof(T), null);
        }

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="paramters">构造参数</param>
        /// <returns></returns>
        public T Resolve<T>(params object[] paramters)
        {
            return (T)Resolve(typeof(T), null, paramters);
        }

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="name">注册名</param>
        /// <returns></returns>
        public T Resolve<T>(string name)
        {
            return (T)Resolve(typeof(T), name);
        }

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="name">注册名</param>
        /// <param name="paramters">构造参数</param>
        /// <returns></returns>
        public T Resolve<T>(string name, params object[] paramters)
        {
            return (T)Resolve(typeof(T), name, paramters);
        }

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="typeFrom">对象类型</param>
        /// <param name="name">注册名</param>
        /// <param name="paramters">构造参数</param>
        /// <returns></returns>
        public object Resolve(Type typeFrom, string name, params object[] paramters)
        {
            if (!_mapping.ContainsKey(typeFrom))
                throw new Exception("该类型未注册！");
            var typeMapping = _mapping[typeFrom];
            if (name.IsNullOrEmpty())
                name = _lastName;
            if (!typeMapping.ContainsKey(name))
                throw new Exception("该类型实现名为注册！");

            return Activator.CreateInstance(typeMapping[name], paramters);
        }

        #endregion
    }
}
