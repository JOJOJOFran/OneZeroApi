using Microsoft.Extensions.Logging;
using SouthStar.VehSch.Api.Areas.Dispatch.Services;
using SouthStar.VehSch.Api.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthStar.VehSch.Api.Areas.Dispatch.Controllers
{
    public class DispatchController:BaseController
    {
        private readonly ILogger<DispatchController> _logger;
        private readonly VehicleDispatchService _dispatchService;


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="vehicleManageService"></param>
        public DispatchController(ILogger<DispatchController> logger, VehicleDispatchService dispatchService)
        {
            _logger = logger;
            _dispatchService = dispatchService;
        }
    }
}
