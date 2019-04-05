using Microsoft.EntityFrameworkCore;
using OneZero.Domain;
using OneZero.EntityFrameworkCore.Extensions;
using OneZero.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.EntityFrameworkCore
{
    public class DefaultDbContext : DbContext, IDbContext
    {
        //private readonly OneZeroOption _oneZeroOption;
        //private readonly DbContextOptions<DefaultDbContext> _options;
        //private DbContextOptionsBuilder _optionsBuilder;
        public DefaultDbContext(DbContextOptions<DefaultDbContext> options) : base(options)  //, OneZeroOption oneZeroOption
        {
            //_options = options;
            //_oneZeroOption = oneZeroOption;
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
        /// 创建上下文数据模型时，对各个实体类的数据库映射细节进行配置
        /// </summary>
        /// <param name="modelBuilder">上下文数据模型构建器</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //if (_oneZeroOption.DbContextCenter.ContainsKey(typeof(DefaultDbContext)))
            //{
            //    foreach (var item in _oneZeroOption.DbContextCenter[typeof(DefaultDbContext)].EntityInstanceList)
            //    {
            //        dynamic instance = Activator.CreateInstance(item);
            //        modelBuilder.ApplyConfiguration(instance);
            //    }
            //}
            //else
            //{
            //    DbContextOption dbContextOption = new DbContextOption();
            //    dbContextOption.DBType = Enums.DatabaseType.SqlServer;
            //    dbContextOption.ConnectString= _optionsBuilder.Options.
            //    dbContextOption.EntityInstanceList=new System.Collections.Concurrent.ConcurrentBag<Type>();
            //    _oneZeroOption.DbContextCenter.GetOrAdd(typeof(DefaultDbContext), dbContextOption);
            modelBuilder.AddEntityConfigFromAssembly();
            //}

            base.OnModelCreating(modelBuilder);
        }
    }
}
