using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OneZero.Entity.EntityConfiguration.Extension;

namespace OneZero.Entity.DatabaseContext.SqlContext
{
    public class MSSqlContext:DbContext, IMSSqlContext
    {


        public MSSqlContext(DbContextOptions<MSSqlContext> options) :base(options)
        {
 
         
            //var sql= DbLoggerCategory.Database.Command.Name;
            var name=DbLoggerCategory.Database.Connection.Name;
            //var trans=DbLoggerCategory.Database.Transaction.Name;
            //EntityState

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
    }
}