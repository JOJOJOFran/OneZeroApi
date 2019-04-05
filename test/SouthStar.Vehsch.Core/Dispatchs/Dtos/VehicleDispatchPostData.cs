using OneZero.Dtos;
using SouthStar.VehSch.Core.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthStar.VehSch.Core.Dispatch.Dtos
{
    public class VehicleDispatchPostData:DataDto
    {
        /// <summary>
        /// 司机ID
        /// </summary>
        public Guid DriverId { get; set; }

        /// <summary>
        /// 司机名称
        /// </summary>
        public string DriverName { get; set; }

        /// <summary>
        /// 司机电话
        /// </summary>
        public string DriverPhone { get; set; }

        /// <summary>
        /// 车辆Id
        /// </summary>
        public Guid VehicleId { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string PlateNumber { get; set; }

        /// <summary>
        /// 派车方式
        /// 0：自动派车  1：手动派车
        /// </summary>
        public DispatchType DispatchType { get; set; }

        /// <summary>
        /// 派车队列编号
        /// 自动派车的时候带入
        /// </summary>
        public int? QueueNo { get; set; }

        /// <summary>
        /// 队列ID
        /// </summary>
        public Guid? QueueId { get; set; }
    }
}
