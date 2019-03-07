using OneZero.Common.Dtos;
using SouthStar.VehSch.Api.Areas.ApplicationFlow.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthStar.VehSch.Api.Areas.ApplicationFlow.Dtos
{
    public class CheckContentPostData:DataDto
    {
        /// <summary>
        /// 申请单编号
        /// </summary>
        public string ApplyNum { get; set; }

        /// <summary>
        /// 审批人Id
        /// </summary>
        public Guid CheckUserId { get; set; }

        /// <summary>
        /// 审批人姓名
        /// </summary>
        public string CheckUserName { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public CheckStatus CheckStatus { get; set; }

        /// <summary>
        /// 审核意见
        /// </summary>
        public string CheckReply { get; set; }
    }
}
