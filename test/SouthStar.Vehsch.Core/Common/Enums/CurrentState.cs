﻿using OneZero.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthStar.VehSch.Core.Common.Enums
{
    /// <summary>
    /// 车辆当前状态
    /// </summary>
    public enum CurrentState
    {
        /// <summary>
        /// 空闲
        /// </summary>
        [Remark("空闲")]
        OnWait =1,

        /// <summary>
        /// 出车
        /// </summary>
        [Remark("出车")]
        OnDuty =2,

        /// <summary>
        /// 不可用
        /// </summary>
        [Remark("不可用")]
        Unable = 3,

        /// <summary>
        /// 不可用
        /// </summary>
        [Remark("维修")]
        Repair = 4
    }
}
