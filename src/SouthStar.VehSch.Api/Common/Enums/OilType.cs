using OneZero.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthStar.VehSch.Api.Areas.Setting.Models.Enum
{
    public enum OilType
    {
        /// <summary>
        /// 柴油
        /// </summary>
        [Remark("柴油")]
        DieselOil =0,

        /// <summary>
        /// 汽油
        /// </summary>
        [Remark("汽油")]
        GasOilne = 1,

        /// <summary>
        /// 天然气
        /// </summary>
        [Remark("天然气")]
        Gas = 2,

        /// <summary>
        /// 电能源
        /// </summary>
        [Remark("电能源")]
        ElectricEnergy =3
    }
}
