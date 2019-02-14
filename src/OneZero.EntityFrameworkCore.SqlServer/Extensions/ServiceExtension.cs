using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.EntityFrameworkCore.SqlServer.Extensions
{
    public static class ServiceExtension
    {
        /// <summary>
        /// 使用SqlServer EFCore（使用连接池方式以提高性能）
        /// </summary>
        /// <typeparam name="T">上下文类型</typeparam>
        /// <param name="services"></param>
        /// <param name="loggerFactory">日志工厂</param>
        /// <param name="dbConnection">连接字符串</param>
        /// <param name="poolSize">连接池大小（默认128）</param>
        /// <returns></returns>
        public static IServiceCollection AddSqlServerContextPool<T>(this IServiceCollection services, ILoggerFactory loggerFactory, string dbConnection, string assmblyName,int poolSize = 128) where T : DbContext
        {

            services.AddDbContextPool<T>(Options =>
            {
                Options.UseSqlServer(dbConnection, b => b.MigrationsAssembly(assmblyName));
                Options.UseLoggerFactory(loggerFactory);

            });
            //OneZeroEntityBuilder builder = new OneZeroEntityBuilder(services);
            return services;
        }
    }
}
