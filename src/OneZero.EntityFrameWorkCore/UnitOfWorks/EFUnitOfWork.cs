using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using OneZero.EntityFrameworkCore.Extensions;
using OneZero.Common.Exceptions;
using OneZero.Common.Enums;
using OneZero.Domain.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using OneZero.Domain.Models;
using OneZero.EntityFrameworkCore.Repositories;

namespace OneZero.EntityFrameworkCore.UnitOfWorks
{
    /// <summary>
    /// EF工作单元（Scope）
    /// </summary>
    public class EFUnitOfWork : IUnitOfWork
    {
        #region Field
        public bool HasCommited { get; private set; } = false;
        private readonly IServiceProvider _provider;
        private IDbContext DbContext => GetDbContext();
        private IDbContextTransaction _trans { get; set; }
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
        /// 后面如果多上下文则需要根据实体类型去获得上下文，暂时这里给空取默认的注入DbContext
        /// </summary>
        /// <param name="entityType">实体类型</param>
        /// <returns></returns>
        public virtual IDbContext GetDbContext(Type entityType=null)
        {
            return  _provider.GetDbContext();
        }

        /// <summary>
        /// 获取数据库上下文
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <returns></returns>
        public virtual IDbContext GetDbContext<TEntity, TKey>() where TEntity : IEntity<TKey>
        {
            var type = typeof(TEntity);
            return GetDbContext(type);
        }

        /// <summary>
        /// 手动开启事务
        /// </summary>
        /// <returns></returns>
        public virtual async Task BeginTransAsync()
        {
             _trans =await ((DbContext)DbContext).Database.BeginTransactionAsync();
        }

        /// <summary>
        /// 根据实体类型得到对应仓储类
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <returns></returns>
        public virtual IRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : class,IEntity<TKey> 
        {
            if (repositories == null)
                repositories = new Hashtable();

            var entityType = typeof(TEntity);
            try
            {
                if (!repositories.ContainsKey(entityType.Name))
                {
                    var baseType = typeof(EFRepository<,>);
<<<<<<< HEAD
                   
                    var repositoryInstance = Activator.CreateInstance(baseType.MakeGenericType(entityType, typeof(TKey)), DbContext,_provider.GetLogger<EFRepository<TEntity, TKey>>());
=======
                    var repositoryInstance = Activator.CreateInstance(baseType.MakeGenericType(entityType), DbContext,_provider.GetLogger< EFRepository <TEntity,TKey>> ());
>>>>>>> master
                    repositories.Add(entityType.Name, repositoryInstance);
                }
            }
            catch (Exception e)
            {
                throw new OneZeroException("UnitOfWork构建仓储示例失败",e, ResponseCode.Error);
            }
          

            return (IRepository<TEntity, TKey>)repositories[entityType.Name];
        }
        #endregion

        #region Interface Implemention
        /// <summary>
        /// 工作单元提交事务
        /// </summary>
        /// <returns></returns>
        public virtual async Task<int> CommitAsync()
        {
            if (HasCommited)
                return 0;

            int result = 0;
            try
            {
                if (_trans != null)
                    _trans.Commit();
                HasCommited = true;  
            }
            catch (DbUpdateException e)
            {
                result = -1;
                _trans.Rollback();
                if (e.InnerException != null && e.InnerException.InnerException is SqlException)
                {
                    throw new OneZeroException("EFUnitOfWork提交事务失败", e, ResponseCode.UnExpectedException);
                }
                throw new OneZeroException();
            }

            return await Task.FromResult(result);
        }

        public virtual void Dispose()
        {
            if (_trans != null)
                _trans.Dispose();
            
        }

        /// <summary>
        /// 回滚：暂时不具体实现
        /// </summary>
        /// <returns></returns>
        public virtual async Task RollbackAsync()
        {
            if (_trans != null)
            {
                _trans.Rollback();
                _trans.Dispose();
            }              
            HasCommited = false;
           
        }

        #endregion
    }
}
