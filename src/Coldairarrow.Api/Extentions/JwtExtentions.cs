using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace Coldairarrow.Api
{
    public static class JwtExtentions
    {
        public static IServiceCollection AddJwt(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtOptions>(configuration.GetSection("jwt"));
            var jwtOptions = configuration.GetSection("jwt").Get<JwtOptions>();

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = false;
                //Token Validation Parameters
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ClockSkew = TimeSpan.Zero,//到时间立即过期
                    ValidateIssuerSigningKey = true,
                    //获取或设置要使用的Microsoft.IdentityModel.Tokens.SecurityKey用于签名验证。
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.
                    GetBytes(jwtOptions.Secret)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });

            return services;
        }
    }
}
