using MediatR;
using Microsoft.Extensions.Logging;
using OneZero.Common.Dtos;
using OneZero.Common.Exceptions;
using SouthStar.VehSch.Api.Areas.Setting.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SouthStar.VehSch.Api.EventBues.DispatchVehilceEvent
{
    public class VehicleHandler : INotificationHandler<DispatchVehicleEventArgs>
    {
        private readonly VehcileService _vehcileService;
        private readonly ILogger<VehicleHandler> _logger;

        public VehicleHandler(VehcileService vehcileService,ILogger<VehicleHandler> logger)
        {
            _vehcileService = vehcileService;
        }

        public async Task Handle(DispatchVehicleEventArgs notification, CancellationToken cancellationToken)
        {

            try
            {
                if (notification.VehicleId == default(Guid))
                    throw new OneZeroException("车辆ID不能为空");
                string msg;
                msg = await _vehcileService.ChangeStatusHandlerAsync((Guid)notification.OldDriverId, notification.DriverId, notification.VehicleStatus);
                _logger.LogInformation($"派车后，修改车辆状态:{msg}");
            }
            catch (Exception e)
            {
                _logger.LogWarning($"派车后，修改车辆状态失败", e.InnerException.Message);
            }
           
        }
    }
}
