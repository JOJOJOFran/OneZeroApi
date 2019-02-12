using OneZero.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Repository.Extensions
{
    public static class ServiceProviderExtension
    {
        public static IDbContext GetDbContext(this IServiceProvider serviceProvider)
        {
            return (IDbContext)serviceProvider.GetService(typeof(IDbContext));
        }


    }
}
