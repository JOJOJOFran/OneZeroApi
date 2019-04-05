using OneZero.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Core.Dtos.Permission
{
    public class ChangePhoneNumData : DataDto
    {
        public string UserId { get; set; }

        public string PhoneNum { get; set; }
    }
}
