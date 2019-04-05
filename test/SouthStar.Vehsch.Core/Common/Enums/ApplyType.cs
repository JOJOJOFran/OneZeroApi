using OneZero.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthStar.VehSch.Core.Common.Enums
{
    public enum ApplyType
    {
        [Remark("用车申请")]
        VehicleApply=0,
        [Remark("请假申请")]
        LeaveApply=1,
    }
}
