using OneZero.Application.Models;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SouthStar.VehSch.Api.Areas.ApplicationFlow.Models.Enum;

namespace SouthStar.VehSch.Api.Areas.ApplicationFlow.Models
{
    public class CheckContents : BaseEntity<Guid>
    {
        /// <summary>
        /// 申请单编号
        /// </summary>
        [MaxLength(50)]
        public string ApplyNum { get; set; }

        public Guid ApplyId { get; set; }

        /// <summary>
        /// 审批人Id
        /// </summary>
        public Guid CheckUserId { get; set; }

        /// <summary>
        /// 审批人姓名
        /// </summary>
        [MaxLength(30)]
        public string CheckUserName { get; set; }

        /// <summary>
        /// 审批类型
        /// </summary>
        public ApplyType ApplyType { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public CheckStatus CheckStatus { get; set; }

        /// <summary>
        /// 审核意见
        /// </summary>
        [MaxLength(500)]
        public string CheckReply { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime LastUpdateTime { get; set; }
    }
}
