using Microsoft.EntityFrameworkCore;
using OneZero.Domain.Repositories;
using OneZero.EntityFrameworkCore.SqlServer.Extensions;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace OneZero.EntityFrameworkCore.SqlServer
{
    /// <summary>
    /// SqlServer EF Context
    /// </summary>
    public class MSSqlContext:DbContext,IDbContext
    {
        public MSSqlContext(DbContextOptions<MSSqlContext> options) : base(options)
        {

        }

        /// <summary>
        /// OnConfiguring方法每次创建实例都要被调用
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        /// <summary>
        /// 加载所有配置了IEntityTypeConfiguration<TEntity>接口的配置类，生成表结构
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.AddEntityConfigFromAssembly();
            base.OnModelCreating(builder);
        }


    }
}
