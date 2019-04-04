using MediatR;
using Microsoft.Extensions.Logging;
using SouthStar.VehSch.Core.ApplicationFlow.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SouthStar.VehSch.Core.EventBues.DispatchVehilceEvent
{
    public class VehicleApplyHandler : INotificationHandler<DispatchVehicleEventArgs>
    {
        private readonly VehicleApplyService _applyService;
        private readonly ILogger<VehicleApplyHandler> _logger;

        public VehicleApplyHandler(VehicleApplyService vehcileService, ILogger<VehicleApplyHandler> logger)
        {
            _applyService = vehcileService;
            _logger = logger;
        }

        public async Task Handle(DispatchVehicleEventArgs notification, CancellationToken cancellationToken)
        {

            try
            {
                if (notification.VehicleId == default(Guid))
                    //说明可能是重新调度不做任何动作
                    return;
                string msg;
                //将审核状态改为已派车
                msg = await _applyService.ChangeStatusHandlerAsync(notification.ApplyId);
                _logger.LogInformation($"派车后，修改申请单状态:{msg}");
            }
            catch (Exception e)
            {
                _logger.LogWarning($"派车后，修改申请单状态失败", e.InnerException.Message);
            }

        }
    }
}
