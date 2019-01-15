using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Entity
{
    public class EFLogger : ILogger
    {
        private readonly string categoryName;
        private ILogger _logger;

        public EFLogger(string categoryName, ILogger logger)   // ILogger<EFLogger> logger
        {
           this.categoryName = categoryName;
            _logger = logger;
        }

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {

            var logContent = formatter(state, exception);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(logContent);

            if (categoryName == "Microsoft.EntityFrameworkCore.Database.Connection"
                   && logLevel == LogLevel.Information)
            {
                 logContent = formatter(state, exception);

                //TODO: 拿到日志内容想怎么玩就怎么玩吧
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(logContent);
                Console.ResetColor();
            }

            if (categoryName == "Microsoft.EntityFrameworkCore.Database.Transaction"
                   && logLevel == LogLevel.Information)
            {
                 logContent = formatter(state, exception);
               // var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
                //TODO: 拿到日志内容想怎么玩就怎么玩吧
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(logContent);
                Console.ResetColor();
            }

            //ef core执行数据库查询时的categoryName为Microsoft.EntityFrameworkCore.Database.Command,日志级别为Information
            if (categoryName == "Microsoft.EntityFrameworkCore.Database.Command"
                    && logLevel == LogLevel.Information)
            {
                 logContent = formatter(state, exception);
                //测试玩儿
                //   _logger.LogCritical(logContent);
                //TODO: 拿到日志内容想怎么玩就怎么玩吧
             
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(logContent);
                Console.ResetColor();
            }
        }

        public IDisposable BeginScope<TState>(TState state) => null;
    }
}
