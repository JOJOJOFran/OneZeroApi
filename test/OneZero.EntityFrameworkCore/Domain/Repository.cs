using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OneZero.Common.Extensions;
using OneZero.Common.Helpers;
using OneZero.Domain;
using OneZero.Dtos;
using OneZero.Enums;
using OneZero.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace OneZero.EntityFrameworkCore.Domain
{
    public abstract class Repository< TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        #region field
        /// <summary>
        /// 工作单元
        /// </summary>
        public IUnitOfWork _unitOfWork { get; }

        /// <summary>
        /// 数据库上下文
        /// </summary>
        protected readonly DbContext _dbContext;


        protected readonly DbSet<TEntity> _dbSet;
        protected readonly ILogger _logger;
        protected OutputDto _output = new OutputDto();
        #endregion

        #region 构造函数
        public Repository(IDbContext dbContext)
        {
            _dbContext = (DbContext)dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public Repository(IDbContext dbContext, ILogger<Repository<TEntity, TKey>> logger)
        {
            _dbContext = (DbContext)dbContext;
            _dbSet = _dbContext.Set<TEntity>();
            _logger = logger;
        }
        #endregion

        #region IQueryable用于暴露查询
        public IQueryable<TEntity> Entities
        {
            get
            {
                return _dbSet.AsQueryable().AsNoTracking().Where(DataFilter());
            }
        }

        /// <summary>
        /// 不加过滤条件的实体
        /// </summary>
        public IQueryable<TEntity> NoFilterEntities
        {
            get
            {
                return _dbSet.AsQueryable().AsNoTracking();
            }
        }

        /// <summary>
        /// 设置额外的过滤条件
        /// </summary>
        public Expression<Func<TEntity, bool>> FilterExpression { get; set; }

        /// <summary>
        /// 数据筛选，可以来用来排除IsDelete数据
        /// </summary>
        /// <returns></returns>
        public virtual Expression<Func<TEntity, bool>> DataFilter()
        { 
            return x => x.IsDelete == default(Guid);
        }
        #endregion

        #region Interface Implemention
        #region 新增方法
        public virtual async Task<int> AddAsync(TEntity entity)
        {
            int result = 0;
            try
            {
                await _dbContext.AddAsync(entity);
                result = await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new OneZeroException("新增失败", e, ResponseCode.UnExpectedException);
            }
            return result;
        }

        public virtual async Task<int> AddAsync(params TEntity[] entities)
        {
            try
            {
                await _dbContext.AddRangeAsync(entities);
                _output.Message = "新增成功！";
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new OneZeroException("新增失败", e, ResponseCode.UnExpectedException);
            }
            return entities.Count();
        }

        public virtual async Task<OutputDto> AddAsync<TInputDto>(TInputDto dto,
                                                                 Func<TInputDto, bool> checkAction = null,
                                                                 Func<TInputDto, TEntity> convertFunc = null)
        {
            if (checkAction != null && checkAction(dto))
                _output.Message = "输入模型不合法，请检查";

            var entity = convertFunc(dto);
            try
            {

                await _dbContext.AddAsync(entity);
                _output.Message = "新增成功！";
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new OneZeroException("新增失败", e, ResponseCode.UnExpectedException);
            }
            return _output;
        }
        #endregion

        #region 删除方法
        public virtual async Task<OutputDto> DeleteAsync(params TEntity[] entities)
        {
            entities.NotNull();
            return await BasicDeleteBatchAsync(entities);
        }

        public virtual async Task<OutputDto> DeleteAsync(TKey key)
        {
            key.NotNull();
            var entity = await Entities.FirstOrDefaultAsync(v => v.Id.Equals(key));
            return await BasicDeleteAsync(entity);
        }

        public virtual async Task<OutputDto> MarkDeleteAsync(TKey key)
        {
            var entity = await Entities.FirstOrDefaultAsync(v => v.Id.Equals(key));
            if (entity != null)
            {
                entity.IsDelete = GuidHelper.NewGuid();
                await UpdateAsync(entity, true);
            }
            _output.Message = "操作成功";
            return _output;
        }

        /// <summary>
        ///  异步以标识集合批量删除实体
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="checkAction"></param>
        /// <param name="whereFunc"></param>
        /// <returns></returns>
        public virtual async Task<OutputDto> DeleteAsync(ICollection<TKey> ids, Func<TEntity, bool> checkAction = null, Func<TEntity, TEntity> whereFunc = null)
        {
            ids.NotNull(ids, nameof(ids));
            List<string> names = new List<string>();
            foreach (TKey id in ids)
            {
                TEntity entity = await _dbSet.FindAsync(id);
                if (entity == null)
                {
                    continue;
                }
                try
                {
                    checkAction?.Invoke(entity);
                    if (whereFunc != null)
                    {
                        entity =  whereFunc(entity);
                    }
                    _dbSet.Remove(entity);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, e.Message);
                    throw new OneZeroException("删除失败",e,ResponseCode.UnExpectedException);
                }
            }
            int count = await _dbContext.SaveChangesAsync();
            _output.Message = $"操作成功，共删除{count}条记录";
            return _output;
        }


        public virtual async Task<OutputDto> DeleteAsync(Expression<Func<TEntity, bool>> wherePredicate)
        {
            wherePredicate.NotNull(wherePredicate, nameof(wherePredicate));
            var entities = await Entities.Where(wherePredicate).ToListAsync();
            return await BasicDeleteBatchAsync(entities);
        }

        #region basic delete
        public virtual async Task<OutputDto> BasicDeleteAsync(TEntity entity)
        {
            if (entity != null)
            {
                try
                {
                    _dbContext.Remove(entity);
                    await _dbContext.SaveChangesAsync();
                    _output.Message = "删除成功";
                }
                catch (Exception e)
                {
                    throw new OneZeroException("删除失败", e, ResponseCode.UnExpectedException);
                }
            }
            else
            {
                _output.Message = "，该数据已经被删除！";
                _output.Code = ResponseCode.ExpectedException;
            }
            return _output;
        }

        public virtual async Task<OutputDto> BasicDeleteBatchAsync(ICollection<TEntity> entities)
        {
            if (entities?.Count > 0)
            {
                try

                {
                    _dbContext.RemoveRange(entities);
                    await _dbContext.SaveChangesAsync();
                    _output.Message = $"删除成功,共{entities.Count()}条！";
                }
                catch (Exception e)
                {
                    throw new OneZeroException("删除失败", e, ResponseCode.UnExpectedException);
                }
                return _output;
            }
            _output.Message = ",该数据已经被删除！";
            return _output;
        }
        #endregion

        #region 
        #endregion

        #endregion

        #region 更新方法
        /// <summary>
        /// 更新单个实体
        /// </summary>
        /// <param name="entitie"></param>
        /// <returns></returns>
        public virtual async Task<int> UpdateOneAsync(TEntity entitie)
        {
            try
            {
                _dbContext.Update(entitie);
                var result = await _dbContext.SaveChangesAsync();
                return result;
            }
            catch (Exception e)
            {
                throw new OneZeroException($"更新失败", e, ResponseCode.UnExpectedException);
            }
        }

        /// <summary>
        /// 更新单个实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="IsMarkDelete">是否式标记删除</param>
        /// <returns></returns>
        public virtual async Task<OutputDto> UpdateAsync(TEntity entity, bool IsMarkDelete = false)
        {
            string action = (IsMarkDelete ? "清除" : "更新");
            try
            {
                _dbContext.Update(entity);
                var result = await _dbContext.SaveChangesAsync();
                _output.Message = $"{action}成功,共{result}条.";
            }
            catch (Exception e)
            {
                throw new OneZeroException($"{action}失败", e, ResponseCode.UnExpectedException);
            }
            return _output;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <typeparam name="TEditDto"></typeparam>
        /// <param name="dto"></param>
        /// <param name="whereFunc"></param>
        /// <param name="convertFunc"></param>
        /// <returns></returns>
        public virtual async Task<OutputDto> UpdateAsync<TEditDto>(TEditDto dto,
                                                                   Func<TEntity, bool> whereFunc,
                                                                   Func<TEditDto, TEntity, TEntity> convertFunc)
        {
            whereFunc.NotNull();
            convertFunc.NotNull();
            try
            {
                var entity = await _dbContext.Set<TEntity>().FirstOrDefaultAsync(v => whereFunc(v));
                var newEntity = convertFunc(dto, entity);
                var result = _dbContext.Update(entity);
                await _dbContext.SaveChangesAsync();
                _output.Message = $"更新成功,共{result}条.";
            }
            catch (Exception e)
            {
                throw new OneZeroException("更新失败", e, ResponseCode.UnExpectedException);
            }
            return _output;
        }

        /// <summary>
        /// 暂不使用
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="updateExpression"></param>
        /// <returns></returns>
        public virtual async Task<int> UpdateBatchAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TEntity>> updateExpression)
        {
            //await _dbContext.Database.BeginTransactionAsync(CancellationToken.None);
            return await _dbSet.Where(predicate).UpdateAsync(updateExpression);
        }

        #endregion

        /// <summary>
        /// 异步检查实体是否存在
        /// </summary>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <param name="id">编辑的实体标识</param>
        /// <returns>是否存在</returns>
        public virtual async Task<bool> CheckExistsAsync(Expression<Func<TEntity, bool>> predicate, TKey id = default(TKey))
        {
            predicate.NotNull(nameof(predicate));

            TKey defaultId = default(TKey);
            var entity = await _dbSet.Where(predicate).Select(m => new { m.Id }).FirstOrDefaultAsync();
            bool exists = !typeof(TKey).IsValueType && ReferenceEquals(id, null) || id.Equals(defaultId)
                ? entity != null
                : entity != null && !IEntity<TKey>.Equals(entity.Id, id);
            return exists;
        }

        /// <summary>
        /// 根据主键查询
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> GetAsync(TKey key)
        {
            return await _dbSet.FindAsync(key);
        }

        /// <summary>
        /// 根据条件检查过滤后的唯一性
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual async Task<bool> CheckUniqueAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return (await Entities.FirstOrDefaultAsync(predicate))==null;
        }
        #endregion
    }
}
