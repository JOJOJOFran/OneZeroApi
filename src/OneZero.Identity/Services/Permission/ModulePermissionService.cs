using System;
using AutoMapper;
using Microsoft.Extensions.Logging;
using OneZero.Core.Models.Permissions;
using OneZero.Common.Dapper;
using OneZero.Dtos;
using OneZero.Domain;
using OneZero.Core;
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
using OneZero.Core.Dtos.Permissions;

namespace OneZero.Core.Services.Permission
{
    public class ModulePermissionService : BaseService
    {
        private readonly ILogger<ModulePermissionService> _logger;
        private readonly OutputDto output = new OutputDto();

        private readonly IRepository<ModuleType, Guid> _moduleRepository;
        private readonly IRepository<PermissionType, Guid> _permissionRepository;


        public ModulePermissionService(IUnitOfWork unitOfWork, ILogger<ModulePermissionService> logger, IDapperProvider dapper, IMapper mapper) : base(unitOfWork, dapper, mapper)
        {
            _moduleRepository = _unitOfWork.Repository<ModuleType, Guid>();
            _permissionRepository = _unitOfWork.Repository<PermissionType, Guid>();
            _logger = logger;
        }

        /// <summary>
        /// 获取所有菜单
        /// </summary>
        /// <returns></returns>
        public async Task<OutputDto> GetAllModuleTreeAsync()
        {
            var modules = _moduleRepository.Entities.Select(b => new
            {
                b.Id,
                b.Name,
                b.DisplayName,
                b.ParentId,
                b.Path,
                b.Description
            });

            output.Datas = await modules?.ToListAsync();
            return output;
        }



        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public async Task<OutputDto> GetModuleListAsync(string code = null, string name = null, int page = 1, int limit = 20)
        {
            int skipCount = 0;
            var roles = _moduleRepository.Entities.Where(v => (EF.Functions.Like(v.DisplayName, "%" + name + "%") || string.IsNullOrWhiteSpace(name))
                                                               && (string.IsNullOrWhiteSpace(code) || EF.Functions.Like(v.Name, "%" + code + "%")))
                                                  .OrderBy(v => v.DisplayName)
                                                  .Select(v => new { v.Id });

            var sumCount = await roles.Select(v => new { v.Id }).CountAsync();
            if (sumCount <= 0)
                return output;

            output.PageInfo = Paging(page, limit, sumCount, out skipCount);
            if (skipCount < 0 || output.PageInfo == null)
                return output;

            var query = from a in roles.Skip(skipCount).Take(limit)
                        join b in _moduleRepository.Entities on a.Id equals b.Id
                        select new
                        {
                            b.Id,
                            b.Name,
                            b.DisplayName,
                            b.ParentId,
                            b.Path,
                            b.Description
                        };
            output.Datas = await query?.ToListAsync();
            return output;
        }


        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public async Task<OutputDto> GetPermissionListAsync(string code = null, string name = null, int page = 1, int limit = 20)
        {
            int skipCount = 0;
            var roles = _permissionRepository.Entities.Where(v => (EF.Functions.Like(v.DisplayName, "%" + name + "%") || string.IsNullOrWhiteSpace(name))
                                                               && (string.IsNullOrWhiteSpace(code) || EF.Functions.Like(v.Name, "%" + code + "%")))
                                                      .Select(v => new { v.Id, v.ModuleId, v.RowNo })
                                                      .OrderBy(v => new { v.ModuleId, v.RowNo });

            var sumCount = await roles.Select(v => new { v.Id }).CountAsync();
            if (sumCount <= 0)
                return output;

            output.PageInfo = Paging(page, limit, sumCount, out skipCount);
            if (skipCount < 0 || output.PageInfo == null)
                return output;

            var query = from a in roles.Skip(skipCount).Take(limit)
                        join b in _permissionRepository.Entities on a.Id equals b.Id
                        join c in _moduleRepository.Entities on b.ModuleId equals c.Id
                        orderby b.ModuleId, b.RowNo
                        select new
                        {
                            b.Id,
                            b.ModuleId,
                            PermissionCode = b.Name,
                            PermissionName = b.DisplayName,
                            ModuleName = c.DisplayName,
                            b.ApiPath,
                            b.Remark,
                            b.RowNo
                        };
            output.Datas = await query?.ToListAsync();
            return output;
        }

        /// <summary>
        /// 获取菜单信息
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public async Task<OutputDto> GetModuleItmeAsync(Guid moduleId)
        {
            var module = await _moduleRepository.Entities.Where(v => v.Id.Equals(moduleId)).FirstOrDefaultAsync();
            output.Datas = module;
            return output;
        }


        /// <summary>
        /// 获取权限信息
        /// </summary>
        /// <param name="permissionId"></param>
        /// <returns></returns>
        public async Task<OutputDto> GetPermissionItmeAsync(Guid permissionId)
        {
            var module = await _permissionRepository.Entities.Where(v => v.Id.Equals(permissionId)).FirstOrDefaultAsync();
            output.Datas = module;
            return output;
        }


        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name="moduleData"></param>
        /// <returns></returns>
        public async Task<OutputDto> AddModuleAsync(ModuleData moduleData)
        {
            if (moduleData == null)
                throw new OneZeroException("菜单信息不能为空", ResponseCode.ExpectedException);

            if (await _moduleRepository.CheckExistsAsync(v => v.Name.Equals(moduleData.Name)))
                throw new OneZeroException("已存在相同Code的菜单，请修改后重试！", ResponseCode.ExpectedException);

            return await _moduleRepository.AddAsync(moduleData, null, v => (ConvertToModel<ModuleData, ModuleType>(moduleData)));
        }

        /// <summary>
        /// 新增权限
        /// </summary>
        /// <param name="moduleData"></param>
        /// <returns></returns>
        public async Task<OutputDto> AddPermissionAsync(PermissionData permissionData)
        {
            if (permissionData == null)
                throw new OneZeroException("角色信息不能为空", ResponseCode.ExpectedException);

            if(await _permissionRepository.CheckUniqueAsync(v => v.Name.Equals(permissionData.Name)))
                throw new OneZeroException("已存在相同Code的权限，请修改后重试！", ResponseCode.ExpectedException);

            return await _permissionRepository.AddAsync(permissionData, null, v => ConvertToModel<PermissionData, PermissionType>(permissionData));
        }

        /// <summary>
        /// 标记删除菜单
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<OutputDto> MarkDeleteModuleAsync(Guid Id)
        {
            return await _moduleRepository.MarkDeleteAsync(Id);
        }

        /// <summary>
        /// 删除权限(不使用标记删除，因为行号唯一)
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<OutputDto> DeletePermissionAsync(Guid Id)
        {
            return await _permissionRepository.DeleteAsync(Id);
        }

        /// <summary>
        /// 更新菜单信息
        /// </summary>
        /// <param name="moduleId"></param>
        /// <param name="moduleData"></param>
        /// <returns></returns>
        public async Task<OutputDto> UpdateModuleAsync(Guid moduleId, ModuleData moduleData)
        {
            var module = ConvertToModel<ModuleData, ModuleType>(moduleData);
            module.Id = moduleId;
            return await _moduleRepository.UpdateAsync(module);
        }

        /// <summary>
        /// 更新菜单权限
        /// </summary>
        /// <param name="moduleId"></param>
        /// <param name="moduleData"></param>
        /// <returns></returns>
        public async Task<OutputDto> UpdatePermissionAsync(Guid permissionId, PermissionData permissionData)
        {
            var permission = ConvertToModel<PermissionData, PermissionType>(permissionData);
            permission.Id = permissionId;
            return await _permissionRepository.UpdateAsync(permission);
        }

        /// <summary>
        /// 获取菜单权限
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public async Task<OutputDto> GetModulePermissionAsync(Guid moduleId)
        {
            var permissions = _permissionRepository.Entities.Where(v => v.ModuleId.Equals(moduleId)).Select(v => new { v.Id, v.ModuleId, v.Name, v.DisplayName, v.ApiPath, v.Remark, v.RowNo }).OrderBy(v => v.RowNo);
            output.Datas = await permissions?.ToListAsync();
            return output;
        }
    }
}
