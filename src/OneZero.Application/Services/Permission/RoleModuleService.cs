using AutoMapper;
using Microsoft.Extensions.Logging;
using OneZero.Application.Models.Permissions;
using OneZero.Common.Dapper;
using OneZero.Common.Dtos;
using OneZero.Domain.Repositories;
using OneZero.Application;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Application.Services.Permission
{
    public class RoleModuleService: BaseService
    {
        private ILogger<RoleModuleService> _logger;
        private readonly OutputDto output = new OutputDto();
        private IRepository<Role, Guid> _roleRepository;
        private IRepository<RoleModule, Guid> _roleModuleRepository;

        public RoleModuleService(IUnitOfWork unitOfWork, ILogger<RoleModuleService> logger, IDapperProvider dapper, IMapper mapper) : base(unitOfWork, dapper, mapper)
        {
            _roleRepository = _unitOfWork.Repository<Role, Guid>();
            _roleModuleRepository = _unitOfWork.Repository<RoleModule, Guid>();
        }
    }
}
