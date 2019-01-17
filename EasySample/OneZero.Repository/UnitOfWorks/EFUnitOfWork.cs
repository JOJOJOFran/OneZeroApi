using OneZero.Domain.Entities;
using OneZero.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Repository.UnitOfWorks
{
    public class EFUnitOfWork : IUnitOfWork
    {
        public bool HasCommited { get; set; } = false;

        public void Commit()
        {
            HasCommited = true;
        }


        public IDbContext GetDbContext<TEntity, TKey>() where TEntity : IEntity<TKey>
        {
            return null;
        }

        public void Rollback()
        {
            HasCommited = false;
        }
    }
}
