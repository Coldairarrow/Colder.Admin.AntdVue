using Autofac;
using Autofac.Extras.DynamicProxy;
using Coldairarrow.DataRepository;
using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

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

        static void Main(string[] args)
        {
            //int number = Process.GetCurrentProcess().Threads.Cast<ProcessThread>().Where(x => x.ThreadState == ThreadState.Running).Count();
            //Console.WriteLine($"当前线程数:{number}");
            //int thread1 = 0;
            //int thread2 = 0;
            //ThreadPool.ThreadCount;
            //ThreadPool.GetMaxThreads(out thread1, out thread2);
            Console.WriteLine("完成");
            Console.ReadLine();
        }
    }
}