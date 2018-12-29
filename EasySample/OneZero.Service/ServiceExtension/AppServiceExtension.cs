using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using OneZero.Service.ServiceInterface;

namespace OneZero.Service.ServiceExtensions
{
    public static class AppServiceExtensions
    {
        public static void AddService(this IServiceCollection services)
        {

        }

        public static void AddAuditByRedisService<T>(this IServiceBuilder builder,T service) where T:IAduitByRedis
        {

        }
    }
}