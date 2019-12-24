using Autofac;
using Autofac.Extras.DynamicProxy;
using Coldairarrow.Util;
using System;
using System.IO;
using System.Linq;
using System.Text;

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
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            string path = $"D:\\文档\\0软件项目\\GitHub\\Colder.Admin.AntdVue";
            var files = Directory.GetFiles(path);
            files.ForEach(aFile =>
            {
                var oldEncoding = new IdentifyEncoding().GetEncodingString(new FileInfo(aFile));
                var content = File.ReadAllText(aFile, Encoding.GetEncoding(oldEncoding));
                File.WriteAllText(aFile, content, Encoding.UTF8);
            });
            Console.WriteLine("完成");
            Console.WriteLine();
        }
    }
}