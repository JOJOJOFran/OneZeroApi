using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OneZero.Common.Dapper;
using OneZero.Common.Dtos;
using OneZero.Domain.Repositories;
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

        public async Task<OutputDto> GetVehicleDispatchListAsync( string applyNum, DateTime? startDate = null, DateTime? endDate = null, int page = 1, int limit = 20)
        {
            int skipCount = 0;
            //找到申请状态为已审核且审核状态为通过或者已派车的申请单
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

           
           // output.Datas = await query?.ToListAsync();
            return output;
        }
    }
}
