using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SouthStar.VehSch.Api.Controllers;
using SouthStar.VehSch.Core.ApplicationFlow.Dtos;
using SouthStar.VehSch.Core.Common.Enums;
using SouthStar.VehSch.Core.ApplicationFlow.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OneZero.Common.Extensions;

namespace SouthStar.VehSch.Api.Areas.ApplicationFlow.Controllers
{
    [Route("api/[controller]/[action]")]
    public class CheckController : BaseController
    {
        private readonly ILogger<CheckController> _logger;
        private readonly CheckService _checkService;


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="checkService"></param>
        public CheckController(ILogger<CheckController> logger, CheckService checkService)
        {
            _logger = logger;
            _checkService = checkService;
        }

        /// <summary>
        /// 查询用车审核列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="applicantId"></param>
        /// <param name="status"></param>
        /// <param name="applyNum"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> List(int page, int limit, string applicantId, int? status = null, string applyNum = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            if (applicantId != null)
                if (!Guid.TryParse(applicantId, out _id))
                {
                    return Json(BadParameter("Id格式不匹配"));
                }

            var vehicleList = await _checkService.GetVehicleApplyListAsync( (ApplyState?)status, applyNum, startDate, endDate, page, limit);
            return Json(vehicleList);
        }


        /// <summary>
        /// 获取审核信息
        /// </summary>
        /// <param name="checkId">审核ID</param>
        /// <returns></returns>
        [HttpGet("{checkId}")]
        public async Task<IActionResult> CheckItem(string checkId)
        {
            if (!Guid.TryParse(checkId, out _id))
            {
                return Json(BadParameter("Id格式不匹配"));
            }
            var checkInfo = await _checkService.GetCheckItemAsync(_id);
            return Json(checkInfo);
        }


        /// <summary>
        /// 获取申请信息和审核信息
        /// </summary>
        /// <param name="applyId"></param>
        /// <returns></returns>
        [HttpGet("{applyId}")]
        public async Task<IActionResult> DetailItem(string applyId)
        {

            var checkInfo = await _checkService.GetApplyWithCheckContent(applyId.ConvertToGuid());
            return Json(checkInfo);
        }


        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="checkId">审核Id</param>
        /// <param name="value">审核状态</param>
        /// <returns></returns>
        [HttpPost("{checkId}")]
        public async Task<IActionResult> Check(string checkId, [FromBody]CheckContentPostData value)
        {
            if (checkId == null || value == null)
            {
                return Json(BadParameter("审核ID不能为空，请提供正确的审核上下文和审核内容"));
            }

            if (!Guid.TryParse(checkId, out _id))
            {
                return Json(BadParameter("Id格式不匹配"));
            }
            dto = await _checkService.CheckAsync(_id, value);
            return Json(dto);
        }
    }
}
