using OneZero.Dtos;
using SouthStar.VehSch.Core.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthStar.VehSch.Core.Dispatch.Dtos
{
    /// <summary>
    /// 派车队列
    /// </summary>
    public class DispatchQueueListData:DataDto
    {
        public Guid? Id { get; set; }

        public int? QueueNo { get; set; }

        public Guid? VehicleId { get; set; }

        public string PlateNum { get; set; }

        public Guid? DriverId { get; set; }

        public string DriverName { get; set; }

        public string MobileNum { get; set; }

        public string VehicleType { get; set; }

        public CarProperty VehicleProperty { get; set; }

        public int? WorkCount { get; set; }

        public DateTime? WorkTime { get; set; }

        public string Status { get; set; }

        /// <summary>
        /// 队列类型（以车辆为主，还是以司机为主）
        /// </summary>
        public string QueueType { get; set; }
    }
}
