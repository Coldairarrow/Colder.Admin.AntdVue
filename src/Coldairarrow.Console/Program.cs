using Coldairarrow.DataRepository;
using Coldairarrow.Entity.Base_Manage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Coldairarrow.Util;
using System.Data;
using Autofac;
using Autofac.Extras.DynamicProxy;

namespace Coldairarrow.Console1
{
    class Program
    {
        static Program()
        {
            var builder = new ContainerBuilder();

            var baseType = typeof(IDependency);
            var baseTypeCircle = typeof(ICircleDependency);

            //Coldairarrow相关程序集
            var assemblys = Assembly.GetEntryAssembly().GetReferencedAssemblies()
                .Select(Assembly.Load)
                .Cast<Assembly>()
                .Where(x => x.FullName.Contains("Coldairarrow")).ToList();

            //自动注入IDependency接口,支持AOP
            builder.RegisterAssemblyTypes(assemblys.ToArray())
                .Where(x => baseType.IsAssignableFrom(x) && x != baseType)
                .AsImplementedInterfaces()
                .PropertiesAutowired()
                .InstancePerDependency()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(Interceptor));

            //自动注入ICircleDependency接口,循环依赖注入,不支持AOP
            builder.RegisterAssemblyTypes(assemblys.ToArray())
                .Where(x => baseTypeCircle.IsAssignableFrom(x) && x != baseTypeCircle)
                .AsImplementedInterfaces()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
                .InstancePerLifetimeScope();

            //AOP
            builder.RegisterType<Interceptor>();

            AutofacHelper.Container = builder.Build();
        }

        static void Main(string[] args)
        {
            string payload = "{\"data\":{\"ip\":\"115.215.229.234\",\"lid\":\"39e5a88b33c1da3d82ba55ba9c4f21735ad8\",\"ret_code\":\"300\",\"server_time\":\"1568817367327\",\"ver\":\"1.0\"},\"key_id\":\"7\",\"sign\":\"601f67e6\"}";
            string secret = Guid.NewGuid().ToString();
            string token = JWTHelper.GetToken(payload, secret);

            Console.WriteLine(payload);
            Console.WriteLine(secret);
            Console.WriteLine(token);
            Console.WriteLine(JWTHelper.CheckToken(token, secret));
            Console.WriteLine($"数据:{JWTHelper.GetPayload(token).ToJson()}");
            Console.WriteLine("完成");
            Console.ReadLine();
        }
    }
}