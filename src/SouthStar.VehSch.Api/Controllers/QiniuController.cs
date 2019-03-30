﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OneZero;
using OneZero.Common.Dtos;
using OneZero.Common.Exceptions;
using OneZero.Common.QiniuTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthStar.VehSch.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    public class QiniuController : BaseController
    {
        private readonly QiniuOption _option;
        private readonly QiniuHelper _qiniu;

        public QiniuController(OneZeroOption option)
        {
            _option = option.QiniuOption;
            QiniuHelper qiniu = new QiniuHelper(_option);
            _qiniu = qiniu;
        }

        /// <summary>
        /// 获取文件上传凭证
        /// </summary>
        /// <param name="fileKey"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> UploadToken(string fileKey)
        {
            var token = _qiniu.CreateUploadToken(fileKey);
            dto.Datas = new QiniuDto() { UploadToken = token };
            return Json(dto);
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
                throw new OneZeroException("文件参数字符为空", OneZero.Common.Enums.ResponseCode.ExpectedException);
            dto.Datas = new QiniuDto() { DownloadUrl = _qiniu.CreatePrivateUrlList(fileKeys) };
            return Json(dto);
        }
    }
}
