using Microsoft.EntityFrameworkCore;
using OneZero.Domain;
using OneZero.EntityFrameworkCore.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.EntityFrameworkCore
{
    /// <summary>
    /// 数据库上下文
    /// </summary>
    public abstract class DbContextBase: DbContext,IDbContext
    {

        public DbContextBase(DbContextOptions<DbContextBase> options) : base(options)
        {

        }


        /// <summary>
        /// 创建上下文数据模型时，对各个实体类的数据库映射细节进行配置
        /// </summary>
        /// <param name="modelBuilder">上下文数据模型构建器</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.AddEntityConfigFromAssembly();
            base.OnModelCreating(modelBuilder);
        }
    }
}
