using Microsoft.AspNetCore.Mvc.Filters;
using OneZero.Api.Model.Log;
using StackExchange.Redis;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneZero.Api.Filter
{
    /// <summary>
    /// 审计特性
    /// </summary>
    public class AuditAttribute:ActionFilterAttribute
    {
        private readonly string _moduleName;
        private readonly string _pageName;
        private readonly string _actionName;
        private readonly bool _isOpenLog;
        private readonly bool _isOpenRedis;
        private AuditModel audit;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="moduleName">模块名称</param>
        /// <param name="actionName">控制器Action名称</param>
        /// <param name="pageName">页面名称</param>
        /// <param name="isOpenLog">是否记录日志</param>
        /// <param name="isUseRedis">是否使用Redis记录日志</param>
        public AuditAttribute(string moduleName,  string actionName, string pageName = null, bool isOpenLog=true,bool isOpenRedis = false)
        {
            _moduleName = moduleName;
            _pageName = pageName;
            _actionName = actionName;
            _isOpenLog = isOpenLog;
            _isOpenRedis = isOpenRedis;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!_isOpenLog)
                return;

            audit = new AuditModel();
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                audit.UserID = "";//context.HttpContext.User.UserIdentity();
                audit.UserName = "";
            }

            audit.ModuleName = _moduleName;
            audit.PageName = _pageName?? context.HttpContext.Request.Headers["PageName"];
            audit.ActionName = _actionName;
            audit.OpretionTime = DateTime.Now;
            audit.RequestIP = context.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            audit.ClinetName= System.Net.Dns.GetHostEntry(context.HttpContext.Connection.RemoteIpAddress).HostName;          
            audit.RequestUrl = context.HttpContext.Request.Path+ "/"+context.HttpContext.Request.Method;

            var redis = new CSRedis.CSRedisClient("127.0.0.1:6379,defaultDatabase=11,poolsize=10");

            ConnectionMultiplexer conn = ConnectionMultiplexer.Connect("localhost");
            IDatabase db = conn.GetDatabase(0);
            db.HashSetAsync("AuditLog", DateTime.Now.ToLongTimeString(),JsonConvert.SerializeObject(audit));
            base.OnActionExecuting(context);
        }

        #region 异步方法
        //public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        //{


        //    return base.OnActionExecutionAsync(context, next);
        //}

        //public override Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        //{
        //    return base.OnResultExecutionAsync(context, next);
        //}
        #endregion
    }
}
