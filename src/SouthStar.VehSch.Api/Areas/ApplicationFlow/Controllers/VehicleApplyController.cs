using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SouthStar.VehSch.Api.Areas.ApplicationFlow.Dtos;
using SouthStar.VehSch.Api.Areas.ApplicationFlow.Models;
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
    public class VehicleApplyController : BaseController
    {
        private readonly ILogger<VehicleApplyController> _logger;
        private readonly VehicleApplyService _applyService;


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="vehicleManageService"></param>
        public VehicleApplyController(ILogger<VehicleApplyController> logger, VehicleApplyService applyService)
        {
            _logger = logger;
            _applyService = applyService;
        }


        /// <summary>
        /// 查询用车申请列表
        /// </summary>
        /// <param name="plateNumber">车牌号</param>
        /// <param name="currentState">用车申请当前状态</param>
        /// <param name="departmentId">部门ID</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> List(int page, int limit, string applicantId, int? status=null, string applyNum=null, DateTime? startDate = null, DateTime? endDate = null)
        {
            if (applicantId != null)
                if (!Guid.TryParse(applicantId, out _id))
                {
                    return Json(BadParameter("Id格式不匹配"));
                }

            var vehicleList = await _applyService.GetListAsync(applicantId == null ? null : (Guid?)_id, (ApplyState?)status, applyNum, startDate, endDate, limit);
            return Json(vehicleList);
        }


        /// <summary>
        /// 获取申请用车信息
        /// </summary>
        /// <param name="applyId">申请用车ID</param>
        /// <returns></returns>
        [HttpGet("{applyId}")]
        public async Task<IActionResult> Item(string applyId)
        {
            if (!Guid.TryParse(applyId, out _id))
            {
                return Json(BadParameter("Id格式不匹配"));
            }
            var applyInfo = await _applyService.GetItemAsync(_id);
            return Json(applyInfo);
        }

        /// <summary>
        /// 起草申请信息但不送审核
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddNotSend([FromBody]object value)
        {
            if (value == null)
            {
                return Json(BadParameter("申请用车信息内容不能为空"));
            }
            var vehicleApplyInfo = JsonConvert.DeserializeObject<VehicleApplicationData>(value.ToString());
            var dto = await _applyService.AddNotSendAsync(vehicleApplyInfo);
            return Json(dto);
        }

        /// <summary>
        /// 起草申请信息并送审核
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddAndSend([FromBody]object value)
        {
            if (value == null)
            {
                return Json(BadParameter("申请用车信息内容不能为空"));
            }
            var vehicleApplyInfo = JsonConvert.DeserializeObject<VehicleApplicationData>(value.ToString());
            var dto = await _applyService.AddAndSendAsync(vehicleApplyInfo);
            return Json(dto);
        }

        /// <summary>
        /// 更新用车申请信息
        /// </summary>
        /// <param name="applyId">用车申请ID</param>
        /// <param name="value">用车申请信息</param>
        /// <returns></returns>
        [HttpPost("{applyId}")]
        public async Task<IActionResult> Update(string applyId, [FromBody]object value)
        {
            if (!Guid.TryParse(applyId, out _id))
            {
                return Json(BadParameter("Id格式不匹配"));
            }

            if (value == null)
            {
                return Json(BadParameter("用车申请信息内容不能为空"));
            }
            var vehicleInfo = JsonConvert.DeserializeObject<VehicleApplications>(value.ToString());
            var dto = await _applyService.UpdateAsync(_id, vehicleInfo);
            return Json(dto);
        }

        /// <summary>
        /// 删除用车申请信息(标记删除)
        /// </summary>
        /// <param name="applyId">用车申请ID</param>
        /// <returns></returns>
        [HttpDelete("{applyId}")]
        public async Task<IActionResult> Delete(string applyId)
        {
            if (!Guid.TryParse(applyId, out _id))
            {
                return Json(BadParameter("Id格式不匹配"));
            }
            dto = await _applyService.MarkDeleteAsync(_id);
            return Ok(dto);
            //return Json(dto);
        }
    }
}
