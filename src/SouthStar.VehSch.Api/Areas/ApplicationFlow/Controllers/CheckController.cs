using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SouthStar.VehSch.Api.Areas.ApplicationFlow.Dtos;
using SouthStar.VehSch.Api.Areas.ApplicationFlow.Models.Enum;
using SouthStar.VehSch.Api.Areas.ApplicationFlow.Services;
using SouthStar.VehSch.Api.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        /// <param name="vehicleManageService"></param>
        public CheckController(ILogger<CheckController> logger, CheckService checkService)
        {
            _logger = logger;
            _checkService = checkService;
        }

        /// <summary>
        /// 查询用车申请列表
        /// </summary>
        /// <param name="plateNumber">车牌号</param>
        /// <param name="currentState">用车申请当前状态</param>
        /// <param name="departmentId">部门ID</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> List(int page, int limit, string applicantId, int? status = null, string applyNum = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            if (applicantId != null)
                if (!Guid.TryParse(applicantId, out _id))
                {
                    return Json(BadParameter("Id格式不匹配"));
                }

            var vehicleList = await _checkService.GetVehicleApplyListAsync( (ApplyState?)status, applyNum, startDate, endDate, limit);
            return Json(vehicleList);
        }


        /// <summary>
        /// 获取详细信息
        /// </summary>
        /// <param name="CheckId">审核ID</param>
        /// <returns></returns>
        [HttpGet("{applynum}")]
        public async Task<IActionResult> DetailItem(string checkId)
        {
            if (!Guid.TryParse(checkId, out _id))
            {
                return Json(BadParameter("Id格式不匹配"));
            }
            var checkInfo = await _checkService.GetCheckItemAsync(_id);
            return Json(checkInfo);
        }

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="checkId">审核Id</param>
        /// <param name="value">审核状态</param>
        /// <returns></returns>
        [HttpPost("{checkId}")]
        public async Task<IActionResult> Check(string checkId, [FromBody]object value)
        {
            if (checkId == null || value == null)
            {
                return Json(BadParameter("审核ID不能为空，请提供正确的审核上下文和审核内容"));
            }

            if (!Guid.TryParse(checkId, out _id))
            {
                return Json(BadParameter("Id格式不匹配"));
            }
            var postData = JsonConvert.DeserializeObject<CheckContentPostData>(value.ToString());
            dto = await _checkService.CheckAsync(_id, postData);
            return Json(dto);
        }
    }
}
