using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SouthStar.VehSch.Core.Setting.Dtos;
using SouthStar.VehSch.Core.Setting.Models;
using SouthStar.VehSch.Core.Setting.Services;
using SouthStar.VehSch.Api.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OneZero.AspNetCore;
using OneZero;

namespace SouthStar.VehSch.Api.Areas.Setting.Controllers
{
    [Route("api/[controller]/[action]")]
    public class DepartmentController : BaseController
    {
        private readonly ILogger<DepartmentController> _logger;
        private readonly DepartmentService _departmentService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="departmentService"></param>
        public DepartmentController(ILogger<DepartmentController> logger, DepartmentService departmentService)
        {
            _logger = logger;
            _departmentService = departmentService;
        }


        /// <summary>
        /// 获取全部部门（支持部门名称模糊查询）
        /// </summary>
        /// <param name="name">部门名称</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> AllList( string name = null)
        {
            var depList = await _departmentService.GetAllListAsync(name);
            return Json(depList);
        }


        /// <summary>
        /// 查询部门列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> List(int page, int limit, string name = null)
        {
            var depList = await _departmentService.GetListAsync(name, page, limit);
            return Json(depList);
        }


        /// <summary>
        /// 获取部门信息
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        /// <returns></returns>
        [HttpGet("{departmentId}")]
        public async Task<IActionResult> Item(string departmentId)
        {

            if (!Guid.TryParse(departmentId, out _id))
            {
                return Json(BadParameter("Id格式不匹配"));
            }
            var depInfo = await _departmentService.GetItemAsync(_id);
            return Json(depInfo);
        }

        /// <summary>
        /// 增加部门信息
        /// </summary>
        /// <param name="value">部门信息</param>
        /// <remarks>
        /// 新增部门信息示例
        /// {
        /// }
        /// </remarks>
        /// <model></model>
        /// <returns></returns>      
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]DepartmentData value)
        {
            if (value == null)
            {
                return Json(BadParameter("部门信息内容不能为空"));
            }
            var dto = await _departmentService.AddAsync(value);
            return Json(dto);
        }


        /// <summary>
        /// 更新部门信息
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        /// <param name="value">部门信息</param>
        /// <returns></returns>
        [HttpPost("{departmentId}")]
        public async Task<IActionResult> Update(string departmentId, [FromBody]DepartmentData value)
        {
            if (!Guid.TryParse(departmentId, out _id))
            {
                return Json(BadParameter("Id格式不匹配"));
            }

            if (value == null)
            {
                return Json(BadParameter("部门信息内容不能为空"));
            }

            var dto = await _departmentService.UpdateAsync(_id, value);
            return Json(dto);
        }

        /// <summary>
        /// 删除部门信息(标记删除)
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        /// <returns></returns>
        [HttpDelete("{departmentId}")]
        public async Task<IActionResult> Delete(string departmentId)
        {
            if (!Guid.TryParse(departmentId, out _id))
            {
                return Json(BadParameter("Id格式不匹配"));
            }
            dto = await _departmentService.MarkDeleteAsync(_id);
            return Json(dto);
        }
    }
}
