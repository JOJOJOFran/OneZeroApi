using OneZero.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthStar.VehSch.Api.Areas.Dispatch.Models.Enums
{
    public enum DispatchType
    {
        [Remark("自动派车")]
        Auto=0,
        [Remark("手动派车")]
        Manually =1
    }
}
