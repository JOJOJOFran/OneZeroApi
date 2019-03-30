﻿using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OneZero.Domain.Repositories;
using OneZero.Common.Dapper;
using OneZero.EntityFrameworkCore.SqlServer;
using OneZero.EntityFrameworkCore.UnitOfWorks;
using OneZero.Extensions;
using SouthStar.VehSch.Api.Areas.Setting.Services;
using System.IO;
using Swashbuckle.AspNetCore.Swagger;
using OneZero.Middleware;
using SouthStar.VehSch.Api.Areas.ApplicationFlow.Services;
using SouthStar.VehSch.Api.Areas.Dispatch.Services;
using MediatR;
using OneZero.Common.QiniuTool;
using OneZero;
using SouthStar.VehSch.Api.Areas.Home.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace SouthStar.VehSch.Api
{
    public class Startup
    {
        private JwtOption jwtSettings;
        private OneZeroOption oneZeroOption;

        public Startup(IConfiguration configuration, ILoggerFactory factory)
        {
            Configuration = configuration;
            LoggerFactory = factory;
            jwtSettings = new JwtOption();
            oneZeroOption=OptionConfig();
        }

        public ILoggerFactory LoggerFactory { get; }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddOneZero(option =>
            {
                option.DbContextOption = new OneZero.DbContextOption
                { ConnectString = Configuration.GetConnectionString("DefaultConnection"), DBType = OneZero.DbType.SqlServer };
                
            }, LoggerFactory
            , Assembly.GetExecutingAssembly().GetName().ToString());

            //添加Swagger API文档
            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new Info { Title = "VehSch API", Version = "v1" });
                var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SouthStar.VehSch.Api.xml");
                swagger.IncludeXmlComments(filePath);
            });

            //设置跨域访问
            services.AddCors(options =>
            {
                //AddPolicy ( string name, Action<CorsPolicyBuilder> configurePolicy)
                //policy name, A delegate which can use a policy builder to build a policy.
                options.AddPolicy("AllowCrossOrigin", b =>
                {
                    //从配置文件读取允许的跨域地址
                    //.WithOrigins(Configuration.GetSection("Origin").GetValue<string>("LocalOrigin"))
                    b.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin();
                    //.SetIsOriginAllowed (Configuration.GetSection("").ToString().Split(","),true);
                });
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }) //认证middleware配置
            .AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
                    ClockSkew = new TimeSpan(0, 0, 30),
                    RequireExpirationTime = true,
                    ValidateLifetime = true

                };


            });
            //services.AddDefaultIdentity();
            services.AddMediatR();
            services.AddHttpContextAccessor();
            services.AddTransient<ExceptionMiddleware>();
            services.AddScoped<IDbContext, MSSqlContext>();
            services.AddScoped<IUnitOfWork, EFUnitOfWork>();       
            services.AddScoped<IDapperProvider, DapperProvider>();
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<VehcileService>();
            services.AddScoped<DriverService>();
            services.AddScoped<DepartmentService>();
            services.AddScoped<VehicleApplyService>();
            services.AddScoped<CheckService>();
            services.AddScoped<VehicleDispatchService>();
            services.AddScoped<DispatchFeeService>();
            services.AddScoped<QiniuHelper>();
            services.AddScoped<LoginService>();
            services.AddSingleton(oneZeroOption);


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions(options =>
            {
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd";
            }); ;
         
        }

        private OneZeroOption OptionConfig()
        {
            QiniuOption qiniuOption = new QiniuOption();         
            OneZeroOption oneZeroOption = new OneZeroOption();
            Configuration.GetSection("Qiniu").Bind(qiniuOption);
            Configuration.GetSection("JWT").Bind(jwtSettings);
            oneZeroOption.QiniuOption = qiniuOption;
            oneZeroOption.JwtOption = jwtSettings;

            return oneZeroOption;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            
            //app.UseOneZreoException();
            //app.UseOneZero();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //else
            //{
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
            //}
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", " API V1");

            });
            app.UseHttpsRedirection();
            app.UseMvc();
           

        }
    }
}
