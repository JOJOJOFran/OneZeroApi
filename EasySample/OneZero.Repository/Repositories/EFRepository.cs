using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OneZero.Common.CommonHelper;
using OneZero.Domain.Entities;
using OneZero.Domain.Models;
using OneZero.Domain.Repositories;
using OneZero.Model.CustomException;
using OneZero.Repository.UnitOfWorks;
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
    public abstract class EFRepository<TEntity, TKey> : IRepository<TEntity, TKey>
                                                   where TEntity : class, IEntity<TKey>
                                                   where TKey : IEquatable<TKey>
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;
        private readonly ILogger _logger;
        private readonly string _moduleName;
        private OutputModel _output;

        public IUnitOfWork UnitOfWork { get; }

        public EFRepository(OutputModel output, Logger<EFRepository<TEntity, TKey>> logger)
        {
            _dbContext = ((EFUnitOfWork)UnitOfWork).GetDbContext<TEntity, TKey>();
            _dbSet = _dbContext.Set<TEntity>();
            _logger = logger;
        }

        public EFRepository(IServiceProvider serviceProvider)
        {
            //UnitOfWork = serviceProvider.GetUnitOfWork<TEntity, TKey>();
            _dbContext = (DbContext)((EFUnitOfWork)UnitOfWork).GetDbContext<TEntity, TKey>();
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
            return x => x.Id != null;
        }

        #region 异步方法
        #region 新增
        public virtual async Task<int> AddAsync(params TEntity[] entities)
        {
            entities.NotNull();
            await BasciAddRangeAsync(entities);
            return entities.Count();
        }

        /// <summary>
        /// Dto增加
        /// </summary>
        /// <typeparam name="TIntputDto"></typeparam>
        /// <param name="dto"></param>
        /// <param name="checkAction"></param>
        /// <param name="convertFunc"></param>
        /// <returns></returns>
        public virtual async Task<OutputModel> AddAsync<TIntputDto>(TIntputDto dto, Func<TIntputDto, Task<bool>> checkAction = null, Func<TIntputDto, Task<TEntity>> convertFunc = null) where TIntputDto : InputModel
        {
            if (checkAction != null && !await checkAction(dto))
                _output.Message = "输入模型不合法，请检查";

            var entity = await convertFunc(dto);
            await BasicAddAsync(entity);
            return _output;
        }

        /// <summary>
        /// Dto列表增加（暂时未实现可控的批量删除，若需实现可以引用EFCORE.Plus，或者采用CacellentionToken去实现）
        /// </summary>
        /// <typeparam name="TIntputDto"></typeparam>
        /// <param name="dtos"></param>
        /// <param name="checkAction"></param>
        /// <param name="convertFunc"></param>
        /// <returns></returns>
        public virtual async Task<OutputModel> AddAsync<TIntputDto>(ICollection<TIntputDto> dtos, Func<TIntputDto, Task<bool>> checkAction = null, Func<TIntputDto, Task<TEntity>> convertFunc = null) where TIntputDto : InputModel
        {
            dtos.NotNull();

            foreach (var item in dtos)
            {
                try
                {
                    if (checkAction != null && !await checkAction(item))
                    {
                        _output.Message = "输入模型不合法，请检查";
                        return _output;
                    }
                    await _dbContext.AddAsync(await convertFunc(item));
                }
                catch (Exception e)
                {
                    throw new DefineException(_moduleName + ":新增失败", e, ResponseCode.UnExpectedException);
                }

            }
            var count = await _dbContext.SaveChangesAsync();
            _output.Message = _moduleName + $":共{count}条，新增成功！";
            return _output;
        }

        /// <summary>
        /// 添加单个实体
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        protected virtual async Task BasicAddAsync(TEntity entity)
        {
            if (!EntityValidate(entity, out string entityInfo))
            {
                _output.Code = ResponseCode.ExpectedException;
                _output.Message = "数据实体不合法，请检查" + entityInfo + "后重新提交！";

            }
            else
            {
                try
                {
                    await _dbContext.AddAsync(entity);
                    _output.Message = _moduleName + ":新增成功！";
                    await _dbContext.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    throw new DefineException(_moduleName + ":新增失败", e, ResponseCode.UnExpectedException);
                }
            }
        }

        /// <summary>
        /// 添加多个实体
        /// </summary>
        /// <param name="entities">实例集合</param>
        /// <returns></returns>
        protected async Task<bool> BasciAddRangeAsync(IEnumerable<TEntity> entities)
        {
            if (!EntityValidate(entities, out string entityInfo))
            {
                _output.Message = _moduleName + "新增失败，数据实体不合法，请检查" + entityInfo + "后重新提交！";
                _output.Code = ResponseCode.ExpectedException;
                return false;
            }
            else
            {
                try
                {
                    await _dbContext.AddRangeAsync(entities);
                    _output.Message = _moduleName + "新增成功！";
                    await _dbContext.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    throw new DefineException(_moduleName + "新增失败", e, ResponseCode.UnExpectedException);
                }
            }
            return true;
        }
        #endregion

        #region 删除
        /// <summary>
        /// 根据实体数组删除
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual async Task<int> DeleteAsync(params TEntity[] entities)
        {
            entities.NotNull();
            await BasicDeleteBatchAsync(entities);
            return entities.Count();
        }


        /// <summary>
        /// 根据主键删除
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual async Task<OutputModel> DeleteAsync(TKey key)
        {
            key.NotNull();
            var entity = await Entities.FirstOrDefaultAsync(v => v.Id.Equals(key));
            return await BasicDeleteAsync(entity);
        }

        /// <summary>
        /// 暂时不实现
        /// </summary>
        /// <typeparam name="TIntputDto"></typeparam>
        /// <param name="ids"></param>
        /// <param name="checkAction"></param>
        /// <param name="whereFunc"></param>
        /// <returns></returns>
        public virtual async Task<OutputModel> DeleteAsync(ICollection<TKey> ids, Func<TEntity, Task<bool>> checkAction = null, Func<TEntity, TEntity> whereFunc = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 按条件删除
        /// </summary>
        /// <param name="wherePredicate"></param>
        /// <returns></returns>
        public virtual async Task<OutputModel> DeleteAsync(Expression<Func<TEntity, Task<bool>>> wherePredicate)
        {
            wherePredicate.NotNull(wherePredicate, nameof(wherePredicate));
            var entities = await Entities.ToListAsync();  //Where(await wherePredicate).
            await BasicDeleteBatchAsync(entities);
            return null;
        }

        #region Basic Delete
        public virtual async Task<OutputModel> BasicDeleteAsync(TEntity entity)
        {
            if (entity != null)
            {
                try
                {
                    _dbContext.Remove(entity);
                    await _dbContext.SaveChangesAsync();
                    _output.Message = _moduleName + "删除成功";
                }
                catch (Exception e)
                {
                    throw new DefineException(_moduleName + "删除失败", e, ResponseCode.UnExpectedException);
                }
            }
            else
            {
                _output.Message = _moduleName + "，该数据已经被删除！";
                _output.Code = ResponseCode.ExpectedException;
            }
            return _output;
        }

        public virtual async Task<OutputModel> BasicDeleteBatchAsync(ICollection<TEntity> entities)
        {
            if (entities?.Count > 0)
            {
                try
                {
                    _dbContext.RemoveRange(entities);
                    await _dbContext.SaveChangesAsync();
                    _output.Message = String.Format("{0}，共{1}条，删除成功！", _moduleName, entities.Count());
                }
                catch (Exception e)
                {
                    throw new DefineException(_moduleName + "删除失败", e, ResponseCode.UnExpectedException);
                }
                return _output;
            }
            _output.Message = _moduleName + ",该数据已经被删除！";
            return _output;
        }
        #endregion
        #endregion

        #region 查询
        /// <summary>
        /// 根据主键查询
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> GetAsync(TKey key)
        {
            return await Entities.FirstOrDefaultAsync(v => v.Id.Equals(key));
        }


        #endregion

        #region 更新方法



        #endregion

        #endregion



        #region 抽象方法
        /// <summary>
        /// 实体类验证
        /// </summary>
        /// <returns></returns>
        public abstract bool EntityValidate(TEntity entity, out string entityInfo);
        public abstract bool EntityValidate(IEnumerable<TEntity> entities, out string entityInfo);
        public abstract Task<IEnumerable<OutputData>> GetListAsync(IEnumerable<TEntity> entities);
        public abstract Task<IEnumerable<OutputData>> GetItemAsync(TEntity entity);

        public Task<OutputModel> DeleteAsync(ICollection<TKey> ids, Func<TEntity, Task<bool>> checkAction = null, Func<TEntity, Task<TEntity>> whereFunc = null)
        {
            throw new NotImplementedException();
        }



        public Task<int> UpdateAsync(params TEntity[] entities)
        {
            throw new NotImplementedException();
        }

        public Task<OutputModel> UpdateAsync<TEditDto>(TEditDto dto, Func<TEditDto, TEntity, Task<TEntity>> convertFunc = null) where TEditDto : InputModel
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateBatchAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TEntity>> updateExpression)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckExistsAsync(Expression<Func<TEntity, bool>> predicate, TKey id = default(TKey))
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
