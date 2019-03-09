using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.Threading.Tasks;

namespace OneZero.Middleware
{
    public class OneZeroMiddleware
    {
        private readonly RequestDelegate _next;
        private  IOneZeroContext _oneZeroContext;


        public OneZeroMiddleware(RequestDelegate next)
        {
            _next = next;
            

        }

        /// <summary>
        /// 使用约定激活的中间件：
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <param name="oneZeroContext"></param>
        /// <returns></returns>
        public Task Invoke(HttpContext context,  IOneZeroContext oneZeroContext)
        {
            _oneZeroContext = oneZeroContext;
            return _next(context);
        }
    }
}
