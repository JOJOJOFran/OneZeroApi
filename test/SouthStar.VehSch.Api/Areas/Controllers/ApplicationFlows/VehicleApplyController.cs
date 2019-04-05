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
using SouthStar.VehSch.Core.ApplicationFlow.Models;

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
        /// <param name="applyService"></param>
        public VehicleApplyController(ILogger<VehicleApplyController> logger, VehicleApplyService applyService)
        {
            _logger = logger;
            _applyService = applyService;
        }


        /// <summary>
        /// 查询用车申请列表
        /// </summary>
        /// <param name="page">页数</param>
        /// <param name="limit">每页行数</param>
        /// <param name="applicantId">申请人ID</param>
        /// <param name="status">状态</param>
        /// <param name="applyNum">申请编号（支持模糊查询）</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> List(int page, int limit, string applicantId, int? status=null, string applyNum=null, DateTime? startDate = null, DateTime? endDate = null)
        {
            if (applicantId != null)
                if (!Guid.TryParse(applicantId, out _id))
                {
                    return Json(BadParameter("Id格式不匹配"));
                }

            var vehicleList = await _applyService.GetListAsync(applicantId == null ? null : (Guid?)_id, (ApplyState?)status, applyNum, startDate, endDate, page, limit);
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
        public async Task<IActionResult> AddNotSend([FromBody]VehicleApplicationData value)
        {
            if (value == null)
            {
                return Json(BadParameter("申请用车信息内容不能为空"));
            }
            var dto = await _applyService.AddNotSendAsync(value);
            return Json(dto);
        }

        /// <summary>
        /// 起草申请信息并送审核
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddAndSend([FromBody]VehicleApplicationData value)
        {
            if (value == null)
            {
                return Json(BadParameter("申请用车信息内容不能为空"));
            }
            var dto = await _applyService.AddAndSendAsync(value);
            return Json(dto);
        }

        /// <summary>
        /// 更新用车申请信息
        /// </summary>
        /// <param name="applyId">用车申请ID</param>
        /// <param name="value">用车申请信息</param>
        /// <returns></returns>
        [HttpPost("{applyId}")]
        public async Task<IActionResult> Update(string applyId, [FromBody]VehicleApplicationData value)
        {
            if (!Guid.TryParse(applyId, out _id))
            {
                return Json(BadParameter("Id格式不匹配"));
            }

            if (value == null)
            {
                return Json(BadParameter("用车申请信息内容不能为空"));
            }
            var dto = await _applyService.UpdateAsync(_id, value);
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
