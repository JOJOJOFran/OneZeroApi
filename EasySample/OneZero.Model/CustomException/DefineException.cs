using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Model.CustomException
{
    public class DefineException:Exception
    {
        public ResponseCode Code { get; set; }

        public DefineException() : base()
        {

        }

        public DefineException(string message) : base(message)
        {

        }

        public DefineException(string message, ResponseCode code) : base(message)
        {
            Code = code;
        }

        public DefineException(string message, Exception innerException) : base(message, innerException)
        {

        }

        public DefineException(string message, Exception innerException, ResponseCode code) : base(message, innerException)
        {
            Code = code;
        }
    }

}