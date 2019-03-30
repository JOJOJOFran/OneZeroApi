using Microsoft.AspNetCore.Mvc;
using SouthStar.VehSch.Api.Areas.Home.Dtos;
using SouthStar.VehSch.Api.Areas.Home.Services;
using SouthStar.VehSch.Api.Controllers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace SouthStar.VehSch.Api.Areas.Home.Controllers
{
    [Route("api/[controller]/[action]")]
    public class LoginController:BaseController
    {
        private readonly LoginService _loginService;


        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        //[Allow]
        public async Task<IActionResult> Login(LoginPostData login)
        {
            var token = await _loginService.LoginAsync(login);
            return Ok(token);
        }

        [HttpGet]
        public async Task<IActionResult> test(string id)
        {
            Guid.TryParse(id, out _id);
            var token = await _loginService.CreateToken(_id);
            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        }
    }
}
