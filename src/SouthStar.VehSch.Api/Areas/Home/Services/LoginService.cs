using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OneZero;
using OneZero.Application;
using OneZero.Application.Models.Permissions;
using OneZero.Common.Dapper;
using OneZero.Common.Dtos;
using OneZero.Common.Extensions;
using OneZero.Domain.Repositories;
using SouthStar.VehSch.Api.Areas.Home.Dtos;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SouthStar.VehSch.Api.Areas.Home.Services
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
        private ILogger<LoginService> _logger;
        //HttpContext

        public LoginService(IUnitOfWork unitOfWork, ILogger<LoginService> logger, IDapperProvider dapper, IMapper mapper, OneZeroOption oneZeroOption,IHttpContextAccessor httpContextAccessor) : base(unitOfWork, dapper, mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = unitOfWork.Repository<User, Guid>();
            _roleRepository = unitOfWork.Repository<Role, Guid>();
            _moduleRepository = unitOfWork.Repository<ModuleType, Guid>();
            _permissionRepository = unitOfWork.Repository<PermissionType, Guid>();
            _logger = logger;
            _oneZeroOption = oneZeroOption;
        }


        public async Task<OutputDto> LoginAsync(LoginPostData postData)
        {
            var user = await _userRepository.Entities.Where(v => v.Account.Equals(postData.Account) && v.PasswordHash.Equals(SecretHelper.MD5Hash(postData.Password))).FirstOrDefaultAsync();
            if (user == null)
            {
                
                output.Message = "账号或密码错误";
                output.Code = OneZero.Common.Enums.ResponseCode.ExpectedException;
                return output;
            }
            try
            {
                output.Datas = new JwtSecurityTokenHandler().WriteToken(await CreateToken(user));
                output.Message = "登陆成功";
            }
            catch (Exception e)
            {
                output.Message = "生成token失败";
                output.Code = OneZero.Common.Enums.ResponseCode.UnExpectedException;
                _logger.LogWarning("登陆：生成token失败", e);
            }

            return output;

        }


        public async Task<JwtSecurityToken> CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>();

            //添加Ip
            claims.Add(new Claim("UserID", _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString()));


            //添加User信息
            if (user != null)
            {
                claims.Add(new Claim("UserID", user.Id.ToString()));
                claims.Add(new Claim("UserName", user.DisplayName));
                claims.Add(new Claim("Account", user.Account));
            }

            if (user.UserRoles != null)
            {
                //添加角色信息
                var roles = from a in user.UserRoles
                            join b in _roleRepository.Entities on a.RoleId equals b.Id
                            select b;

                foreach (var role in roles)
                {
                    claims.Add(new Claim("Role", role.Name));
                }


                //添加菜单信息
                var modules = from a in roles.SelectMany(v => v.RoleModules)
                              join b in _moduleRepository.Entities on a.ModuleId equals b.Id
                              select new { b.Id, b.Name, b.DisplayName };

                foreach (var module in modules)
                {
                    claims.Add(new Claim("Menu", module.Name));
                }
                //添加权限信息
                var permissions = from a in roles.SelectMany(v => v.RolePermission)
                                  join b in _permissionRepository.Entities on a.PermissionId equals b.Id
                                  orderby a.ModuleId, b.RowNo
                                  select new { a.ModuleId, b.Id, b.Name, b.ApiPath };

                foreach (var permission in permissions)
                {
                    claims.Add(new Claim("Permission", permission.ApiPath));
                }
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_oneZeroOption.JwtOption.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _oneZeroOption.JwtOption.Issuer,
                _oneZeroOption.JwtOption.Audience,
                claims,
                DateTime.Now,
                DateTime.Now.AddMinutes(120),
                creds
            );

            return token;
        }


        public async Task<JwtSecurityToken> CreateToken(Guid userId)
        {
            List<Claim> claims = new List<Claim>();
            var user = await _userRepository.Entities.Where(v => v.Id.Equals(userId)).FirstOrDefaultAsync();
            // _userRepository.Entities.Include()
            //添加Ip
            claims.Add(new Claim("Ip", _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString()));
            //添加User信息
            if (user != null)
            {
                claims.Add(new Claim("UserID", user.Id.ToString()));
                claims.Add(new Claim("UserName", user.DisplayName));
                claims.Add(new Claim("Account", user.Account));
            }


            //添加角色信息
            var roles = from a in user.UserRoles
                        join b in _roleRepository.Entities on a.RoleId equals b.Id
                        select b;

            foreach (var role in roles)
            {
                claims.Add(new Claim("Role", role.Name));
            }


            //添加菜单信息
            var modules = from a in roles.SelectMany(v => v.RoleModules)
                          join b in _moduleRepository.Entities on a.ModuleId equals b.Id
                          select new { b.Id, b.Name, b.DisplayName };

            foreach (var module in modules)
            {
                claims.Add(new Claim("Menu", module.Name));
            }
            //添加权限信息
            var permissions = from a in roles.SelectMany(v => v.RolePermission)
                              join b in _permissionRepository.Entities on a.PermissionId equals b.Id
                              orderby a.ModuleId, b.RowNo
                              select new { a.ModuleId, b.Id, b.Name, b.ApiPath };

            foreach (var permission in permissions)
            {
                claims.Add(new Claim("Permission", permission.ApiPath));
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_oneZeroOption.JwtOption.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _oneZeroOption.JwtOption.Issuer,
                _oneZeroOption.JwtOption.Audience,
                claims,
                DateTime.Now,
                DateTime.Now.AddMinutes(120),
                creds
            );

            return token;
        }
    }
}
