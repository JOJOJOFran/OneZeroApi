using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OneZero.Domain.Repositories;
using OneZero.EntityFramwork.EntityConfiguration.Extension;

namespace OneZero.EntityFramwork.DatabaseContext.EFContext
{
    /// <summary>
    /// SqlServer数据库上下文
    /// </summary>
    public class MSSqlContext:DbContext, IDbContext, IMSSqlContext
    {

        public MSSqlContext(DbContextOptions<MSSqlContext> options) :base(options)
        {

        }
     
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //LoggerFactory loggerFactory = new LoggerFactory();
            //loggerFactory.AddConsole(LogLevel.Debug);
            //optionsBuilder.UseLoggerFactory(loggerFactory);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.AddEntityConfigurationFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }

        #region 实现IDbContext接口方法
        public async Task<int> SaveChangesAsync(CancellationToken token = default(CancellationToken))
        {
            return await base.SaveChangesAsync(token);
        }

        public int SaveChanges()
        {
            return base.SaveChanges();
        }
        #endregion

    }

    public  interface IMSSqlContext
    {
    }
}