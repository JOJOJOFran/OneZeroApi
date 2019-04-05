using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SouthStar.VehSch.Core.Dispatch.Services;
using SouthStar.VehSch.Core.Dispatch.Dtos;
using SouthStar.VehSch.Api.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthStar.VehSch.Api.Areas.Dispatch.Controllers
{
    /// <summary>
    /// 用车费用
    /// </summary>
    public class FeeController : BaseController
    {
        private readonly ILogger<FeeController> _logger;
        private readonly DispatchFeeService _feeService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="feeService"></param>
        public FeeController(ILogger<FeeController> logger, DispatchFeeService feeService)
        {
            _logger = logger;
            _feeService = feeService;
        }

        /// <summary>
        /// 查询费用列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="applyNum"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> List(int page, int limit, string applyNum = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            var vehicleList = await _feeService.GetFeeListAsync(applyNum, startDate, endDate, page, limit);
            return Json(vehicleList);
        }

        /// <summary>
        /// 新增费用
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]DispatchFeeData value)
        {
            if (value == null)
            {
                return Json(BadParameter("参数不能为空"));
            }
            var dto = await _feeService.AddFeeAsync(value);
            return Json(dto);
        }

        /// <summary>
        /// 更新费用
        /// </summary>
        /// <param name="feeId">用车费用ID</param>
        /// <param name="value">用车费用信息</param>
        /// <returns></returns>
        [HttpPost("{feeId}")]
        public async Task<IActionResult> Update(string feeId, [FromBody]DispatchFeeData value)
        {
            if (!Guid.TryParse(feeId, out _id))
            {
                return Json(BadParameter("Id格式不匹配"));
            }

            if (value == null)
            {
                return Json(BadParameter("用车费用信息内容不能为空"));
            }

            var dto = await _feeService.UpdateFeeAsync(_id, value);
            return Json(dto);
        }
    }
}
