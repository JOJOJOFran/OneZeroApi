using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SouthStar.VehSch.Api.Areas.Dispatch.Dtos;
using SouthStar.VehSch.Api.Areas.Dispatch.Services;
using SouthStar.VehSch.Api.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthStar.VehSch.Api.Areas.Dispatch.Controllers
{
    [Route("api/[controller]/[action]")]
    public class DispatchController:BaseController
    {
        private readonly ILogger<DispatchController> _logger;
        private readonly VehicleDispatchService _dispatchService;


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="VehicleDispatchService"></param>
        public DispatchController(ILogger<DispatchController> logger, VehicleDispatchService dispatchService)
        {
            _logger = logger;
            _dispatchService = dispatchService;
        }

        /// <summary>
        /// 查询派车列表
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
            var vehicleList = await _dispatchService.GetVehicleDispatchListAsync( applyNum, startDate, endDate, limit);
            return Json(vehicleList);
        }

        /// <summary>
        /// 获取派车信息
        /// </summary>
        /// <param name="applyId">申请用车ID</param>
        /// <returns></returns>
        [HttpGet("{applyId}")]
        public async Task<IActionResult> Item(string dispatchId)
        {
            if (!Guid.TryParse(dispatchId, out _id))
            {
                return Json(BadParameter("Id格式不匹配"));
            }
            var applyInfo = await _dispatchService.GetItemAsync(_id);
            return Json(applyInfo);
        }

        /// <summary>
        /// 生成派车单
        /// </summary>
        /// <param name="applyId"></param>
        /// <returns></returns>
        [HttpPost("{applyId}")]
        public async Task<IActionResult> Dispatch(string applyId, [FromBody]Object value)
        {
            if (!Guid.TryParse(applyId, out _id)|| value==null)
            {
                return Json(BadParameter("Id格式不匹配"));
            }
            var postData = JsonConvert.DeserializeObject<VehicleDispatchPostData>(value.ToString());
            var dispatchList = await _dispatchService.AddDispatchAsync(_id, postData);
            return Json(dispatchList);
        }


        /// <summary>
        /// 重新调度
        /// </summary>
        /// <param name="applyId"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost("{applyId}")]
        public async Task<IActionResult> ReDispatch(string applyId, [FromBody]Object value)
        {
            if (!Guid.TryParse(applyId, out _id) || value == null)
            {
                return Json(BadParameter("Id格式不匹配"));
            }
            var postData = JsonConvert.DeserializeObject<VehicleDispatchPostData>(value.ToString());
            var dispatchList = await _dispatchService.UpdateDispatchAsync(_id, postData);
            return Json(dispatchList);
        }
    }
}
