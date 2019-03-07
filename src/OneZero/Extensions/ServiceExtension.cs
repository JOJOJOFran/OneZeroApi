using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OneZero.EntityFrameworkCore.SqlServer;
using OneZero.EntityFrameworkCore.SqlServer.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddOneZero(this IServiceCollection services, Action<OneZeroOption> func,ILoggerFactory loggerFactory,string assmblyName, int poolSize = 128)
        {
            var option = new OneZeroOption();
            func?.Invoke(option);
            services.AddSqlServerContextPool<MSSqlContext>(loggerFactory, option.DbContextOption.ConnectString, assmblyName, poolSize);
            return services;
        }
    }
}
