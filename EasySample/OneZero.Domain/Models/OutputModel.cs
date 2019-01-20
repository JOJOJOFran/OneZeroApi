using System;
using System.Collections.Generic;
using System.Net;

namespace OneZero.Domain.Models
{
    public class OutputModel
    {
        public OutputModel()
        {
            //默认200
            StatusCode = HttpStatusCode.OK;
            //默认0,Success
            Code = ResponseCode.Success;
        }

        public string Message { get; set; }

        public List<OutputData> Datas { get; set; }

        public ResponseCode Code { get; set; }

        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
    }

    /// <summary>
    /// 请求响应结果码枚举值
    /// </summary>
    public enum ResponseCode
    {
        /// <summary>
        /// 请求成功
        /// </summary>
        Success = 0,

        /// <summary>
        /// 意料中的异常
        /// </summary>
        ExpectedException = 1,

        /// <summary>
        /// 不可预料的异常
        /// </summary>
        UnExpectedException = 2,

        /// <summary>
        /// 内部错误
        /// </summary>
        Error = 3
    }
}
