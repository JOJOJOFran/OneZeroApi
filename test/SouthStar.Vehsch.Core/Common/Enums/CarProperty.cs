using OneZero.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthStar.VehSch.Core.Common.Enums
{
    public enum CarProperty
    {
        [Remark("公务用车")]
        Official=0,
        [Remark("应急执法")]
        Emergency =1
    }
}
