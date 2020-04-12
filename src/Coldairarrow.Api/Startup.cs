using Coldairarrow.Util;
using EFCore.Sharding;
using ImpromptuInterface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
            services.UseEFCoreSharding(DatabaseType.SqlServer, Configuration.GetConnectionString("BaseDb"),
                config => config.SetEntityAssembly(GlobalData.FXASSEMBLY));
            services.AddScoped(_ =>
                DbFactory.GetRepository(Configuration.GetConnectionString("BaseDb"), DatabaseType.SqlServer).ActLike<IMyRepository>());
            services.AddControllers(options =>
            {
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

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<CorsMiddleware>()//跨域
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
            InitId();
            ApiLog();
        }

        private void InitId()
        {
            new IdHelperBootstrapper()
                //设置WorkerId
                .SetWorkderId(Configuration["WorkerId"].ToLong())
                //使用Zookeeper
                //.UseZookeeper("127.0.0.1:2181", 200, GlobalSwitch.ProjectName)
                .Boot();
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
