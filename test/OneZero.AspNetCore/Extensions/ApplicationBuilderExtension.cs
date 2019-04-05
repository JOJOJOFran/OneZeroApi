using Microsoft.AspNetCore.Builder;
using OneZero.AspNetCore.Middlewares;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using LogDashboard;
using Hangfire;

namespace OneZero.AspNetCore.Extensions
{
    public static class ApplicationBuilderExtension
    {

        public static IApplicationBuilder UseOneZero(this IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseMiddleware<OneZeroExceptionMiddleware>();
            app.UseAuthentication();
            app.UseMiddleware<OneZeroMiddleware>();
            app.UseOneZeroSwagger(configuration)
               .UseOneZeroHangfire(configuration);
            app.UseLogDashboard();
            return app;
        }

        #region Swagger
        /// <summary>
        /// 使用swagger
        /// </summary>
        /// <param name="app"></param>
        /// <param name="url">Endpoint url </param>
        /// <param name="title">title</param>
        public static IApplicationBuilder UseOneZeroSwagger(this IApplicationBuilder app, IConfiguration configuration)
        {
            if (Convert.ToBoolean(configuration["OneZero:Swagger:IsUse"]))
            {
                string url = configuration["OneZero:Swagger:Url"];
                string title = configuration["OneZero:Swagger:Title"];
                string version = configuration["OneZero:Swagger:Version"];
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint(url, $"{title} V{version}");
                });
            }
            return app;
        }
        #endregion


        #region Hangfire
        public static IApplicationBuilder UseOneZeroHangfire(this IApplicationBuilder app, IConfiguration configuration)
        {
            if (Convert.ToBoolean(configuration["OneZero:Hangfire:IsUse"]))
            {
                app.UseHangfireServer();
                //DashboardOptions dashboardOptions=
                app.UseHangfireDashboard();
            }
            return app;
        }
        #endregion



    }
}
