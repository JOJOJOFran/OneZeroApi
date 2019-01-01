using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OneZero.Entity;
using OneZero.Model;

namespace OneZero.Service.Respository
{
    public class BaseRespository<TEntity, TKey> : IRespository<TEntity, TKey> where TEntity : BaseEntity<TKey> where TKey : IEquatable<TKey>
    {
        public DbContext Context => throw new NotImplementedException();

        public IQueryable<TEntity> Entities => throw new NotImplementedException();

        public IQueryable<TEntity> EntitieWithTracking => throw new NotImplementedException();

        public virtual Task<int> AddAndGetCountAsync(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public virtual Task<IDto> AddAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public virtual  Task<IDto> AddRangeAsync(IEnumerable<TEntity> entity)
        {
            throw new NotImplementedException();
        }

        public virtual Task<IDto> DeleteAsync(TKey key)
        {
            throw new NotImplementedException();
        }

        public virtual  Task<IDto> DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public virtual Task<IDto> DeleteRangeAsync(IEnumerable<TKey> keys)
        {
            throw new NotImplementedException();
        }

        public virtual Task<int> GetDeleteCountAsync(IEnumerable<TKey> keys)
        {
            throw new NotImplementedException();
        }

        public virtual TEntity GetItemAsync(Func<TEntity, bool> whereFunc)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<TEntity> GetListAsync(Func<TEntity, bool> whereFunc)
        {
            throw new NotImplementedException();
        }

        public virtual Task<int> GetUpdateCountAsync(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public virtual Task<IDto> RecycleAsync(TKey key)
        {
            throw new NotImplementedException();
        }

        public virtual Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public virtual Task<IDto> UpdateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public virtual Task<IDto> UpdateAsync<TInputDto>(TInputDto inputDto, Func<TInputDto, TEntity, TEntity> convertExpression, TKey key)
        {
            throw new NotImplementedException();
        }
    }
}
