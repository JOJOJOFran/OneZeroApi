using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OneZero.Application.Dtos.Permission;
using OneZero.Application.Models.Permissions;
using OneZero.Common.Dapper;
using OneZero.Common.Dtos;
using OneZero.Common.Exceptions;
using OneZero.Common.Extensions;
using OneZero.Domain.Repositories;
using OneZero.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneZero.Application.Services.Permission
{
    public class UserRoleService : BaseService
    {
        private ILogger<UserRoleService> _logger;
        private readonly OutputDto output = new OutputDto();
        private IRepository<User, Guid> _userRepository;
        private IRepository<Role, Guid> _roleRepository;
        private IRepository<UserRole, Guid> _userRoleRepository;

        public UserRoleService(IUnitOfWork unitOfWork, ILogger<UserRoleService> logger, IDapperProvider dapper, IMapper mapper) : base(unitOfWork, dapper, mapper)
        {
            _userRepository = _unitOfWork.Repository<User, Guid>();
            _roleRepository= _unitOfWork.Repository<Role, Guid>();
            _userRoleRepository = _unitOfWork.Repository<UserRole, Guid>();
        }


        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public async Task<OutputDto> GetUserListAsync(string userName, string Account, int page = 1, int limit = 20)
        {
            int skipCount = 0;

            var users = _userRepository.Entities.Where(v => ((EF.Functions.Like(v.DisplayName, "%" + userName + "%") || string.IsNullOrWhiteSpace(userName))
                                                            && (EF.Functions.Like(v.Account, "%" + Account + "%") || string.IsNullOrWhiteSpace(Account)))
                                                            ).OrderBy(v => v.DisplayName);
            var sumCount = await users.Select(v => new { v.Id }).CountAsync();
            if (sumCount <= 0)
                return output;

            output.PageInfo = Paging(page, limit, sumCount, out skipCount);
            if (skipCount < 0 || output.PageInfo == null)
                return output;

            var query = from a in users.Skip(skipCount).Take(limit)
                        join b in _userRepository.Entities on a.Id equals b.Id

                        select new
                        {
                            b.Id,
                            // Roles = string.Join(',', a.Roles.Select(v => v.DisplayName)),
                            b.Account,
                            Name = b.DisplayName,
                            b.Phone,
                            b.Email,
                            State = b.LockoutEnabled ? "已锁定" : "启用",
                            LockTime = b.LockoutEnd
                        };
            output.Datas = await query?.ToListAsync();
            return output;
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
                            Id = b.Id,                    
                            Name = b.Name,
                            DisplayName = b.DisplayName,
                            Remark = b.Remark
                            
                        };
            output.Datas = await query?.ToListAsync();
            return output;
        }

        /// <summary>
        /// 用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<OutputDto> GetUserItmeAsync(Guid userId)
        {
            var user = await _userRepository.Entities.Where(v => v.Id.Equals(userId)).FirstOrDefaultAsync();
            output.Datas = user;
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
        /// 获取用户角色列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<OutputDto> GetUserRoleAsync(Guid userId)
        {
            var query = from a in _userRoleRepository.Entities.Where(v => v.UserId.Equals(userId))
                        join b in _roleRepository.Entities on a.RoleId equals b.Id
                        select new { a.UserId, b.Id, b.DisplayName, b.Name };
            output.Datas = await query?.ToListAsync();
            return output;
        }

        /// <summary>
        /// 新增用户角色
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<OutputDto> AddUserRole(Guid userId, Guid roleId)
        {          
            var role = await _roleRepository.Entities.Where(v => v.Id.Equals(roleId)).FirstOrDefaultAsync();
            if (role == null)
            {
                output.Message = "不存在对应的角色，请重试";
                return output;
            }

            var userRole = await _userRoleRepository.NoFilterEntities.Where(v => (v.UserId.Equals(userId) && v.RoleId.Equals(roleId))).FirstOrDefaultAsync();
            if (userRole != null && userRole.IsDelete != true)
            {
                output.Message = "该角色已存在";
                return output;
            }


            if (userRole != null)
            {
                userRole.IsDelete = false;
                var output= await _userRoleRepository.UpdateAsync(userRole);
                output.Message = "操作成功";
                return output;
            }

            UserRole newUserRole = new UserRole();
            newUserRole.Id = GuidHelper.NewGuid();
            newUserRole.UserId = userId;
            newUserRole.RoleId = roleId;

            var result= await _userRoleRepository.AddAsync(newUserRole);
            if (result == 1)
            {
                output.Message = "操作成功";
            }
            else
            {
                output.Message = "操作失败";
            }
            return output;
        }

        /// <summary>
        /// 删除用户角色
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<OutputDto> DeleteUserRole(Guid userId, Guid roleId)
        {
            var userRole = await _userRoleRepository.NoFilterEntities.Where(v => (v.UserId.Equals(userId) && v.RoleId.Equals(roleId))).FirstOrDefaultAsync();
            return await _userRoleRepository.DeleteAsync(userRole);
        }

        /// <summary>
        /// 标记删除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<OutputDto> MarkDeleteUserAsync(Guid Id)
        {
            return await _userRepository.MarkDeleteAsync(Id);
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        public async Task<OutputDto> AddUserAsync(UserData userData)
        {
            if (userData == null)
            {
                throw new OneZeroException("用户信息不能为空", Common.Enums.ResponseCode.ExpectedException);
            }
            userData.Id= GuidHelper.NewGuid();
            userData.Password = SecretHelper.MD5Hash(userData.Password);
            return await _userRepository.AddAsync(userData, null, v => (ConvertToModel<UserData, User>(userData)));
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="departmentInfo"></param>
        /// <returns></returns>
        public async Task<OutputDto> UpdateUserAsync(Guid userId, User user)
        {
            user.Id = userId;
            return await _userRepository.UpdateAsync(user);
        }

        /// <summary>
        /// 锁定用户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<OutputDto> LockUserAsync(Guid userId)
        {
            var user = await _userRepository.Entities.Where(v => v.Id.Equals(userId)).FirstOrDefaultAsync();
            user.LockoutEnabled = true;
            var result = await _userRepository.UpdateOneAsync(user);
            if (result == 1)
            {
                output.Message = "用户锁定成功";
            }
            else
            {
                output.Message = "用户锁定失败";
            }
            return output;
        }

        /// <summary>
        /// 解锁用户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<OutputDto> UnLockUserAsync(Guid userId)
        {
            var user = await _userRepository.Entities.Where(v => v.Id.Equals(userId)).FirstOrDefaultAsync();
            user.LockoutEnabled = false;
            var result = await _userRepository.UpdateOneAsync(user);
            if (result == 1)
            {
                output.Message = "用户解锁成功";
            }
            else
            {
                output.Message = "用户解锁失败";
            }
            return output;
        }

    }
}
