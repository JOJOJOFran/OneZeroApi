using AutoMapper;
using Microsoft.Extensions.Logging;
using OneZero.Common.Dapper;
using OneZero.Common.Dtos;
using OneZero.Common.Exceptions;
using OneZero.Common.Extensions;
using OneZero.Domain.Repositories;
using SouthStar.VehSch.Api.Areas.Setting.Dtos;
using SouthStar.VehSch.Api.Areas.Setting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthStar.VehSch.Api.Areas.Setting.Services
{
    public class DepartmentService : BaseService
    {
        private IRepository<Departments, Guid> _departmentRepository;
        private ILogger<VehcileService> _logger;
        private readonly OutputDto output = new OutputDto();

        public DepartmentService(IUnitOfWork unitOfWork, ILogger<VehcileService> logger, IDapperProvider dapper, IMapper mapper) : base(unitOfWork, dapper, mapper)
        {
            _departmentRepository = unitOfWork.Repository<Departments, Guid>();
            _logger = logger;
        }
    }
}
