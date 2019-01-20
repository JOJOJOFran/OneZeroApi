using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Model.Identity
{
   
    public class User:IdentityUser<Guid>
    {

        /// <summary>
        /// 角色集合
        /// </summary>
        public ICollection<Role> Roles { get; set; }
    }
}