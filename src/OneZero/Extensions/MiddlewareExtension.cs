using Microsoft.AspNetCore.Builder;
using OneZero.Middleware;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Extensions
{
    public static class MiddlewareExtension
    {
        public static void UseOneZero(this IApplicationBuilder app)
        {
            app.UseMiddleware<OneZeroMiddleware>();
        }

        public static void UseOneZreoException(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
