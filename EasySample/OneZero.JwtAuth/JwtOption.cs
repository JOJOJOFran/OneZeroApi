using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace OneZero.JwtAuth
{
    public class JwtOption
    {

        public string Issuer { get; set; }

        public string Audience { get; set; }

        /// <summary>
        /// Creds
        /// example:
        /// var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
        /// var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        /// </summary>
        public string SecretKey { get; set; }

        public Claim[] claims { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

    }
}
