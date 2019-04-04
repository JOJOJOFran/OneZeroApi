using System;
using System.Threading.Tasks;

namespace OneZero.Domain
{
    /// <summary>
    /// 工作单元接口
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// 是否提交
        /// </summary>
        bool HasCommited { get; }

        /// <summary>
        /// 根据类型获取数据库上下文
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <returns></returns>
        IDbContext GetDbContext<TEntity, TKey>() where TEntity : IEntity<TKey>;

        /// <summary>
        /// 根据类型获取数据库上下文
        /// </summary>
        /// <param name="entityType">实体类型，不填写的话则代表获取默认数据库</param>
        /// <returns></returns>
        IDbContext GetDbContext(Type entityType = null);

        /// <summary>
        /// 根据实体类型获取仓储
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <returns></returns>
        IRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : class, IEntity<TKey>;

        /// <summary>
        /// 提交
        /// </summary>
        Task<int> CommitAsync();

        /// <summary>
        /// 开始事务
        /// </summary>
        /// <returns></returns>
        Task BeginTransAsync();

        /// <summary>
        /// 回滚(暂时不实现)
        /// </summary>
        Task RollbackAsync();

    }
}
