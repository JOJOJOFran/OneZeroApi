using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OneZero.Domain.Entities;
using OneZero.Domain.Models;
using OneZero.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OneZero.Repository.Repositories
{
    /// <summary>
    /// EF基础仓储层
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public class EFBaseRepository<TEntity, TKey> : IRepository<TEntity, TKey> 
                                                   where TEntity : class, IEntity<TKey> 
                                                   where TKey : IEquatable<TKey>
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;
        private readonly ILogger _logger;

        public IUnitOfWork UnitOfWork { get; }

        public EFBaseRepository()
        {
            _dbContext = (DbContext)UnitOfWork.GetDbContext<TEntity, TKey>();
            _dbSet = _dbContext.Set<TEntity>();
        }

        public EFBaseRepository(IServiceProvider serviceProvider)
        {
            //UnitOfWork = serviceProvider.GetUnitOfWork<TEntity, TKey>();
            _dbContext = (DbContext)UnitOfWork.GetDbContext<TEntity, TKey>();
            _dbSet = _dbContext.Set<TEntity>();
            //_logger=serviceProvider.GetLogger<Repository<TEntity, TKey>>();
        }

        public virtual IQueryable<TEntity> Entities
        {
            get
            {
                return _dbSet.AsQueryable().AsNoTracking().Where(GetDataFilter());
            }
        }

        public virtual IQueryable<TEntity> TrackingEntities
        {
            get
            {
                return _dbSet.AsQueryable().Where(GetDataFilter());
            }
        }

        private static Expression<Func<TEntity, bool>> GetDataFilter()
        {
            // return FilterHelper.GetDataFilterExpression<TEntity>(operation: operation);
            return null;
        }

        #region 异步方法
        public virtual async Task<int> AddAsync(params TEntity[] entities)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<IOutputModel> AddAsync<TIntputDto>(TIntputDto dto, Func<TIntputDto, Task> checkAction = null, Func<TIntputDto, TEntity, Task<TEntity>> convertFunc = null) where TIntputDto : IInputModel
        {
            throw new NotImplementedException();
        }

        public virtual async Task<IOutputModel> AddAsync<TIntputDto>(ICollection<TIntputDto> dtos, Func<TIntputDto, Task> checkAction = null, Func<TIntputDto, TEntity, Task<TEntity>> convertFunc = null) where TIntputDto : IInputModel
        {
            throw new NotImplementedException();
        }

        public virtual async Task<bool> CheckExistsAsync(Expression<Func<TEntity, bool>> predicate, TKey id = default(TKey))
        {
            throw new NotImplementedException();
        }

        public virtual async Task<int> DeleteAsync(params TEntity[] entities)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<int> DeleteAsync(TKey key)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<IOutputModel> DeleteAsync(ICollection<TKey> ids, Func<TEntity, Task> checkAction = null, Func<TEntity, TEntity> whereFunc = null)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<IOutputModel> DeleteAsync(Expression<Func<TEntity, bool>> wherePredicate)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<TEntity> GetAsync(TKey key)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<int> UpdateAsync(params TEntity[] entities)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<IOutputModel> UpdateAsync<TEditDto>(TEditDto dto, Func<TEditDto, TEntity, Task> checkAction = null, Func<TEditDto, TEntity, Task<TEntity>> convertFunc = null) where TEditDto : IInputModel
        {
            throw new NotImplementedException();
        }

        public virtual async Task<IOutputModel> UpdateAsync<TEditDto>(ICollection<TEditDto> dtos, Func<TEditDto, TEntity, Task> checkAction = null, Func<TEditDto, TEntity, Task<TEntity>> convertFunc = null) where TEditDto : IInputModel
        {
            throw new NotImplementedException();
        }

        public virtual async Task<int> UpdateBatchAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TEntity>> updateExpression)
        {
            throw new NotImplementedException();
        }
        #endregion 
    }
}
