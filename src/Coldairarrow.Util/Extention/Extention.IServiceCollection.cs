using AutoMapper;
using Castle.DynamicProxy;
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
        private static readonly ProxyGenerator _generator = new ProxyGenerator();

        /// <summary>
        /// 使用AutoMapper自动映射拥有MapAttribute的类
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="configure">自定义配置</param>
        public static IServiceCollection AddAutoMapper(this IServiceCollection services, Action<IMapperConfigurationExpression> configure = null)
        {
            List<(Type from, Type[] targets)> maps = new List<(Type from, Type[] targets)>();

            maps.AddRange(GlobalData.AllFxTypes.Where(x => x.GetCustomAttribute<MapAttribute>() != null)
                .Select(x => (x, x.GetCustomAttribute<MapAttribute>().TargetTypes)));

            var configuration = new MapperConfiguration(cfg =>
            {
                maps.ForEach(aMap =>
                {
                    aMap.targets.ToList().ForEach(aTarget =>
                    {
                        cfg.CreateMap(aMap.from, aTarget).IgnoreAllNonExisting(aMap.from, aTarget).ReverseMap();
                    });
                });

                cfg.AddMaps(GlobalData.AllFxAssemblies);

                //自定义映射
                configure?.Invoke(cfg);
            });

#if DEBUG
            //只在Debug时检查配置
            configuration.AssertConfigurationIsValid();
#endif
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
            Dictionary<Type, ServiceLifetime> lifeTimeMap = new Dictionary<Type, ServiceLifetime>
            {
                { typeof(ITransientDependency), ServiceLifetime.Transient},
                { typeof(IScopedDependency),ServiceLifetime.Scoped},
                { typeof(ISingletonDependency),ServiceLifetime.Singleton}
            };

            GlobalData.AllFxTypes.ForEach(aType =>
            {
                lifeTimeMap.ToList().ForEach(aMap =>
                {
                    var theDependency = aMap.Key;
                    if (theDependency.IsAssignableFrom(aType) && theDependency != aType && !aType.IsAbstract && aType.IsClass)
                    {
                        //注入实现
                        services.Add(new ServiceDescriptor(aType, aType, aMap.Value));

                        var interfaces = GlobalData.AllFxTypes.Where(x => x.IsAssignableFrom(aType) && x.IsInterface && x != theDependency).ToList();
                        //有接口则注入接口
                        if (interfaces.Count > 0)
                        {
                            interfaces.ForEach(aInterface =>
                            {
                                //注入AOP
                                services.Add(new ServiceDescriptor(aInterface, serviceProvider =>
                                {
                                    CastleInterceptor castleInterceptor = new CastleInterceptor(serviceProvider);

                                    return _generator.CreateInterfaceProxyWithTarget(aInterface, serviceProvider.GetService(aType), castleInterceptor);
                                }, aMap.Value));
                            });
                        }
                        //无接口则注入自己
                        else
                        {
                            services.Add(new ServiceDescriptor(aType, aType, aMap.Value));
                        }
                    }
                });
            });

            return services;
        }

        /// <summary>
        /// 忽略所有不匹配的属性。
        /// </summary>
        /// <param name="expression">配置表达式</param>
        /// <param name="from">源类型</param>
        /// <param name="to">目标类型</param>
        /// <returns></returns>
        public static IMappingExpression IgnoreAllNonExisting(this IMappingExpression expression, Type from, Type to)
        {
            var flags = BindingFlags.Public | BindingFlags.Instance;
            to.GetProperties(flags).Where(x => from.GetProperty(x.Name, flags) == null).ForEach(aProperty =>
            {
                expression.ForMember(aProperty.Name, opt => opt.Ignore());
            });

            return expression;
        }

        /// <summary>
        /// 忽略所有不匹配的属性。
        /// </summary>
        /// <typeparam name="TSource">源类型</typeparam>
        /// <typeparam name="TDestination">目标类型</typeparam>
        /// <param name="expression">配置表达式</param>
        /// <returns></returns>
        public static IMappingExpression<TSource, TDestination> IgnoreAllNonExisting<TSource, TDestination>(this IMappingExpression<TSource, TDestination> expression)
        {
            Type from = typeof(TSource);
            Type to = typeof(TDestination);
            var flags = BindingFlags.Public | BindingFlags.Instance;
            to.GetProperties(flags).Where(x => from.GetProperty(x.Name, flags) == null).ForEach(aProperty =>
            {
                expression.ForMember(aProperty.Name, opt => opt.Ignore());
            });

            return expression;
        }
    }
}
