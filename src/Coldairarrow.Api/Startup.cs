﻿using Coldairarrow.Util;
using EFCore.Sharding;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
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
            _configuration = configuration;
        }

        private readonly IConfiguration _configuration;

        public void ConfigureServices(IServiceCollection services)
        {
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
                settings.AllowReferencesWithProperties = true;
                settings.AddSecurity("身份认证Token", Enumerable.Empty<string>(), new OpenApiSecurityScheme()
                {
                    Scheme = "bearer",
                    Description = "Authorization:Bearer {your JWT token}<br/><b>授权地址:/Base_Manage/Home/SubmitLogin</b>",
                    Name = "Authorization",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Type = OpenApiSecuritySchemeType.Http
                });
            });

            //jwt
            services.AddJwt(_configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //跨域
            app.UseCors(x =>
                {
                    x.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .DisallowCredentials();
                })
                .UseForwardedHeaders(new ForwardedHeadersOptions
                {
                    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
                })
                .UseMiddleware<RequestBodyMiddleware>()
                .UseMiddleware<RequestLogMiddleware>()
                .UseDeveloperExceptionPage()
                .UseStaticFiles(new StaticFileOptions
                {
                    ServeUnknownFileTypes = true,
                    DefaultContentType = "application/octet-stream"
                })
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers().RequireAuthorization();
                })
                .UseOpenApi()//添加swagger生成api文档（默认路由文档 /swagger/v1/swagger.json）
                .UseSwaggerUi3()//添加Swagger UI到请求管道中(默认路由: /swagger).
                ;
        }
    }
}
