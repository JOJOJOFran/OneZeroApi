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
        Task<OutputModel> AddAsync<TIntputDto>(TIntputDto dto,
                                                Func<TIntputDto, bool> checkAction = null,
                                                Func<TIntputDto, Task<TEntity>> convertFunc = null) where TIntputDto : InputModel;

        /// <summary>
        /// 以DTO为载体批量插入实体
        /// </summary>
        /// <typeparam name="TIntputDto">添加DTO类型</typeparam>
        /// <param name="dtos">添加DTO信息集合</param>
        /// <param name="checkAction">合法性验证</param>
        /// <param name="convertFunc">Dto到实体转换</param>
        /// <returns>操作输出结果</returns>
        Task<OutputModel> AddAsync<TIntputDto>(ICollection<TIntputDto> dtos, 
                                                Func<TIntputDto, bool> checkAction = null, 
                                                Func<TIntputDto, Task<TEntity>> convertFunc = null) where TIntputDto : InputModel;

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
        Task<OutputModel> DeleteAsync(TKey key);

        /// <summary>
        /// 按条件从Id集合中选择删除
        /// </summary>
        /// <param name="checkAction">合法性验证</param>
        /// <param name="whereFunc">条件语句</param>
        /// <returns></returns>
        Task<OutputModel> DeleteAsync(ICollection<TKey> ids , 
                                       Func<TEntity, bool> checkAction = null, 
                                       Func<TEntity, TEntity> whereFunc=null);

        /// <summary>
        /// 按条件删除
        /// </summary>
        /// <param name="wherePredicate"></param>
        /// <returns></returns>
        Task<OutputModel> DeleteAsync(Expression<Func<TEntity, bool>> wherePredicate);

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
        Task<OutputModel> UpdateAsync<TEditDto>(TEditDto dto, 
                                                Func<TEditDto, TEntity, Task> checkAction = null, 
                                                Func<TEditDto, TEntity, Task<TEntity>> convertFunc = null) where TEditDto : InputModel;

        /// <summary>
        /// 异步以DTO为载体批量更新实体
        /// </summary>
        /// <typeparam name="TEditDto">更新DTO类型集合</typeparam>
        /// <param name="dtos"></param>
        /// <param name="checkAction"></param>
        /// <param name="convertFunc"></param>
        /// <returns></returns>
        Task<OutputModel> UpdateAsync<TEditDto>(ICollection<TEditDto> dtos,
                                                 Func<TEditDto,TEntity,Task> checkAction=null,
                                                 Func<TEditDto, TEntity, Task<TEntity>> convertFunc=null) where TEditDto : InputModel;


        /// <summary>
        /// 异步更新所有符合特定条件的实体
        /// </summary>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <param name="updateExpression">实体更新表达式</param>
        /// <returns>操作影响的行数</returns>
        Task<int> UpdateBatchAsync(Expression<Func<TEntity, bool>> predicate, 
                                   Expression<Func<TEntity, TEntity>> updateExpression);

        /// <summary>
        /// 检查实体是否存在
        /// </summary>
        /// <param name="predicate">查询表达式</param>
        /// <param name="id">Id</param>
        /// <returns></returns>
        Task<bool> CheckExistsAsync(Expression<Func<TEntity, bool>> predicate, TKey id = default(TKey));

        /// <summary>
        /// 按主键查找
        /// </summary>
        /// <param name="key">Id</param>
        /// <returns></returns>
        Task<TEntity> GetAsync(TKey key);
        #endregion

      

    }
}
