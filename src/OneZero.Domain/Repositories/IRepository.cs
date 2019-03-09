using OneZero.Common.Dtos;
using OneZero.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OneZero.Domain.Repositories
{
    public interface IRepository<TEntity, TKey> where TEntity : IEntity<TKey> 
    {
        /// <summary>
        /// 工作单元
        /// </summary>
        IUnitOfWork _unitOfWork { get; }
        
        /// <summary>
        /// 延迟加载实体
        /// </summary>
        /// <value></value>
        IQueryable<TEntity> Entities{get;}

        /// <summary>
        /// 延迟加载实体(未过滤)
        /// </summary>
        /// <value></value>
        IQueryable<TEntity> NoFilterEntities { get; }

        Expression<Func<TEntity, bool>> FilterExpression { get;  }
        #region 数据库操作

        #region 异步操作
        #region 新增操作
        /// <summary>
        /// 插入实体
        /// </summary>
        /// <param name="entity">单个实体</param>
        /// <returns>操作影响行数</returns>
        Task<int> AddAsync(TEntity entity);

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
                                            Func<TInputDto, bool> checkAction = null,
                                            Func<TInputDto, TEntity> convertFunc = null) where TInputDto : DataDto;

        //Task<OutputDto> AddRangeAsync 暂时不写批量操作
        #endregion

        #region 删除操作
        /// <summary>
        /// 异步删除实体
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        /// <returns>操作影响的行数</returns>
        Task<OutputDto> DeleteAsync(params TEntity[] entities);

        /// <summary>
        /// 按ID删除实体
        /// </summary>
        /// <param name="key"></param>
        /// <returns>操作影响的行数</returns>
        Task<OutputDto> DeleteAsync(TKey key);

        /// <summary>
        /// 标记删除
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<OutputDto> MarkDeleteAsync(TKey key);

        /// <summary>
        /// 按条件从Id集合中选择删除
        /// </summary>
        /// <param name="checkAction">合法性验证</param>
        /// <param name="whereFunc">条件语句</param>
        /// <returns></returns>
        Task<OutputDto> DeleteAsync(ICollection<TKey> ids,
                                       Func<TEntity, bool> checkAction = null,
                                       Func<TEntity, TEntity> whereFunc = null);

        /// <summary>
        /// 按条件删除
        /// </summary>
        /// <param name="wherePredicate"></param>
        /// <returns></returns>
        Task<OutputDto> DeleteAsync(Expression<Func<TEntity, bool>> wherePredicate);

        #endregion

        #region 更新操作
        /// <summary>
        /// 更新单个实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="IsMarkDelete">是否式标记删除</param>
        /// <returns></returns>
        Task<OutputDto> UpdateAsync(TEntity entitie, bool IsMarkDelete = false);

        /// <summary>
        /// 异步以DTO为载体更新实体
        /// </summary>
        /// <typeparam name="TEditDto">更新DTO类型</typeparam>
        /// <param name="dto"></param>
        /// <param name="checkAction"></param>
        /// <param name="convertFunc"></param>
        /// <returns></returns>
        Task<OutputDto> UpdateAsync<TEditDto>(TEditDto dto,Func<TEntity,bool> whereFunc,Func<TEditDto, TEntity, TEntity> convertFunc = null) where TEditDto : DataDto;

        /// <summary>
        /// 异步更新所有符合特定条件的实体
        /// </summary>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <param name="updateExpression">实体更新表达式</param>
        /// <returns>操作影响的行数</returns>
        Task<int> UpdateBatchAsync(Expression<Func<TEntity, bool>> predicate,Expression<Func<TEntity, TEntity>> updateExpression);
        #endregion

        /// <summary>
        /// 按主键查找
        /// </summary>
        /// <param name="key">Id</param>
        /// <returns></returns>
        Task<TEntity> GetAsync(TKey key);

        #endregion
        #endregion

        Task<bool> CheckExistsAsync(Expression<Func<TEntity, bool>> predicate, TKey id = default(TKey));
    }
}
