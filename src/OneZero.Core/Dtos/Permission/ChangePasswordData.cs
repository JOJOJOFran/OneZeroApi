using OneZero.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Core.Dtos.Permission
{
    public class ChangePasswordData : DataDto
    {
        public string UserId { get; set; }

        public string OldPasswordData { get; set; }

        public string NewPasswordData { get; set; }
    }
}
