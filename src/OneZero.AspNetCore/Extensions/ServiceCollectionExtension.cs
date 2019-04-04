using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OneZero.AspNetCore.Middlewares;
using OneZero.Common.Qiniu;
using OneZero;
using OneZero.Options;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Reflection;
using OneZero.Domain;
using OneZero.Common.Dapper;
using LogDashboard;
using OneZero.Common.Extensions;
using Hangfire;
using OneZero.Enums;
using OneZero.Exceptions;
using OneZero.AspNetCore.JwtBreaer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Hosting;

namespace OneZero.AspNetCore.ServiceExtensions
{
    /// <summary>
    /// ServiceCollectionExtension
    /// </summary>
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// 配置框架服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="oneZeroOption"></param>
        /// <returns></returns>
        public static IServiceCollection AddOneZero(this IServiceCollection services, IConfiguration configuration, OneZeroOption oneZeroOption, IHostingEnvironment env)
        {
            //获取所有程序集
            var assemblies = GetAssemblies();
            services.AddMediatR(assemblies);
            services.AddAutoMapper(assemblies);
            services.AddHttpContextAccessor();
            services.AddScoped<OneZeroContext>();
            services.AddScoped<OneZeroExceptionMiddleware>();
            services.AddScoped<OneZeroMiddleware>();
            services.AddScoped<QiniuHelper>();
            services.AddScoped<JwtService>();          
            services.AddScoped<IDapperProvider, DapperProvider>();
            services.AddSingleton(oneZeroOption);
            //添加日志面板
            services.AddLogDashboard(option => { option.CustomLogModel<MyLogModel>(); });
            //根据配置决定是否使用Swagger，Hangfire
            services.ConfigSwagger(configuration,oneZeroOption.IsAuthentic.CastTo(false))
                    .ConfigHangfire(configuration, env)
                    .ConfigAuthentication(oneZeroOption);
            //添加跨域
            services.AddCors(options =>
            {
                options.AddPolicy("AllowCrossOrigin", b =>
                {
                    b.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin();
                });
            });
            return services;
        }

        #region Swagger
        /// <summary>
        /// 配置swagger
        /// </summary>
        /// <param name="app"></param>
        /// <param name="url">Endpoint url </param>
        /// <param name="title">title</param>
        public static IServiceCollection ConfigSwagger(this IServiceCollection services, IConfiguration configuration,bool IsAutentication=false)
        {
            if (Convert.ToBoolean(configuration["OneZero:Swagger:IsUse"]))
            {
                string title = configuration["OneZero:Swagger:Title"];
                string version = configuration["OneZero:Swagger:Version"];
                string Xml = configuration["OneZero:Swagger:Xml"];

                services.AddSwaggerGen(swagger =>
                {
                    swagger.SwaggerDoc($"v{version}", new Info() { Title = title, Version = $"v{version}" });
                    var basePath = Path.GetDirectoryName(AppContext.BaseDirectory);
                    var filePath = Path.Combine(basePath, Xml);
                    swagger.IncludeXmlComments(filePath);
                    #region  权限Token
                    //权限Token
                    if (IsAutentication)
                    {
                        swagger.AddSecurityDefinition("Bearer", new ApiKeyScheme()
                        {
                            Description = "请输入带有Bearer的Token，形如 “Bearer {Token}” ",
                            Name = "Authorization",
                            In = "header",
                            Type = "apiKey"
                        });
                    }

                    #endregion
                });
            }
            return services;
        }
        #endregion

        #region Hangfire
        /// <summary>
        /// 配置Hangfire
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigHangfire(this IServiceCollection services, IConfiguration configuration, IHostingEnvironment env)
        {
            bool IsUse = configuration["OneZero:Hangfire:IsUse"].CastTo(false);
            if (IsUse)
            {
                var connectString = env.IsDevelopment()? configuration["OneZero:Hangfire:DefaultConnection"] : configuration["OneZero:Hangfire:ProductionConnection"];
                if (string.IsNullOrWhiteSpace(connectString))
                    throw new OneZeroException("Hangfire连接字符串配置有误，请检查配置", ResponseCode.Fatal);
                services.AddHangfire(x=>x.UseSqlServerStorage(connectString));
            }
            return services;
        }

        #endregion

        #region Authentication
        public static IServiceCollection ConfigAuthentication(this IServiceCollection services, OneZeroOption oneZeroOption)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidIssuer = oneZeroOption.JwtOption.Issuer,
                    ValidAudience = oneZeroOption.JwtOption.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(oneZeroOption.JwtOption.SecretKey)),
                    //时间偏移量
                    ClockSkew = new TimeSpan(0, 0, 30),
                    RequireExpirationTime = true,
                    ValidateLifetime = true

                };

            });
            return services;
        }
        #endregion

        #region Private Methods
        private static Assembly[] GetAssemblies(string path = null)
        {
            List<Assembly> assemblies = new List<Assembly>(10);
            foreach (var file in Directory.GetFiles(path ?? AppDomain.CurrentDomain.BaseDirectory, "*.dll"))
            {
                assemblies.Add(Assembly.LoadFrom(file));
            }
            return assemblies.ToArray();
        }

        #endregion
    }
}
