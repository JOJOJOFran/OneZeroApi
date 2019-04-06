using AutoMapper;
using Microsoft.Extensions.Logging;
using OneZero.Core.Models.Permissions;
using OneZero.Common.Dapper;
using OneZero.Dtos;
using OneZero.Domain;
using OneZero.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OneZero.Exceptions;
using OneZero.Enums;
using OneZero.Common.Helpers;
using OneZero.Core.Dtos.Permission;
using OneZero.Common.Extensions;

namespace  SouthStar.VehSch.Core.Permissions

{

    /// <summary>
    /// 角色模块服务
    /// </summary>
    public class RoleService : BaseService
    {
        private readonly ILogger<RoleService> _logger;
        private readonly OutputDto output = new OutputDto();
        private readonly IRepository<Role, Guid> _roleRepository;
        private readonly IRepository<RoleModule, Guid> _roleModuleRepository;
        private readonly IRepository<ModuleType, Guid> _moduleRepository;
        private readonly IRepository<PermissionType, Guid> _permissionRepository;
        private readonly IRepository<RolePermission, Guid> _rolePermissionRepository;

        public RoleService(IUnitOfWork unitOfWork, ILogger<RoleService> logger, IDapperProvider dapper, IMapper mapper) : base(unitOfWork, dapper, mapper)
        {
            _roleRepository = _unitOfWork.Repository<Role, Guid>();
            _roleModuleRepository = _unitOfWork.Repository<RoleModule, Guid>();
            _rolePermissionRepository = _unitOfWork.Repository<RolePermission, Guid>();
            _moduleRepository = _unitOfWork.Repository<ModuleType, Guid>();
            _permissionRepository = _unitOfWork.Repository<PermissionType, Guid>();
            _logger = logger;
        }

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public async Task<OutputDto> GetRoleListAsync(string roleName, int page = 1, int limit = 20)
        {
            int skipCount = 0;
            var roles = _roleRepository.Entities.Where(v => (EF.Functions.Like(v.DisplayName, "%" + roleName + "%") || string.IsNullOrWhiteSpace(roleName))).OrderBy(v => v.DisplayName);

            var sumCount = await roles.Select(v => new { v.Id }).CountAsync();
            if (sumCount <= 0)
                return output;

            output.PageInfo = Paging(page, limit, sumCount, out skipCount);
            if (skipCount < 0 || output.PageInfo == null)
                return output;

            var query = from a in roles.Skip(skipCount).Take(limit)
                        join b in _roleRepository.Entities on a.Id equals b.Id
                        select new
                        {
                            b.Id,
                            b.Name,
                            b.DisplayName,
                            b.Remark
                        };
            output.Datas = await query?.ToListAsync();
            return output;
        }

        /// <summary>
        /// 获取角色信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<OutputDto> GetRoleItmeAsync(Guid roleId)
        {
            var role = await _roleRepository.Entities.Where(v => v.Id.Equals(roleId)).FirstOrDefaultAsync();
            output.Datas = role;
            return output;
        }


        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        public async Task<OutputDto> AddRoleAsync(RoleData userData)
        {
            if (userData == null)
            {
                throw new OneZeroException("角色信息不能为空", ResponseCode.ExpectedException);
            }
            userData.Id = GuidHelper.NewGuid();

            return await _roleRepository.AddAsync(userData, null, v => (ConvertToModel<RoleData, Role>(userData)));
        }

        /// <summary>
        /// 标记删除角色
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<OutputDto> MarkDeleteRoleAsync(Guid Id)
        {
            return await _roleRepository.MarkDeleteAsync(Id);
        }

        /// <summary>
        /// 更新角色信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public async Task<OutputDto> UpdateRoleAsync(Guid roleId, RoleData roleData)
        {
            var role = ConvertToModel<RoleData, Role>(roleData);
            role.Id = roleId;
            return await _roleRepository.UpdateAsync(role);
        }

        /// <summary>
        /// 获取角色拥有的菜单
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<OutputDto> GetRoleModuleAsync(Guid roleId)
        {
            var query = from a in _roleModuleRepository.Entities.Where(v => v.RoleId.Equals(roleId)).Select(v => new { v.RoleId, v.ModuleId })
                        join b in _moduleRepository.Entities on a.ModuleId equals b.Id
                        select new { a.RoleId, a.ModuleId, b.DisplayName, b.Path, b.ParentId };

            output.Datas = await query?.ToListAsync();
            return output;
        }


        /// <summary>
        /// 获取角色权限信息
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns></returns>
        public async Task<OutputDto> GetRolePermissionAsync(Guid roleId)
        {
            var query = from a in _rolePermissionRepository.Entities.Where(v => v.RoleId.Equals(roleId)).Select(v => new { v.Id,v.RoleId, v.PermissionId })
                        join b in _permissionRepository.Entities on a.PermissionId equals b.Id
                        join c in _moduleRepository.Entities on b.ModuleId equals c.Id
                        select new {  a.RoleId, a.PermissionId, b.ModuleId, ModuleCode = c.Name, ModuleName = c.DisplayName, PermissionCode = b.Name, b.ApiPath, PermissionName = b.DisplayName };

            output.Datas = await query?.ToListAsync();
            return output;
        }


        /// <summary>
        /// 获取指定模块的角色权限
        /// </summary>
        /// <param name="moduleId">模块ID</param>
        /// <param name="roleId">角色ID</param>
        /// <returns></returns>
        public async Task<OutputDto> GetRoleModulePermissionAsync(Guid moduleId, Guid roleId)
        {
            var query = from a in _rolePermissionRepository.Entities.Where(v => v.RoleId.Equals(roleId)).Select(v => new { v.RoleId, v.PermissionId })
                        join b in _permissionRepository.Entities.Where(v => v.ModuleId.Equals(moduleId)) on a.PermissionId equals b.Id
                        join c in _moduleRepository.Entities.Where(v => v.Id.Equals(moduleId)) on b.ModuleId equals c.Id
                        select new { a.RoleId, a.PermissionId, b.ModuleId, ModuleCode = c.Name, ModuleName = c.DisplayName, PermissionCode = b.Name, b.ApiPath, PermissionName = b.DisplayName };

            output.Datas = await query?.ToListAsync();
            return output;
        }

        /// <summary>
        /// 为角色分配菜单
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <param name="allotRoleModuleData">分配菜单数据</param>
        /// <returns></returns>
        public async Task<OutputDto> AllotRoleModuleAsync(Guid roleId, AllotRoleModuleData allotRoleModuleData)
        {
            var role = await _roleRepository.Entities.Where(v => v.Id.Equals(roleId)).FirstOrDefaultAsync();
            if (role == null)
                throw new OneZeroException("分配菜单失败：传入的上下文有误，找不到用户", ResponseCode.ExpectedException);

            List<RoleModule> roleModules = new List<RoleModule> { };
            foreach (var item in allotRoleModuleData.ModuleIds)
            {
                roleModules.Add(new RoleModule { Id = GuidHelper.NewGuid(), ModuleId = item.ConvertToGuid("分配菜单"), RoleId = roleId });
            }
            try
            {
                await _unitOfWork.BeginTransAsync();
                var oldRoleModules = role.RoleModules;
                if (oldRoleModules != null && oldRoleModules.Count() > 0)
                {
                    await _roleModuleRepository.DeleteAsync(oldRoleModules.ToArray());
                }
                await _roleModuleRepository.AddAsync(roleModules.ToArray());
                await _unitOfWork.CommitAsync();
                output.Message = "分配成功";
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackAsync();
                throw new OneZeroException("分配菜单失败", e, ResponseCode.ExpectedException);
            }
            return output;
        }


        /// <summary>
        /// 为角色分配权限
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <param name="allotRolePermissionData">分配菜单数据</param>
        /// <returns></returns>
        public async Task<OutputDto> AllotRolePermissionAsync(Guid roleId, AllotRolePermissionData allotRolePermissionData)
        {
            var role = await _roleRepository.Entities.Where(v => v.Id.Equals(roleId)).FirstOrDefaultAsync();
            if (role == null)
                throw new OneZeroException("分配权限失败：传入的上下文有误，找不到用户", ResponseCode.ExpectedException);

            List<RolePermission> RolePermissions = new List<RolePermission> { };
            foreach (var item in allotRolePermissionData.PermissioIds)
            {
                RolePermissions.Add(new RolePermission { Id = GuidHelper.NewGuid(), ModuleId = item.ModuleId, RoleId = roleId, PermissionId=item.PermissioId});
            }
            try
            {
                await _unitOfWork.BeginTransAsync();
                var oldRolePermissions = role.RolePermission;
                if (oldRolePermissions != null && oldRolePermissions.Count() > 0)
                {
                    await _rolePermissionRepository.DeleteAsync(oldRolePermissions.ToArray());
                }
                await _rolePermissionRepository.AddAsync(RolePermissions.ToArray());
                await _unitOfWork.CommitAsync();
                output.Message = "分配成功";
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackAsync();
                throw new OneZeroException("分配权限失败", e, ResponseCode.ExpectedException);
            }
            return output;
        }


        /// <summary>
        /// 清空角色菜单
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns></returns>
        public async Task<OutputDto> ClearRoleModulesAsync(Guid roleId)
        {
            var role = await _roleRepository.Entities.Where(v => v.Id.Equals(roleId)).FirstOrDefaultAsync();
            if (role == null)
                throw new OneZeroException("清空菜单失败：传入的上下文有误，找不到角色", ResponseCode.ExpectedException);

            var oldRoleModules = role.RoleModules;
            if (oldRoleModules != null && oldRoleModules.Count() > 0)
            {
                await _roleModuleRepository.DeleteAsync(v => v.RoleId.Equals(roleId));
            }
            output.Message = "清空成功";
            return output;
        }


        /// <summary>
        /// 清空角色权限
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns></returns>
        public async Task<OutputDto> ClearRolePermissionsAsync(Guid roleId)
        {
            var role = await _roleRepository.Entities.Where(v => v.Id.Equals(roleId)).FirstOrDefaultAsync();
            if (role == null)
                throw new OneZeroException("清空菜单失败：传入的上下文有误，找不到角色", ResponseCode.ExpectedException);

            var oldRoleModules = role.RoleModules;
            if (oldRoleModules != null && oldRoleModules.Count() > 0)
            {
                await _rolePermissionRepository.DeleteAsync(v => v.RoleId.Equals(roleId));
            }
            output.Message = "清空成功";
            return output;
        }


        /// <summary>
        /// 删除角色菜单（不使用标记删除）
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<OutputDto> DeleteRoleMouleAsync(RoleModuleData data)
        {
            return await _roleModuleRepository.DeleteAsync(v => v.RoleId.Equals(data.RoleId) && v.ModuleId.Equals(data.ModuleId));
        }


        /// <summary>
        /// 删除角色权限（不使用标记删除）
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<OutputDto> DeleteRolePermissionAsync(RolePermissionData data)
        {
            return await _rolePermissionRepository.DeleteAsync(v=>v.RoleId.Equals(data.RoleId)&&v.PermissionId.Equals(data.PermissionId));
        }
    }
}
