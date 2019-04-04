using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OneZero.Common.Extensions;
using OneZero.Core.Dtos.Permission;
using OneZero.Core.Models.Permissions;
using OneZero.Core.Services.Permission;
using SouthStar.VehSch.Api.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthStar.VehSch.Api.Areas.Controllers.Permissions
{
    public class RoleController : BaseController
    {
        private readonly RoleService _roleService;

        public RoleController(RoleService roleService)
        {
            _roleService = roleService;
        }

        /// <summary>
        /// 查询角色列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="name">角色名称</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> List(int page, int limit, string name = null)
        {
            var list = await _roleService.GetRoleListAsync(name, page, limit);
            return Json(list);
        }


        /// <summary>
        /// 获取角色信息
        /// </summary>
        /// <param name="Id">角色ID</param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        public async Task<IActionResult> Item(string Id)
        {
            var userInfo = await _roleService.GetRoleItmeAsync(Id.ConvertToGuid());
            return Json(userInfo);
        }


        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]RoleData value)
        {
            if (value == null)
            {
                return Json(BadParameter("用户信息内容不能为空"));
            }
            var dto = await _roleService.AddRoleAsync(value);
            return Json(dto);
        }

        /// <summary>
        /// 更新角色信息
        /// </summary>
        /// <param name="Id">角色ID</param>
        /// <param name="value">角色信息</param>
        /// <returns></returns>
        [HttpPost("{Id}")]
        public async Task<IActionResult> Update(string Id, [FromBody]RoleData value)
        {

            if (value == null)
            {
                return Json(BadParameter("用户信息内容不能为空"));
            }
            var dto = await _roleService.UpdateRoleAsync(Id.ConvertToGuid(), value);
            return Json(dto);
        }

        /// <summary>
        /// 删除用户信息(标记删除)
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
            dto = await _roleService.MarkDeleteRoleAsync(_id);
            return Ok(dto);
        }


        /// <summary>
        /// 获取角色菜单列表
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
            var dto = await _roleService.GetRoleModuleAsync(_id);
            return Json(dto);
        }


        /// <summary>
        /// 获取角色权限列表
        /// </summary>
        /// <param name="Id">用户ID</param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        public async Task<IActionResult> PermissionList(string Id)
        {
            if (!Guid.TryParse(Id, out _id))
            {
                return Json(BadParameter("Id格式不匹配"));
            }
            var dto = await _roleService.GetRolePermissionAsync(_id);
            return Json(dto);
        }


        /// <summary>
        /// 分配角色菜单
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost("Id")]
        public async Task<IActionResult> AllotModule(string Id, AllotRoleModuleData value)
        {
            if (!Guid.TryParse(Id, out _id))
                return Json(BadParameter("Id格式不匹配"));


            if (value == null)
                return Json(BadParameter("内容不能为空"));

            var dto = await _roleService.AllotRoleModuleAsync(_id, value);
            return Json(dto);
        }


        /// <summary>
        /// 分配角色权限
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost("Id")]
        public async Task<IActionResult> AllotPermission(string Id, AllotRolePermissionData value)
        {
            if (!Guid.TryParse(Id, out _id))
                return Json(BadParameter("Id格式不匹配"));


            if (value == null)
                return Json(BadParameter("内容不能为空"));

            var dto = await _roleService.AllotRolePermissionAsync(_id, value);
            return Json(dto);
        }


        /// <summary>
        /// 清空菜单
        /// </summary>
        /// <param name="Id">用户ID</param>
        /// <returns></returns>
        [HttpDelete("{Id}")]
        public async Task<IActionResult> ClearModule(string Id)
        {
            if (!Guid.TryParse(Id, out _id))
            {
                return Json(BadParameter("Id格式不匹配"));
            }

            var dto = await _roleService.ClearRoleModulesAsync(_id);
            return Json(dto);
        }


        /// <summary>
        /// 清空角色权限
        /// </summary>
        /// <param name="Id">用户ID</param>
        /// <returns></returns>
        [HttpDelete("{Id}")]
        public async Task<IActionResult> ClearPermission(string Id)
        {
            if (!Guid.TryParse(Id, out _id))
            {
                return Json(BadParameter("Id格式不匹配"));
            }
            
            var dto = await _roleService.ClearRolePermissionsAsync(_id);
            return Json(dto);
        }


        /// <summary>
        /// 删除单个角色权限
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> DeleteRolePermission(RolePermissionData data)
        {
            if(data==null)
                return Json(BadParameter("内容不能为空"));

            var dto = await _roleService.DeleteRolePermissionAsync(data);
            return Json(dto);
        }


        /// <summary>
        /// 删除单个角色菜单
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteRoleModule(RoleModuleData data)
        {
            if (data == null)
                return Json(BadParameter("内容不能为空"));

            var dto = await _roleService.DeleteRoleMouleAsync(data);
            return Json(dto);
        }



    }
}
