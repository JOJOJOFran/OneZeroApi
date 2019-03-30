using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthStar.VehSch.Api.Areas.Notifications.Models
{
    public class RepairRecord
    {
        /// <summary>
        /// 车牌号
        /// </summary>
        public string PlateNum { get; set; }

        /// <summary>
        /// 处理人
        /// </summary>
        public string DealPerson { get; set; }

        /// <summary>
        /// 维修开始日期
        /// </summary>
        public DateTime RepairStartDate { get; set; }

        /// <summary>
        /// 维修结束日期
        /// </summary>
        public DateTime RepairEndDate { get; set; }

        /// <summary>
        /// 维修费用
        /// </summary>
        public float RepairFee { get; set; }

        /// <summary>
        /// 维修厂
        /// </summary>
        public string Garage { get; set; }

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
