using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OneZero.Common.Extensions;
using OneZero.Core.Dtos.Permission;
using OneZero.Core.Dtos.Permissions;
using OneZero.Core.Models.Permissions;
using OneZero.Core.Services.Permission;
using SouthStar.VehSch.Api.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SouthStar.VehSch.Api.Areas.Controllers.Permissions
{
    /// <summary>
    /// 带单模块
    /// </summary>
    public class ModuleController : BaseController
    {
        private readonly ModulePermissionService _modulePermissionService;

        public ModuleController(ModulePermissionService modulePermissionService)
        {
            _modulePermissionService = modulePermissionService;
        }


        /// <summary>
        /// 查询菜单列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="name"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> List(int page, int limit, string name = null,string code=null)
        {
            var list = await _modulePermissionService.GetModuleListAsync(code,name, page, limit);
            return Json(list);
        }

        /// <summary>
        /// 获得所有菜单树
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> AllList()
        {
            var list = await _modulePermissionService.GetAllModuleTreeAsync();
            return Json(list);
        }

        /// <summary>
        /// 获取菜单信息
        /// </summary>
        /// <param name="Id">角色ID</param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        public async Task<IActionResult> Item(string Id)
        {
            var userInfo = await _modulePermissionService.GetModuleItmeAsync(Id.ConvertToGuid());
            return Json(userInfo);
        }

        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]ModuleData value)
        {
            if (value == null)
            {
                return Json(BadParameter("用户信息内容不能为空"));
            }
            var dto = await _modulePermissionService.AddModuleAsync(value);
            return Json(dto);
        }

        /// <summary>
        /// 更新菜单信息
        /// </summary>
        /// <param name="Id">ID</param>
        /// <param name="value">菜单信息</param>
        /// <returns></returns>
        [HttpPost("{Id}")]
        public async Task<IActionResult> Update(string Id, [FromBody]ModuleData value)
        {

            if (value == null)
            {
                return Json(BadParameter("用户信息内容不能为空"));
            }
            var dto = await _modulePermissionService.UpdateModuleAsync(Id.ConvertToGuid(), value);
            return Json(dto);
        }

        /// <summary>
        /// 删除菜单信息(标记删除)
        /// </summary>
        /// <param name="roleId">用户ID</param>
        /// <returns></returns>
        [HttpDelete("{roleId}")]
        public async Task<IActionResult> Delete(string roleId)
        {
            if (!Guid.TryParse(roleId, out _id))
            {
                return Json(BadParameter("Id格式不匹配"));
            }
            dto = await _modulePermissionService.MarkDeleteModuleAsync(_id);
            return Ok(dto);
        }


        /// <summary>
        /// 获取菜单权限列表
        /// </summary>
        /// <param name="Id">用户ID</param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        public async Task<IActionResult> ModuleList(string Id)
        {
            if (!Guid.TryParse(Id, out _id))
            {
                return Json(BadParameter("Id格式不匹配"));
            }
            var dto = await _modulePermissionService.GetModulePermissionAsync(_id);
            return Json(dto);
        }

    }
}
