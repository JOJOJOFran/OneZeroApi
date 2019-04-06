using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Hangfire;
using Hangfire.Common;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OneZero.AspNetCore.Extensions;
using OneZero.AspNetCore.Middlewares;
using OneZero.AspNetCore.ServiceExtensions;
using OneZero.Common.Dapper;
using OneZero.Common.Qiniu;
using OneZero.Core;
using OneZero.Core.Domains;
using OneZero.Core.Extensions;
using OneZero.Core.Services.Permission;
using OneZero.Domain;
using OneZero.EntityFrameworkCore;
using OneZero.EntityFrameworkCore.Domain;
using OneZero.Enums;
using OneZero.Options;
using SouthStar.VehSch.Api.Extensions;
using SouthStar.VehSch.Core.ApplicationFlow.Services;
using SouthStar.VehSch.Core.Dispatch.Services;
using SouthStar.VehSch.Core.Logins.Services;
using SouthStar.VehSch.Core.Setting.Services;
using Swashbuckle.AspNetCore.Swagger;

namespace SouthStar.VehSch.Api
{
    public class Startup
    {
        private readonly OneZeroOption oneZeroOption = new OneZeroOption();
        private ILoggerFactory _loggerFactory { get; }
        private IHostingEnvironment _env { get; }

        public Startup(IConfiguration configuration, ILoggerFactory loggerFactory, IHostingEnvironment env)
        {
            Configuration = configuration;
            _loggerFactory = loggerFactory;
            _env = env;
            OneZeroOptionInit(configuration);
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //引入OneZero配置的服务
            services.AddOneZero(Configuration, oneZeroOption);
            services.AddScoped<IDbContext, DefaultDbContext>();
            services.AddScoped<IUnitOfWork, OneZeroUnitOfWork>();
            services.BatchScopedServiceRegister(typeof(BaseService));
            //配置数据库上下文
            services.ConfigDbContext<DefaultDbContext>(oneZeroOption, _loggerFactory, GetDbConnection(), Assembly.GetExecutingAssembly().GetName().ToString());

            services.AddMvc()
                    .AddJsonOptions(option => { option.SerializerSettings.DateFormatString = "yyyy-MM-dd"; })
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseOneZero(Configuration);
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            //RecurringJob.AddOrUpdate(() => Console.WriteLine("hello22"), Cron.Minutely());
            //JobStorage.
            //RecurringJob.RemoveIfExists(;
            app.UseMvc();
        }

        /// <summary>
        /// 初始化框架配置
        /// </summary>
        private void OneZeroOptionInit(IConfiguration configuration)
        {
            configuration.GetSection("OneZero").Bind(oneZeroOption);
        }

        /// <summary>
        /// 根据环境获取字符串
        /// </summary>
        /// <returns></returns>
        private string GetDbConnection()
        {
            if (_env.IsProduction())
            {
                return Configuration.GetConnectionString("ProductionConnection");
            }
            else
            {
                return Configuration.GetConnectionString("DefaultConnection");
            }
        }

    }
}
