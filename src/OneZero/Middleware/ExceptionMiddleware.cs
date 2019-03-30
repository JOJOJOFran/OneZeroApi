using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OneZero.Common.Dtos;
using OneZero.Common.Enums;
using OneZero.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OneZero.Middleware
{
    public class ExceptionMiddleware 
    {

        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                var statusCode = context.Response.StatusCode;
                Console.WriteLine("Test Error Middleware");
            }
            finally
            {
                var statusCode = context.Response.StatusCode;
                var msg = "";
                if (statusCode == 401)
                {
                    msg = "未授权";
                }
                else if (statusCode == 404)
                {
                    msg = "未找到服务";
                }
                else if (statusCode == 502)
                {
                    msg = "请求错误";
                }
                else if (statusCode != 200)
                {
                    msg = "未知错误";
                }
                if (!string.IsNullOrWhiteSpace(msg))
                {
                    await HandleExceptionAsync(context, statusCode, msg);
                }
            }
            
        }

        private static Task HandleExceptionAsync(HttpContext context, int statusCode, string msg)
        {
            var data = new { code = statusCode.ToString(), is_success = false, msg = msg };
            var result = JsonConvert.SerializeObject(new { data = data });
            context.Response.ContentType = "application/json;charset=utf-8";
            return context.Response.WriteAsync(result);
        }
    }
}




//OutputDto output = new OutputDto();
//                if (e is OneZeroException)
//                {
//                    output.Message = ((OneZeroException) e).Message;
//                    output.Code = ((OneZeroException) e).Code;
//                    output.ErrorMessage = ((OneZeroException) e).ErrorMsg;
//                }
//                else
//                {
//                    output.Message = "Unkown Error";
//                    output.Code = ResponseCode.UnExpectedException;
//                    output.StatusCode = (HttpStatusCode) context.Response.StatusCode;
//                }
