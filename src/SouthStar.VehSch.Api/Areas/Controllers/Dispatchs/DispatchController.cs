using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SouthStar.VehSch.Api.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SouthStar.VehSch.Core.Dispatch.Services;
using SouthStar.VehSch.Core.Dispatch.Dtos;
using SouthStar.VehSch.Core.Dispatchs.Dtos;

namespace SouthStar.VehSch.Api.Areas.Dispatch.Controllers
{
    [Route("api/[controller]/[action]")]
    public class DispatchController : BaseController
    {
        private readonly ILogger<DispatchController> _logger;
        private readonly VehicleDispatchService _dispatchService;


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="dispatchService"></param>
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
            var vehicleList = await _dispatchService.GetVehicleDispatchListAsync(applyNum, startDate, endDate, page, limit);
            return Json(vehicleList);
        }

        /// <summary>
        /// 获取派车信息
        /// </summary>
        /// <param name="dispatchId">申请用车ID</param>
        /// <returns></returns>
        [HttpGet("{dispatchId}")]
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
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost("{applyId}")]
        public async Task<IActionResult> Dispatch(string applyId, [FromBody]VehicleDispatchPostData value)
        {
            if (!Guid.TryParse(applyId, out _id) || value == null)
            {
                return Json(BadParameter("Id格式不匹配"));
            }
            var dispatchList = await _dispatchService.AddDispatchAsync(_id, value);
            return Json(dispatchList);
        }


        /// <summary>
        /// 重新调度
        /// </summary>
        /// <param name="applyId"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost("{applyId}")]
        public async Task<IActionResult> ReDispatch(string applyId, [FromBody]VehicleDispatchPostData value)
        {
            if (!Guid.TryParse(applyId, out _id) || value == null)
            {
                return Json(BadParameter("Id格式不匹配"));
            }
            var dispatchList = await _dispatchService.UpdateDispatchAsync(_id, value);
            return Json(dispatchList);
        }


        /// <summary>
        /// 快速调度派车(不走申请，直接手动调度派车)
        /// </summary>
        /// <param name="quickDispatch">quickDispatch</param>
        /// <remarks>
        /// {
        ///"applicantId": "99BF7D96-BCD5-4083-8197-BFFBEBE4501F",
        ///"applicantName": "管理员",
        ///"departmentId": "B1B744CF-C67F-48D5-A838-AA230184FF43",
        ///"departmentName": "江夏区财政局",
        ///"userName": "wjf",
        ///"userMobile": "13009876443",
        ///"applyReson": "出差",
        ///"userCount": "1",
        ///"carType": "轿车",
        ///"startPoint": "普安新村",
        ///"destination": "体育馆",
        ///"carProperty": "0",
        ///"startPlanTime": "2019-04-02T15:04:49.361Z",
        ///"backPlanTime": "2019-04-02T15:04:49.362Z",
        ///"remark": "一个小时",
        ///"driverId":"AE363D32-0A70-49C8-9BA3-9A9FDAE8B1F9",
        ///"driverName": "wangjf",
        ///"driverPhone": "110",
        ///"vehicleId": "9280B0D7-D7F6-4A62-8443-CED52A703939",
        ///"plateNumber": "鄂AX39007"
        ///}
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> QuickDispatch([FromBody] QuickDispatchPostData quickDispatch)
        {
            if (quickDispatch == null)
            {
                return Json(BadParameter("派车单内容不能为空 "));
            }
            //var value= JsonConvert.DeserializeObject<QuickDispatchPostData>(quickDispatch.ToString());
            var output = await _dispatchService.QuickDispatchAsync(quickDispatch);
            return Ok(output);
        }


    }
}
