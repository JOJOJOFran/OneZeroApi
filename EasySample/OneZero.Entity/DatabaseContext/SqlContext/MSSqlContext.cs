using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;
using OneZero.Entity.EntityConfiguration.Extension;

namespace OneZero.Entity.DatabaseContext.SqlContext
{
    public class MSSqlContext:DbContext, IMSSqlContext
    {
        public MSSqlContext(DbContextOptions<MSSqlContext> options) :base(options)
        {
       
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.AddEntityConfigurationFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
    }
}