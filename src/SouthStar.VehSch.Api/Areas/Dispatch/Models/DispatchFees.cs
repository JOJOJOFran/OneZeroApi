using OneZero.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthStar.VehSch.Api.Areas.Dispatch.Models
{
    public class DispatchFees : BaseEntity<Guid>
    {
        /// <summary>
        /// 派车单ID
        /// </summary>
        public int DispatchId { get; set; }

        /// <summary>
        /// 结束里程
        /// </summary>
        public float EndMiles { get; set; }

        /// <summary>
        /// 起始里程
        /// </summary>
        public float StartMiles { get; set; }

        /// <summary>
        /// 里程单价
        /// </summary>
        public float UnitPrice { get; set; }

        /// <summary>
        /// 高速费用
        /// </summary>
        public float HighSpeedFee { get; set; }

        /// <summary>
        /// ETC费用
        /// </summary>
        public float EtcFee { get; set; }

        /// <summary>
        /// 停车费用
        /// </summary>
        public float ParkFee { get; set; }

        /// <summary>
        /// 油费
        /// </summary>
        public float OilFee { get; set; }

        /// <summary>
        /// 总价
        /// </summary>
        public float TotalPrice { get; set; }
    }
}
