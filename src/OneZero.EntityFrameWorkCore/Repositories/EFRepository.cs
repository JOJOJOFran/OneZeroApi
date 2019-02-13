﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OneZero.EntityFrameworkCore.UnitOfWorks;
using OneZero.Common.Dtos;
using OneZero.Common.Exceptions;
using OneZero.Common.Extensions;
using OneZero.Domain.Enums;
using OneZero.Domain.Models;
using OneZero.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OneZero.EntityFrameWorkCore.Repositories
{
    public abstract class EFRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class, IEntity<TKey> where TKey : IEquatable<TKey>
    {
        #region field
        /// <summary>
        /// 工作单元
        /// </summary>
        public IUnitOfWork _unitOfWork { get; }
        public readonly DbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;
        private readonly ILogger _logger;
        private readonly string _moduleName;
        private OutputDto _output;
        #endregion

        #region 构造函数
        public EFRepository(OutputDto output, Logger<EFRepository<TEntity, TKey>> logger, IUnitOfWork unitOfWork)
        {
            _dbContext = ((EFUnitOfWork)unitOfWork).DbContext;
            _dbSet= _dbContext.Set<TEntity>();
            _logger = logger;
        }
        #endregion

        #region IQueryable用于暴露查询
        public virtual  IQueryable<TEntity> Entities
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

        public virtual Expression<Func<TEntity, bool>> GetDataFilter()
        {
            return x => 1==1;
        }
        #endregion

        #region Interface Implemention
        #region 新增方法
        public virtual async Task<int> AddAsync(params TEntity[] entities)
        {
            if (!EntityValidate(entities, out string entityInfo))
            {
                _output.Message = _moduleName + "新增失败，数据实体不合法，请检查" + entityInfo + "后重新提交！";
                _output.Code = ResponseCode.ExpectedException;
                return 0;
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
                    throw new OneZeroException(_moduleName + "新增失败", e, ResponseCode.UnExpectedException);
                }
            }
            return entities.Count();
        }

        public virtual async Task<OutputDto> AddAsync<TInputDto>(TInputDto dto, 
                                                                 Func<TInputDto, Task<bool>> checkAction = null, 
                                                                 Func<TInputDto, Task<TEntity>> convertFunc = null) where TInputDto : InputDto
        {
            if (checkAction != null && !await checkAction(dto))
                _output.Message = "输入模型不合法，请检查";

            var entity = await convertFunc(dto);
            try
            {
                await _dbContext.AddAsync(entity);
                _output.Message = _moduleName + ":新增成功！";
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new OneZeroException(_moduleName + ":新增失败", e, ResponseCode.UnExpectedException);
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

        /// <summary>
        /// 暂时不实现
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="checkAction"></param>
        /// <param name="whereFunc"></param>
        /// <returns></returns>
        public virtual async Task<OutputDto> DeleteAsync(ICollection<TKey> ids, Func<TEntity, Task<bool>> checkAction = null, Func<TEntity, TEntity> whereFunc = null)
        {
            throw new NotImplementedException();
        }


        public virtual async Task<OutputDto> DeleteAsync(Expression<Func<TEntity, Task<bool>>> wherePredicate)
        {
            wherePredicate.NotNull(wherePredicate, nameof(wherePredicate));
            var entities = await Entities.ToListAsync();  //Where(await wherePredicate).
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
                    _output.Message = _moduleName + "删除成功";
                }
                catch (Exception e)
                {
                    throw new OneZeroException(_moduleName + "删除失败", e, ResponseCode.UnExpectedException);
                }
            }
            else
            {
                _output.Message = _moduleName + "，该数据已经被删除！";
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
                    _output.Message = String.Format("{0}，共{1}条，删除成功！", _moduleName, entities.Count());
                }
                catch (Exception e)
                {
                    throw new OneZeroException(_moduleName + "删除失败", e, ResponseCode.UnExpectedException);
                }
                return _output;
            }
            _output.Message = _moduleName + ",该数据已经被删除！";
            return _output;
        }
        #endregion
        #endregion

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

        #region 抽象方法
        /// <summary>
        /// 实体类验证
        /// </summary>
        /// <returns></returns>
        public abstract bool EntityValidate(TEntity entity, out string entityInfo);
        public abstract bool EntityValidate(IEnumerable<TEntity> entities, out string entityInfo);
        public abstract Task<IEnumerable<DataDto>> GetListAsync(IEnumerable<TEntity> entities);
        public abstract Task<IEnumerable<DataDto>> GetItemAsync(TEntity entity);

        //public Task<bool> CheckExistsAsync(Expression<Func<TEntity, bool>> predicate, TKey id = default(TKey))
        //{
        //    throw new NotImplementedException();
        //}
        #endregion
    }
}
