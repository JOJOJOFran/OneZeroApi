using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OneZero.Core.Dtos.Permission;
using OneZero.Core.Models.Permissions;
using OneZero.Core.Services.Permission;
using SouthStar.VehSch.Api.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthStar.VehSch.Api.Areas.Permission.Controllers
{
    public class UserController : BaseController
    {
        private readonly UserRoleService _userRoleService;

        public UserController(UserRoleService userRoleService)
        {
            _userRoleService = userRoleService;
        }

        /// <summary>
        /// 查询用户列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="name"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> List(int page, int limit, string name = null ,string account=null)
        {
            var list= await _userRoleService.GetUserListAsync(name, account, page, limit);
            return Json(list);
        }


        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="Id">用户ID</param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        public async Task<IActionResult> Item(string Id)
        {
            if (!Guid.TryParse(Id, out _id))
            {
                return Json(BadParameter("Id格式不匹配"));
            }
            var userInfo = await _userRoleService.GetUserItmeAsync(_id);
            return Json(userInfo);
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]UserData value)
        {
            if (value == null)
            {
                return Json(BadParameter("用户信息内容不能为空"));
            }
            var dto = await _userRoleService.AddUserAsync(value);
            return Json(dto);
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="value">用户信息</param>
        /// <returns></returns>
        [HttpPost("{userId}")]
        public async Task<IActionResult> Update(string userId, [FromBody]UserData value)
        {
            if (!Guid.TryParse(userId, out _id))
            {
                return Json(BadParameter("Id格式不匹配"));
            }
            if (value == null)
            {
                return Json(BadParameter("用户信息内容不能为空"));
            }
            var dto = await _userRoleService.UpdateUserAsync(_id, value);
            return Json(dto);
        }

        /// <summary>
        /// 删除用户信息(标记删除)
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete(string userId)
        {
            if (!Guid.TryParse(userId, out _id))
            {
                return Json(BadParameter("Id格式不匹配"));
            }
            dto = await _userRoleService.MarkDeleteUserAsync(_id);
            return Ok(dto);
        }

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> ChangePassword([FromBody]ChangePasswordData data)
        {
            if (data == null)
            {
                return Json(BadParameter("密码不能为空"));
            }
            var dto = await _userRoleService.ChangePasswordAsync(data);
            return Json(dto);
        }


        /// <summary>
        /// 修改用户手机号
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> ChangePhoneNum([FromBody]ChangePhoneNumData data)
        {
            if (data == null)
            {
                return Json(BadParameter("密码不能为空"));
            }
            var dto = await _userRoleService.ChangePhoneNumAsync(data);
            return Json(dto);
        }

        /// <summary>
        /// 锁定用户
        /// </summary>
        /// <param name="Id">用户ID</param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        public async Task<IActionResult> Lock(string Id)
        {
            if (!Guid.TryParse(Id, out _id))
            {
                return Json(BadParameter("Id格式不匹配"));
            }
            var dto = await _userRoleService.LockUserAsync(_id);
            return Json(dto);
        }

        /// <summary>
        /// 解锁用户
        /// </summary>
        /// <param name="Id">用户ID</param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        public async Task<IActionResult> UnLock(string Id)
        {
            if (!Guid.TryParse(Id, out _id))
            {
                return Json(BadParameter("Id格式不匹配"));
            }
            var dto = await _userRoleService.UnLockUserAsync(_id);
            return Json(dto);
        }


        /// <summary>
        /// 获取用户角色列表
        /// </summary>
        /// <param name="Id">用户ID</param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        public async Task<IActionResult> RoleList(string Id)
        {
            if (!Guid.TryParse(Id, out _id))
            {
                return Json(BadParameter("Id格式不匹配"));
            }
            var dto = await _userRoleService.GetUserRoleAsync(_id);
            return Json(dto);
        }

        /// <summary>
        /// 分配角色
        /// </summary>
        /// <param name="Id">用户ID</param>
        /// <param name="allotRoleData"></param>
        /// <returns></returns>
        [HttpPost("{Id}")]
        public async Task<IActionResult> AllotRole(string Id,[FromBody]AllotRoleData allotRoleData)
        {
            if (!Guid.TryParse(Id, out _id))
            {
                return Json(BadParameter("Id格式不匹配"));
            }

            if (allotRoleData == null || allotRoleData.RoleIds == null || allotRoleData.RoleIds.Count < 1)
            {
                return Json(BadParameter("角色信息不能为空"));
            }
            var dto = await _userRoleService.AllotRolesAsync(_id, allotRoleData);
            return Json(dto);
        }

        /// <summary>
        /// 清空角色
        /// </summary>
        /// <param name="Id">用户ID</param>
        /// <returns></returns>
        [HttpDelete("{Id}")]
        public async Task<IActionResult> ClearRole(string Id)
        {
            if (!Guid.TryParse(Id, out _id))
            {
                return Json(BadParameter("Id格式不匹配"));
            }

            var dto = await _userRoleService.ClearRolesAsync(_id);
            return Json(dto);
        }


        /// <summary>
        /// 删除单个角色
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> DeleteUserRole(UserRoleData data)
        {
            if (data==null)
                return Json(BadParameter("内容不能为空"));

            var dto = await _userRoleService.DeleteUserRolesAsync(data);
            return Json(dto);
        }


    }
}
