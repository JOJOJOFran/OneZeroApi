using OneZero.Enums;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace OneZero.Dtos
{
    public class OutputDto
    {
        public OutputDto()
        {
            //默认200
            StatusCode = HttpStatusCode.OK;
            //默认0,Success
            Code = ResponseCode.Success;
        }

        public string Message { get; set; }

        public string ErrorMessage { get; set; }

        public PageInfo PageInfo { get; set; }

        public Object Datas { get; set; }

        public ResponseCode Code { get; set; }

        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
    }
}
