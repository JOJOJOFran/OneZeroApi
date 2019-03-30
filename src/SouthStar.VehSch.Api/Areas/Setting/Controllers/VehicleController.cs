using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SouthStar.VehSch.Api.Areas.Setting.Dtos;
using SouthStar.VehSch.Api.Areas.Setting.Models;
using SouthStar.VehSch.Api.Areas.Setting.Models.Enum;
using SouthStar.VehSch.Api.Areas.Setting.Services;
using SouthStar.VehSch.Api.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthStar.VehSch.Api.Areas.Setting.Controllers
{
    [Route("api/[controller]/[action]")]
    public class VehicleController:BaseController
    {
        private readonly ILogger<VehicleController> _logger;
        private readonly VehcileService _vehicleManageService;
        

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="vehicleManageService"></param>
        public VehicleController(ILogger<VehicleController> logger, VehcileService vehicleManageService)
        {
            _logger = logger;
            _vehicleManageService = vehicleManageService;
        }


        /// <summary>
        /// 查询车辆列表
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="limit">每一页行数</param>
        /// <param name="plateNumber">车牌号</param>
        /// <param name="currentState">车辆当前状态</param>
        /// <param name="departmentId">部门ID</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> List(int page, int limit,string plateNumber = null, int? currentState = null, string departmentId = null)
        {
            Guid depId = default(Guid);
            if (departmentId != null)
                if (!Guid.TryParse(departmentId, out depId))
                {
                    return Json(BadParameter("Id格式不匹配"));
                }     

            var vehicleList = await _vehicleManageService.GetListAsync(plateNumber, currentState, departmentId==null? null: (Guid?)depId, page,limit);
            return Json(vehicleList);
        }

 
        /// <summary>
        /// 获取可用车辆
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> EnableList()
        {
            var vehicleList = await _vehicleManageService.GetEnableList();
            return Ok(vehicleList);
        }


        /// <summary>
        /// 获取车辆信息
        /// </summary>
        /// <param name="VehicleId">车辆ID</param>
        /// <returns></returns>
        [HttpGet("{vehicleId}")]
        public async Task<IActionResult> Item(string vehicleId)
        {

            if (!Guid.TryParse(vehicleId, out _id))
            {
                return Json(BadParameter("Id格式不匹配"));
            }
            var vehicleInfo = await _vehicleManageService.GetItemAsync(_id);
            return Ok(Json(vehicleInfo));
        }

        /// <summary>
        /// 增加车辆信息
        /// </summary>
        /// <param name="value">车辆信息</param>
        /// <remarks>
        /// 新增车辆信息示例
        /// {
        /// "driverId": 驾驶员ID,
        /// "driverName": 驾驶员信息,
        /// "departmentId": 部门ID,
        /// "departmentName": 部门名称,
        /// "plateNumber": 车牌号,
        /// "plateColor": 车牌颜色,
        /// "vehicleBrand": 车辆品牌Code,
        /// "vechileType": 车辆类型Code,
        /// "approvedSeating": 核定座位数,
        /// "vin": 车辆VIN（唯一）,
        /// "engineNo": 发动机编号（唯一）,
        /// "purchasePrice": 购入价格,
        /// "terminalNo": GPS终端编号,
        /// "initMileage": 初始里程数,
        /// "tankCapacity": 油箱容积,
        /// "vehicleLicOwner": 行驶证拥有者,
        /// "bookOriginValue": 账面净值,
        /// "displacament": 排量,
        /// "vehicleProperties": 车辆性质,
        /// "faCardNum": 固定资产编号,
        /// "oilType": 汽油类型Code,
        /// "carIcon": 车辆图标,
        /// "loadWeight": 载重质量,
        /// "curbWeight": 整备质量,
        /// "remark": 备注,
        /// "image": 车辆图片
        /// }
        /// </remarks>
        /// <model></model>
        /// <returns></returns>      
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]object value)
        {
            if (value == null)
            {
                return Json(BadParameter("车辆信息内容不能为空"));
            }
            var vehicleInfo = JsonConvert.DeserializeObject<VehicleData>(value.ToString());
            var dto = await _vehicleManageService.AddAsync(vehicleInfo);
            return Json(dto);
        }


        /// <summary>
        /// 更新车辆信息
        /// </summary>
        /// <param name="vehicleId">车辆ID</param>
        /// <param name="value">车辆信息</param>
        /// <returns></returns>
        [HttpPost("{vehicleId}")]
        public async Task<IActionResult> Update(string vehicleId, [FromBody]object value)
        {
            if (!Guid.TryParse(vehicleId, out _id))
            {
                return Json(BadParameter("Id格式不匹配"));
            }

            if (value == null)
            {
                return Json(BadParameter("车辆信息内容不能为空"));
            }
            var vehicleInfo = JsonConvert.DeserializeObject<Vehicles>(value.ToString());
            var dto = await _vehicleManageService.UpdateAsync(_id, vehicleInfo);
            return Json(dto);
        }


        /// <summary>
        /// 改成车辆状态
        /// </summary>
        /// <param name="vehicleId"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [HttpPost("{vehicleId}")]
        public async Task<IActionResult> ChangeStatus(string vehicleId, CurrentState state)
        {
            if (!Guid.TryParse(vehicleId, out _id))
            {
                return Json(BadParameter("Id格式不匹配"));
            }

            var dto = await _vehicleManageService.ChangeStatusAsync(_id, state);
            return Json(dto);
        }

        /// <summary>
        /// 删除车辆信息(标记删除)
        /// </summary>
        /// <param name="vehicleId">车辆ID</param>
        /// <returns></returns>
        [HttpDelete("{VehicleId}")]
        public async Task<IActionResult> Delete(string vehicleId)
        {
            if (!Guid.TryParse(vehicleId, out _id))
            {
                return Json(BadParameter("Id格式不匹配"));
            }
            dto = await _vehicleManageService.MarkDeleteAsync(_id);
            return Ok(dto);
        }
    }

}
