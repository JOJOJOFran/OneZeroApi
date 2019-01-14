using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using OneZero.Domain.Entities;
using OneZero.Domain.Models;

namespace OneZero.Domain.Repositories
{
    /// <summary>
    /// 仓储层接口
    /// </summary>
    public interface IRepository<TEntity,TKey> where TEntity:IEntity<TKey>
    {
        /// <summary>
        /// 从工作单元获取上下文
        /// </summary>
        /// <value></value>
        IUnitOfWork UnitOfWork{get;}

        #region 异步操作
        Task<int> AddAsync(params TEntity[] entities);
        Task<int> DeleteAsync(params TEntity[] entities);
        Task<int> DeleteAsync(TKey key);        
        Task<int> DeleteAsync(ICollection<TKey> ids);
        Task<IOutputModel> DeleteAsync(Func<TEntity, bool> whereFunc);
        Task<IOutputModel> DeleteAsync(Expression<Func<TEntity, bool>> wherePredicate);
        #endregion

    }
}
