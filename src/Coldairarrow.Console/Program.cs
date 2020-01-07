using Autofac;
using Autofac.Extras.DynamicProxy;
using Coldairarrow.Util;
using System;
using System.Linq;

namespace Coldairarrow.Console1
{
    class Program
    {
        static Program()
        {
            var builder = new ContainerBuilder();

            var baseType = typeof(IDependency);
            //自动注入IDependency接口,支持AOP,生命周期为InstancePerDependency
            var diTypes = GlobalData.FxAllTypes
                .Where(x => baseType.IsAssignableFrom(x) && x != baseType)
                .ToArray();
            builder.RegisterTypes(diTypes)
                .AsImplementedInterfaces()
                .PropertiesAutowired()
                .InstancePerDependency()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(Interceptor));

            //AOP
            builder.RegisterType<Interceptor>();

            AutofacHelper.Container = builder.Build();
        }
        static void Main()
        {
            Console.WriteLine("完成");
            Console.WriteLine();
        }
    }
}