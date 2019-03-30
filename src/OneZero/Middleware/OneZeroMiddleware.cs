using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.Threading.Tasks;
using OneZero.Common.Exceptions;
using OneZero.Common.Enums;
using System.Net;
using OneZero.Common.Dtos;

namespace OneZero.Middleware
{
    public class OneZeroMiddleware
    {
        private readonly RequestDelegate _next;
        private  OneZeroContext _oneZeroContext;


        public OneZeroMiddleware(RequestDelegate next)
        {
            _next = next;

        }

        /// <summary>
        /// 使用约定激活的中间件,注入的对象无法通过构造函数获取，只能在Invoke里获取
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <param name="oneZeroContext"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context,  OneZeroContext oneZeroContext)
        {

                _oneZeroContext = oneZeroContext;
              // _oneZeroContext.TenanId= context.User.Claims
                await _next(context);
        }
    }
}
