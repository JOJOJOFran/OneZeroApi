using Microsoft.AspNetCore.Mvc;
using OneZero.Common.Extensions;
using OneZero.Core.Dtos.Permission;
using OneZero.Core.Services.Permission;
using SouthStar.VehSch.Api.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthStar.VehSch.Api.Areas.Controllers.Permissions
{
    public class PermissionController : BaseController
    {
        private readonly ModulePermissionService _modulePermissionService;

        public PermissionController(ModulePermissionService modulePermissionService)
        {
            _modulePermissionService = modulePermissionService;
        }

        /// <summary>
        /// 查询权限列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="name"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> List(int page, int limit, string name = null, string code = null)
        {
            var list = await _modulePermissionService.GetPermissionListAsync(code, name, page, limit);
            return Json(list);
        }


        /// <summary>
        /// 获取权限信息
        /// </summary>
        /// <param name="Id">权限ID</param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        public async Task<IActionResult> Item(string Id)
        {
            var userInfo = await _modulePermissionService.GetPermissionItmeAsync(Id.ConvertToGuid());
            return Json(userInfo);
        }

        /// <summary>
        /// 新增权限
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]PermissionData value)
        {
            if (value == null)
            {
                return Json(BadParameter("用户信息内容不能为空"));
            }
            var dto = await _modulePermissionService.AddPermissionAsync(value);
            return Json(dto);
        }

        /// <summary>
        /// 更新权限信息
        /// </summary>
        /// <param name="Id">ID</param>
        /// <param name="value">菜单信息</param>
        /// <returns></returns>
        [HttpPost("{Id}")]
        public async Task<IActionResult> Update(string Id, [FromBody]PermissionData value)
        {

            if (value == null)
            {
                return Json(BadParameter("用户信息内容不能为空"));
            }
            var dto = await _modulePermissionService.UpdatePermissionAsync(Id.ConvertToGuid(), value);
            return Json(dto);
        }

        /// <summary>
        /// 删除权限(不使用标记删除，因为行号唯一)
        /// </summary>
        /// <param name="Id">权限ID</param>
        /// <returns></returns>
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(string Id)
        {
            if (!Guid.TryParse(Id, out _id))
            {
                return Json(BadParameter("Id格式不匹配"));
            }
            dto = await _modulePermissionService.DeletePermissionAsync(_id);
            return Ok(dto);
        }
    }
}
