﻿using OneZero.Domain.Dtos;
using OneZero.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OneZero.Domain.Repositories
{
    public interface IRepository<TEntity, TKey> where TEntity : IEntity<TKey> where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// 工作单元
        /// </summary>
        IUnitOfWork _unitOfWork { get; }

        #region 数据库操作

        #region 异步操作
        #region 新增操作
        /// <summary>
        /// 插入实体
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        /// <returns>操作影响行数</returns>
        Task<int> AddAsync(params TEntity[] entities);

        /// <summary>
        /// 以DTO为载体插入实体
        /// </summary>
        /// <typeparam name="TIntputDto">添加DTO类型</typeparam>
        /// <param name="dto">添加DTO信息</param>
        /// <param name="checkAction">合法性验证</param>
        /// <param name="convertFunc">Dto到实体转换</param>
        /// <returns>操作输出结果</returns>
        Task<OutputDto> AddAsync<TInputDto>(TInputDto dto,
                                            Func<TInputDto, Task<bool>> checkAction = null,
                                            Func<TInputDto, Task<TEntity>> convertFunc = null) where TInputDto : InputDto;

        //Task<OutputDto> AddRangeAsync 暂时不写批量操作
        #endregion

        #region 删除操作
        /// <summary>
        /// 异步删除实体
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        /// <returns>操作影响的行数</returns>
        Task<int> DeleteAsync(params TEntity[] entities);

        /// <summary>
        /// 按ID删除实体
        /// </summary>
        /// <param name="key"></param>
        /// <returns>操作影响的行数</returns>
        Task<OutputDto> DeleteAsync(TKey key);

        /// <summary>
        /// 按条件从Id集合中选择删除
        /// </summary>
        /// <param name="checkAction">合法性验证</param>
        /// <param name="whereFunc">条件语句</param>
        /// <returns></returns>
        Task<OutputDto> DeleteAsync(ICollection<TKey> ids,
                                       Func<TEntity, Task<bool>> checkAction = null,
                                       Func<TEntity, TEntity> whereFunc = null);

        /// <summary>
        /// 按条件删除
        /// </summary>
        /// <param name="wherePredicate"></param>
        /// <returns></returns>
        Task<OutputDto> DeleteAsync(Expression<Func<TEntity, Task<bool>>> wherePredicate);

        #endregion

        #region 更新操作
        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task<int> UpdateAsync(params TEntity[] entities);

        /// <summary>
        /// 异步以DTO为载体更新实体
        /// </summary>
        /// <typeparam name="TEditDto">更新DTO类型</typeparam>
        /// <param name="dto"></param>
        /// <param name="checkAction"></param>
        /// <param name="convertFunc"></param>
        /// <returns></returns>
        Task<OutputDto> UpdateAsync<TEditDto>(TEditDto dto,
                                              Func<TEditDto, TEntity, Task<TEntity>> convertFunc = null) where TEditDto : InputDto;

        /// <summary>
        /// 异步更新所有符合特定条件的实体
        /// </summary>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <param name="updateExpression">实体更新表达式</param>
        /// <returns>操作影响的行数</returns>
        Task<int> UpdateBatchAsync(Expression<Func<TEntity, bool>> predicate,
                                   Expression<Func<TEntity, TEntity>> updateExpression);
        #endregion

        /// <summary>
        /// 按主键查找
        /// </summary>
        /// <param name="key">Id</param>
        /// <returns></returns>
        Task<TEntity> GetAsync(TKey key);

        #endregion
        #endregion
    }
}