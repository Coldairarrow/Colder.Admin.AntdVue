using Autofac;
using Autofac.Extras.DynamicProxy;
using Coldairarrow.DataRepository;
using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

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

        public static async Task HttpClientFactoryTest()
        {
            var serviceProvider = new ServiceCollection().AddHttpClient().BuildServiceProvider();
            var httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();
            var client = httpClientFactory.CreateClient();
            var response = await client.SendAsync(new HttpRequestMessage(System.Net.Http.HttpMethod.Get, "http://www.baidu.com"));
            var content = await response.Content.ReadAsStringAsync();
        }

        public static async Task AsyncTest()
        {
            await Task.Run(() =>
            {
                throw new Exception("11");
            });
        }

        static async Task Main()
        {
            //var db = DbFactory.GetRepository();
            //var list = await db.GetListAsync(typeof(Base_User));
            //var count = await IQueryableHelper.CountAsync(db.GetIQueryable<Base_User>());
            try
            {
                await AsyncTest();
                //TaskHelper.RunSync(() => AsyncTest());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            //AsyncTest();

            Console.WriteLine();
        }
    }
}