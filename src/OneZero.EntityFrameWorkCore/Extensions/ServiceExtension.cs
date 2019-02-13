using Microsoft.EntityFrameworkCore;
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
    }
}
