using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OneZero.Entity.Configuration;
using OneZero.Entity.DatabaseContext;
using OneZero.Entity.DatabaseContext.SqlContext;
using OneZero.Entity.Identity;

namespace OneZero.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSqlServerContext<MSSqlContext>(Configuration.GetConnectionString("DefaultConnection"), 1000);
            //services.Configure<ApiBehaviorOptions>(Options=>)
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

           


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
         
            app.UseMvc();
        }
    }
}
