using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthStar.VehSch.Api.Areas.Notifications.Models
{
    public class MaintainReminder
    {
        /// <summary>
        /// 车牌号
        /// </summary>
        public string PlateNum { get; set; }

        /// <summary>
        /// 车辆类型
        /// </summary>
        public string VehicleType { get; set; }

        /// <summary>
        /// 上次保养时间
        /// </summary>
        public DateTime LastMaintainTime { get; set; }

        /// <summary>
        /// 保养里程
        /// </summary>
        public string MaintainMiles { get; set; }

        /// <summary>
        /// 提醒时间
        /// </summary>
        public DateTime ReminderDate { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 提醒范围
        /// </summary>
        public int ReminderRange { get; set; }

        /// <summary>
        /// 起草日期
        /// </summary>
        public DateTime CreateDate { get; set; }
    }
}
