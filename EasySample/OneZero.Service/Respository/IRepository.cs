﻿using Microsoft.EntityFrameworkCore;
using OneZero.Entity;
using OneZero.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OneZero.Service.Repository
{
    /// <summary>
    /// 仓储层接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IRepository<TEntity, TKey> where TEntity : BaseEntity<TKey> where TKey : IEquatable<TKey>
    {
        DbContext Context { get; }
        IQueryable<TEntity> Entities { get; }
        IQueryable<TEntity> EntitieWithTracking { get; }

        #region IEnumerable<T>类型操作
        #region Insert
        /// <summary>
        /// 批量插入实体
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        /// <returns>影响行数</returns>
        Task<int> AddAndGetCountAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// 插入实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>影响行数</returns>
        Task<Dto<DtoData>> AddAsync(TEntity entity);

        /// <summary>
        /// 批量插入实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>业务操作结果</returns>
        Task<Dto<DtoData>> AddRangeAsync(IEnumerable<TEntity> entity);
        #endregion

        #region Update
        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="entities">实体对象</param>
        /// <returns>影响行数</returns>
        Task<int> GetUpdateCountAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// 更新指定实体
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns>业务操作结果</returns>
        Task<Dto<DtoData>> UpdateAsync(TEntity entity);

        /// <summary>
        /// 更新指定主键的
        /// </summary>
        /// <param name="key"></param>
        /// <param name="updatExpression"></param>
        /// <returns>业务操作结果</returns>
        Task<Dto<DtoData>> UpdateAsync<TInputDto>(TInputDto inputDto, Func<TInputDto, TEntity, TEntity> convertExpression, TKey key);
        #endregion

        #region Delete
        /// <summary>
        /// 根据键集合删除并获取影响行数
        /// </summary>
        /// <param name="keys"></param>
        /// <returns>影响行数</returns>
        Task<int> GetDeleteCountAsync(IEnumerable<TKey> keys);

        /// <summary>
        /// 根据键删除
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        Task<Dto<DtoData>> DeleteAsync(TKey key);


        /// <summary>
        /// 根据键集合删除
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        Task<Dto<DtoData>> DeleteRangeAsync(IEnumerable<TKey> keys);

        /// <summary>
        /// 根据条件
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<Dto<DtoData>> DeleteAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 标记删除
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task<Dto<DtoData>> RecycleAsync(TKey key);
        #endregion
        #region
        IEnumerable<TEntity> GetListAsync(Func<TEntity, bool> whereFunc);

        TEntity GetItemAsync(Func<TEntity, bool> whereFunc);
        #endregion
        #endregion

        #region IQueruable<T>类型操作
        #region Insert
        #endregion

        #region Update
        /// <summary>
        /// 更新指定条件的数据
        /// </summary>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <param name="updatExpression">更新属性表达式</param>
        /// <returns>业务操作结果</returns>
        //Task<IDto> UpdateAsync(Expression<Func<TEntity, bool>> predicate, Func<IEnumerable<TEntity>, IEnumerable<TEntity>> updatExpression);
        #endregion
        #endregion

        Task<int> SaveChangesAsync();
    }
}