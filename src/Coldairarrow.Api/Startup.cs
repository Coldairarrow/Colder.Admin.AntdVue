using Coldairarrow.Util;
using EFCore.Sharding;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using System.IO;

namespace Coldairarrow.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddFxServices();
            services.AddAutoMapper();
            services.UseEFCoreSharding(config =>
            {
                string conName = Configuration["ConnectionName"];
                if (Configuration["LogicDelete"].ToBool())
                    config.UseLogicDelete();
                config.UseDatabase(Configuration.GetConnectionString(conName), Configuration["DatabaseType"].ToEnum<DatabaseType>());
                config.SetEntityAssembly(GlobalData.FXASSEMBLY_PATTERN);
            });
            services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
            services.AddControllers(options =>
            {
                options.Filters.Add<ValidFilterAttribute>();
                options.Filters.Add<GlobalExceptionFilter>();
            })
            .AddControllersAsServices()
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });
            services.AddHttpContextAccessor()
            .AddLogging()
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
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
                // 为 Swagger JSON and UI设置xml文档注释路径
                //获取应用程序所在目录（绝对，不受工作目录影响，建议采用此方法获取路径）
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);

                var xmls = Directory.GetFiles(basePath, "*.xml");
                xmls.ForEach(aXml =>
                {
                    c.IncludeXmlComments(aXml);
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //允许body重用
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
            ApiLog();
        }

        private void ApiLog()
        {
            //HttpHelper.HandleLog = log =>
            //{
            //    //接口日志
            //    using (var lifescope = AutofacHelper.Container.BeginLifetimeScope())
            //    {
            //        lifescope.Resolve<IMyLogger>().Info(LogType.系统跟踪, log);
            //    }
            //};
        }
    }
}
