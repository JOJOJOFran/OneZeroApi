using OneZero.Domain.Models;
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



}
