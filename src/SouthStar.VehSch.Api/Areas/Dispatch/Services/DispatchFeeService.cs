using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OneZero.Application;
using OneZero.Common.Dapper;
using OneZero.Common.Dtos;
using OneZero.Common.Extensions;
using OneZero.Domain.Repositories;
using SouthStar.VehSch.Api.Areas.Dispatch.Dtos;
using SouthStar.VehSch.Api.Areas.Dispatch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthStar.VehSch.Api.Areas.Dispatch.Services
{
    public class DispatchFeeService : BaseService
    {
        private ILogger<DispatchFeeService> _logger;
        private readonly OutputDto output = new OutputDto();
        private IRepository<VehicleDispatchs, Guid> _dispatchRepository;
        private IRepository<DispatchFees, Guid> _feeRepository;

        public DispatchFeeService(IUnitOfWork unitOfWork, ILogger<DispatchFeeService> logger, IDapperProvider dapper, IMapper mapper) : base(unitOfWork, dapper, mapper)
        {
            _feeRepository = unitOfWork.Repository<DispatchFees, Guid>();
            _dispatchRepository = unitOfWork.Repository<VehicleDispatchs, Guid>();
            _logger = logger;
        }

        /// <summary>
        /// 获取费用列表
        /// </summary>
        /// <param name="applyNum"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public async Task<OutputDto> GetFeeListAsync( string applyNum, DateTime? startDate = null, DateTime? endDate = null, int page = 1, int limit = 20)
        {
            int skipCount = 0;
            var dispatch = from a in _dispatchRepository.Entities.Where(v => (startDate == null || v.CreateDate >= startDate)
                                    && (endDate == null || v.CreateDate <= endDate))
                           join b in _feeRepository.Entities on a.Id equals b.DispatchId into b_join from bi in b_join.DefaultIfEmpty()
                           select new { a.Id};

            var sumCount = await dispatch.CountAsync();
            if (sumCount <= 0)
                return output;

            output.PageInfo = Paging(page, limit, sumCount, out skipCount);
            if (skipCount < 0 || output.PageInfo == null)
                return output;

            var query = from a in dispatch
                        join item in _dispatchRepository.Entities on a.Id equals item.Id
                        join c in _feeRepository.Entities on item.Id equals c.DispatchId into c_join
                        from fi in c_join.DefaultIfEmpty()
                        orderby item.ApplyNum
                        select new DispatchFeeData
                        {
                            Id = fi.Id,
                            DispatchId = item.Id,
                            ApplyNum = item.ApplyNum,
                            EndMiles = fi.EndMiles,
                            EtcFee = fi.EtcFee,
                            HighSpeedFee = fi.HighSpeedFee,
                            OilFee = fi.OilFee,
                            ParkFee = fi.ParkFee,
                            StartMiles = fi.StartMiles,
                            TotalPrice = fi.TotalPrice,
                            UnitPrice = fi.UnitPrice
                        };
            output.Datas = await query?.ToListAsync();
            return output;
        }

        /// <summary>
        /// 生成派车费用
        /// </summary>
        /// <param name="feeData"></param>
        /// <returns></returns>
        public async Task<OutputDto> AddFeeAsync(DispatchFeeData feeData)
        {
            feeData.NotNull("车辆信息(新增)");
            feeData.Id = GuidHelper.NewGuid();
            return await _feeRepository.AddAsync(feeData,
                                                     null,
                                                     v => (ConvertToModel<DispatchFeeData, DispatchFees>(feeData)));
        }

        /// <summary>
        /// 修改费用
        /// </summary>
        /// <param name="id"></param>
        /// <param name="feeData"></param>
        /// <returns></returns>
        public async Task<OutputDto> UpdateFeeAsync(Guid id, DispatchFeeData feeData)
        {
            feeData.Id = id;
            return await _feeRepository.UpdateAsync(ConvertToModel<DispatchFeeData, DispatchFees>(feeData));
        }
    }
}
