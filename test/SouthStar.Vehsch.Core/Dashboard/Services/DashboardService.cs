using AutoMapper;
using Microsoft.Extensions.Logging;
using OneZero.Common.Dapper;
using OneZero.Core;
using OneZero.Domain;
using OneZero.Dtos;
using SouthStar.VehSch.Core.Setting.Models;
using SouthStar.VehSch.Core.Setting.Services;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OneZero.Common.Extensions;
using SouthStar.VehSch.Core.Common.Enums;

namespace SouthStar.VehSch.Core.Dashboard.Services
{
    /// <summary>
    /// 主面板
    /// </summary>
    public class DashboardService : BaseService
    {
        private readonly IRepository<Drivers, Guid> _driverRepository;
        private readonly IRepository<Vehicles, Guid> _vehicleRepository;
        private readonly ILogger<DashboardService> _logger;
        private readonly OutputDto output = new OutputDto();

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="logger"></param>
        /// <param name="dapper"></param>
        /// <param name="mapper"></param>
        public DashboardService(IUnitOfWork unitOfWork,ILogger<DashboardService> logger,IDapperProvider dapper,IMapper mapper) : base(unitOfWork, dapper, mapper)
        {
            _vehicleRepository = unitOfWork.Repository<Vehicles, Guid>();
            _driverRepository = unitOfWork.Repository<Drivers, Guid>();
            _logger = logger;
        }

        /// <summary>
        /// 获取各状态车辆数量
        /// </summary>
        /// <returns></returns>
        public async Task<OutputDto> GetVehiclesStateCountAsync()
        {
            var result = _vehicleRepository.Entities.GroupBy(v => new { v.CurrentState }).Select(v => new { v.Key.CurrentState, StateName = v.Key.CurrentState.GetRemark(), Count = v.Count() });
            output.Datas = await result?.ToListAsync();
            return output;
        }


        /// <summary>
        /// 获取指定状态或者性质的车辆列表
        /// </summary>
        /// <param name="currentState"></param>
        /// <returns></returns>
        public async Task<OutputDto> GetSpecificVehicleListAsync(CurrentState? currentState = null, CarProperty? carProperty = null)
        {
            var query = _vehicleRepository.Entities.Where(v => (v.CurrentState == currentState || currentState == null) && (v.VehicleProperties == carProperty || carProperty == null))
                                                .OrderBy(v => v.PlateNumber)
                                                .Select(v => new { v.Id, v.VIN, v.EngineNo, v.PlateNumber, v.VehicleProperties, v.VechileType });
            output.Datas = await query?.ToListAsync();
            return output;
        }


        /// <summary>
        /// 获取各性质车辆数量
        /// </summary>
        /// <returns></returns>
        public async Task<OutputDto> GetVehiclesPropertyCountAsync()
        {
            var result = _vehicleRepository.Entities.GroupBy(v => new { v.VehicleProperties }).Select(v => new { Property = v.Key.VehicleProperties, PropertyName = v.Key.VehicleProperties.GetRemark(), Count = v.Count() });
            output.Datas = await result?.ToListAsync();
            return output;
        }


        /// <summary>
        /// 获取各状态司机人数
        /// </summary>
        /// <returns></returns>
        public async Task<OutputDto> GetDriversStateCountAsync()
        {
            var result = _driverRepository.Entities.GroupBy(v => new { v.Status }).Select(v => new { v.Key.Status, StateName = v.Key.Status.GetRemark(), Count = v.Count() });
            output.Datas = await result?.ToListAsync();
            return output;
        }


        /// <summary>
        /// 获取指定状态的司机列表
        /// </summary>
        /// <param name="personState"></param>
        /// <returns></returns>
        public async Task<OutputDto> GetSpecificDriverListAsync(PersonState? personState)
        {
            var query = _driverRepository.Entities.Where(v => (v.Status == personState || personState == null))
                                                .OrderBy(v => v.Name)
                                                .Select(v => new { v.Id, v.Name, v.PhoneNum, v.Status, v.PermittedType });
            output.Datas = await query?.ToListAsync();
            return output;
        }



    }
}
