using OneZero.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthStar.VehSch.Api.Common.Enum
{
    public enum PersonState
    {
        /// <summary>
        /// 空闲（可用）
        /// </summary>
        [Remark("空闲")]
        OnWait =0,

        /// <summary>
        /// 值班
        /// </summary>
        [Remark("值班")]
        OnDuty =1,

        /// <summary>
        /// 出车
        /// </summary>
        [Remark("出车")]
        OnWork =2,

        /// <summary>
        /// 请假
        /// </summary>
        [Remark("请假")]
        Leave =3
    }
}
