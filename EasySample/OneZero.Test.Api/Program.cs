using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;
using OneZero.Entity;

namespace OneZero.Test.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            try
            {
                logger.Info("Init Log API Information");
                logger.Info("111");
                CreateWebHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Stop Log Information Because Of Exception");
                throw;
            }
            finally
            {
                LogManager.Shutdown();
            }
            //CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
             .ConfigureLogging(logging =>
             {
                 logging.ClearProviders();
                 //logging.AddProvider(new EFLoggerProvider());
                 logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Information);
             })
            .UseNLog()  // NLog: setup NLog for Dependency injection
            .UseStartup<Startup>();
    }
}
