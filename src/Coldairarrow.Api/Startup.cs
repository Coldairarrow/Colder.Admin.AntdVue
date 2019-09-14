using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.DynamicProxy;
using AutoMapper;
using Coldairarrow.Business.Base_Manage;
using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Coldairarrow.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add<GlobalExceptionFilter>();
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
            .AddControllersAsServices();
            services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IActionContextAccessor, ActionContextAccessor>();
            services.AddSingleton(Configuration);
            services.AddLogging();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1.0.0",
                    Title = "接口文档",
                    Contact = new Contact { Name = "Coldairarrow", Url = "https://github.com/Coldairarrow" }
                });
                // 为 Swagger JSON and UI设置xml文档注释路径
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);//获取应用程序所在目录（绝对，不受工作目录影响，建议采用此方法获取路径）

                //基础层
                c.IncludeXmlComments(Path.Combine(basePath, "Coldairarrow.Util.xml"));

                //实体层
                c.IncludeXmlComments(Path.Combine(basePath, "Coldairarrow.Entity.xml"));

                //业务逻辑层
                c.IncludeXmlComments(Path.Combine(basePath, "Coldairarrow.Business.xml"));

                //控制器层
                c.IncludeXmlComments(Path.Combine(basePath, "Coldairarrow.Api.xml"), true);
            });

            //使用Autofac替换自带IOC
            var builder = InitAutofac();
            builder.Populate(services);
            var container = builder.Build();

            AutofacHelper.Container = container;

            return new AutofacServiceProvider(container);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //Request.Body重用
            app.Use(next => context =>
            {
                context.Request.EnableRewind();

                return next(context);
            })
            //跨域
            .UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
            )
            .UseDeveloperExceptionPage()
            .UseStaticFiles();

            //Swagger配置
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "1.0.0");
            });
            app.UseMvc(routes =>
            {
                //默认路由
                routes.MapRoute(
                    name: "Default",
                    template: "Api/{controller=Home}/{action=Index}/{id?}",
                    defaults: "/swagger"
                );
            });
            InitAutoMapper();
            InitId();
        }

        private ContainerBuilder InitAutofac()
        {
            var builder = new ContainerBuilder();

            var baseType = typeof(IDependency);
            var baseTypeCircle = typeof(ICircleDependency);

            //Coldairarrow相关程序集
            var assemblys = Assembly.GetEntryAssembly().GetReferencedAssemblies()
                .Select(Assembly.Load)
                .Cast<Assembly>()
                .Where(x => x.FullName.Contains("Coldairarrow")).ToList();

            //自动注入IDependency接口,支持AOP,生命周期为InstancePerDependency
            builder.RegisterAssemblyTypes(assemblys.ToArray())
                .Where(x => baseType.IsAssignableFrom(x) && x != baseType)
                .AsImplementedInterfaces()
                .PropertiesAutowired()
                .InstancePerDependency()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(Interceptor));

            //自动注入ICircleDependency接口,循环依赖注入,不支持AOP,生命周期为InstancePerLifetimeScope
            builder.RegisterAssemblyTypes(assemblys.ToArray())
                .Where(x => baseTypeCircle.IsAssignableFrom(x) && x != baseTypeCircle)
                .AsImplementedInterfaces()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
                .InstancePerLifetimeScope();

            //注册Controller
            builder.RegisterAssemblyTypes(typeof(Startup).GetTypeInfo().Assembly)
                .Where(t => typeof(Controller).IsAssignableFrom(t) && t.Name.EndsWith("Controller", StringComparison.Ordinal))
                .PropertiesAutowired();

            //AOP
            builder.RegisterType<Interceptor>();

            //请求结束自动释放
            builder.RegisterType<DisposableContainer>()
                .As<IDisposableContainer>()
                .InstancePerLifetimeScope();

            return builder;
        }

        private void InitAutoMapper()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Base_User, Base_UserDTO>();
            });
        }

        private void InitId()
        {
            new IdHelperBootstrapper()
                //设置WorkerId
                .SetWorkderId(ConfigHelper.GetValue("WorkerId").ToLong())
                //使用Zookeeper
                //.UseZookeeper("127.0.0.1:2181", 200, GlobalSwitch.ProjectName)
                .Boot();
        }
    }
}
