using Microsoft.AspNetCore.Http;
using OneZero.Common.Extensions;
using OneZero.Exceptions;
using OneZero.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OneZero.AspNetCore.Middlewares
{
    /// <summary>
    /// 框架中间件
    /// </summary>
    public class OneZeroMiddleware
    {
        private readonly RequestDelegate _next;
        private OneZeroContext _oneZeroContext;


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
        public async Task Invoke(HttpContext context, OneZeroContext oneZeroContext, OneZeroOption oneZeroOption)
        {
            _oneZeroContext = oneZeroContext;
            _oneZeroContext.IsAuththentic = oneZeroOption.IsAuthentic.CastTo(false);
            _oneZeroContext.RequestIP = context.Connection.RemoteIpAddress.ToString();
            _oneZeroContext.ActionPath = context.Request.Path;
            //是否开启身份验证
            if (_oneZeroContext.IsAuththentic)
            {
                _oneZeroContext.TokenIP = context.User.HasClaim(v => v.Type == "ip") ? context.User.Claims.Where(v => v.Type == "ip").Select(v => v.Value).First() : "";
                _oneZeroContext.UserId = context.User.HasClaim(v => v.Type == "userid") ? context.User.Claims.Where(v => v.Type == "userid").Select(v => v.Value).First().CastTo(default(Guid)) : default(Guid);
                _oneZeroContext.TenanId = context.User.HasClaim(v => v.Type == "tenanid") ? context.User.Claims.Where(v => v.Type == "tenanid").Select(v => v.Value).First().CastTo(default(Guid)) : oneZeroOption.DefaultTenanId.ConvertToGuid("TenanId配置");
                _oneZeroContext.MenuList = context.User.Claims.Where(v => v.Type == "menus")?.Select(v => v.Value);
                _oneZeroContext.RoleList = context.User.Claims.Where(v => v.Type == "roles")?.Select(v => v.Value);
                _oneZeroContext.PermissionList = context.User.Claims.Where(v => v.Type == "permissions")?.Select(v => v.Value);
                //通过token携带信息判断，该请求是否合法
                TokenValidate();
            }
            else
            {
                //默认租户ID
                _oneZeroContext.TenanId = oneZeroOption.DefaultTenanId.ConvertToGuid("TenanId配置");
            }
            await _next(context);
        }


        private void TokenValidate()
        {
            //if (_oneZeroContext.RequestIP != _oneZeroContext.TokenIP)
            //{
            //    throw new OneZeroException("检测到您登陆的地址不同，请重新登陆", HttpStatusCode.Redirect);
            //}
        }
    }
}
