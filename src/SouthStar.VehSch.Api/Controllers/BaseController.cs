using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using OneZero.Dtos;
using OneZero.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthStar.VehSch.Api.Controllers
{
    [EnableCors("AllowCrossOrigin")]
    [Route("api/[controller]/[action]")]
    public class BaseController : Controller
    {
        protected OutputDto dto;
        protected Guid _id;

        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseController()
        {
            dto = new OutputDto();
        }

        /// <summary>
        /// Controller层参数校验
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected OutputDto BadParameter(string message)
        {
            dto.Code = ResponseCode.ExpectedException;
            dto.Message = message == null ? "传入API的参数错误" : message;
            return dto;
        }
    }
}
