using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using OneZero.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.EntityFrameworkCore.Extensions
{
    public static class ServiceExtension
    {
        public static DbContext GetEFDbContext(this IServiceProvider provider, Type type = null)
        {
            return (DbContext)provider.GetService(typeof(DbContext));
        }

        public static IDbContext GetDbContext(this IServiceProvider provider, Type type = null)
        {
            return (IDbContext)provider.GetService(typeof(IDbContext));
        }

        public static ILogger GetLogger(this IServiceProvider provider,string name)
        {
            ILoggerFactory factory = provider.GetService<ILoggerFactory>();
            return factory.CreateLogger(name);
        }

        public static ILogger GetLogger(this IServiceProvider provider, Type type)
        {
            ILoggerFactory factory = provider.GetService<ILoggerFactory>();
            return factory.CreateLogger(type);
        }

        public static ILogger<T> GetLogger<T>(this IServiceProvider provider)
        {
            ILoggerFactory factory = provider.GetService<ILoggerFactory>();
            return factory.CreateLogger<T>();
        }

    }
}
