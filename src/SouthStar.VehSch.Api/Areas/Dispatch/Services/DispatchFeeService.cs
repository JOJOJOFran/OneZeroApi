using AutoMapper;
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
    }
}
