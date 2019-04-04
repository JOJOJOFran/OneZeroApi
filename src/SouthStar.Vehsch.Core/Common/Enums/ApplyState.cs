using OneZero.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthStar.VehSch.Core.Common.Enums
{
    public enum ApplyState
    {
        /// <summary>
        /// 起草
        /// </summary>
        [Remark("起草")]
        Draft = 0,

        /// <summary>
        /// 待审核
        /// </summary>
        [Remark("待审核")]
        WaitCheck = 1,

        /// <summary>
        /// 已审核
        /// </summary>
        [Remark("已审核")]
        Checked = 2,

        /// <summary>
        /// 拒绝
        /// </summary>
        [Remark("拒绝")]
        Refused = 3
    }
}
