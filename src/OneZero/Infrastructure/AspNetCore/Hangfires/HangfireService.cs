using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Hangfire;

namespace OneZero.AspNetCore.Hangfires
{
    public class HangfireService
    {
        private readonly IServiceProvider _serviceProvider;
        public HangfireService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }




        /// <summary>
        /// 获取所有可以用作Hangfier任务的服务类型
        /// </summary>
        /// <param name="name"></param>
        public void GetAllService(string name)
        {
            var service= _serviceProvider.GetService(Type.GetType("name"));
            if (service.GetType().IsSubclassOf(typeof(IHangfireJobsService)))
            {

            }
        }


    


        public void RecurringJobAddorUpdate(string JobId, Expression<Action> expression,string cron)
        {
            JobId = $"RecurringJob:{JobId}";
            RecurringJob.AddOrUpdate(JobId, expression, cron);
        }

        public void RecurringJobRemoveIfExists(string JobId)
        {
            JobId = $"RecurringJob:{JobId}";
            RecurringJob.RemoveIfExists(JobId);
        }

    }
}
