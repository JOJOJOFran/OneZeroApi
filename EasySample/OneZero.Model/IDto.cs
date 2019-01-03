using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace OneZero.Model
{
    public interface IDto<T> where T : IDtoData
    {

        /// <summary>
        /// 数据集合
        /// </summary>
        IEnumerable<T> Datas { get; set; }
        
        /// <summary>
        /// 返回状态码
        /// </summary>
        HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// 请求响应结果码
        /// </summary>
        ResponseCode Code { get; set; }

        /// <summary>
        /// 请求消息
        /// </summary>
        string Message { get; set; }
    }
}
