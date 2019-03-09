using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SouthStar.VehSch.Api.Areas.Setting.Dtos;
using SouthStar.VehSch.Api.Areas.Setting.Models;
using SouthStar.VehSch.Api.Areas.Setting.Services;
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
        /// <param name="vehicleManageService"></param>
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
        public async Task<IActionResult> List(int page , int limit ,string name = null, string drivingLicNum = null, string departmentId = null)
        {
            Guid depId = default(Guid);
            if (departmentId != null)
                if (!Guid.TryParse(departmentId, out depId))
                {
                    return Json(BadParameter("Id格式不匹配"));
                }

            var vehicleList = await _driverService.GetListAsync(name, drivingLicNum, departmentId == null ? null : (Guid?)depId, page, limit);
            return Json(vehicleList);
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
            var vehicleInfo = await _driverService.GetItemAsync(_id);
            return Json(vehicleInfo);
        }

        /// <summary>
        /// 增加司机信息
        /// </summary>
        /// <param name="value">司机信息</param>
        /// <remarks>
        /// 新增司机信息示例
        /// {

        /// }
        /// </remarks>
        /// <model></model>
        /// <returns></returns>      
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]object value)
        {
            if (value == null)
            {
                return Json(BadParameter("司机信息内容不能为空"));
            }
            var vehicleInfo = JsonConvert.DeserializeObject<DriverData>(value.ToString());
            var dto = await _driverService.AddAsync(vehicleInfo);
            return Json(dto);
        }


        /// <summary>
        /// 更新司机信息
        /// </summary>
        /// <param name="driverId">司机ID</param>
        /// <param name="value">司机信息</param>
        /// <returns></returns>
        [HttpPost("{driverId}")]
        public async Task<IActionResult> Update(string driverId, [FromBody]object value)
        {
            if (!Guid.TryParse(driverId, out _id))
            {
                return Json(BadParameter("Id格式不匹配"));
            }

            if (value == null)
            {
                return Json(BadParameter("司机信息内容不能为空"));
            }
            var vehicleInfo = JsonConvert.DeserializeObject<Drivers>(value.ToString());
            var dto = await _driverService.UpdateAsync(_id, vehicleInfo);
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
