using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthStar.VehSch.Api.Areas.Notifications.Models
{
    public class InsuranceReminder
    {
        /// <summary>
        /// 车牌号
        /// </summary>
        public string PlateNum { get; set; }

        /// <summary>
        /// 车辆品牌
        /// </summary>
        public string VehiclBrand { get; set; }

        /// <summary>
        /// 保险开始日期
        /// </summary>
        public DateTime InsuranceStartDate { get; set; }

        /// <summary>
        /// 保险到期日期
        /// </summary>
        public DateTime InsuranceEndDate { get; set; }


        /// <summary>
        /// 保险类型
        /// </summary>
        public string InsuranceType { get; set; }

        /// <summary>
        /// 保险费用
        /// </summary>
        public float InsuranceFee { get; set; }

        /// <summary>
        /// 经办人
        /// </summary>
        public string DealPerson { get; set; }

        /// <summary>
        /// 开始提醒时间
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
