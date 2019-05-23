using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OneZero.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.EntityFrameworkCore
{
    public abstract class EntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>, IEntityRegister where TEntity:class
    {
        public Type DbContextType { get; set; }

        /// <summary>
        /// 重写以实现实体类型各个属性的数据库配置
        /// </summary>
        /// <param name="builder">实体类型创建器</param>
        public abstract void Configure(EntityTypeBuilder<TEntity> builder);

        /// <summary>
        /// 注册到实体上下文管理中心
        /// </summary>
        public void RegisterToManageCenter()
        {
            if (!EntityContextManageCenter.EntityContextDic.ContainsKey(typeof(TEntity)))
            {
                EntityContextManageCenter.EntityContextDic.Add(typeof(TEntity), DbContextType);
            }
        }
    }
}
