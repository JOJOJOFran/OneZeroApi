using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Text;
using OneZero.Domain;

namespace OneZero.EntityFrameworkCore.Extensions
{
    public static class ServiceProviderExtension
    {
        #region 获取数据库上下文
        public static IDbContext GetDbContext(this IServiceProvider provider, Type type = null)
        {
            return (IDbContext)provider.GetService(typeof(IDbContext));
        }
        #endregion

        #region 获取日志
        public static ILogger GetLogger(this IServiceProvider provider, string name)
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
        #endregion
    }
}
