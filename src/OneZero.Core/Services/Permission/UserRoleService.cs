using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OneZero.Core.Dtos.Permission;
using OneZero.Core.Models.Permissions;
using OneZero.Common.Dapper;
using OneZero.Dtos;
using OneZero.Exceptions;
using OneZero.Common.Extensions;

using OneZero.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneZero.Domain;
using OneZero.Common.Helpers;
using OneZero.Enums;

namespace OneZero.Core.Services.Permission
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
            _logger = logger;
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
                           b.Id,                    
                           b.Name,
                           b.DisplayName,
                           b.Remark                           
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
                        select new { a.UserId, a.RoleId, b.DisplayName, b.Name };
            output.Datas = await query?.ToListAsync();
            return output;
        }


        /// <summary>
        /// 给用户分配角色
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleData"></param>
        /// <returns></returns>
        public async Task<OutputDto> AllotRolesAsync(Guid userId, AllotRoleData roleData)
        {
            var user = await _userRepository.Entities.Where(v => v.Id.Equals(userId)).FirstOrDefaultAsync();
            if (user == null)
                throw new OneZeroException("分配角色失败：传入的上下文有误，找不到用户",ResponseCode.ExpectedException);

            List<UserRole> userRoles = new List<UserRole> { };
            foreach (var item in roleData.RoleIds)
            {
                Guid roleId;
                if (Guid.TryParse(item, out roleId))
                {
                    userRoles.Add(new UserRole { Id = GuidHelper.NewGuid(), UserId = userId, RoleId = roleId });
                }
                else
                {
                    throw new OneZeroException("分配角色失败：传入的角色有误",ResponseCode.ExpectedException);
                }
            }

            try
            {

                await _unitOfWork.BeginTransAsync();
                var userRole = user.UserRoles;
                if (userRole != null && userRole.Count() > 0)
                {
                    await _userRoleRepository.DeleteAsync(userRole.ToArray());
                }
                await _userRoleRepository.AddAsync(userRoles.ToArray());
                await _unitOfWork.CommitAsync();
                output.Message = "分配成功";
            }
            catch (Exception e)
            {
                await  _unitOfWork.RollbackAsync();
                throw new OneZeroException("分配角色失败",e,ResponseCode.ExpectedException);
            }
            return output;
        }

        /// <summary>
        /// 删除单个用户角色
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<OutputDto> DeleteUserRolesAsync(UserRoleData data)
        {
            return await _userRoleRepository.DeleteAsync(v => v.UserId.Equals(data.UserId) && v.RoleId.Equals(data.RoleId));
        }

        /// <summary>
        /// 清空角色
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<OutputDto> ClearRolesAsync(Guid userId)
        {
            var user = await _userRepository.Entities.Where(v => v.Id.Equals(userId)).FirstOrDefaultAsync();
            if (user == null)
                throw new OneZeroException("清空角色失败：传入的上下文有误，找不到用户",ResponseCode.ExpectedException);

            var userRole = user.UserRoles;
            if (userRole != null && userRole.Count() > 0)
            {
                await _userRoleRepository.DeleteAsync(userRole.ToArray());
            }
            output.Message = "清空成功";
            return output;
        } 

        /// <summary>
        /// 新增用户角色
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<OutputDto> AddUserRoleAsync(Guid userId, Guid roleId)
        {          
            var role = await _roleRepository.Entities.Where(v => v.Id.Equals(roleId)).FirstOrDefaultAsync();
            if (role == null)
            {
                output.Message = "不存在对应的角色，请重试";
                return output;
            }

            var userRole = await _userRoleRepository.NoFilterEntities.Where(v => (v.UserId.Equals(userId) && v.RoleId.Equals(roleId))).FirstOrDefaultAsync();
            if (userRole != null && userRole.IsDelete != default(Guid))
            {
                output.Message = "该角色已存在";
                return output;
            }


            if (userRole != null)
            {
                userRole.IsDelete = GuidHelper.NewGuid();
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
        /// 删除用户角色（不使用标记删除）
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<OutputDto> DeleteUserRoleAsync(Guid userId, Guid roleId)
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
                throw new OneZeroException("用户信息不能为空",ResponseCode.ExpectedException);
            }
            userData.Password = userData.Password.MD5Hash();
            return await _userRepository.AddAsync(userData, null, v => (ConvertToModel<UserData, User>(userData)));
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="departmentInfo"></param>
        /// <returns></returns>
        public async Task<OutputDto> UpdateUserAsync(Guid userId, UserData userData)
        {
            var user = ConvertToModel<UserData, User>(userData);
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

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<OutputDto> ChangePasswordAsync(ChangePasswordData data)
        {
            Guid userId;
            if (!Guid.TryParse(data.UserId, out userId))
            {
                throw new OneZeroException("修改密码失败：传入的用户ID格式错误",ResponseCode.ExpectedException);
            }

            var user = await _userRepository.Entities.Where(v => v.Id.Equals(userId)).FirstOrDefaultAsync();
            if (user != null && !string.IsNullOrWhiteSpace(data.NewPasswordData) && data.NewPasswordData != data.OldPasswordData && user.PasswordHash == data.OldPasswordData.MD5Hash())
            {
                user.PasswordHash = data.NewPasswordData.MD5Hash();
                await _userRepository.UpdateAsync(user);
                output.Message = "修改成功";
            }
            else
            {
                output.Message = "修改失败：请保证新密码正确有效，且不与旧密码一样";
            }
            return output;
        }


        /// <summary>
        /// 修改手机号
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<OutputDto> ChangePhoneNumAsync(ChangePhoneNumData data)
        {
            Guid userId;
            if (!Guid.TryParse(data.UserId, out userId))
            {
                throw new OneZeroException("修改手机号失败：传入的用户ID格式错误",ResponseCode.ExpectedException);
            }

            var user = await _userRepository.Entities.Where(v => v.Id.Equals(userId)).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new OneZeroException("修改手机号失败：传入的用户上下文错误，找不到用户",ResponseCode.ExpectedException);
            }
           
            if ( !string.IsNullOrWhiteSpace(data.PhoneNum) )
            {
                user.Phone = data.PhoneNum;
                await _userRepository.UpdateAsync(user);
                output.Message = "修改成功";
            }
            else
            {
                output.Message = "修改失败：请保证手机号正确有效";
            }
            return output;
        }

    }
}
