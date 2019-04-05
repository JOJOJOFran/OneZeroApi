using OneZero.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SouthStar.VehSch.Core.Logins.Dtos
{
    public class LoginPostData
    {
        public string Account { get; set; }

        public string Password { get; set; }

        public string ValidateCode { get; set; }

        public LoginWay LoginWay { get; set; }

        public string LoginFrom { get; set; }
    }
}
