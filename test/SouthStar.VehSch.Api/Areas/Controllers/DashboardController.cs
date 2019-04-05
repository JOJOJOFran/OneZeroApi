using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SouthStar.VehSch.Api.Controllers;
using SouthStar.VehSch.Core.Common.Enums;
using SouthStar.VehSch.Core.Dashboard.Services;
using SouthStar.VehSch.Core.Setting.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthStar.VehSch.Api.Areas.Controllers
{
    /// <summary>
    /// 主页面板
    /// </summary>
    public class DashboardController : BaseController
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly VehcileService _vehicleService;
        private readonly DriverService _driverService;
        private readonly DashboardService _dashboardService;


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="vehicleManageService"></param>
        /// <param name="driverService"></param>
        /// <param name="dashboardService"></param>
        public DashboardController(ILogger<DashboardController> logger, VehcileService vehicleManageService, DriverService driverService,DashboardService dashboardService)
        {
            _logger = logger;
            _vehicleService = vehicleManageService;
            _driverService = driverService;
            _dashboardService = dashboardService;
        }


        /// <summary>
        /// 获取所有状态的车辆数量
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> VehicleStateCount()
        {
           var result= await _dashboardService.GetVehiclesStateCountAsync();
            return Ok(result);
        }

        /// <summary>
        /// 获取所有性质的车辆数量
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> VehiclePropertyCount()
        {
            var result = await _dashboardService.GetVehiclesPropertyCountAsync();
            return Ok(result);
        }

        /// <summary>
        /// 指定性质或状态的车辆列表
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> SpecificVehicleList(CurrentState? currentState = null, CarProperty? carProperty = null)
        {
            var result = await _dashboardService.GetSpecificVehicleListAsync(currentState, carProperty);
            return Ok(result);
        }


        /// <summary>
        /// 获取所有状态的司机人数
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> DriverStateCount()
        {
            var result = await _dashboardService.GetDriversStateCountAsync();
            return Ok(result);
        }

        /// <summary>
        /// 指定状态的司机列表
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> SpecificDriverList(PersonState? personState)
        {
            var result = await _dashboardService.GetSpecificDriverListAsync(personState);
            return Ok(result);
        }
    }
}
