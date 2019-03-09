using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OneZero.Middleware
{
    public class ExceptionMiddleware : IMiddleware
    {
 
        public ExceptionMiddleware() { }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {

            }
            
        }
    }
}
