using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OneZero.Common.Extensions;
using OneZero.Common.Helpers;
using OneZero.Core.Models;
using OneZero.Domain;
using OneZero.Dtos;
using OneZero.EntityFrameworkCore.Domain;
using OneZero.Enums;
using OneZero.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OneZero.Core.Domains
{
    public class OneZeroRepository<TEntity> : Repository<TEntity, Guid> where TEntity :  BaseEntity<Guid>
    {
        private readonly OneZeroContext _oneZeroContext;

        public OneZeroRepository(IDbContext dbContext, ILogger<Repository<TEntity, Guid>> logger, OneZeroContext oneZeroContext) : base(dbContext, logger)
        {
            _oneZeroContext = oneZeroContext;
        }


        #region 重写过滤条件
        public override Expression<Func<TEntity, bool>> DataFilter()
        {
            if (_oneZeroContext.IsAuththentic)
            {
                return x => x.IsDelete == default(Guid) && x.TenanId.Equals(_oneZeroContext.TenanId);
            }
            else
            {
                return x => x.IsDelete == default(Guid);
            }
        }
        #endregion 

        #region 重写新增方法
        public override async Task<int> AddAsync(TEntity entity)
        {
            EntityIdCheck(entity);
            entity.TenanId = _oneZeroContext.TenanId;
            return await base.AddAsync(entity);
        }

        public override async Task<int> AddAsync(params TEntity[] entities)
        {
            foreach (var entity in entities)
            {
                EntityIdCheck(entity);
                entity.TenanId = _oneZeroContext.TenanId;
            }
            return await base.AddAsync(entities);
        }

        public override async Task<OutputDto> AddAsync<TInputDto>(TInputDto dto,
                                                                 Func<TInputDto, bool> checkAction = null,
                                                                 Func<TInputDto, TEntity> convertFunc = null)
        {
            if (checkAction != null && checkAction(dto))
                _output.Message = "输入模型不合法，请检查";

            var entity = convertFunc(dto);
            try
            {
                EntityIdCheck(entity);
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

        #region 重写更新方法
        public override  async Task<int> UpdateOneAsync(TEntity entity)
        {
            EntityIdCheck(entity);
            return await base.UpdateOneAsync(entity);
        }

        /// <summary>
        /// 更新单个实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="IsMarkDelete">是否式标记删除</param>
        /// <returns></returns>
        public override async Task<OutputDto> UpdateAsync(TEntity entity, bool IsMarkDelete = false)
        {
            EntityIdCheck(entity);
            return await base.UpdateAsync(entity);
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
                entity.TenanId = _oneZeroContext.TenanId;
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
        #endregion

        /// <summary>
        /// 保证实体的主键和租户ID有效
        /// </summary>
        /// <param name="entity"></param>
        private void EntityIdCheck(TEntity entity)
        {
            if (entity.Id.Equals(default(Guid)))
                entity.Id = GuidHelper.NewGuid();

            if (entity.TenanId.Equals(default(Guid)))
                entity.TenanId = _oneZeroContext.TenanId;
        }
    }
}
