using OneZero.Common.Enums;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace OneZero.Common.Dtos
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

        public List<DataDto> Datas { get; set; }

        public ResponseCode Code { get; set; }

        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
    }
}
