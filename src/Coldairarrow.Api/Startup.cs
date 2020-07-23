using Coldairarrow.Util;
using EFCore.Sharding;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSwag;
using System.Linq;

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
            services.AddEFCoreSharding(config =>
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
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.GetType().GetProperties().ForEach(aProperty =>
                {
                    var value = aProperty.GetValue(JsonExtention.DefaultJsonSetting);
                    aProperty.SetValue(options.SerializerSettings, value);
                });
            });
            services.AddHttpContextAccessor();

            //swagger
            services.AddOpenApiDocument(settings =>
            {
                settings.AddSecurity("身份认证Token", Enumerable.Empty<string>(), new OpenApiSecurityScheme()
                {
                    Scheme = "bearer",
                    Description = "Authorization:Bearer {your JWT token}<br/><b>授权地址:/Base_Manage/Home/SubmitLogin</b>",
                    Name = "Authorization",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Type = OpenApiSecuritySchemeType.Http
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
            .UseRouting()
            .UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseOpenApi(); //添加swagger生成api文档（默认路由文档 /swagger/v1/swagger.json）
            app.UseSwaggerUi3();//添加Swagger UI到请求管道中(默认路由: /swagger).
        }
    }
}
