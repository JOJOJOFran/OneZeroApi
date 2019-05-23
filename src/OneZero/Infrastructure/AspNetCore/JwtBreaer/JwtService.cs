using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using OneZero.DependencyInjections;
using OneZero.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OneZero.AspNetCore.JwtBreaer
{
    [Dependency(ServiceLifetime.Singleton)]
    public class JwtService
    {
        private readonly OneZeroOption _oneZeroOption;

        public JwtService(OneZeroOption oneZeroOption)
        {
            _oneZeroOption = oneZeroOption;
        }

        /// <summary>
        /// 写token字符串
        /// </summary>
        /// <param name="securityToken">token</param>
        /// <returns></returns>
        public string WriteToken(SecurityToken securityToken)
        {
            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }


        /// <summary>
        /// 生成token
        /// </summary>
        /// <param name="claims"></param>
        /// <returns></returns>
        public async Task<JwtSecurityToken> CreateToken(IEnumerable<Claim> claims)
        {
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

            return await Task.FromResult(token);
        }
    }
}
