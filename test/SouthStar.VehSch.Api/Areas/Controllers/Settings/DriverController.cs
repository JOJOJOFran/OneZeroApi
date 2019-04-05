using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SouthStar.VehSch.Core.Setting.Dtos;
using SouthStar.VehSch.Core.Setting.Models;
using SouthStar.VehSch.Core.Setting.Services;
using SouthStar.VehSch.Core.Common.Enums;
using SouthStar.VehSch.Api.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthStar.VehSch.Api.Areas.Setting.Controllers
{
    [Route("api/[controller]/[action]")]
    public class DriverController : BaseController
    {

        private readonly ILogger<VehicleController> _logger;
        private readonly DriverService _driverService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="driverService"></param>
        public DriverController(ILogger<VehicleController> logger, DriverService driverService)
        {
            _logger = logger;
            _driverService = driverService;
        }


        /// <summary>
        /// 查询司机列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="name"></param>
        /// <param name="drivingLicNum"></param>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> List(int page, int limit, string name = null, string drivingLicNum = null, string departmentId = null)
        {
            Guid depId = default(Guid);
            if (departmentId != null)
                if (!Guid.TryParse(departmentId, out depId))
                {
                    return Json(BadParameter("Id格式不匹配"));
                }

            var driverList = await _driverService.GetListAsync(name, drivingLicNum, departmentId == null ? null : (Guid?)depId, page, limit);
            var a = JsonConvert.SerializeObject(driverList);
            JsonSerializerSettings settings = new JsonSerializerSettings();

            return Json(driverList);
        }


        /// <summary>
        /// 获取可用司机
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> EnableList()
        {
            var driverList = await _driverService.GetEnableList();
            return Ok(driverList);
        }


        /// <summary>
        /// 获取司机信息
        /// </summary>
        /// <param name="driverId">司机ID</param>
        /// <returns></returns>
        [HttpGet("{driverId}")]
        public async Task<IActionResult> Item(string driverId)
        {

            if (!Guid.TryParse(driverId, out _id))
            {
                return Json(BadParameter("Id格式不匹配"));
            }
            var driverInfo = await _driverService.GetItemAsync(_id);
            return Json(driverInfo);
        }

        /// <summary>
        /// 增加司机信息
        /// </summary>
        /// <param name="value">司机信息</param>
        /// <remarks>
        /// 新增司机信息示例
        ///{
        ///  "id": "a45228f2-259c-4204-b1a3-bc31a7f64cd8",
        ///  "departmentId": "d92ba7ef-8930-452d-a003-e80b155929cd",
        ///  "departmentName": null,
        ///  "name": "wangjf",
        ///  "sex": 0,
        ///  "age": 20,
        ///  "phoneNum": "110",
        ///  "mobileNum": "119",
        ///  "address": "普安新村",
        ///  "drivingLicNum": "423r31314",
        ///  "issueDate": "2019-03-09T17:15:03.2681419",
        ///  "expirationDate": "2019-03-09T17:15:03.2681419",
        ///  "permittedType": "unkown",
        ///  "drivingLicType": "unkown",
        ///  "remark": "unkown",
        ///  "status": 0,
        ///  "image": ""
        ///}
        /// </remarks>
        /// <model></model>
        /// <returns></returns>      
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]DriverData value)
        {
            if (value == null)
            {
                return Json(BadParameter("司机信息内容不能为空"));
            }
            var dto = await _driverService.AddAsync(value);
            return Json(dto);
        }


        /// <summary>
        /// 更新司机信息
        /// </summary>
        /// <param name="driverId">司机ID</param>
        /// <param name="value">司机信息</param>
        /// <returns></returns>
        [HttpPost("{driverId}")]
        public async Task<IActionResult> Update(string driverId, [FromBody]DriverData value)
        {
            if (!Guid.TryParse(driverId, out _id))
            {
                return Json(BadParameter("Id格式不匹配"));
            }

            if (value == null)
            {
                return Json(BadParameter("司机信息内容不能为空"));
            }
            var dto = await _driverService.UpdateAsync(_id, value);
            return Json(dto);
        }

        /// <summary>
        /// 改变司机状态
        /// </summary>
        /// <param name="driverId"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [HttpPost("{driverId}")]
        public async Task<IActionResult> ChangeStatus(string driverId, PersonState state)
        {
            if (!Guid.TryParse(driverId, out _id))
            {
                return Json(BadParameter("Id格式不匹配"));
            }
            var dto = await _driverService.ChangeStatusAsync(_id, state);
            return Json(dto);
        }



        /// <summary>
        /// 删除司机信息(标记删除)
        /// </summary>
        /// <param name="driverId">司机ID</param>
        /// <returns></returns>
        [HttpDelete("{driverId}")]
        public async Task<IActionResult> Delete(string driverId)
        {
            if (!Guid.TryParse(driverId, out _id))
            {
                return Json(BadParameter("Id格式不匹配"));
            }
            dto = await _driverService.MarkDeleteAsync(_id);
            return Json(dto);
        }


    }

}
