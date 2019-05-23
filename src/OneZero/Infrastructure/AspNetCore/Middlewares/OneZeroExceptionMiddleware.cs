using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OneZero.Dtos;
using OneZero.Enums;
using OneZero.Exceptions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OneZero.AspNetCore.Middlewares
{
    public class OneZeroExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<OneZeroExceptionMiddleware> _logger;

        public OneZeroExceptionMiddleware(RequestDelegate next, ILogger<OneZeroExceptionMiddleware> logger)

        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            Exception exception = null;
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                exception = e;
            }
            finally
            {
                if (exception != null)
                {
                    await HandleExceptionAsync(context, exception);
                }
            }

        }


        private Task HandleExceptionAsync(HttpContext context, Exception e)
        {
            var exeptionOutput = new OutputDto();
            if (e is OneZeroException)
            {
                exeptionOutput.Code = ((OneZeroException)e).Code;
                exeptionOutput.Message = ((OneZeroException)e).Message;
                exeptionOutput.ErrorMessage = ((OneZeroException)e)?.GetBaseException().Message;
                LogExceptionByResponseCode(exeptionOutput.Message, exeptionOutput.Code);
            }
            else
            {
                string message = "系统消息：未知错误";
                switch (context.Response.StatusCode)
                {
                    case 401:
                        message = "系统消息：没有访问权限";
                        break;
                    case 404:
                        message = "系统消息：未找到服务";
                        break;
                    case 500:
                        message = "系统消息：内部错误";
                        break;
                }
                exeptionOutput.Message = message;
                exeptionOutput.ErrorMessage = e.GetBaseException()?.Message;
                exeptionOutput.Code = ResponseCode.UnExpectedException;
                _logger.LogWarning(message,e);
            }

            exeptionOutput.StatusCode = (HttpStatusCode)context.Response.StatusCode;
            var result = JsonConvert.SerializeObject(exeptionOutput);
            context.Response.ContentType = "application/json;charset=utf-8";
            return context.Response.WriteAsync(result);
        }


        private void LogExceptionByResponseCode(string message, ResponseCode Code)
        {
            switch (Code)
            {
                case ResponseCode.ExpectedException:
                    _logger.LogDebug(message);
                    break;
                case ResponseCode.UnExpectedException:
                    _logger.LogWarning(message);
                    break;
                case ResponseCode.Error:
                    _logger.LogError(message);
                    break;
                case ResponseCode.Fatal:
                    _logger.LogCritical(message);
                    break;
            }
        }

    }
}
