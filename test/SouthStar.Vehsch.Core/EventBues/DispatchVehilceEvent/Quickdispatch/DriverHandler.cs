﻿using MediatR;
using Microsoft.Extensions.Logging;
using OneZero.Exceptions;
using SouthStar.VehSch.Core.Setting.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SouthStar.VehSch.Core.EventBues.DispatchVehilceEvent.Quickdispatch
{
    public class DriverHandler : INotificationHandler<QuickDispatchEventArgs>
    {
        private readonly DriverService _driverService;
        private readonly ILogger<VehicleHandler> _logger;

        public DriverHandler(DriverService driverService, ILogger<VehicleHandler> logger)
        {
            _driverService = driverService;
            _logger = logger;
        }

        public async Task Handle(QuickDispatchEventArgs notification, CancellationToken cancellationToken)
        {
            try
            {
                if (notification.VehicleId == default(Guid))
                    throw new OneZeroException("司机ID不能为空");
                string msg;
                msg = await _driverService.ChangeStatusHandlerAsync(notification.DriverId, notification.DriverStatus);
                _logger.LogInformation($"派车后，修改司机状态:{msg}");
            }
            catch (Exception e)
            {
                _logger.LogWarning($"派车后，修改司机状态失败", e.InnerException.Message);
            }
        }
    }
}
