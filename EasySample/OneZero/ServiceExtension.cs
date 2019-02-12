using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Builder;

namespace OneZero.Core
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddOneZero(this IServiceCollection services, OneZeroOption options)
        {

            //services.AddScoped<>            
            return services;
        }

        /// <summary>
        /// 根据配置添加JWT验证服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IServiceCollection AddAuthenticationByJwt(this IServiceCollection services, OneZeroOption options)
        {
            if (options == null || options.jwtOption == null)
                throw new ArgumentNullException("OneZero.Core.ServiceExtension.AddOneZeroJwt:参数OneZeroOption关于JWT的配置为空");

            var jwtOption = options.jwtOption;
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;         
            })
            .AddJwtBearer(option=> {
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidAudience = jwtOption.Audience,
                    ValidIssuer  =jwtOption.Issuer,
                    IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOption.SecretKey))
                };
            });
            return services;
        }


        public static void UseOneZero(this IApplicationBuilder app, OneZeroOption options)
        {
            
        }
    }
}
