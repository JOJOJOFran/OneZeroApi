using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using OneZero.EntityFrameworkCore.Extensions;
using OneZero.Common.Exceptions;
using OneZero.Domain.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using OneZero.Domain.Models;
using OneZero.EntityFrameWorkCore.Repositories;

namespace OneZero.EntityFrameworkCore.UnitOfWorks
{
    /// <summary>
    /// EF工作单元（Scope,如果想注册成单例，Hashtable需换成线程安全的哈希表）
    /// </summary>
    public class EFUnitOfWork : IUnitOfWork
    {
        #region Field
        public bool HasCommited { get; private set; } = false;
        private readonly IServiceProvider _provider;
        public DbContext DbContext => GetDbContext();
        //public IDbContextTransaction _transaction { get; set; }
        private Hashtable repositories;
        #endregion

        #region Ctor
        public EFUnitOfWork(IServiceProvider provider)
        {
            _provider = provider;
        }
        #endregion

        #region Func
        /// <summary>
        /// 获取数据库上下文
        /// 后面如果多上下文则需要根据实体类型去获得上下文，暂时这里给空
        /// </summary>
        /// <param name="entityType">实体类型</param>
        /// <returns></returns>
        private DbContext GetDbContext(Type entityType=null)
        {
          return  _provider.GetEFDbContext();
        }


        /// <summary>
        /// 根据实体类型得到对应仓储类
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <returns></returns>
        public IRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : IEntity<TKey> where TKey : IEquatable<TKey>
        {
            if (repositories == null)
                repositories = new Hashtable();

            var entityType = typeof(TEntity);
            if (!repositories.ContainsKey(entityType.Name))
            {
                var baseType = typeof(EFRepository<,>);
                var repositoryInstance = Activator.CreateInstance(baseType.MakeGenericType(entityType), DbContext);
                repositories.Add(entityType.Name,repositoryInstance);
            }
            if (!repositories.ContainsKey(entityType.Name))
                throw new OneZeroException();

            return (IRepository<TEntity, TKey>)repositories[entityType.Name];
        }
        #endregion

        #region Interface Implemention
        /// <summary>
        /// 工作单元提交事务
        /// </summary>
        /// <returns></returns>
        public async Task<int> CommitAsync()
        {
            if (HasCommited)
                return 0;

            int result = 0;
            try
            {
                result = await DbContext.SaveChangesAsync();
                HasCommited = true;  
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException != null && e.InnerException.InnerException is SqlException)
                {
                    //throw ..
                }
                throw new OneZeroException();
            }

            return await Task.FromResult(result);
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }

        /// <summary>
        /// 回滚：暂时不具体实现
        /// </summary>
        /// <returns></returns>
        public async Task RollbackAsync()
        {
            HasCommited = false;
        }
        #endregion
    }
}
