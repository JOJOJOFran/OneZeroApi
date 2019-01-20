using System;
using System.Collections.Generic;
using System.Net;

namespace OneZero.Domain.Models
{
    public class OutputModel
    {
        public OutputModel()
        {
            //Ĭ��200
            StatusCode = HttpStatusCode.OK;
            //Ĭ��0,Success
            Code = ResponseCode.Success;
        }

        public string Message { get; set; }

        public List<OutputData> Datas { get; set; }

        public ResponseCode Code { get; set; }

        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
    }

    /// <summary>
    /// ������Ӧ�����ö��ֵ
    /// </summary>
    public enum ResponseCode
    {
        /// <summary>
        /// ����ɹ�
        /// </summary>
        Success = 0,

        /// <summary>
        /// �����е��쳣
        /// </summary>
        ExpectedException = 1,

        /// <summary>
        /// ����Ԥ�ϵ��쳣
        /// </summary>
        UnExpectedException = 2,

        /// <summary>
        /// �ڲ�����
        /// </summary>
        Error = 3
    }
}
