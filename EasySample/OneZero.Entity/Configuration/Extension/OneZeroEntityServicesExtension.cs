using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OneZero.Entity.DatabaseContext.SqlContext;

namespace OneZero.Entity.Configuration
{

    public static class OneZeroEntityServicesExtension
    {

        public static IOneZeroEntityBuilder AddOneZeroDbContext(this IServiceCollection services )
        {
            OneZeroEntityBuilder builder = new OneZeroEntityBuilder(services);
            return builder;
        }

        public static IOneZeroEntityBuilder AddSqlServerContext<T>(this IServiceCollection services,  string dbConnection, int poolSize = 128) where T : DbContext, IMSSqlContext
        {
            services.AddDbContextPool<T>(Options =>
            {
                Options.UseSqlServer(dbConnection,b=>b.MigrationsAssembly("OneZero.Api"));
               
            });
            OneZeroEntityBuilder builder = new OneZeroEntityBuilder(services);
            return builder;
        }
    }
}