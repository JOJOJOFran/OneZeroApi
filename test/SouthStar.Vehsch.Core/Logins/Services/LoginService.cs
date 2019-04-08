using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OneZero.Common.Dapper;
using OneZero.Common.Extensions;
using OneZero.Core;
using OneZero.Core.Models.Permissions;
using OneZero.Domain;
using OneZero.Dtos;
using OneZero.Enums;
using OneZero.Options;
using SouthStar.VehSch.Core.Logins.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using OneZero.AspNetCore.JwtBreaer;

namespace SouthStar.VehSch.Core.Logins.Services
{
    /// <summary>
    /// 登陆服务
    /// </summary>
    public class LoginService : BaseService
    {
        private readonly OutputDto output = new OutputDto();
        private readonly IRepository<User, Guid> _userRepository;
        private readonly IRepository<Role, Guid> _roleRepository;
        private readonly IRepository<ModuleType, Guid> _moduleRepository;
        private readonly IRepository<PermissionType, Guid> _permissionRepository;
        private readonly OneZeroOption _oneZeroOption;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<LoginService> _logger;
        private readonly JwtService _jwtService;


        public LoginService(IUnitOfWork unitOfWork, ILogger<LoginService> logger, IDapperProvider dapper, IMapper mapper, OneZeroOption oneZeroOption, IHttpContextAccessor httpContextAccessor, JwtService jwtService) : base(unitOfWork, dapper, mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = unitOfWork.Repository<User, Guid>();
            _roleRepository = unitOfWork.Repository<Role, Guid>();
            _moduleRepository = unitOfWork.Repository<ModuleType, Guid>();
            _permissionRepository = unitOfWork.Repository<PermissionType, Guid>();
            _logger = logger;
            _oneZeroOption = oneZeroOption;
            _jwtService = jwtService;
        }

        /// <summary>
        /// 常规账号密码登陆
        /// </summary>
        /// <param name="postData"></param>
        /// <returns></returns>
        public async Task<OutputDto> LoginByCommonWay(LoginPostData postData)
        {
            var user = await _userRepository.Entities.Where(v => v.Account.Equals(postData.Account) && v.PasswordHash.Equals(postData.Password.MD5Hash())).FirstOrDefaultAsync();
            if (user == null)
            {
                output.Message = "账号或密码错误";
                output.Code = ResponseCode.ExpectedException;
                return output;
            }
            else
            {
                if (user.UserRoles != null)
                {
                    //添加角色信息
                    var role = from a in user.UserRoles
                                join b in _roleRepository.Entities on a.RoleId equals b.Id
                                select new {b.DisplayName,b.Id,b.Name };

                    output.Datas = new { user.Id,
                                         user.Account,
                                         Name = user.DisplayName,
                                         RoleNames =string.Join("," ,role.Select(v=>v.DisplayName)),
                                         user.Phone,
                                         RoleId = role.Select(v => v.Id),
                                         Roles=role.Select(v=>v.Name),
                                         Token= _jwtService.WriteToken(await CreateToken(user)),
                                         user.DepartmentId};
                    output.Message = "登陆成功";
                }
                else
                {
                    output.Message = "该用户尚未分配角色，请联系管理员";
                    output.Code = ResponseCode.ExpectedException;
                }
                   
            }
            return output;
        }

        /// <summary>
        /// 登陆获取token
        /// </summary>
        /// <param name="postData"></param>
        /// <returns></returns>
        public async Task<OutputDto> LoginToGetTokenAsync(LoginPostData postData)
        {
            var user = await _userRepository.Entities.Where(v => v.Account.Equals(postData.Account) && v.PasswordHash.Equals(postData.Password.MD5Hash())).FirstOrDefaultAsync();
            if (user == null)
            {
                output.Message = "账号或密码错误";
                output.Code = ResponseCode.ExpectedException;
                return output;
            }
            try
            {
                output.Datas = _jwtService.WriteToken(await CreateToken(user));
                output.Message = "登陆成功";
            }
            catch (Exception e)
            {
                output.Message = "生成token失败";
                output.Code = ResponseCode.UnExpectedException;
                _logger.LogWarning("登陆：生成token失败", e);
            }
            return output;
        }

        /// <summary>
        /// 创建token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private async Task<JwtSecurityToken> CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>();

            //添加Ip
            claims.Add(new Claim("ip", _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString()));
            //添加User信息
            if (user != null)
            {
                claims.Add(new Claim("userid", user.Id.ToString()));
                claims.Add(new Claim("username", user.DisplayName));
                claims.Add(new Claim("account", user.Account));
                claims.Add(new Claim("tenanid", user.TenanId.ToString()));
            }

            if (user.UserRoles != null)
            {
                //添加角色信息
                var roles = from a in user.UserRoles
                            join b in _roleRepository.Entities on a.RoleId equals b.Id
                            select b;

                foreach (var role in roles)
                {
                    claims.Add(new Claim("roles", role.Name));
                }

                //添加菜单信息
                var modules = from a in roles.SelectMany(v => v.RoleModules)
                              join b in _moduleRepository.Entities on a.ModuleId equals b.Id
                              select new { b.Id, b.Name, b.DisplayName };

                foreach (var module in modules)
                {
                    claims.Add(new Claim("menus", module.Name));
                }
                //添加权限信息
                var permissions = from a in roles.SelectMany(v => v.RolePermission)
                                  join b in _permissionRepository.Entities on a.PermissionId equals b.Id
                                  orderby a.ModuleId, b.RowNo
                                  select new { a.ModuleId, b.Id, b.Name, b.ApiPath };

                foreach (var permission in permissions)
                {
                    claims.Add(new Claim("permissions", permission.ApiPath));
                }
            }
            return await _jwtService.CreateToken(claims);
        }

    }
}
