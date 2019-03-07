using System;
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
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OneZero.Domain.Repositories;
using OneZero.Common.Dapper;
using OneZero.EntityFrameworkCore.SqlServer;
using OneZero.EntityFrameworkCore.UnitOfWorks;
using OneZero.Extensions;
using SouthStar.VehSch.Api.Areas.Setting.Services;

namespace SouthStar.VehSch.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, ILoggerFactory factory)
        {
            Configuration = configuration;
            LoggerFactory = factory;
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddScoped<IUnitOfWork, EFUnitOfWork>();
            services.AddScoped<IDbContext, MSSqlContext>();
            services.AddScoped<IDapperProvider, DapperProvider>();
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<VehcileServeice>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
