using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogDashboard;
using LogDashboard.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OneZero.Entity.Configuration;
using OneZero.Entity.DatabaseContext.EFContext;

namespace OneZero.Test.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, ILoggerFactory factory)
        {
            Configuration = configuration;
            loggerFactory = factory;
        }

        public IConfiguration Configuration { get; }
        public ILoggerFactory loggerFactory { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddLogDashboard(
                option => {
   
                   // option
                  //  option.SetRootPath("");
                    option.CustomLogModel<LogModelTest>();
                    option.RootPath = "C:\temp\\OneZero.Test.Api"; } //C:\英雄时刻
                );
            //ILoggerFactory loggerFactory = services.AddLogging().BuildServiceProvider().GetService<ILoggerFactory>();
            //var logger = services.AddLogging().BuildServiceProvider().GetService<ILoggerFactory>().AddConsole().CreateLogger("App");
           // logger.LogInformation("测试1111");
            
            services.AddSqlServerContext<MSSqlContext>(loggerFactory,Configuration.GetConnectionString("DefaultConnection"), 1000);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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
            app.UseLogDashboard();

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
