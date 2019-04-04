using OneZero.Dtos;
using SouthStar.VehSch.Core.Dispatch.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthStar.VehSch.Core.Dispatch.Services
{
    public interface IAutoDispatch
    {
        /// <summary>
        /// 自动派车方法
        /// </summary>
        /// <returns>返回自动派车数据，如果为空表示派车队列为空</returns>
        AutoDispatchData AutoDispatch();
    }
}
