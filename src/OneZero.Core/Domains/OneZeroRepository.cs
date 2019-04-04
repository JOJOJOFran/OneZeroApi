using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
            entity.Id = GuidHelper.NewGuid();
            entity.TenanId = _oneZeroContext.TenanId;
            return await base.AddAsync(entity);
        }

        public override async Task<int> AddAsync(params TEntity[] entities)
        {
            foreach (var entity in entities)
            {
                entity.Id = GuidHelper.NewGuid();
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
                entity.Id = GuidHelper.NewGuid();
                entity.TenanId= _oneZeroContext.TenanId;
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
    }
}
