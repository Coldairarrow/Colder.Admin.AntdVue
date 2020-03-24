using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.DynamicProxy;
using AutoMapper;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
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
            ConfigHelper.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add<GlobalExceptionFilter>();
            })
            .AddControllersAsServices()
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });

            services.AddScoped<IHttpContextAccessor, HttpContextAccessor>()
            .AddTransient<IActionContextAccessor, ActionContextAccessor>()
            .AddSingleton(Configuration)
            .AddLogging()
            .Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            })
            .Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            })
            .AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1.0.0",
                    Title = "接口文档"
                });
                // JWT认证                                                 
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Type = SecuritySchemeType.Http,
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Description = "Authorization:Bearer {your JWT token}<br/><b>授权地址:/Base_Manage/Home/SubmitLogin</b>",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme{Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
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
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            // 在这里添加服务注册
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

            //注册Controller
            builder.RegisterAssemblyTypes(typeof(Startup).GetTypeInfo().Assembly)
                .Where(t => typeof(Controller).IsAssignableFrom(t) && t.Name.EndsWith("Controller", StringComparison.Ordinal))
                .PropertiesAutowired();

            //AOP
            builder.RegisterType<Interceptor>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Request.Body重用
            app.Use(next => context =>
            {
                context.Request.EnableBuffering();

                return next(context);
            })
            .UseMiddleware<CorsMiddleware>()//跨域
            .UseDeveloperExceptionPage()
            .UseStaticFiles(new StaticFileOptions
            {
                ServeUnknownFileTypes = true,
                DefaultContentType = "application/octet-stream"
            })
            //Swagger配置
            .UseSwagger()
            .UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "1.0.0");
                c.RoutePrefix = string.Empty;
            })
            .UseRouting()
            .UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            AutofacHelper.Container = app.ApplicationServices.GetAutofacRoot();
            InitAutoMapper();
            InitId();
            ApiLog();
        }

        private void InitAutoMapper()
        {
            List<(Type from, Type target)> maps = new List<(Type from, Type target)>();

            maps.AddRange(GlobalData.FxAllTypes.Where(x => x.GetCustomAttribute<MapToAttribute>() != null)
                .Select(x => (x, x.GetCustomAttribute<MapToAttribute>().TargetType)));
            maps.AddRange(GlobalData.FxAllTypes.Where(x => x.GetCustomAttribute<MapFromAttribute>() != null)
                .Select(x => (x.GetCustomAttribute<MapFromAttribute>().FromType, x)));
            Mapper.Initialize(cfg =>
            {
                maps.ForEach(aMap =>
                {
                    cfg.CreateMap(aMap.from, aMap.target);
                });
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

        private void ApiLog()
        {
            HttpHelper.HandleLog = log =>
            {
                //接口日志
                using (var lifescope = AutofacHelper.Container.BeginLifetimeScope())
                {
                    lifescope.Resolve<ILogger>().Info(LogType.系统跟踪, log);
                }
            };
        }
    }
}
