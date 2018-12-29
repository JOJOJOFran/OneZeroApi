using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace OneZero.Entity.Configuration
{

    public static class OneZeroEntityServicesExtension
    {
        
        public static IOneZeroEntityBuilder AddSqlServerContext<T>(this IServiceCollection services, string dbConnection,int poolSize=128) where T:DbContext
        {
            IOneZeroEntityBuilder builder =new  IOneZeroEntityBuilder();
            services.AddDbContextPool<T>(option => {
                     option.UseSqlServer(configuration.GetConnectionString(dbConnection),poolSize);
            });
            builder.Services=services;
            return builder;
        }
    }
}