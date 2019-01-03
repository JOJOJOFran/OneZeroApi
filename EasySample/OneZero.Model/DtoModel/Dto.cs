using System;
using System.Collections.Generic;
using System.Net;

namespace OneZero.Model
{
    /// <summary>
    /// Dto基类
    /// </summary>
    public class Dto : IDto<DtoData>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public Dto()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="message"></param>
        public Dto(HttpStatusCode statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
            //初始化空数据集合
            Datas = new List<DtoData>();
        }

        /// <summary>
        /// 返回状态码
        /// </summary>
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;

        /// <summary>
        /// 请求响应结果码
        /// </summary>
        public ResponseCode Code { get; set; } = ResponseCode.Success;

        /// <summary>
        /// 请求消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 数据集合
        /// </summary>
        public IEnumerable<DtoData> Datas { get; set; } = new List<DtoData>();
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
