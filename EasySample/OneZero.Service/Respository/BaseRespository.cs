using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OneZero.Entity;
using OneZero.Model;
using OneZero.Model.CustomException;

namespace OneZero.Service.Respository
{
    public abstract class BaseRespository<TEntity, TKey, TDto, TDtoData> : IRespository<TEntity, TKey, TDto, TDtoData>
                                                                            where TEntity : BaseEntity<TKey>
                                                                            where TKey : IEquatable<TKey>
                                                                            where TDto : IDto<TDtoData>
                                                                            where TDtoData : IDtoData
    {
        #region 字段和属性
        private readonly DbContext _context;
        protected readonly DbSet<TEntity> _entities;
        private readonly string _moduleName;

        private readonly string _pageName;

        private readonly string _actionName;

        public TDtoData _dtoData;

        public TDto _dto;

        public DbContext Context { get { return _context; } }

        public IQueryable<TEntity> Entities => _context.Set<TEntity>().AsNoTracking();

        public IQueryable<TEntity> EntitieWithTracking => _context.Set<TEntity>();
        #endregion

        #region  构造函数
        public BaseRespository(DbContext dbContext, IDtoData dtoData, TDto dto)
        {
            _context = dbContext;
            _entities = _context.Set<TEntity>();
            _dtoData = (TDtoData)dtoData;
            _dto = dto;
        }

        public BaseRespository(DbContext dbContext, string moduleName, IDtoData dtoData, TDto dto)
        {
            _context = dbContext;
            _entities = _context.Set<TEntity>();
            _moduleName = moduleName;
            _dtoData = (TDtoData)dtoData;
            _dto = dto;
        }

        public BaseRespository(DbContext dbContext, string moduleName, TDtoData dtoData, TDto dto, string pageName, string actionName)
        {
            _context = dbContext;
            _entities = _context.Set<TEntity>();
            _moduleName = moduleName;
            _dto = dto;
            _dtoData = dtoData;
            _actionName = actionName;
            _pageName = pageName;
        }

        #endregion 

        #region 新增方法
        public virtual async Task<int> AddAndGetCountAsync(IEnumerable<TEntity> entities)
        {
            await BasciAddRangeAsync(entities);
            return entities.Count();
        }

        public virtual async Task<TDto> AddAsync(TEntity entity)
        {
            await BasicAddAsync(entity);
            return _dto;
        }

        public virtual async Task<TDto> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await BasciAddRangeAsync(entities);
            return _dto;
        }

        /// <summary>
        /// 添加单个实体
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        protected virtual async Task BasicAddAsync(TEntity entity)
        {
            if (!await EntityValidate(entity, out string entityInfo))
            {
                _dto.Code = ResponseCode.ExpectedException;
                _dto.Message = "数据实体不合法，请检查" + entityInfo + "后重新提交！";
            }
            else
            {
                try
                {
                    await _entities.AddAsync(entity);
                    _dto.Message = _moduleName + "新增成功！";
                    await SaveChangesAsync();
                }
                catch (Exception e)
                {
                    throw new DefineException(_moduleName + "新增失败", e, ResponseCode.UnExpectedException);
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
            if (!await EntityValidate(entities, out string entityInfo))
            {
                _dto.Message = _moduleName + "新增失败，数据实体不合法，请检查" + entityInfo + "后重新提交！";
                _dto.Code = ResponseCode.ExpectedException;
                return false;
            }
            else
            {
                try
                {
                    await _entities.AddRangeAsync(entities);
                    _dto.Message = _moduleName + "新增成功！";
                    await SaveChangesAsync();
                }
                catch (Exception e)
                {
                    throw new DefineException(_moduleName + "新增失败", e, ResponseCode.UnExpectedException);
                }
            }
            return true;
        }

        #endregion

        #region 删除方法
        public virtual async Task<TDto> DeleteAsync(TKey key)
        {
            var entity = await Entities.FirstOrDefaultAsync(v => v.Id.Equals(key));
            return await BasicDeleteAsync(entity);
        }

        public virtual async Task<TDto> DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = await  Entities.Where(predicate).ToListAsync();
            return await BasicDeleteRangeAsync(entities);

        }

        public virtual async Task<TDto> DeleteRangeAsync(IEnumerable<TKey> keys)
        {
            var entities =await  Entities.Where(v => keys.Contains(v.Id))?.ToListAsync();
            return await BasicDeleteRangeAsync(entities);

        }

        public virtual async Task<int> GetDeleteCountAsync(IEnumerable<TKey> keys)
        {
            var entities = await Entities.Where(v => keys.Contains(v.Id))?.ToListAsync();
            await BasicDeleteRangeAsync(entities);
            return entities.Count;
        }

        /// <summary>
        /// 删除单个实体
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        public async Task<TDto> BasicDeleteAsync(TEntity entity)
        {
            if (entity != null)
            {
                try
                {
                    _entities.Remove(entity);
                    await SaveChangesAsync();
                    _dto.Message = _moduleName + "删除成功";
                }
                catch (Exception e)
                {
                    throw new DefineException(_moduleName + "删除失败", e, ResponseCode.UnExpectedException);
                }
            }
            else
            {
                _dto.Message = _moduleName + "，该数据已经被删除！";
                _dto.Code = ResponseCode.ExpectedException;
            }
            return _dto;
        }

        /// <summary>
        /// 删除多实体
        /// </summary>
        /// <param name="entities">实体集合</param>
        /// <returns></returns>
        public async Task<TDto> BasicDeleteRangeAsync(ICollection<TEntity> entities)
        {
            if (entities?.Count > 0)
            {
                try
                {
                    _entities.RemoveRange(entities);
                    await SaveChangesAsync();
                    _dto.Message = String.Format("{0}，共{1}条，更新成功！", _moduleName, entities.Count());
                }
                catch (Exception e)
                {
                    throw new DefineException(_moduleName + "删除失败", e, ResponseCode.UnExpectedException);
                }
                return _dto;
            }
            _dto.Message = _moduleName + ",该数据已经被删除！";
            return _dto;
        }
        #endregion

        #region 查询方法
        public virtual  TEntity GetItemAsync(Func<TEntity, bool> whereFunc)
        {
            return   Entities.Where(whereFunc).FirstOrDefault();
        }

        public virtual IEnumerable<TEntity> GetListAsync(Func<TEntity, bool> whereFunc)
        {
            return  Entities.Where(whereFunc);
        }
        #endregion

        #region 更新方法
        public virtual async Task<int> GetUpdateCountAsync(IEnumerable<TEntity> entities)
        {
            await BasicUpdateRangeAsync(entities);
            return entities.Count();
        }

        public virtual async Task<TDto> UpdateAsync(TEntity entity)
        {
            return await BasciUpdateAsync(entity);
        }

        public virtual async Task<TDto> UpdateAsync<TInputDto>(TInputDto inputDto, Func<TInputDto, TEntity, TEntity> convertFunc, TKey key)
        {
            var entity = await Entities.FirstOrDefaultAsync(v => v.Id.Equals(key));
            if (convertFunc != null)
            {
                entity = convertFunc(inputDto, entity);
            }
            return await BasciUpdateAsync(entity);
        }

        /// <summary>
        /// 更新单个实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="flag">操作标识，默认为更新</param>
        /// <returns></returns>
        public async Task<TDto> BasciUpdateAsync(TEntity entity, string actionFlag = null)
        {
            string actionType = actionFlag == null ? "更新" : (actionFlag == "Recycle" ? "清除" : "还原");
            if (!await EntityValidate(entity, out string entityInfo))
            {
                _dto.Code = ResponseCode.ExpectedException;
            }
            else
            {
                try
                {
                    _entities.Update(entity);
                    await _context.SaveChangesAsync();
                    _dto.Message = String.Format("{0},{1}成功！", _moduleName, actionType);
                }
                catch (Exception e)
                {
                    throw new DefineException(String.Format("{0},{1}失败！", _moduleName, actionType), e, ResponseCode.UnExpectedException);
                }

            }
            return _dto;
        }

        /// <summary>
        /// 批量更新实体
        /// </summary>
        /// <param name="entities">实体集合</param>
        /// <param name="flag">操作标识，默认为更新</param>
        /// <returns></returns>
        public async Task<TDto> BasicUpdateRangeAsync(IEnumerable<TEntity> entities, string flag = null)
        {
            string actionType = flag == null ? "更新" : (flag == "Recycle" ? "清除" : "还原");
            if (!await EntityValidate(entities, out string entityInfo))
            {
                _dto.Code = ResponseCode.ExpectedException;
            }
            else
            {
                try
                {
                    _entities.UpdateRange(entities);
                    await _context.SaveChangesAsync();
                    _dto.Message = String.Format("{0}，共{1}条，{2}成功！", _moduleName, entities.Count(), actionType);
                }
                catch (Exception e)
                {
                    throw new DefineException(String.Format("{0},{1}失败！", _moduleName, actionType), e, ResponseCode.UnExpectedException);
                }

            }
            return _dto;
        }

        #endregion

        #region 标记删除和还原
         public virtual async Task<TDto> RecycleAsync(TKey key=default(TKey))
        {
            var entity =await Entities.FirstOrDefaultAsync(v => v.Id.Equals(key) && v.IsDelete == false);
            return await BasicRecycleAsync(entity);
        }

        public virtual async Task<TDto> RecycleRangeAsync(IEnumerable<TKey> keys)
        {
            var entity = (await _entities.ToListAsync()).Where(v=>keys.Contains(v.Id)).Where(v => v.IsDelete == false);
            return await BasicRecycleRangeAsync(entity);
        }


         public async Task<TDto>  BasicRecycleAsync(TEntity entity)
        {
            entity.IsDelete = true;
            await BasciUpdateAsync(entity, "Recycle");
            return _dto; 
        }

        public async Task<TDto> BasicRecycleRangeAsync(IEnumerable<TEntity> entities)
        {
            foreach (var item in entities)
            {
                item.IsDelete = true;
            }
            await BasicUpdateRangeAsync(entities, "Recycle");
            return _dto;
        }


         public virtual async Task<TDto> RestoreAsync(TKey key = default(TKey))
        {
            var entity = await Entities.FirstOrDefaultAsync(v => v.Id.Equals(key)&&v.IsDelete==true);
            return await BasicRestoreAsync(entity);
        }

        public virtual async Task<TDto> RestoreRangeAsync(IEnumerable<TKey> keys)
        {
            var entity = (await _entities.ToListAsync()).Where(v => keys.Contains(v.Id)).Where(v=>v.IsDelete==true);
            return await BasicRestoreRangeAsync(entity);
        }

             public async Task<TDto> BasicRestoreAsync(TEntity entity)
        {
            entity.IsDelete = false;
            await BasciUpdateAsync(entity, "Restore");
            return _dto;
        }

        public async Task<TDto> BasicRestoreRangeAsync(IEnumerable<TEntity> entities)
        {
            foreach (var item in entities)
            {
                item.IsDelete = false;
            }
            await BasicUpdateRangeAsync(entities, "Restore");
            return _dto;
        }

        #endregion

        #region  保存
        public virtual async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
        #endregion 

        #region 抽象方法
        /// <summary>
        /// 实体类验证
        /// </summary>
        /// <returns></returns>
        public abstract Task<bool> EntityValidate(TEntity entity, out string entityInfo);
        public abstract Task<bool> EntityValidate(IEnumerable<TEntity> entities, out string entityInfo);
        public abstract Task<IEnumerable<DtoData>> GetDtoList(IEnumerable<TEntity> entities);
        public abstract Task<IEnumerable<DtoData>> GetDtoItem(TEntity entity);
        #endregion

    }
}
