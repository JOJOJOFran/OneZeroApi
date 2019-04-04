
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OneZero.Common.Dapper;
using OneZero.Core.Models;
using OneZero.Domain;
using OneZero.Domain.Services;
using OneZero.Dtos;
using OneZero.Enums;
using OneZero.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneZero.Core
{
    public abstract class BaseService: IMapService, IPagingService
    {
        #region field
        protected readonly IMapper _mapper;
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IDapperProvider _dapper;

        /// <summary>
        /// 默认连接字符串
        /// </summary>
        protected readonly string DefaultConnectString;
        
        /// <summary>
        /// 字符串连接字典
        /// </summary>
        protected readonly Dictionary<Type, string> connectStringDic;
        #endregion

        #region ctor
        public BaseService(IUnitOfWork unitOfWork, IDapperProvider dapper, IMapper mapper)  
        {
            _unitOfWork = unitOfWork;
            _dapper = dapper;
            _mapper = mapper;
            DefaultConnectString= ((DbContext)_unitOfWork.GetDbContext()).Database.GetDbConnection().ConnectionString;
        }

        #endregion

        #region Interface Impletion(Convert Func)

        /// <summary>
        /// Convert Entity to DataDto
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual TDestination ConvertToDataDto<TSource, TDestination>(TSource entity) where TDestination : DataDto where TSource : class,IEntity<Guid>
        {
            try
            {
                return _mapper.Map<TDestination>(entity);
            }
            catch (Exception e)
            {
                throw new OneZeroException($"{typeof(TSource)}转化到{typeof(TDestination)}类失败，请检查Mapper配置", e, ResponseCode.UnExpectedException);
            }
        }

        /// <summary>
        /// Convert DataDto to Entity
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="dataDto"></param>
        /// <returns></returns>
        public virtual TDestination ConvertToModel<TSource, TDestination>(TSource dataDto) where TSource : DataDto where TDestination : class, IEntity<Guid>
        {
            try
            {
                var model=_mapper.Map<TDestination>(dataDto);
                return model;
            }
            catch (Exception e)
            {
                throw new OneZeroException($"{typeof(TSource)}转化到{typeof(TDestination)}类失败，请检查Mapper配置",e,ResponseCode.UnExpectedException);
            }
        }

        /// <summary>
        /// DataDto Convert to INotification Args
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="dataDto"></param>
        /// <returns></returns>
        public virtual TDestination ModelConvertToEventArgs<TSource, TDestination>(TSource dataDto) where TSource : DataDto where TDestination : INotification
        {
            try
            {
                var model = _mapper.Map<TDestination>(dataDto);
                return model;
            }
            catch (Exception e)
            {
                throw new OneZeroException($"{typeof(TSource)}转化到{typeof(TDestination)}类失败，请检查Mapper配置", e, ResponseCode.UnExpectedException);
            }
        }

        /// <summary>
        /// 分页计算
        /// </summary>
        /// <param name="page">第几页</param>
        /// <param name="limit">页面最大行数</param>
        /// <param name="sum">总数</param>
        /// <param name="skipCount">需跳过的行数</param>
        /// <returns><see cref="PageInfo"/> PageInfo</returns>
        public virtual PageInfo Paging(int page, int limit, int sum, out int skipCount)
        {
            if (page < 1 || limit < 1)
                throw new OneZeroException($"分页参数limit:{limit},page:{page}不能小于1", ResponseCode.ExpectedException);
            if (sum == 0)
            {
                skipCount = -1;
                return null;
            }
            var pageInfo = new PageInfo();
            skipCount = (page - 1) * limit;
            pageInfo.Limit = limit;
            pageInfo.Page = page;
            pageInfo.PageCount = (sum+limit-1)/limit;
            pageInfo.CurrentCount = page < pageInfo.PageCount ? 0 : limit;
            pageInfo.Sum = sum;
            return pageInfo;
        }
        #endregion


    }
}
