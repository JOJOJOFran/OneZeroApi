using OneZero.Application.Models;
using SouthStar.VehSch.Api.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthStar.VehSch.Api.Areas.Reminder.Models
{
    /// <summary>
    /// 通知中心表
    /// </summary>
    public class NotificationCenter : BaseEntity<Guid>
    {
        /// <summary>
        /// 通知类型
        /// </summary>
        public NotificationType NotificationType { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 内容ID
        /// </summary>
        public Guid ContentId { get; set; }

        /// <summary>
        /// 是否展示
        /// </summary>
        public bool Visible { get; set; }

        /// <summary>
        /// 是否处理完成
        /// </summary>
        public bool IsDone { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 提醒方式（常规，周期）
        /// </summary>
        public NotificationWay NotificationWay { get; set; }


    }
}
