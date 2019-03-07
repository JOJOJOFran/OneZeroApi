using OneZero.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthStar.VehSch.Api.Areas.ApplicationFlow.Models.Enum
{
    public enum CheckStatus
    {
        [Remark("未审核")]
        WaitCheck = 0,
        [Remark("审核通过")]
        Approved=1,
        [Remark("拒绝")]
        Refused =2,
        [Remark("已派车")]
        Dispatched =3
    }
}
