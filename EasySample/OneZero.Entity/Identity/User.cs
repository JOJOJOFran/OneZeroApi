using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Entity.Identity
{
   
    public class User:IdentityUser<Guid>
    {

        /// <summary>
        /// 角色集合（导航属性）
        /// </summary>
        public ICollection<Role> Roles { get; set; }
    }
}