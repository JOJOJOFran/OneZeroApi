using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace OneZero.Model
{
    public class Dto<T> where T : DtoData
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
            Datas =new List<T>();
        }

        /// <summary>
        /// 数据集合
        /// </summary>
        public IEnumerable<T> Datas { get; set; }

        /// <summary>
        /// 返回状态码
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// 请求响应结果码
        /// </summary>
        public ResponseCode Code { get; set; }

        /// <summary>
        /// 请求消息
        /// </summary>
        public string Message { get; set; }
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
