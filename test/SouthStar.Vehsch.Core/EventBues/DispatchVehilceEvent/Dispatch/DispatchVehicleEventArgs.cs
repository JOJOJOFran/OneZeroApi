using MediatR;
using SouthStar.VehSch.Core.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthStar.VehSch.Core.EventBues.DispatchVehilceEvent
{
    /// <summary>
    /// 派车事件
    /// </summary>
    public class DispatchVehicleEventArgs : INotification
    {
        public Guid DispatchId { get; set; }

        public Guid ApplyId { get; set; }

        public Guid? OldDriverId { get; set; }

        public Guid DriverId { get; set; }

        public Guid VehicleId { get; set; }

        public Guid? OldVehicleId { get; set; }

        public Guid? QueueId { get; set; }

        public Guid? OldQueueId { get; set; }

        public int? QueueNo { get; set; }

        public DateTime EventDate { get; set; }

        public CurrentState VehicleStatus { get; set; }

        public PersonState DriverStatus { get; set; }

        /// <summary>
        /// 派车方式
        /// 0：自动派车  1：手动派车
        /// </summary>
        public DispatchType DispatchType { get; set; }

    }
}
