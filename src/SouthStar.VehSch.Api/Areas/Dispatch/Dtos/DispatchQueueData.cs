using OneZero.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthStar.VehSch.Api.Areas.Dispatch.Dtos
{
    public class DispatchQueueData:DataDto
    {

        public Guid? Id { get; set; } 

        /// <summary>
        /// 队列编号
        /// </summary>
        public int QueueNo { get; set; }

        /// <summary>
        /// 车辆Id
        /// </summary>
        public Guid? VehicleId { get; set; }

        /// <summary>
        /// 司机Id
        /// </summary>
        public Guid DriverId { get; set; }

        /// <summary>
        /// 车辆类型码
        /// </summary>
        public string VehicleType { get; set; }

        /// <summary>
        /// 车辆类型性质码
        /// </summary>
        public string VehicleProperty { get; set; }

        /// <summary>
        /// 出车次数
        /// </summary>
        public int WorkCount { get; set; }

        /// <summary>
        /// 状态
        /// 0:可用
        /// 1：出车
        /// 2：不可用
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 队列类型（以车辆为主，还是以司机为主）
        /// </summary>
        public string QueueType { get; set; }
    }
}
