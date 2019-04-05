using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OneZero.EntityFrameworkCore;
using OneZero.Enums;
using OneZero.Options;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace OneZero.Core.Extensions
{
    public static class ServiceExtension
    {
        /// <summary>
        /// 配置数据库上下文
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <param name="option"></param>
        /// <param name="loggerFactory"></param>
        /// <param name="dbConnection"></param>
        /// <param name="assmblyName"></param>
        /// <param name="Dbtype"></param>
        /// <param name="poolSize"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigDbContext<T>(this IServiceCollection services, OneZeroOption option, ILoggerFactory loggerFactory, string dbConnection, string assmblyName = null, DatabaseType Dbtype = DatabaseType.SqlServer, int poolSize = 128) where T : DbContext
        {
            //services.AddDbContextPool<T>(options => { options.GetDbCOntextOptionByDatabaseType(Dbtype, loggerFactory, dbConnection, assmblyName); }, poolSize);
            services.AddDbContextPool<T>(options =>
            {
                options.UseSqlServer(dbConnection, b => b.MigrationsAssembly(assmblyName));
                options.UseLoggerFactory(loggerFactory);
            }, poolSize);
            return services;
        }


        /// <summary>
        /// 通过DatabaseType去配置数据库
        /// </summary>
        /// <param name="options"></param>
        /// <param name="databaseType"></param>
        /// <param name="loggerFactory"></param>
        /// <param name="dbConnection"></param>
        /// <param name="assmblyName"></param>
        public static void GetDbCOntextOptionByDatabaseType(this DbContextOptionsBuilder options, DatabaseType databaseType, ILoggerFactory loggerFactory, string dbConnection, string assmblyName)
        {
            switch (databaseType)
            {
                case DatabaseType.SqlServer:
                    options = new EntityFrameworkCore.SqlServer.DbContextOptionBuilderCreator().Create(dbConnection, assmblyName, loggerFactory);
                    break;
            }
        }

    }
}
