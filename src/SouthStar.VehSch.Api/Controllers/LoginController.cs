using Microsoft.AspNetCore.Mvc;
using SouthStar.VehSch.Core.Logins.Dtos;
using SouthStar.VehSch.Core.Logins.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthStar.VehSch.Api.Controllers
{
    /// <summary>
    /// 登陆
    /// </summary>
    public class LoginController : BaseController
    {
        private readonly LoginService _loginService;


        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }

        /// <summary>
        /// 登陆获取token
        /// </summary>
        /// <param name="loginData"></param>
        /// <example>
        /// {
        ///"account": "fran",
        ///"password": "m111111",
        ///"validateCode": "string",
        ///"loginWay": "string",
        ///"loginFrom": "string"
        ///}
        /// </example>
        /// <returns>
        /// {
        /// "message": "登陆成功",
        /// "errorMessage": null,
        /// "pageInfo": null,
        /// "datas": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpcCI6IjEyNy4wLjAuMSIsInVzZXJpZCI6IjQ1NGU2ZDJhLTA1YTMtNDgzYS05NDE2LWFhMjQwMGU2MjQyNSIsInVzZXJuYW1lIjoi5byg5biGIiwiYWNjb3VudCI6ImZyYW4iLCJ0ZW5hbmlkIjoiOGNhYjFjNjEtYTM2Yi00OTEwLWI4ZWMtNzM1MWVhMTFiYTcxIiwicm9sZSI6WyJhZG1pbnRlc3QiLCJ1c2VydGVzdCJdLCJtZW51IjpbIk1hbmFnZSIsIk1hbmFnZSIsIlZlaGljbGVNYW5hZ2UiLCJEcml2ZXJNYW5hZ2UiXSwicGVybWlzc2lvbiI6WyIvVmVoaWNsZS9MaXN0IiwiL1ZlaGljbGUvSXRlbSIsIi9WZWhpY2xlL0l0ZW0iLCIvVmVoaWNsZS9BZGQiLCIvVmVoaWNsZS9EZWxldGUiXSwibmJmIjoxNTU0Mjc3NDM3LCJleHAiOjE1NTQyODQ2MzcsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6MTMzMTEiLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjEzMzExIn0.Z4nIgXdVl-ubP0ApRl5fv3pfLYd0KujY7SMHqd6KHX4",
        /// "code": 0,
        /// "statusCode": 200
        ///}
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> GetToken([FromBody]LoginPostData loginData)
        {
            if (loginData == null)
            {
                return Json(BadParameter("登陆内容不能为空"));
            }
            var token = await _loginService.LoginToGetTokenAsync(loginData);
            return Ok(token);
        }

        /// <summary>
        /// 常规账密登陆
        /// </summary>
        /// <param name="loginData"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Common([FromBody]LoginPostData loginData)
        {
            if (loginData == null)
            {
                return Json(BadParameter("登陆内容不能为空"));
            }
            var output = await _loginService.LoginByCommonWay(loginData);
            return Ok(output);
        }


        //[HttpGet]
        //public async Task<IActionResult> test(string id)
        //{
        //    Guid.TryParse(id, out _id);
        //    var token = await _loginService.CreateToken(_id);
        //    return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        //}
    }
}
