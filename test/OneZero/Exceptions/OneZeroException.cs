using OneZero.Enums;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace OneZero.Exceptions
{
    /// <summary>
    /// 自定义异常
    /// </summary>
    public class OneZeroException : Exception
    {
        public ResponseCode Code { get; set; } = ResponseCode.ExpectedException;
        public string ErrorMsg { get; set; }
        public ExceptionDealWay DealType { get; set; } = ExceptionDealWay.Json;

        public HttpStatusCode StatusCode { get; set; }

        public OneZeroException() : base()
        {

        }

        public OneZeroException(string message) : base(message)
        {

        }

        public OneZeroException(string message, ResponseCode code) : base(message)
        {
            Code = code;
        }


        public OneZeroException(string message, HttpStatusCode statusCode) : base(message)
        {
            StatusCode = statusCode;
        }


        public OneZeroException(string message, Exception innerException) : base(message, innerException)
        {
            ErrorMsg = innerException.Message;
        }

        public OneZeroException(string message, Exception innerException, ResponseCode code) : base(message, innerException)
        {
            Code = code;
            ErrorMsg = innerException.Message;
        }

    }
}
