using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Coldairarrow.Util
{
    /// <summary>
    /// 拓展类
    /// </summary>
    public static partial class Extention
    {
        /// <summary>
        /// 使用AutoMapper自动映射拥有MapAttribute的类
        /// </summary>
        /// <param name="services">服务集合</param>
        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            List<(Type from, Type[] targets)> maps = new List<(Type from, Type[] targets)>();

            maps.AddRange(GlobalData.AllFxTypes.Where(x => x.GetCustomAttribute<MapAttribute>() != null)
                .Select(x => (x, x.GetCustomAttribute<MapAttribute>().TargetTypes)));

            var configuration = new MapperConfiguration(cfg =>
            {
                maps.ForEach(aMap =>
                {
                    aMap.targets.ForEach(aTarget =>
                    {
                        cfg.CreateMap(aMap.from, aTarget);
                        cfg.CreateMap(aTarget, aMap.from);
                    });
                });
            });

            services.AddSingleton(configuration.CreateMapper());

            return services;
        }

        /// <summary>
        /// 自动注入拥有ITransientDependency,IScopeDependency或ISingletonDependency的类
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <returns></returns>
        public static IServiceCollection AddFxServices(this IServiceCollection services)
        {
            Dictionary<Type, string> map = new Dictionary<Type, string>
            {
                { typeof(ITransientDependency),"AddTransient"},
                { typeof(IScopeDependency),"AddScoped"},
                { typeof(ISingletonDependency),"AddSingleton"}
            };

            GlobalData.AllFxTypes.ForEach(aType =>
            {
                map.ForEach(aMap =>
                {
                    var theDependency = aMap.Key;
                    if (theDependency.IsAssignableFrom(aType) && theDependency != aType && !aType.IsAbstract && aType.IsClass)
                    {
                        var interfaces = GlobalData.AllFxTypes.Where(x => x.IsAssignableFrom(aType) && x.IsInterface && x != theDependency).ToList();
                        //有接口则注入接口
                        if (interfaces.Count > 0)
                        {
                            var method = GetMethodInfo(aMap.Value, 2);

                            interfaces.ForEach(aInterface =>
                            {
                                method.Invoke(null, new object[] { services, aInterface, aType });
                            });
                        }
                        //无接口则注入自己
                        else
                        {
                            var method = GetMethodInfo(aMap.Value, 1);
                            method.Invoke(null, new object[] { services, aType });
                        }
                    }
                });
            });

            return services;

            MethodInfo GetMethodInfo(string name, int count)
            {
                List<Type> types = new List<Type>();
                LoopHelper.Loop(count, () => types.Add(typeof(Type)));

                var method = typeof(ServiceCollectionServiceExtensions)
                    .GetMethods(BindingFlags.Public | BindingFlags.Static)
                    .Where(x => x.Name == name
                        && !x.IsGenericMethod
                        && x.GetParameters().Count() == count + 1)
                    .FirstOrDefault();

                return method;
            }
        }
    }
}
