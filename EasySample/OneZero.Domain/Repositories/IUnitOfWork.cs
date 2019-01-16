using System;
using System.Collections.Generic;
using System.Text;
using OneZero.Domain.Entities;

namespace OneZero.Domain.Repositories 
{
    public interface IUnitOfWork
    {
        bool HasCommited{get;}

        IDbContext GetDbContext<TEntity, TKey> () where TEntity : IEntity<TKey>;
        //IDbContext GetDbContext (Type entityType);

        /// <summary>
        /// 提交当前上下文的事务更改
        /// </summary>
        void Commit ();

        /// <summary>
        /// 回滚所有事务
        /// </summary>
        void Rollback ();
    }
}