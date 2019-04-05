﻿using OneZero.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthStar.VehSch.Core.ApplicationFlow.Dtos
{
    /// <summary>
    /// 用车申请审核列表
    /// </summary>
    public class VehicleApplyCheckListData : DataDto
    {
        /// <summary>
        /// 审批ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 申请单编号
        /// </summary>
        public string ApplyNum { get; set; }


        /// <summary>
        /// 审批人Id
        /// </summary>
        public Guid? CheckUserId { get; set; }

        /// <summary>
        /// 审批人姓名
        /// </summary>
        public string CheckUserName { get; set; }

        /// <summary>
        /// 审批类型
        /// </summary>
        public string ApplyType { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public string CheckStatus { get; set; }

        /// <summary>
        /// 审核意见
        /// </summary>
        public string CheckReply { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatDate { get; set; }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime LastUpdateTime { get; set; }
    }
}
