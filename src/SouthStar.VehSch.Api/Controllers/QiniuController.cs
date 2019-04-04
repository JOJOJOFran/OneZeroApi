using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OneZero;
using OneZero.Common.Qiniu;
using OneZero.Dtos;
using OneZero.Exceptions;
using OneZero.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthStar.VehSch.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    public class QiniuController : BaseController
    {
        private readonly OneZeroOption _option;
        private readonly QiniuHelper _qiniu;
        private readonly ILogger<QiniuController> _logger;

        public QiniuController(ILogger<QiniuController> logger, OneZeroOption option,IConfiguration configuration)
        {
            _logger = logger;            
            _option = option;
            _qiniu = new QiniuHelper(_option);

        }

        /// <summary>
        /// 获取文件上传凭证
        /// </summary>
        /// <param name="fileKey"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> UploadToken(string fileKey)
        {
            var token = _qiniu.CreateUploadToken(fileKey);
            dto.Datas = new { UploadToken = token };
            return await Task.FromResult(Json(dto));
        }


        /// <summary>
        /// 获取文件下载列表
        /// </summary>
        /// <param name="fileKeys"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> FileUrls(string fileKeys)
        {
            if (string.IsNullOrWhiteSpace(fileKeys))
                throw new OneZeroException("文件参数字符为空", OneZero.Enums.ResponseCode.ExpectedException);
            dto.Datas = new { DownloadUrl = _qiniu.CreatePrivateUrlList(fileKeys) };
            return await Task.FromResult(Json(dto));
        }
    }
}
