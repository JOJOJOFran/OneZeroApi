using OneZero.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Common.Exceptions
{
    public class OneZeroException : Exception
    {
        public ResponseCode Code { get; set; }
        

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

        public OneZeroException(string message, Exception innerException) : base(message, innerException)
        {

        }

        public OneZeroException(string message, Exception innerException, ResponseCode code) : base(message, innerException)
        {
            Code = code;
        }

    }
}
